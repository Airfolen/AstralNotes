using Microsoft.AspNetCore.Mvc;

namespace AstralNotes.API.Controllers
{
    /// <summary>
    /// Редирект на API
    /// </summary>
    public class HomeController : Controller
    {
//        public IActionResult Index()
//        {
//            return new RedirectResult("~/help");
//        }
        
        public IActionResult Index()
        {
            return View();
        }
    }
}