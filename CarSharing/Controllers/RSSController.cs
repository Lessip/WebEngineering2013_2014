using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarSharing;
using WebMatrix.WebData;
using WebMatrix.Data;

namespace CarSharing.Controllers
{
    public class RSSController : Controller
    {
        private CarSharingEntities db = new CarSharingEntities();

        // GET: /RSS/
        public ActionResult RSS()
        {
            Response.ContentType = "text/xml+rss";
            return View("RSS");
        }

    }
}