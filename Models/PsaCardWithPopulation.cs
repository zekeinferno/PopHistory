﻿using PopHistory.EntityFramework;

namespace PopHistory.Models
{
    public class PsaCardWithPopulation : PsaCard
    {
        public int CurrentTotalPopulation { get; set; }
        public int? CurrentPop10 { get; set; }
    }
}