using Microsoft.Maui.Controls;
using test.Views;
using test.Models;
namespace test
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }
    }
}
