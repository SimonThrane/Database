using System.Collections.Generic;

namespace Database3._5.Models
{
    public class TelefonDTO
    {
        public long TelefonNummerID { get; set; }
        public virtual ICollection<har> hars { get; set; }


    }
}