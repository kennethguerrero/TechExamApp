using System.Collections.Generic;
using System.Threading.Tasks;
using TechExamApp.Model;

namespace TechExamApp.Manager
{
    public interface IUserManager
    {
        Task<UserModel> GetUser(string id);
        Task<IEnumerable<UserModel>> GetUsers();
    }
}