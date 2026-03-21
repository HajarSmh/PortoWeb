using Microsoft.AspNetCore.Mvc;
using WEB_HS.Services;
using WEB_HS.Models;

namespace WEB_HS.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthentificationService _authService;

        public AccountController(AuthentificationService authService)
        {
            _authService = authService;
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var personne = _authService.Login(model.Email, model.Password);

                if (personne != null)
                {
                    // Stocker en session
                    HttpContext.Session.SetInt32("UserId", personne.Id);
                    HttpContext.Session.SetString("UserName", $"{personne.Prenom} {personne.Nom}");
                    HttpContext.Session.SetString("UserEmail", personne.Email);
                    HttpContext.Session.SetString("UserRole", personne.Role.ToString());

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Email ou mot de passe incorrect");
            }

            return View(model);
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(FormulaireCreationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var personne = _authService.Register(model);

                if (personne != null)
                {
                    // Auto-login après inscription
                    HttpContext.Session.SetInt32("UserId", personne.Id);
                    HttpContext.Session.SetString("UserName", $"{personne.Prenom} {personne.Nom}");
                    HttpContext.Session.SetString("UserEmail", personne.Email);
                    HttpContext.Session.SetString("UserRole", personne.Role.ToString());
                    return RedirectToAction("Index", "Home", new { id = personne.Id });
                }

                ModelState.AddModelError("", "Cet email est déjà utilisé");
            }

            return View(model);
        }

        // GET: /Account/Logout
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }

    }
}