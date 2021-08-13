using TechExamApp.Helper;
using TechExamApp.Manager;
using TechExamApp.Service;
using TechExamApp.ViewModel;

namespace TechExamApp
{
    public static class ServiceLocator
    {
        private static readonly IShellHelper ShellHelper = new ShellHelper();
        private static readonly IConnectivityHelper ConnectivityHelper = new ConnectivityHelper();

        private static readonly IUserService UserService = new UserService();

        private static readonly IUserManager UserManager = new UserManager(UserService, ConnectivityHelper);

        public static UserListViewModel GetUserListViewModel()
        {
            return new UserListViewModel(UserManager, ShellHelper);
        }

        public static UserDetailViewModel GetUserDetailViewModel()
        {
            return new UserDetailViewModel(UserManager);
        }
    }
}
