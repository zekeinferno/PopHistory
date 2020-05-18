using Microsoft.AspNetCore.Mvc;
using PopHistory.EntityFramework;
using PopHistory.Models;
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

        [OutputCache(Duration = 14400)]
        [Route("{setId}")]
        public IActionResult Set(int setId)
        {
            var sets = _context.PsaSet.Where(x => x.Id == setId).ToList();
            if (sets.Any())
            {
                var set = sets.First();
                var series = _context.Series.First(x => x.Id == set.SeriesId);
                var cards = _context.PsaCard.Where(x => x.SetId == setId).Select(x => new ExtendedPsaCard(x)).ToList();
                var cardIds = cards.Select(x => x.Id).ToList();
                var popHistories = _context.PsaPopHistory.Where(x => cardIds.Contains(x.CardId)).ToList();

                return View(new SetModel
                {
                    Title = set.Name,
                    SeriesName = series.Name,
                    Cards = cards,
                    PopHistories = popHistories
                });
            }
            
            return NotFound();
        }

        [OutputCache(Duration = 14400)]
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
