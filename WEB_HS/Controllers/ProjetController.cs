using Microsoft.AspNetCore.Mvc;
using WEB_HS.Entities;
using WEB_HS.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace WEB_HS.Controllers
{
    public class ProjetController : Controller
    {
        private readonly MyDbContextPortfolio _context;
        private readonly IWebHostEnvironment _environment;

        public ProjetController(MyDbContextPortfolio context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Projet
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var projets = _context.Projets
                .Where(p => p.PersonneId == userId)
                .ToList();

            return View(projets);
        }

        // GET: Projet/Create
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // POST: /Projet/Create 
        [HttpPost]
        public IActionResult Create(string Titre, string Description, string Technologies,
                                   string ImageUrl, string LienDemo, string LienSource, DateTime DateCreation)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return Redirect("/Account/Login");
            }

            // Créer le projet
            var projet = new Projet
            {
                Titre = Titre,
                Description = Description,
                Technologies = Technologies,
                ImageUrl = string.IsNullOrEmpty(ImageUrl) ? "/images/project-default.jpg" : ImageUrl,
                LienDemo = LienDemo,
                LienSource = LienSource,
                DateCreation = DateCreation,
                PersonneId = userId.Value
            };

            _context.Projets.Add(projet);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Projet créé avec succès !";
            return Redirect("/Projet");
        }

        // GET: Projet/Edit/
        public IActionResult Edit(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var projet = _context.Projets
                .FirstOrDefault(p => p.Id == id && p.PersonneId == userId);

            if (projet == null)
            {
                return NotFound();
            }

            return View(projet);
        }

        // POST: Projet/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Projet projet, IFormFile imageFile, bool removeImage = false)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id != projet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    projet.PersonneId = userId.Value;

                    // Gestion de l'image
                    if (removeImage)
                    {
                        // Supprimer l'ancienne image si elle existe
                        if (!string.IsNullOrEmpty(projet.ImageUrl) &&
                            !projet.ImageUrl.Contains("project-default.jpg"))
                        {
                            var oldImagePath = Path.Combine(_environment.WebRootPath,
                                projet.ImageUrl.TrimStart('/'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }
                        projet.ImageUrl = "/images/project-default.jpg";
                    }
                    else if (imageFile != null && imageFile.Length > 0)
                    {
                        // Supprimer l'ancienne image si elle existe
                        if (!string.IsNullOrEmpty(projet.ImageUrl) &&
                            !projet.ImageUrl.Contains("project-default.jpg"))
                        {
                            var oldImagePath = Path.Combine(_environment.WebRootPath,
                                projet.ImageUrl.TrimStart('/'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        // Upload nouvelle image
                        var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "projets");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            imageFile.CopyTo(stream);
                        }

                        projet.ImageUrl = "/images/projets/" + uniqueFileName;
                    }

                    _context.Update(projet);
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Projet modifié avec succès !";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjetExists(projet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(projet);
        }

        // GET: Projet/Delete/
        public IActionResult Delete(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var projet = _context.Projets
                .FirstOrDefault(p => p.Id == id && p.PersonneId == userId);

            if (projet == null)
            {
                return NotFound();
            }

            return View(projet);
        }

        // POST: Projet/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var projet = _context.Projets
                .FirstOrDefault(p => p.Id == id && p.PersonneId == userId);

            if (projet != null)
            {
                // Supprimer l'image associée
                if (!string.IsNullOrEmpty(projet.ImageUrl) &&
                    !projet.ImageUrl.Contains("project-default.jpg"))
                {
                    var imagePath = Path.Combine(_environment.WebRootPath,
                        projet.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                _context.Projets.Remove(projet);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Projet supprimé avec succès !";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProjetExists(int id)
        {
            return _context.Projets.Any(e => e.Id == id);
        }
    }
}