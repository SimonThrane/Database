using System;
using System.Collections.Generic;

namespace GDL.Model
{
    public class ReadingContainer
    {
        public int version { get; set; }
        public DateTime timestamp { get; set; }
        public List<Reading> reading { get; set; }
    }
}