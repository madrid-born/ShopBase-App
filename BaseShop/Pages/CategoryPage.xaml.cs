using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseShop.Models;
using BaseShop.Pages;

namespace BaseShop.Pages ;

    public partial class CategoryPage : ContentPage
    {
        public CategoryPage(List<Product> passedProduct)
        {
            InitializeComponent();
            GetProduct.ItemsSource = passedProduct;
            NavigationPage.SetHasNavigationBar(this, false);
        }
        
        private async void ProductTapped(object sender, EventArgs e)
        {
            var tappedProduct = (sender as Grid)?.GestureRecognizers
                .OfType<TapGestureRecognizer>()
                .FirstOrDefault()
                ?.CommandParameter;
            List<Product> list = new List<Product>();
            if (tappedProduct is Product product)
            {
            
                foreach (var productStructure in HomePage.FullList)
                {
                    if (product.ProductID == productStructure.ProductID)
                    {
                        list.Add(productStructure);
                    }
                
                }
                await Navigation.PushAsync(new ProductPage(list));
            
            }
            else
            {
                await DisplayAlert("err", $"tappedProductID is null", "ok");
            }
        }
    }