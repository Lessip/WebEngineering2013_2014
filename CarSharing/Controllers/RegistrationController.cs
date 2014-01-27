using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarSharing;

using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;

namespace CarSharing.Controllers
{
    public class RegistrationController : Controller
    {
        private CarSharingEntities db = new CarSharingEntities();

        // GET: /Registration/
        public ActionResult Index()
        {
            return View(db.user.ToList());
        }

        // GET: /Registration/Registrate
        public ActionResult Registrate()
        {
            return View();
        }        
               
        // POST: /User/Registrate
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrate([Bind(Include = "login_name,firstname,name,date_of_birth,identity_number,password,email")] user_account user)
        {
            if (ModelState.IsValid)
            {
                //generate the link
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var random = new Random();
                var result = new string(
                    Enumerable.Repeat(chars, 8)
                              .Select(s => s[random.Next(s.Length)])
                              .ToArray());

                // generate the timestamp in DB
                // var timestamp=DateTime.Now;
                user.access_state = 0;
                user.identity_number = result;
                db.user.Add(user);
                db.SaveChanges();

                // SMTP options
                string Host = "smtp.gmail.com";
                Int16 Port = 587;
                bool SSL = true;
                string Username = "eiffeltowercarsharing@gmail.com";
                string Password = "EiffeltowerJPGM";

                // Mail options
                if (user.identity_number != null)
                {
                    var hosturl =
                            System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) +
                            "/Registration/ConfirmRegistration?id=" + user.id+
                            "identity_number=" + user.identity_number;
                    var confirmationLink = string.Format("<a href=\"{0}\">Click to confirm your registration</a>",
                                                             hosturl);
                    string To = user.email;
                    string From = "eiffeltowercarsharing@gmail.com";
                    string Subject = "Please confirm your email";
                    string Body = confirmationLink;

                    MailMessage mm = new MailMessage(From, To, Subject, Body);
                    mm.IsBodyHtml = true;
                    SmtpClient sc = new SmtpClient(Host, Port);
                    NetworkCredential netCred = new NetworkCredential(Username, Password);
                    sc.EnableSsl = SSL;
                    sc.UseDefaultCredentials = false;
                    sc.Credentials = netCred;
                    try
                    {
                      sc.Send(mm);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: {0}", ex.ToString());
                    }

                }

             }
            return View("Registrate");
        }

        public ActionResult ConfirmRegistration(user_account user)
        {
            //user user;
            //user.getByIdentity(identity_number);
           

            if (user!= null)
            {
                return RedirectToAction("ConfirmationSuccess");
            }
            return RedirectToAction("ConfirmationFailure");
        }

        public ActionResult ConfirmationSuccess(user_account user)
        {
            //user user;
             //user.access_state = 1;
            user_account u=db.user.Find(user.id);
            Console.Write("sdflsjdf"+u);
            //user.access_state = 1;
            //db.SaveChanges();
            
            MessageBox.Show("you register success. Please Log in", "registration message", MessageBoxButtons.OK);
            return View(); 
        }

        public ActionResult ConfirmationFailure()
           {
               MessageBox.Show("registration message", "confirmation failure. Please Register once more", MessageBoxButtons.OK);
               return View(); 
           }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
