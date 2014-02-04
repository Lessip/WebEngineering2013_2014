using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Device.Location;

namespace CarSharing.Controllers
{
    public class HomeController : Controller
    {
        private CarSharingEntities db = new CarSharingEntities();
        public ActionResult Index(string latLng, string pickupLocation, string radius, string pickupDate, string pickupTime, string returnLocation, string returnDate, string returnTime)
        {
            double searchRadius = 10.0;
            if (!String.IsNullOrEmpty(pickupLocation))
            {
                ViewBag.pickupLocation = pickupLocation;
            }
            else
            {
                return View();
            }
            if (!String.IsNullOrEmpty(latLng))
            {
                ViewBag.latLng = latLng;
            }
            if (!String.IsNullOrEmpty(radius))
            {
                ViewBag.radius = radius;
                searchRadius = Convert.ToDouble(radius);
            }
            // Filter parking_pos != null
            var queryResult = from carId in db.car
                              join carType in db.car_type
                              on carId.car_type_id equals carType.id
                              select new CarSharing.Models.CarProfile
                              {
                                  id = carId.id,
                                  car_type_id = carId.car_type_id,
                                  state = (int)carId.state,
                                  name = carId.name,
                                  registration_number = carId.registration_number,
                                  mileage = (int)carId.mileage,
                                  aircon = (Boolean)carId.aircon,
                                  navigation = (Boolean)carId.navigation,
                                  transmission_type = (Boolean)carId.transmission_type,
                                  power = (int)carId.power,
                                  picture_link = carId.picture_link,
                                  parking_pos = carId.parking_pos,
                                  seat_size = (int)carType.seat_size,
                                  car_class = (int)carType.car_class,
                                  price = (int)carType.price
                              };

            queryResult = queryResult.Where(item => item.parking_pos != null);

            var relevantCars = new List<CarSharing.Models.CarProfile>();

            // select relevant cars
            foreach (var item in queryResult)
            {
                bool relevant = true;
                var contractList = from contractId in db.contract
                                   where contractId.car_id == item.id
                                   select contractId;

                // distance from pickupLocation to car.parking_pos
                var dist = item.getDistance(latLng);

                ViewBag.msg += item.name + " " + dist.ToString() + " km \r\n";

                // distance
                relevant = relevant && ((dist <= searchRadius) && (dist >= 0));
                
                // status
                relevant = relevant && (item.getStatus( Convert.ToDateTime(pickupDate + " " + pickupTime),
                                                        Convert.ToDateTime(returnDate + " " + returnTime),
                                                        contractList.ToList()) == 0                        );
                
                if (relevant)
                {
                    relevantCars.Add(item);
                }
            }

            return View(relevantCars);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
