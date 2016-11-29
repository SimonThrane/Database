using System;
using System.Collections.Generic;

namespace GDL.Model
{
    public class CharacteristicContainer
    {
        public int CharacteristicContainerId { get; set; }
        public virtual List<Appartmentcharacteristic> appartmentCharacteristic { get; set; }
        public DateTime timestamp { get; set; }
        public int version { get; set; }
        public virtual List<Sensorcharacteristic> sensorCharacteristic { get; set; }
    }
}