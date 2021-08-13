using System.Threading.Tasks;

namespace TechExamApp.Helper
{
    public interface IShellHelper
    {
        Task DisplayAlert(string message);
        Task GoToAsync(string param);
    }
}