using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using test.Services;
using test.Views;
namespace test.ViewModels
{
    public class LoginViewModel : BindableObject
    {
        private string _username;
        private string _password;
        private bool _rememberMe;
        private readonly ApiService _apiService;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public bool RememberMe
        {
            get => _rememberMe;
            set
            {
                _rememberMe = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            _apiService = new ApiService();
            LoginCommand = new Command(async () => await OnLogin());
        }

        private async Task OnLogin()
        {
            bool isAuthenticated = await _apiService.LoginAsync(Username, Password);

            if (isAuthenticated)
            {
                if (RememberMe)
                {
                    // Logic to remember the user (e.g., save credentials locally)
                }

                await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid username or password.", "OK");
            }
        }
    }

}
