using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using Moq;

namespace CarSharing.Tests.Controllers
{
    [TestClass]
    public class RegistrationControllerTest
    {
        [TestMethod]
         public void GetSha1Test()
        {
            var result = Encoding.ASCII.GetBytes("CarSharing");
            Assert.AreEqual("2AF14A638750EAC02BA9746C2E15CB81C6845A47", result, "Hash password is successful created");
        }
    }
}


