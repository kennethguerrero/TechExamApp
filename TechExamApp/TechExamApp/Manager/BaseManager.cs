using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TechExamApp.Helper;
using Xamarin.Essentials;

namespace TechExamApp.Manager
{
    public class BaseManager
    {
        private readonly IConnectivityHelper connectivityHelper;

        public BaseManager(IConnectivityHelper connectivityHelper)
        {
            this.connectivityHelper = connectivityHelper;
        }

        public bool CheckInternetConnection()
        {
            try
            {
                return connectivityHelper.IsConnected;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
            
        }
    }
}
