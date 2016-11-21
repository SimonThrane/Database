using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database3._5.Models
{
    [Table("erPaa")]
    public partial class erPaa
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long AdresseID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PersonNummer { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        public virtual Adresse Adresse { get; set; }

        public virtual Person Person { get; set; }
    }
}
