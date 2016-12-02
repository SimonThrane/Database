using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GDL.Model
{
    public class ReadingContainer
    {
        [Key]
        public int ReadingContainerId { get; set; }
        public int version { get; set; }
        public DateTime timestamp { get; set; }
        public virtual List<Reading> reading { get; set; }
    }
}