﻿using PopHistory.EntityFramework;
using System.Collections.Generic;

namespace PopHistory.Models
{
    public class SeriesModel
    {
        public string Title { get; set; }
        public List<PsaSet> Sets { get; set; }
    }
}