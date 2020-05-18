using Microsoft.AspNetCore.Mvc;
using PopHistory.EntityFramework;
using PopHistory.Models;
using System.Collections.Generic;
using System.Linq;

namespace PopHistory.Controllers
{
    [Route("CustomSets")]
    public class CustomSetsController : Controller
    {
        private PopHistoryContext _context { get; set; }

        public CustomSetsController(PopHistoryContext context)
        {
            _context = context;
        }

        [OutputCache(Duration = 14400)]
        public IActionResult CustomSets()
        {
            var series = _context.Series.ToList();
            var customSets = _context.PsaCustomSet.ToList();

            List<CustomSetsModel> model = new List<CustomSetsModel>();

            foreach (var customSet in customSets)
            {
                var match = series.FirstOrDefault(x => x.Id == customSet.SeriesId);
                model.Add(new CustomSetsModel
                {
                    Id = customSet.Id,
                    Name = customSet.Name,
                    SeriesName = match.Name
                });
            }

            return View(model);
        }
    }
}
