using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Extensibility;
using System.Net;
using System.Data.Entity;

namespace CarSharing.Views
{
    public class AdminController : Controller
    {
        // Connect to the database
        private CarSharingEntities db = new CarSharingEntities();

        //
        // GET: /Admin/
        // Show the overview/statistics page
        public ActionResult Index()
        {
            return View();
        }
        
        /*
         * The USER-section
         * 
         * where all methods concerning the user-data are defined
         */

        // GET: /Admin/UserView
        // Show the content of the user-table
        public ActionResult UserView()
        {
           // Simply join the table user_address (from the right) to the table user
           var queryResult = from userId in db.user
                             join userAddress in db.user_address 
                             on userId.id equals userAddress.user_id
                             select new CarSharing.Models.UserProfile
           { 
                id = userId.id,
                login_name = userId.login_name,
                firstname = userId.firstname,
                name = userId.name,
                date_of_birth = userId.date_of_birth,
                password = userId.password,
                email = userId.email,
                access_state = (int)userId.access_state,
                identity_number = (Guid)userId.identity_number,
                timelimit = (DateTime)userId.timelimit,
                street = userAddress.street,
                post_code = userAddress.post_code,
                city = userAddress.city
            };

            return View(queryResult.ToList());
        }
        
        // GET: /Admin/UserCreate
        public ActionResult UserCreate()
        {
            return View();
        }

        // POST: /Admin/UserCreate
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserCreate([Bind(Include = "login_name,password,firstname,name,date_of_birth,email,access_state,street,post_code,city")] CarSharing.Models.UserProfile user)
        {
            if (ModelState.IsValid)
            {
                user_account userAccount = new user_account();
                userAccount.login_name = user.login_name;
                userAccount.password = user.password;
                userAccount.firstname = user.firstname;
                userAccount.name = user.name;
                userAccount.date_of_birth = user.date_of_birth;
                userAccount.email = user.email;
                userAccount.access_state = user.access_state;
                user_address userAddress = new user_address();
                userAddress.street = user.street;
                userAddress.post_code = user.post_code;
                userAddress.city = user.city;

                db.user.Add(userAccount);
                db.user_address.Add(userAddress);
                db.SaveChanges();
                return RedirectToAction("UserView");
            }

            return View(user);
        }

        // GET: /Admin/UserEdit/5
        public ActionResult UserDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_account user = db.user.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: /Admin/UserEdit/5
        public ActionResult UserEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_account user = db.user.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /Admin/UserEdit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit([Bind(Include = "id,login_name,firstname,name,date_of_birth,password,email,access_state,street,post_code,city")] CarSharing.Models.UserProfile user)
        {
            if (ModelState.IsValid)
            {
                //user_account userAccount = new user_account();
                //userAccount.login_name = user.login_name;
                //userAccount.password = user.password;
                //userAccount.firstname = user.firstname;
                //userAccount.name = user.name;
                //userAccount.date_of_birth = user.date_of_birth;
                //userAccount.email = user.email;
                //userAccount.access_state = user.access_state;
                //user_address userAddress = new user_address();
                //userAddress.street = user.street;
                //userAddress.post_code = user.post_code;
                //userAddress.city = user.city;

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserView");
            }
            return View(user);
        }

        // GET: /Admin/UserDelete/5
        public ActionResult UserDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_account user = db.user.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /Admin/UserDelete/5
        [HttpPost, ActionName("UserDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult UserDeleteConfirmed(int id)
        {
            user_account user = db.user.Find(id);
            db.user.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /*
         * The CAR-section
         * 
         * where all methods concerning the car-data are defined
         */

        // GET: /Admin/CarView
        // Show the content of the car_account-table
        public ActionResult CarView()
        {
            // Simply join the table user_address (from the right) to the table user
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

            return View(queryResult.ToList());
        }

        // GET: /Admin/CarCreate
        public ActionResult CarCreate()
        {
            return View();
        }

        // POST: /Admin/CarCreate
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult CarCreate([Bind(Include = "car_type_id,state,name,registration_number,mileage,aircon,navigation,transmission_type,power,picture_link,parking_pos")] CarSharing.Models.CarProfile car)
        public ActionResult CarCreate([Bind(Include = "car_type_id,state,name,registration_number,mileage,aircon,navigation,transmission_type,power,picture_link,parking_pos")] car car)
        {
            if (ModelState.IsValid)
            {
                //car carID = new car();
                //carID.car_type_id = car.car_type_id;
                //carID.state = car.state;
                //carID.name = car.name;
                //carID.registration_number = car.registration_number;
                //carID.mileage = car.mileage;
                //carID.aircon = car.aircon;
                //carID.navigation = car.navigation;
                //carID.transmission_type = car.transmission_type;
                //carID.power = car.power;
                //carID.picture_link = car.picture_link;
                //carID.parking_pos = car.parking_pos;

                db.car.Add(car);
                db.SaveChanges();
                return RedirectToAction("CarView");
            }

            return View(car);
        }

        // GET: /Admin/CarEdit/5
        public ActionResult CarDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            car car = db.car.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: /Admin/CarEdit/5
        public ActionResult CarEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            car car = db.car.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: /Admin/CarEdit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CarEdit([Bind(Include = "id,car_type_id,state,name,registration_number,mileage,aircon,navigation,transmission_type,power,picture_link,parking_pos")] car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CarView");
            }
            return View(car);
        }

        // GET: /Admin/CarDelete/5
        public ActionResult CarDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            car car = db.car.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: /Admin/CarDelete/5
        [HttpPost, ActionName("CarDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult CarDeleteConfirmed(int id)
        {
            car car = db.car.Find(id);
            db.car.Remove(car);
            db.SaveChanges();
            return RedirectToAction("CarView");
        }

        //[Log]
        public void updateUserRights()
        {
            // update user rights in the database
        }

        //[Log]
        public void getUserRights()
        {
            // read user rights from the database
        }
    }
}
