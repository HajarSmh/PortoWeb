using Microsoft.AspNetCore.Mvc;
using WEB_HS.Services;
using WEB_HS.Entities;
using WEB_HS.Data;

namespace WEB_HS.Controllers
{
    public class CompetenceController : Controller
    {
        private readonly MyDbContextPortfolio _context;

        public CompetenceController(MyDbContextPortfolio context)
        {
            _context = context;
        }

        // GET: Competence
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var competences = _context.Competences
                .Where(c => c.PersonneId == userId)
                .ToList();

            return View(competences);
        }

        // GET: /Competence/Create
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            
            if (userId == null)
            {
                return Redirect("/Account/Login");
            }
            
            return View();
        }

        // POST: /Competence/Create 
        [HttpPost]
        public IActionResult Create(string Titre, string Description, int Niveau, string Categorie)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return Redirect("/Account/Login");
            }

        // Créer la compétence
            var competence = new Competence
            {
                Titre = Titre,
                Description = Description,
                Niveau = Niveau,
                Categorie = Categorie,
                PersonneId = userId.Value
            };

            _context.Competences.Add(competence);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Compétence créée avec succès !";
            return Redirect("/Competence");
        }

        // GET: Competence/Edit
        public IActionResult Edit(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var competence = _context.Competences
                .FirstOrDefault(c => c.Id == id && c.PersonneId == userId);

            if (competence == null)
            {
                return NotFound();
            }

            return View(competence);
        }

        // POST: Competence/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Competence competence)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id != competence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                competence.PersonneId = userId.Value;
                _context.Update(competence);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Compétence modifiée avec succès !";
                return RedirectToAction(nameof(Index));
            }

            return View(competence);
        }

        // GET: Competence/Delete
        public IActionResult Delete(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var competence = _context.Competences
                .FirstOrDefault(c => c.Id == id && c.PersonneId == userId);

            if (competence == null)
            {
                return NotFound();
            }

            return View(competence);
        }

        // POST: Competence/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var competence = _context.Competences
                .FirstOrDefault(c => c.Id == id && c.PersonneId == userId);

            if (competence != null)
            {
                _context.Competences.Remove(competence);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Compétence supprimée avec succès !";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}