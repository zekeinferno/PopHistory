using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PopHistory.Controllers
{
    [Route("StatusCode")]
    public class StatusCodeController : Controller
    {
        [Route("{code}")]
        public IActionResult Index(int code)
        {
            if (code == (int)HttpStatusCode.NotFound)
            {
                return View("NotFound");
            }
            else if (code == (int)HttpStatusCode.InternalServerError)
            {
                return View("InternalServerError");
            }
            else
            {
                return View();
            }
        }
    }
}
