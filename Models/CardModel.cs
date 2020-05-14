using PopHistory.EntityFramework;
using System.Collections.Generic;

namespace PopHistory.Models
{
    public class CardModel
    {
        public string Title { get; set; }
        public string SeriesName { get; set; }
        public string SetName { get; set; }
        public int SetId { get; set; }
        public List<PsaPopHistory> PopHistories { get; set; }
    }
}
