using PopHistory.EntityFramework;
using System;

namespace PopHistory.Models
{
    public class ExtendedPsaSet : PsaSet
    {
        public int CurrentTotalGraded { get; set; }
        public int CurrentPop10 { get; set; }
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
