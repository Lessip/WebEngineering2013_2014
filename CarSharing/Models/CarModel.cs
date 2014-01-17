using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;


namespace CarSharing.Models
{
    public class CarModels : DbContext
    {
        public DbSet<CarProfile> CarProfiles { get; set; }

        public System.Data.Entity.DbSet<CarSharing.car> cars { get; set; }
    }

    [Table("UserProfile")]
    public class CarProfile
    {
        [Key]
        public int CarId { get; set; }
        public string CarName { get; set; }
    }
}