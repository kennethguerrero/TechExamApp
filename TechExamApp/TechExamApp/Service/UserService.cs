using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TechExamApp.Model;
using Xamarin.Essentials;

namespace TechExamApp.Service
{
    public class UserService : IUserService
    {
        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            try
            {
                var url = "https://gist.githubusercontent.com/erni-ph-mobile-team/c5b401c4fad718da9038669250baff06/raw/7e390e8aa3f7da4c35b65b493fcbfea3da55eac9/test.json";
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return Enumerable.Empty<UserModel>();
                }

                var json = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<IEnumerable<UserModel>>(json);
                return list;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return Enumerable.Empty<UserModel>();
            }

        }

        public async Task<UserModel> GetUser(string id)
        {
            var list = await GetUsers();
            var user = list.Where(u => u.Id.Equals(id));
            var selectedUser = user.FirstOrDefault();
            return selectedUser;
        }

    }
}
