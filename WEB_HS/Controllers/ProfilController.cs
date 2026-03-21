using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WEB_HS.Data;
using WEB_HS.Entities;
using WEB_HS.Models;
using WEB_HS.Services;

namespace WEB_HS.Controllers
{
    public class ProfilController : Controller
    {
        private readonly ProfilService _profilService;
        private readonly AuthentificationService _authService;
        private readonly MyDbContextPortfolio _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProfilController(ProfilService profilService, AuthentificationService authService, MyDbContextPortfolio context, IWebHostEnvironment webHostEnvironment)
        {
            _profilService = profilService;
            _authService = authService;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: /Profil Liste des profils
        public IActionResult Index()
        {
            var profils = _profilService.GetAllProfils();
            return View(profils);
        }

        // GET: /Profil/Details/ Voir un profil
        public IActionResult Details(int id)
        {
            var profil = _profilService.GetProfilById(id);

            if (profil == null)
            {
                return NotFound();
            }

            return View(profil);
        }

        // GET: /Profil/MonProfil Mon profil connecté
        public IActionResult MonProfil()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return Redirect("/Account/Login");
            }

            var profil = _context.Personnes
                .Include(p => p.Competences)
                .Include(p => p.Projets)
                .FirstOrDefault(p => p.Id == userId);

            if (profil == null)
            {
                return NotFound();
            }

            return View(profil);
        }

        // GET: /Profil/Edit/ Modifier le profil
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null || userId != id)
            {
                TempData["ErrorMessage"] = "Vous devez être connecté pour modifier votre profil.";
                return RedirectToAction("Login", "Account");
            }

            var personne = _context.Personnes.Find(id);
            if (personne == null)
            {
                return NotFound();
            }

            return View(personne);
        }

        // POST: /Profil/Edit/ Mettre à jour le profil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Nom,Prenom,Email,Titre,Bio,LinkedIn,GitHub")] Personne personne,
            IFormFile photoFile = null,
            bool removePhoto = false)
        {
            ModelState.Remove("MotDePasse");
            if (id != personne.Id)
            {
                return NotFound();
            }

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null || userId != id)
            {
                TempData["ErrorMessage"] = "Vous ne pouvez modifier que votre propre profil.";
                return RedirectToAction("Login", "Account");
            }

            // Récupère la personne existante depuis la BD
            var personneToUpdate = await _context.Personnes.FindAsync(id);
            if (personneToUpdate == null)
            {
                return NotFound();
            }

                if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(kvp => kvp.Value.Errors != null && kvp.Value.Errors.Count > 0)
                    .Select(kvp => new { Key = kvp.Key, Errors = kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray() })
                    .ToList();

                System.Diagnostics.Debug.WriteLine("ModelState errors in ProfilController.Edit POST:");
                foreach (var e in errors)
                {
                    System.Diagnostics.Debug.WriteLine($"{e.Key}: {string.Join(" | ", e.Errors)}");
                }

                TempData["ModelStateErrors"] = string.Join(" | ", errors.SelectMany(x => x.Errors));
                return View(personneToUpdate);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Garde les champs non modifiables
                    var originalMotDePasse = personneToUpdate.MotDePasse;
                    var originalPhoto = personneToUpdate.Photo;
                    var originalDateInscription = personneToUpdate.DateInscription;
                    var originalRole = personneToUpdate.Role;

                    // Nettoie les URLs vides pour les nouvelles valeurs entrantes
                    var incomingLinkedIn = string.IsNullOrWhiteSpace(personne.LinkedIn) ? "" : personne.LinkedIn;
                    var incomingGitHub = string.IsNullOrWhiteSpace(personne.GitHub) ? "" : personne.GitHub;

                    // Gestion de la photo
                    if (removePhoto)
                    {
                        personneToUpdate.Photo = "/images/default-avatar.jpg";
                    }
                    else if (photoFile != null && photoFile.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "photos");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(photoFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await photoFile.CopyToAsync(stream);
                        }

                        personneToUpdate.Photo = "/uploads/photos/" + uniqueFileName;
                    }
                    else
                    {
                        // Garde la photo existante
                        personneToUpdate.Photo = originalPhoto;
                    }

                    // Met à jour les champs modifiables
                    personneToUpdate.Nom = personne.Nom;
                    personneToUpdate.Prenom = personne.Prenom;
                    personneToUpdate.Email = personne.Email;
                    personneToUpdate.Titre = personne.Titre;
                    personneToUpdate.Bio = personne.Bio;
                    personneToUpdate.LinkedIn = incomingLinkedIn;
                    personneToUpdate.GitHub = incomingGitHub;

                    // Remet les champs non modifiables
                    personneToUpdate.MotDePasse = originalMotDePasse;
                    personneToUpdate.DateInscription = originalDateInscription;
                    personneToUpdate.Role = originalRole;

                    _context.Update(personneToUpdate);
                    await _context.SaveChangesAsync();

                    // Met à jour la session
                    var fullName = $"{personneToUpdate.Nom} {personneToUpdate.Prenom}".Trim();
                    HttpContext.Session.SetString("UserName", fullName);

                    TempData["SuccessMessage"] = "Profil mis à jour avec succès !";
                    return RedirectToAction("Details", new { id = personneToUpdate.Id });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonneExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Erreur: {ex.Message}");
                    TempData["ErrorMessage"] = $"Erreur lors de la mise à jour: {ex.Message}";
                }
            }

            // Si on arrive ici, il y a des erreurs de validation ou exception
            return View(personneToUpdate);
        }

        private bool PersonneExists(int id)
        {
            return _context.Personnes.Any(e => e.Id == id);
        }

        // GET: /Profil/Messages Mes messages reçus
        public IActionResult Messages()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var messages = _context.Messages
                .Where(m => m.PersonneId == userId.Value)
                .OrderByDescending(m => m.DateCreation)
                .ToList();

            return View(messages);
        }

        // POST: /Profil/SendMessage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(int personneId, string nomAuteur, string emailAuteur, string contenu)
        {
            if (string.IsNullOrWhiteSpace(contenu))
            {
                TempData["ErrorMessage"] = "Le message ne peut pas être vide.";
                return RedirectToAction("Details", new { id = personneId });
            }

            var message = new Message
            {
                PersonneId = personneId,
                NomAuteur = nomAuteur,
                EmailAuteur = emailAuteur,
                Contenu = contenu,
                DateCreation = DateTime.Now,
                EstLu = false
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Votre message a été envoyé avec succès !";
            return RedirectToAction("Details", new { id = personneId });
        }

        // POST: /Profil/MarkAllAsRead
        [HttpPost]
        public IActionResult MarkAllAsRead()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // On récupère les messages non lus de l'utilisateur
            var messagesNonLus = _context.Messages
                .Where(m => m.PersonneId == userId.Value && !m.EstLu)
                .ToList();

            foreach (var msg in messagesNonLus)
            {
                msg.EstLu = true;
            }

            _context.SaveChanges();

            // On redirige simplement vers la vue "Messages" du même contrôleur
            return RedirectToAction("Messages");
        }

        // POST: /Profil/MarkAsRead
        [HttpPost]
        public IActionResult MarkAsRead(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var message = _context.Messages.FirstOrDefault(m => m.Id == id && m.PersonneId == userId.Value);

            if (message != null)
            {
                message.EstLu = true;
                _context.SaveChanges();
            }

            // Redirige vers la page précédente
            var referer = Request.Headers["Referer"].ToString();
            return Redirect(!string.IsNullOrEmpty(referer) ? referer : "/Profil/MonProfil");
        }
    }
}