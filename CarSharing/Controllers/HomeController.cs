using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarSharing.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "HomeController.cs";

            return View();
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

        private CarSharingEntities db = new CarSharingEntities();

        public ActionResult SelectLocation(string searchStr)
        {
            if (!String.IsNullOrEmpty(searchStr))
            {
                ViewBag.searchStr = searchStr;
            }
            // Filter auf parking_pos != null
            var cars = from m in db.car select m;
            cars = cars.Where(item => item.parking_pos != null);

            return View(cars);
        }

    }
}
