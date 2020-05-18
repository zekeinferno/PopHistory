using Microsoft.AspNetCore.Mvc;

namespace PopHistory.Controllers
{
    [Route("MissionStatement")]
    public class MissionStatementController : Controller
    {
        [OutputCache(Duration = 14400)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
