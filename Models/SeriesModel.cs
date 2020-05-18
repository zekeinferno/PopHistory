using PopHistory.EntityFramework;
using System.Collections.Generic;

namespace PopHistory.Models
{
    public class SeriesModel
    {
        public string Title { get; set; }
        public List<ExtendedPsaSet> Sets { get; set; }
        public List<MostGradedCard> MostGradedCards { get; set; }
    }
}
