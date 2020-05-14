using Microsoft.AspNetCore.Mvc;

namespace PopHistory.Controllers
{
    [Route("MissionStatement")]
    public class MissionStatementController : Controller
    {
        [OutputCache(Duration = 43200)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
