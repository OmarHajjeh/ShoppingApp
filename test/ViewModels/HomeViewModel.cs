using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using test.Models;
using test.Services;
using test.Views;

namespace test.ViewModels
{
    public class HomeViewModel : BindableObject
    {
        private readonly ApiService _apiService;
        private ObservableCollection<Product> _products;
        private ObservableCollection<Product> _filteredProducts;
        private string _searchText;

        public ObservableCollection<Product> FilteredProducts
        {
            get => _filteredProducts;
            set
            {
                if (_filteredProducts != value)
                {
                    _filteredProducts = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    FilterProducts();
                }
            }
        }

        public ICommand ProductSelectedCommand { get; }

        public HomeViewModel()
        {
            _apiService = new ApiService();
            ProductSelectedCommand = new Command<Product>(OnProductSelected);
            LoadProducts();
        }

        private async void LoadProducts()
        {
            try
            {
                ObservableCollection<Product> products;
   
                products = new ObservableCollection<Product>(await _apiService.GetProductsAsync());                
                _products = products;
                FilteredProducts = new ObservableCollection<Product>(products);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine($"Error loading products: {ex.Message}");
            }
        }

        private void FilterProducts()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    FilteredProducts = new ObservableCollection<Product>(_products);
                }
                else
                {
                    FilteredProducts = new ObservableCollection<Product>(_products.Where(p =>
                        p.Description?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true));
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine($"Error filtering products: {ex.Message}");
            }
        }

        private async void OnProductSelected(Product selectedProduct)
        {
            try
            {
                if (selectedProduct == null)
                    return;

                if (Application.Current?.MainPage != null)
                {
                    // Navigate to the product detail page
                    await Application.Current.MainPage.Navigation.PushAsync(new ProductDetailPage(selectedProduct));
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine($"Error selecting product: {ex.Message}");
            }
        }
    }
}
