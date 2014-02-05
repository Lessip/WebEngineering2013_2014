using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.ComponentModel;
using Moq;
using CarSharing;
using CarSharing.Controllers;
using System.Web.Http.Results;
using System.Web.Routing;
using System.Web.Mvc;

namespace CarSharing.Tests.Controllers
{
    public static class UnitTestHelpers
    {
        public static void ShouldEqual<T>(this T actualValue, T expectedValue)
        {
            Assert.AreEqual(expectedValue, actualValue);
        }

        public static void ShouldBeRedirectionTo(this ActionResult actionResult, object expectedRouteValues)
        {
            RouteValueDictionary actualValues = ((System.Web.Mvc.RedirectToRouteResult)actionResult).RouteValues;
            var expectedValues = new RouteValueDictionary(expectedRouteValues);

            foreach (string key in expectedValues.Keys)
            {
                Assert.AreEqual(expectedValues[key], actualValues[key]);
            }
        }
    }

    [TestClass]
    public class RegistrationControllerTest
    {
        [TestMethod]
        public void GetSha1Test()
        {
            var data = Encoding.ASCII.GetBytes("CarSharing");
            var hashData = new SHA1Managed().ComputeHash(data);

            var result = string.Empty;

            foreach (var b in hashData)
                result += b.ToString("X2");
            Assert.AreEqual("2AF14A638750EAC02BA9746C2E15CB81C6845A47", result, "Hash password is successful created");
        }

        [TestMethod]
        public void ConfirmationFailureTest()
        {
            // Arrange
            RegistrationController controller = new RegistrationController();

            // Act
            ViewResult result = controller.ConfirmationFailure() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        //[TestMethod]
        //public void ConfirmRegistrationTest()
        //{
        //    RegistrationController target = new RegistrationController();
        //   // var user = new CarSharing.user_account()
        //   //{
        //   // id = 2,
        //   // login_name = "user",
        //   // firstname = "test",
        //   // name = "test",
        //   // date_of_birth = System.DateTime.Now,
        //   // identity_number = Guid.NewGuid(),
        //   // password = "userpassword",
        //   // email = "user@password.com",
        //   // access_state = 1,
        //   // timelimit = System.DateTime.Now.AddDays(1).Date,
        //   // remove_date = System.DateTime.Now
        //   //};

        //    var result = target.ConfirmRegistration(null);

        //    result.ShouldBeRedirectionTo(
        //        new
        //        {
        //            controller = "RegistrationController",
        //            action = "ConfirmationFailure"
        //        }
        //    );
        //}

        //[TestMethod]
        //public void ConfirmRegistrationTest()
        //{
        //    RegistrationController target = new RegistrationController();
        //    var user = new CarSharing.user_account()
        //   {
        //    id = 2,
        //    login_name = "user",
        //    firstname = "test",
        //    name = "test",
        //    date_of_birth = System.DateTime.Now,
        //    identity_number = Guid.NewGuid(),
        //    password = "userpassword",
        //    email = "user@password.com",
        //    access_state = 1,
        //    timelimit = System.DateTime.Now.AddDays(1).Date,
        //    remove_date = System.DateTime.Now
        //   };



        //    var ControllerContext = new Mock<System.Web.Mvc.ControllerContext>();
        //    var context = target.HttpContext;


        //    ControllerContext.SetupGet(p => p.HttpContext.Session["user.id"]).Returns(2);
        //    ControllerContext.SetupGet(p => p.HttpContext.Session["user.login_name"]).Returns("user");
        //    ControllerContext.SetupGet(p => p.HttpContext.Session["user.firstname"]).Returns("test");
        //    ControllerContext.SetupGet(p => p.HttpContext.Session["user.name"]).Returns("test");
        //    ControllerContext.SetupGet(p => p.HttpContext.Session["user.date_of_birth"]).Returns(2014-04-02);
        //    ControllerContext.SetupGet(p => p.HttpContext.Session["user.identity_number"]).Returns("AA0C07FEF40667A8313A3E6893480FB");
        //    ControllerContext.SetupGet(p => p.HttpContext.Session["user.password"]).Returns("userpassword");
        //    ControllerContext.SetupGet(p => p.HttpContext.Session["user.email"]).Returns("user@password.com");
        //    ControllerContext.SetupGet(p => p.HttpContext.Session["user.access_state"]).Returns(1);
        //    ControllerContext.SetupGet(p => p.HttpContext.Session["user.timelimit"]).Returns(2014-02-06);
        //    ControllerContext.SetupGet(p => p.HttpContext.Session["user.remove_date"]).Returns(2014-03-06);

        //    //target.ControllerContext = controllerContext.Object;

        //    var action = (System.Web.Http.Results.RedirectToRouteResult)target.ConfirmRegistration(user);

        //    action.RouteValues["action"].Equals("ConfirmationSuccess");
        //    action.RouteValues["controller"].Equals("RegistrationController");

        //    Assert.AreEqual("ConfirmationSuccess", action.RouteValues["action"]);
        //    Assert.AreEqual("RegistrationController", action.RouteValues["controller"]); 
        //}     

    }
}
