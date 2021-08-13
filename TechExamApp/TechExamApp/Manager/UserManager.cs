using System.Collections.Generic;
using System.Threading.Tasks;
using TechExamApp.Helper;
using TechExamApp.Model;
using TechExamApp.Service;

namespace TechExamApp.Manager
{
    public class UserManager : BaseManager, IUserManager
    {
        private readonly IUserService userService;

        public UserManager(IUserService userService, IConnectivityHelper connectivityHelper) : base(connectivityHelper)
        {
            this.userService = userService;
        }

        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            if (!CheckInternetConnection())
                throw new NoInternetException();

            var result = await userService.GetUsers();
            return result;
        }

        public async Task<UserModel> GetUser(string id)
        {
            if (!CheckInternetConnection())
                throw new NoInternetException();

            var result = await userService.GetUser(id);
            return result;
        }
    }
}
