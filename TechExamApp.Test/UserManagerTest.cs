using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechExamApp.Helper;
using TechExamApp.Manager;
using TechExamApp.Model;
using TechExamApp.Service;

namespace TechExamApp.Test
{
    [TestClass]
    public class UserManagerTest
    {
        Mock<IUserService> userServiceMock;
        Mock<IConnectivityHelper> connectivityHelperMock;
        UserManager userManager;
        [TestInitialize]
        public void TestInitialize()
        {
            connectivityHelperMock = new Mock<IConnectivityHelper>();

            userServiceMock = new Mock<IUserService>();

            connectivityHelperMock.Setup(m => m.IsConnected).Returns(true);

            userManager = new UserManager(userServiceMock.Object, connectivityHelperMock.Object);
        }

        [TestMethod]
        public async Task GetUsers_ReturnsUsers()
        {
            userServiceMock.Setup(m => m.GetUsers())
                .ReturnsAsync(new UserModel[]
                {
                    new UserModel(),
                    new UserModel(),
                    new UserModel()
                });

            var result = await userManager.GetUsers();


            Assert.IsTrue(result.Count() == 3);
        }

        [TestMethod]
        [ExpectedException(typeof(NoInternetException))]
        public async Task GetUsers_ThrowException_NoInternet()
        {
            connectivityHelperMock.Setup(m => m.IsConnected).Returns(false);

            await userManager.GetUsers();
        }
    }
}
