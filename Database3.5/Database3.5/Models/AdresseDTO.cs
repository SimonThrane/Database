using System.Collections.Generic;

namespace Database3._5.Models
{
    public class AdresseDTO
    {
        public long AdresseID { get; set; }
        public virtual List<PersonRefAdresse> People { get; set; }
        public int Postnummer_ { get; set; }

    }
}