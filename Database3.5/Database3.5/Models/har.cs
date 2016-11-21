using Database3._5.Models;

namespace Database3._5
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("har")]
    public partial class har
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PersonNummer { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TelefonNummerID { get; set; }

        [Required]
        [StringLength(20)]
        public string type { get; set; }

        public virtual Person Person { get; set; }

        public virtual Telefon Telefon { get; set; }
    }
}
