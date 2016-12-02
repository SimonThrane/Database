using System.ComponentModel.DataAnnotations;

namespace GDL.Model
{
    public class Appartmentcharacteristic
    {
        public int No { get; set; }
        public float Size { get; set; }
        public int Floor { get; set; }
        [Key]
        public int AppartmentId { get; set; }
    }
}