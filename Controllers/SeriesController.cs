using Microsoft.AspNetCore.Mvc;
using PopHistory.EntityFramework;
using PopHistory.Models;
using System.Linq;

namespace PopHistory.Controllers
{
    [Route("Series")]
    public class SeriesController : Controller
    {
        private PopHistoryContext _context { get; set; }

        public SeriesController(PopHistoryContext context)
        {
            _context = context;
        }

        [OutputCache(Duration = 43200)]
        [Route("{name}")]
        public IActionResult ByName(string name)
        {
            var series = _context.Series.First(x => x.Name == name);
            var sets = _context.PsaSet.Where(x => x.SeriesId == series.Id).ToList();

            return View("Index", new SeriesModel
            {
                Title = series.Name,
                Sets = sets
            });
        }
    }
}
