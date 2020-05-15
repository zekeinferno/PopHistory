using PopHistory.EntityFramework;
using System.Collections.Generic;

namespace PopHistory.Models
{
    public class CustomSetModel
    {
        public string Title { get; set; }
        public int SetId { get; set; }
        public List<PsaCardWithPopulation> Cards { get; set; }
        public List<PsaPopHistory> PopHistories { get; set; }
    }
}
