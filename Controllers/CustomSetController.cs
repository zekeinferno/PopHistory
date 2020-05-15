using Microsoft.AspNetCore.Mvc;
using PopHistory.EntityFramework;
using PopHistory.Models;
using System.Collections.Generic;
using System.Linq;

namespace PopHistory.Controllers
{
    [Route("CustomSet")]
    public class CustomSetController : Controller
    {
        private PopHistoryContext _context { get; set; }

        public CustomSetController(PopHistoryContext context)
        {
            _context = context;
        }

        [OutputCache(Duration = 43200)]
        [Route("{customSetId}")]
        public IActionResult CustomSet(int customSetId)
        {
            var customSets = _context.PsaCustomSet.Where(x => x.Id == customSetId).ToList();
            if (customSets.Any())
            {
                var customSet = customSets.First();
                var seriesName = _context.Series.First(x => x.Id == customSet.SeriesId).Name;
                var cardIds = _context.PsaCustomSetCard.Where(x => x.CustomSetId == customSet.Id).Select(x => x.CardId).ToList();
                var cards = _context.PsaCard.Where(x => cardIds.Contains(x.Id)).ToList();
                var popHistories = _context.PsaPopHistory.Where(x => cardIds.Contains(x.CardId) && x.DateCreated >= customSet.DateCreated).ToList();

                var dateCreated = _context.PsaPopHistory.OrderByDescending(x => x.DateCreated).Select(x => x.DateCreated).Distinct().ToList();

                var cardsWithPopulation = from pc in _context.PsaCard
                                          join pph in _context.PsaPopHistory on pc.Id equals pph.CardId
                                          where pc.SetId == customSet.Id && pph.DateCreated == dateCreated.First()
                                          select new
                                          {
                                              pc,
                                              pph
                                          } into temp
                                          group temp by new
                                          {
                                              temp.pc.CardNumber,
                                              temp.pc.NamePrimary,
                                              temp.pc.NameSecondary
                                          } into g
                                          select new PsaCardWithPopulation
                                          {
                                              CardNumber = g.Key.CardNumber,
                                              NamePrimary = g.Key.NamePrimary,
                                              NameSecondary = g.Key.NameSecondary,
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

                return View(new CustomSetModel
                {
                    Title = $"{seriesName} {customSet.Name}",
                    SetId = customSetId,
                    Cards = cardsWithPopulation.ToList(),
                    PopHistories = popHistories
                });
            }
            
            return NotFound();
        }

        [OutputCache(Duration = 43200)]
        [Route("{customSetId}/Card/{cardId}")]
        public IActionResult Card(int customSetId, int cardId)
        {
            var cards = _context.PsaCard.Where(x => x.Id == cardId).ToList();
            if (cards.Any())
            {
                var card = cards.First();
                var customSets = _context.PsaCustomSet.Where(x => x.Id == customSetId).ToList();
                if (customSets.Any())
                {
                    var customSet = customSets.First();
                    var seriesName = _context.Series.Where(x => x.Id == customSet.SeriesId).First().Name;
                    var popHistories = _context.PsaPopHistory.Where(x => x.CardId == card.Id).ToList();

                    var title = card.CardNumber + " - " + card.NamePrimary;
                    if (!string.IsNullOrWhiteSpace(card.NameSecondary))
                    {
                        title += " - " + card.NameSecondary;
                    }

                    return View(new CardModel
                    {
                        Title = title,
                        SetId = customSet.Id,
                        SetName = $"{seriesName} {customSet.Name}",
                        PopHistories = popHistories
                    });
                }
            }
            
            return NotFound();
        }
    }
}
