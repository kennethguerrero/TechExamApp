using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TechExamApp.Helper
{
    public class ShellHelper : IShellHelper
    {
        public async Task GoToAsync(string param)
        {
            await Shell.Current.GoToAsync(param);
        }

        public async Task DisplayAlert(string message)
        {
            await Shell.Current.DisplayAlert(null, message, "Close");
        }
    }
}
