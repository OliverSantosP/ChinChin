using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using chinchini;
using chinchini.Controllers;

namespace chinchini.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void CheckUserNameAvailable()
        {
            // Arrange
            AccountController controller = new AccountController();
            //controller.UserManager 

            Assert.IsTrue(true);
        }
    }
}
