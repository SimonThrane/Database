using System.Collections.Generic;

namespace Database3._5.Models
{
    public class TelefonDTO
    {
        public long TelefonNummerID { get; set; }
        public virtual List<PersonRefAdresse> People { get; set; }


    }
}