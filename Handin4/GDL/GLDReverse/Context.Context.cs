﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Appartmentcharacteristic> Appartmentcharacteristics { get; set; }
        public virtual DbSet<CharacteristicContainer> CharacteristicContainers { get; set; }
        public virtual DbSet<ReadingContainer> ReadingContainers { get; set; }
        public virtual DbSet<Reading> Readings { get; set; }
        public virtual DbSet<Sensorcharacteristic> Sensorcharacteristics { get; set; }
    }
}
