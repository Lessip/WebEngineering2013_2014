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
        // Show the user table
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult UserView()
        {
            return View(db.user.ToList());
        }

        public ActionResult CarView()
        {
            return View(db.car.ToList());
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
        public ActionResult UserCreate([Bind(Include = "login_name,password,firstname,name,date_of_birth,identity_number,email,access_state")] user user)
        {
            if (ModelState.IsValid)
            {
                db.user.Add(user);
                db.SaveChanges();
                return RedirectToAction("UserView");
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
            user user = db.user.Find(id);
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
        public ActionResult UserEdit([Bind(Include = "login_name,firstname,name,date_of_birth,identity_number,password,email,access_state")] user user)
        {
            if (ModelState.IsValid)
            {
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
            user user = db.user.Find(id);
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
            user user = db.user.Find(id);
            db.user.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
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
        public ActionResult CarCreate([Bind(Include = "car_type_id,user_id,state,name,registration_number,mileage,aircon,navigation,transmission_type,power,picture_link,parking_pos")] car car)
        {
            if (ModelState.IsValid)
            {
                db.car.Add(car);
                db.SaveChanges();
                return RedirectToAction("CarView");
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
        public ActionResult CarEdit([Bind(Include = "car_type_id,user_id,state,name,registration_number,mileage,aircon,navigation,transmission_type,power,picture_link,parking_pos")] car car)
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
