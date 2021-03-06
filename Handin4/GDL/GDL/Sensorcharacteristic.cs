﻿using System;
using System.ComponentModel.DataAnnotations;

namespace GDL.Model
{
    public class Sensorcharacteristic
    {
        public string calibrationCoeff { get; set; }
        public string description { get; set; }
        public DateTime calibrationDate { get; set; }
        public string externalRef { get; set; }
        [Key]
        public int sensorId { get; set; }
        public string unit { get; set; }
        public string calibrationEquation { get; set; }
    }
}