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

            var dateCreated = _context.PsaPopHistory.OrderByDescending(x => x.DateCreated).Select(x => x.DateCreated).Distinct().ToList();

            var setsWithPopulation = from ps in _context.PsaSet
                                     join pc in _context.PsaCard on ps.Id equals pc.SetId
                                     join pph in _context.PsaPopHistory on pc.Id equals pph.CardId
                                     where ps.SeriesId == series.Id && pph.DateCreated == dateCreated.First()
                                     select new
                                     {
                                         ps, pph
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
                                         CurrentTotalPopulation = g.Sum(x =>
                                            (x.pph.PopAuth ?? 0) +
                                            (x.pph.Pop01 ?? 0) +
                                            (x.pph.Pop02 ?? 0) +
                                            (x.pph.Pop03 ?? 0) +
                                            (x.pph.Pop04 ?? 0) +
                                            (x.pph.Pop05 ?? 0) +
                                            (x.pph.Pop06 ?? 0) +
                                            (x.pph.Pop07 ?? 0) +
                                            (x.pph.Pop08 ?? 0) +
                                            (x.pph.Pop09 ?? 0) +
                                            (x.pph.Pop10 ?? 0)),
                                         CurrentPop10 = g.Sum(x => x.pph.Pop10)
                                     };

            return View("Index", new SeriesModel
            {
                Title = series.Name,
                Sets = setsWithPopulation.ToList()
            });
        }
    }
}
