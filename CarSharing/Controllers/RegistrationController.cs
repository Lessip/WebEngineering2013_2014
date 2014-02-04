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
using System.Security.Cryptography;
using System.Text;


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
      
        //Hashing password SHA1
        public static string GetSha1(string value)
        {
            var data = Encoding.ASCII.GetBytes(value);
            var hashData = new SHA1Managed().ComputeHash(data);

            var hash = string.Empty;

            foreach (var b in hashData)
                hash += b.ToString("X2");

            return hash;
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
                //generate identity_number
                Guid result = Guid.NewGuid();

                // generate the timestamp in DB
                user.timelimit = System.DateTime.Now.AddDays(1).Date;
                

                user.access_state = 0;
                user.identity_number = result;
                string hashPassword=GetSha1(user.password);
                user.password = hashPassword;
                db.user.Add(user);
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }

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
                            "&identity_number=" + user.identity_number +
                            "&timelimit=" + user.timelimit;
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
            return View("../Home/Index");
        }
      
        public ActionResult ConfirmRegistration(user_account user)
        {                     
            if ((user.identity_number != null) && (user.id!=null) && (user.timelimit >= System.DateTime.Now))
            {
                return RedirectToAction("ConfirmationSuccess", new {  id= user.id, identity_number = user.identity_number });
            }
            return RedirectToAction("ConfirmationFailure");
        }

        public ActionResult ConfirmationSuccess(int id, string identity_number)
        {
           
            string text = Convert.ToString(Request.QueryString["identity_number"]);
            Guid idNumber = Guid.Parse(text);
            Console.Write(text);
            var row = (from usr in db.user where usr.identity_number == idNumber select usr).Single();
            row.access_state = 1;
            db.SaveChanges();
           
            MessageBox.Show("you register success. Please Log in", "registration message", MessageBoxButtons.OK);
            return View("../Home/Index"); 
        }

        public ActionResult ConfirmationFailure()
           {
               MessageBox.Show("registration message", "confirmation failure. Please Register once more", MessageBoxButtons.OK);
               return View("Registrate"); 
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
