﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarSharing
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CarSharingEntities : DbContext
    {
        public CarSharingEntities()
            : base("name=CarSharingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<car> car { get; set; }
        public virtual DbSet<car_type> car_type { get; set; }
        public virtual DbSet<contract> contract { get; set; }
        public virtual DbSet<user> user { get; set; }
        public virtual DbSet<user_address> user_address { get; set; }
    }
}