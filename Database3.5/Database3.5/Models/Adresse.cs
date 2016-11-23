using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database3._5.Models
{
    [Table("Adresse")]
    public partial class Adresse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Adresse()
        {
            erPaas = new HashSet<erPaa>();
            People = new HashSet<Person>();
        }

        public long AdresseID { get; set; }

        [Required]
        [StringLength(50)]
        public string Vejnavn { get; set; }

        [Required]
        [StringLength(10)]
        public string Husnummer { get; set; }

        [Required]
        [StringLength(50)]
        public string Bynavn { get; set; }

        [Column("Postnummer ")]
        public int Postnummer_ { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<erPaa> erPaas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Person> People { get; set; }
    }
}
