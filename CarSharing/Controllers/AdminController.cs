using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Extensibility;

namespace CarSharing.Views
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/


        [Log]
        public ActionResult Index()
        {
            return View();
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
