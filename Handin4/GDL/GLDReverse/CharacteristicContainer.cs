//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GLDReverse
{
    using System;
    using System.Collections.Generic;
    
    public partial class CharacteristicContainer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CharacteristicContainer()
        {
            this.Appartmentcharacteristics = new HashSet<Appartmentcharacteristic>();
            this.Sensorcharacteristics = new HashSet<Sensorcharacteristic>();
        }
    
        public int CharacteristicContainerId { get; set; }
        public System.DateTime timestamp { get; set; }
        public int version { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appartmentcharacteristic> Appartmentcharacteristics { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sensorcharacteristic> Sensorcharacteristics { get; set; }
    }
}
