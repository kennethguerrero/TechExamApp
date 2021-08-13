using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using TechExamApp.Helper;
using TechExamApp.Manager;
using TechExamApp.Model;
using TechExamApp.ViewModel;

namespace TechExamApp.Test
{
    [TestClass]
    public class UserListTest
    {
        Mock<IUserManager> userManagerMock;
        UserListViewModel userListVM;
        Mock<IShellHelper> shellHelperMock;
        [TestInitialize]
        public void Initialize()
        {
            userManagerMock = new Mock<IUserManager>();
            shellHelperMock = new Mock<IShellHelper>();
            userListVM = new UserListViewModel(userManagerMock.Object, shellHelperMock.Object);
        }

        [TestMethod]
        public async Task LoadUsers_ReturnUsers()
        {
            userManagerMock.Setup(m => m.GetUsers())
                .ReturnsAsync(new UserModel[]
                {
                    new UserModel(){ Id = "1"},
                    new UserModel(){ Id = "2"},
                    new UserModel(){ Id = "3"}
                });

            await userListVM.LoadUsers();
            Assert.IsTrue(userListVM.Users.Count == 3);
        }
        
        [TestMethod]
        public async Task LoadUsers_ReturnUsers_CombinesDuplicate()
        {
            userManagerMock.Setup(m => m.GetUsers())
                .ReturnsAsync(new UserModel[]
                { 
                    new UserModel(){ Id = "1"},
                    new UserModel(){ Id = "1"},
                    new UserModel(){ Id = "3"}
                });

            await userListVM.LoadUsers();
            Assert.IsTrue(userListVM.Users.Count == 2);
        }

        [TestMethod]
        public async Task LoadUsers_DisplayAlert_ThrowsException()
        {
            userManagerMock.Setup(m => m.GetUsers())
                .Throws<NoInternetException>();

            await userListVM.LoadUsers();

            shellHelperMock.Verify(m => m.DisplayAlert("No Internet Connection"), Times.Once());
        }

        [TestMethod]
        public async Task Selected_UserIsNull_GotoAsyncNotCalled()
        {
            await userListVM.Selected(null);

            shellHelperMock.Verify(m => m.GoToAsync(It.IsAny<string>()), Times.Never());
        }

        [TestMethod]
        [DataRow("1")]
        [DataRow("1000")]
        [DataRow("1123")]
        public async Task Selected_UserHasValue_GotoAsync(string userId)
        {
            await userListVM.Selected(new UserModel { Id = userId });

            shellHelperMock.Verify(m => m.GoToAsync(It.Is<string>(x=>x.Contains(userId))), Times.Once());
        }
    }
}
