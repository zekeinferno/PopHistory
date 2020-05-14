using System;
using System.Collections.Generic;

namespace PopHistory.EntityFramework
{
    public partial class PsaCustomSetCard
    {
        public int Id { get; set; }
        public int CustomSetId { get; set; }
        public int CardId { get; set; }
    }
}
