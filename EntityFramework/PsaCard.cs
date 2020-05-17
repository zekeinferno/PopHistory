using System;
using System.Collections.Generic;

namespace PopHistory.EntityFramework
{
    public partial class PsaCard
    {
        public int Id { get; set; }
        public int SetId { get; set; }
        public string CardNumber { get; set; }
        public string NamePrimary { get; set; }
        public string NameSecondary { get; set; }
        public int CurrentTotalGraded { get; set; }
        public int CurrentPop10 { get; set; }
    }
}
