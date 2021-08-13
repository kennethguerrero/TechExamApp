using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TechExamApp.Helper;
using TechExamApp.Manager;
using TechExamApp.Model;
using TechExamApp.Service;
using TechExamApp.View;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TechExamApp.ViewModel
{
    public class UserListViewModel : BaseViewModel
    {
        public ObservableCollection<UserModel> Users { get; }
        public Command LoadUsersCommand { get; }
        IUserManager userManager;
        private readonly IShellHelper shellHelper;

        public Command<UserModel> SelectCommand { get; }
        
        public UserListViewModel(IUserManager userManager, IShellHelper shellHelper)
        {
            this.userManager = userManager;
            this.shellHelper = shellHelper;
            Users = new ObservableCollection<UserModel>();
            LoadUsersCommand = new Command(async () => await LoadUsers());
            SelectCommand = new Command<UserModel>(async (user) => await Selected(user));

            Initialize = LoadUsers();
        }

        private Task Initialize { get; set; }

        public async Task LoadUsers()
        {
            try
            {
                IsBusy = true;
                Users.Clear();
                var users = await userManager.GetUsers();
                var sorted = users.GroupBy(u => u.Id).Select(u => u.FirstOrDefault());
                foreach (var user in sorted)
                {
                    Users.Add(user);
                }
            }
            catch (NoInternetException)
            {
                await shellHelper.DisplayAlert("No Internet Connection");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                await Task.Delay(200);
                IsBusy = false;
            }
        }

        UserModel previouslySelected;
        UserModel selectedUser;
        public UserModel SelectedUser
        {
            get => selectedUser;
            set => SetProperty(ref selectedUser, value, onChanged: async () => await Selected(selectedUser));
        }

        public async Task Selected(UserModel user)
        {
            if (user == null)
                return;

            SelectedUser = null;

            await shellHelper.GoToAsync($"{nameof(UserDetailPage)}?{nameof(UserDetailViewModel.Id)}={user.Id}");
        }
    }
}
