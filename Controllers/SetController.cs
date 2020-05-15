using Microsoft.AspNetCore.Mvc;
using PopHistory.EntityFramework;
using PopHistory.Models;
using System.Collections.Generic;
using System.Linq;

namespace PopHistory.Controllers
{
    [Route("Set")]
    public class SetController : Controller
    {
        private PopHistoryContext _context { get; set; }

        public SetController(PopHistoryContext context)
        {
            _context = context;
        }

        [OutputCache(Duration = 43200)]
        [Route("{setId}")]
        public IActionResult Set(int setId)
        {
            var sets = _context.PsaSet.Where(x => x.Id == setId).ToList();
            if (sets.Any())
            {
                var set = sets.First();
                var series = _context.Series.First(x => x.Id == set.SeriesId);
                var cards = _context.PsaCard.Where(x => x.SetId == setId).ToList();
                var cardIds = cards.Select(x => x.Id).ToList();
                var popHistories = _context.PsaPopHistory.Where(x => cardIds.Contains(x.CardId)).ToList();

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

                return View(new SetModel
                {
                    Title = set.Name,
                    SeriesName = series.Name,
                    Cards = cardsWithPopulation,
                    PopHistories = popHistories
                });
            }
            
            return NotFound();
        }

        [OutputCache(Duration = 43200)]
        [Route("Card/{cardId}")]
        public IActionResult Card(int cardId)
        {
            var cards = _context.PsaCard.Where(x => x.Id == cardId).ToList();
            if (cards.Any())
            {
                var card = cards.First();
                var set = _context.PsaSet.Where(x => x.Id == card.SetId).First();
                var seriesName = _context.Series.Where(x => x.Id == set.SeriesId).First().Name;
                var popHistories = _context.PsaPopHistory.Where(x => x.CardId == card.Id).ToList();

                var title = card.CardNumber + " - " + card.NamePrimary;
                if (!string.IsNullOrWhiteSpace(card.NameSecondary))
                {
                    title += " - " + card.NameSecondary;
                }

                return View(new CardModel
                {
                    Title = title,
                    SeriesName = seriesName,
                    SetId = set.Id,
                    SetName = set.Name,
                    PopHistories = popHistories
                });
            }
            
            return NotFound();
        }
    }
}
