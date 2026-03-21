using Microsoft.AspNetCore.Mvc;
using WEB_HS.Data;

namespace WEB_HS.Controllers
{
    public class EntrepriseController : Controller
    {
        private readonly MyDbContextPortfolio _context;

        public EntrepriseController(MyDbContextPortfolio context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var listeEntreprise = _context.Entreprise.ToList();
            return View(listeEntreprise);
        }
    }
}
