using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Database3._5.Models
{
    public class PersonPostDTO
    {
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        public long Personnummer { get; set; }
        public string Type { get; set; }
        public long AdresseId { get; set; }
    }
}