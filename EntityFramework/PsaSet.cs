using System;
using System.Collections.Generic;

namespace PopHistory.EntityFramework
{
    public partial class PsaSet
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int HeadingId { get; set; }
        public int SeriesId { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
    }
}
