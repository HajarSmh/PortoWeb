using Microsoft.AspNetCore.Mvc;

namespace WEB_HS.Controllers
{
    public class PortfolioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
