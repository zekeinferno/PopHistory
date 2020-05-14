using System;
using System.Collections.Generic;

namespace PopHistory.EntityFramework
{
    public partial class PsaCustomSet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SeriesId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
