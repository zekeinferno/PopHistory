using PopHistory.EntityFramework;
using System.Collections.Generic;

namespace PopHistory.Models
{
    public class SetModel
    {
        public string Title { get; set; }
        public string SeriesName { get; set; }
        public List<PsaCardWithPopulation> Cards { get; set; }
        public List<PsaPopHistory> PopHistories { get; set; }
    }
}
