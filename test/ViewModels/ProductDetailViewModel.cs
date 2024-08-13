using test.Models;
using Microsoft.Maui.Controls;

namespace test.ViewModels
{
    public class ProductDetailViewModel : BindableObject
    {
        private Product _product;

        public Product Product
        {
            get => _product;
            set
            {
                _product = value;
                OnPropertyChanged();
            }
        }

        public ProductDetailViewModel(Product selectedProduct)
        {
            Product = selectedProduct;
        }
    }
}
