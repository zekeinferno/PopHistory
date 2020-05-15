using PopHistory.EntityFramework;

namespace PopHistory.Models
{
    public class PsaCardWithPopulation : PsaCard
    {
        public PsaCardWithPopulation(PsaCard card)
        {
            Id = card.Id;
            SetId = card.SetId;
            CardNumber = card.CardNumber;
            NamePrimary = card.NamePrimary;
            NameSecondary = card.NameSecondary;
        }

        public int TotalPopulation { get; set; }
        public int? CurrentPop10 { get; set; }
    }
}
