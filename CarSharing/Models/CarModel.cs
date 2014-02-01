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
        public System.Data.Entity.DbSet<CarSharing.car_type> car_types { get; set; }
    }

    [Table("CarProfile")]
    public class CarProfile
    {
        [Key]
        public int id { get; set; }
        public int car_type_id { get; set; }
        public int state { get; set; }
        public String name { get; set; }
        public String registration_number { get; set; }
        public int mileage { get; set; }
        public Boolean aircon { get; set; }
        public Boolean navigation { get; set; }
        public Boolean transmission_type { get; set; }
        public int power { get; set; }
        public String picture_link { get; set; }
        public String parking_pos { get; set; }
        public String type { get; set; }
        public Nullable<int> seat_size { get; set; }
        public Nullable<int> car_class { get; set; }
        public Nullable<int> price { get; set; }

        // Constructors for new userprofiles
        // Standard contrustor
        public CarProfile()
        {

        }

        // Constructor for admin user-profile view
        public CarProfile(CarSharing.car new_car, CarSharing.car_type new_car_type)
        {
            car_type_id = new_car.car_type_id;
            state = (int)new_car.state;
            name = new_car.name;
            registration_number = new_car.registration_number;
            mileage = (int)new_car.mileage;
            aircon = (Boolean)new_car.aircon;
            navigation = (Boolean)new_car.navigation;
            transmission_type = (Boolean)new_car.transmission_type;
            power = (int)new_car.power;
            picture_link = new_car.picture_link;
            parking_pos = new_car.parking_pos;
            type = new_car_type.type;
            seat_size = (int)new_car_type.seat_size;
            car_class = (int)new_car_type.car_class;
            price = (int)new_car_type.price;
        }
    }
}