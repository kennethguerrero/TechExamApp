using Xamarin.Essentials;

namespace TechExamApp.Helper
{
    public class ConnectivityHelper : IConnectivityHelper
    {
        public bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
    }
}
