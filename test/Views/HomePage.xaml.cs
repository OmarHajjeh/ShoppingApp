using Microsoft.Maui.Controls;
using test.Models;
using test.ViewModels;

namespace test.Views
{
    public partial class HomePage : ContentPage
    {

        public HomePage()
        {
            BindingContext = new HomeViewModel();
            InitializeComponent();
        }

        private async void OnProductSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Product selectedProduct)
            {
                await Navigation.PushAsync(new ProductDetailPage(selectedProduct));
            }
        }
    }
}
