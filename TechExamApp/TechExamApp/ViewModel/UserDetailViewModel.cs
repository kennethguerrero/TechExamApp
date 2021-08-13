using System;
using System.Diagnostics;
using TechExamApp.Manager;
using TechExamApp.Model;
using TechExamApp.View;
using Xamarin.Forms;

namespace TechExamApp.ViewModel
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class UserDetailViewModel : BaseViewModel
    {
        IUserManager userManager;
        public UserDetailViewModel(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        string imageUrl;
        public string ImageUrl
        {
            get => imageUrl;
            set => SetProperty(ref imageUrl, value);
        }

        string id;
        public string Id
        {
            get => id;
            set
            {
                id = value;
                LoadId(value);
            }
        }

        public async void LoadId(string id)
        {
            try
            {
                var user = await userManager.GetUser(id);
                Name = user.Name;
                ImageUrl = user.ImageUrl;
            }
            catch (NoInternetException)
            {
                await Shell.Current.DisplayAlert(null,"No Internet Connection", "Close");

                await Shell.Current.GoToAsync($"//{nameof(UserListPage)}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
