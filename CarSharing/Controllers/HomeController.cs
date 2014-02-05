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
        public ActionResult Index(string latLng1, string latLng2, string pickupLocation, string pickupDate, string pickupTime, string returnLocation, string returnDate, string returnTime, string radius, string carNumber)
        {
            if (String.IsNullOrEmpty(pickupLocation)) { return View(); }

            if (!String.IsNullOrEmpty(carNumber))
                return RedirectToAction("signContract", new { pickupLocation = pickupLocation, pickupDate = pickupDate, pickupTime = pickupTime, returnLocation = returnLocation, returnDate = returnDate, returnTime = returnTime, carNumber = carNumber, latLng1 = latLng1, latLng2 = latLng2});
                //return RedirectToAction("signContract", "Home", new {id = Convert.ToInt32(carNumber)});

            double searchRadius = 10.0;
            ViewBag.latLng1 = latLng1;
            ViewBag.latLng2 = latLng2;
            ViewBag.pickupLocation = pickupLocation;
            ViewBag.pickupDate = pickupDate;
            ViewBag.pickupTime = pickupTime;
            ViewBag.returnLocation = returnLocation;
            ViewBag.returnDate = returnDate;
            ViewBag.returnTime = returnTime;
            ViewBag.radius = radius;

            if (!String.IsNullOrEmpty(radius)) { searchRadius = Convert.ToDouble(radius); }

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
                var dist = item.getDistance(latLng1);

                //ViewBag.msg += item.name + " " + dist.ToString() + " km \r\n";

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

        public ActionResult signContract(string latLng1, string latLng2, string pickupLocation, string pickupDate, string pickupTime, string returnLocation, string returnDate, string returnTime, string carNumber)
        {
            if (String.IsNullOrEmpty(pickupLocation)){
                ViewBag.msg = "pickup Location missing!";
            }
            if (String.IsNullOrEmpty(pickupDate)){
                ViewBag.msg = "pickup Location missing!";
            }
            if (String.IsNullOrEmpty(pickupTime)){
                ViewBag.msg = "pickup Location missing!";
            }

            ViewBag.latLng1 = latLng1;
            ViewBag.latLng2 = latLng2;
            ViewBag.pickupLocation = pickupLocation;
            ViewBag.pickupDate = pickupDate;
            ViewBag.pickupTime = pickupTime;
            ViewBag.returnLocation = returnLocation;
            ViewBag.returnDate = returnDate;
            ViewBag.returnTime = returnTime;
            ViewBag.carNumber = carNumber;
            ViewBag.carName = "";
            ViewBag.price = 0;

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
            int tmp = Convert.ToInt32(carNumber);
            queryResult = queryResult.Where(item => item.id == tmp);

            foreach (var item in queryResult)
            {
                ViewBag.carName = item.name;
                ViewBag.price = item.price;
            }

            return View();
        }

        // POST: /Home/signContract
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult signContract([Bind(Include = "user_id,car_id,state,pick_up_date,return_date,start_location,end_location,distance,price")] contract contract_element)
        {
            if (ModelState.IsValid)
            {
                db.contract.Add(contract_element);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contract_element);
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
