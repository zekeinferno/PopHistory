using PopHistory.EntityFramework;
using System;

namespace PopHistory.Models
{
    public class ExtendedPsaCard : PsaCard
    {
        public ExtendedPsaCard(PsaCard card)
        {
            CardNumber = card.CardNumber;
            CurrentPop10 = card.CurrentPop10;
            CurrentTotalGraded = card.CurrentTotalGraded;
            Id = card.Id;
            NamePrimary = card.NamePrimary;
            NameSecondary = card.NameSecondary;
            SetId = card.SetId;
        }

        public decimal? CurrentPop10Percentage
        {
            get
            {
                if (CurrentTotalGraded > 0)
                {
                    return Math.Round(Decimal.Divide(CurrentPop10, CurrentTotalGraded), 2);
                }

                return null;
            }
        }
    }
}
