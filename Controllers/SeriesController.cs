using Microsoft.AspNetCore.Mvc;
using PopHistory.EntityFramework;
using PopHistory.Models;
using System.Collections.Generic;
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

            var setsWithPopulation = from ps in _context.PsaSet
                                     join pc in _context.PsaCard on ps.Id equals pc.SetId
                                     where ps.SeriesId == series.Id
                                     select new
                                     {
                                         ps, pc
                                     } into temp
                                     group temp by new
                                     {
                                         temp.ps.Id,
                                         temp.ps.Name,
                                         temp.ps.Year
                                     } into g
                                     select new PsaSetWithPopulation
                                     {
                                         Id = g.Key.Id,
                                         Name = g.Key.Name,
                                         Year = g.Key.Year,
                                         CurrentTotalPopulation = g.Sum(x => x.pc.CurrentTotalGraded),
                                         CurrentPop10 = g.Sum(x => x.pc.CurrentPop10)
                                     };

            return View("Index", new SeriesModel
            {
                Title = series.Name,
                Sets = setsWithPopulation.ToList()
            });
        }
    }
}
