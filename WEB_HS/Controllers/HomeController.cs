using Microsoft.AspNetCore.Mvc;
using WEB_HS.Services;
using WEB_HS.Entities;

namespace WEB_HS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProfilService _profilService;

        public HomeController(ProfilService profilService)
        {
            _profilService = profilService;
        }

        public IActionResult Index()
        {
            ViewBag.MessageBienvenue = "Découvrez les portfolios de nos développeurs talentueux !";

            // Récupérer tous les profils 
            var tousLesProfils = _profilService.GetAllProfils();

            // Calculer les statistiques 
            ViewBag.TotalDevs = tousLesProfils.Count();
            ViewBag.TotalProjets = tousLesProfils.Sum(p => p.Projets?.Count ?? 0);
            ViewBag.TotalCompetences = tousLesProfils
                .SelectMany(p => p.Competences)
                .Select(c => c.Titre)
                .Distinct()
                .Count();

            //Envoyer les 6 derniers à la vue pour affichage
            var profilsRecents = tousLesProfils.Take(6).ToList();

            return View(profilsRecents);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}