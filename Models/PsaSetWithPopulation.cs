using PopHistory.EntityFramework;

namespace PopHistory.Models
{
    public class PsaSetWithPopulation : PsaSet
    {
        public int CurrentTotalPopulation { get; set; }
        public int? CurrentPop10 { get; set; }
    }
}
