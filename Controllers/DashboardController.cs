using Microsoft.AspNetCore.Mvc;

namespace PopHistory.Controllers
{
    public class DashboardController : Controller
    {
        [OutputCache(Duration = 43200)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
