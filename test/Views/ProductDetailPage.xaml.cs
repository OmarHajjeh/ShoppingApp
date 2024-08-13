using Microsoft.Maui.Controls;
using test.Models;
using test.ViewModels;
namespace test.Views;

public partial class ProductDetailPage : ContentPage
{
    public ProductDetailPage(Product selectedProduct)
    {
        InitializeComponent();
        BindingContext = new ProductDetailViewModel(selectedProduct);
    }


}
