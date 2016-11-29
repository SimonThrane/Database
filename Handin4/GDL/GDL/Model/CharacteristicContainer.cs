using System;
using System.Collections.Generic;

namespace GDL.Model
{
    public class CharacteristicContainer
    {
        public List<Appartmentcharacteristic> appartmentCharacteristic { get; set; }
        public DateTime timestamp { get; set; }
        public int version { get; set; }
        public List<Sensorcharacteristic> sensorCharacteristic { get; set; }
    }
}