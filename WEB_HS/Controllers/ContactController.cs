using Microsoft.AspNetCore.Mvc;
using WEB_HS.Entities;
using WEB_HS.Data;
using System.Threading.Tasks;

namespace WEB_HS.Controllers
{
    public class ContactController : Controller
    {
        private readonly MyDbContextPortfolio _context;

        public ContactController(MyDbContextPortfolio context)
        {
            _context = context;
        }

        // GET: Contact
        public IActionResult Index()
        {
            return View();
        }

        // POST: Contact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Message message)
        {
            message.PersonneId = 13;
            message.DateCreation = DateTime.Now;
            message.EstLu = false;

            ModelState.Remove("PersonneId");
            ModelState.Remove("DateCreation");
            ModelState.Remove("Personne");
            ModelState.Remove("EstLu");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Messages.Add(message);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Votre message a été envoyé avec succès !";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // En cas d'erreur de base de données
                    ModelState.AddModelError("", "Erreur lors de l'enregistrement : " + ex.Message);
                }
            }

            // Si on arrive ici, c'est qu'il y a une erreur
            return View(message);
        }
    }
}