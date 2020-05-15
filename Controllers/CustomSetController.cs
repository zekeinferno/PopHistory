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

                var cardsWithPopulation = new List<PsaCardWithPopulation>();
                foreach (var card in cards)
                {
                    var popHistory = popHistories.Where(x => x.CardId == card.Id).ToList();

                    if (popHistory.Count > 0)
                    {
                        var currentHistory = popHistory.OrderByDescending(x => x.DateCreated).First();

                        cardsWithPopulation.Add(new PsaCardWithPopulation(card)
                        {
                            CurrentPop10 = currentHistory.Pop10,
                            TotalPopulation =
                                (currentHistory.PopAuth ?? 0) +
                                (currentHistory.Pop01 ?? 0) +
                                (currentHistory.Pop02 ?? 0) +
                                (currentHistory.Pop03 ?? 0) +
                                (currentHistory.Pop04 ?? 0) +
                                (currentHistory.Pop05 ?? 0) +
                                (currentHistory.Pop06 ?? 0) +
                                (currentHistory.Pop07 ?? 0) +
                                (currentHistory.Pop08 ?? 0) +
                                (currentHistory.Pop09 ?? 0) +
                                (currentHistory.Pop10 ?? 0)
                        });
                    }
                }

                return View(new CustomSetModel
                {
                    Title = $"{seriesName} {customSet.Name}",
                    SetId = customSetId,
                    Cards = cardsWithPopulation,
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
