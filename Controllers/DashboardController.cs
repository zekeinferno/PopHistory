using Microsoft.AspNetCore.Mvc;

namespace PopHistory.Controllers
{
    public class DashboardController : Controller
    {
        [OutputCache(Duration = 14400)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
