using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseShop.Models;

namespace BaseShop.Pages ;

    public partial class AllProducts : ContentPage
    {
        private List<Product> FullList;
        public AllProducts(List<Product> passedProduct)
        {
            InitializeComponent();
            GetProducts.ItemsSource = passedProduct;
            FullList = passedProduct;
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
            
                foreach (var productStructure in FullList)
                {
                    if (product.ProductID == productStructure.ProductID)
                    {
                        list.Add(productStructure);
                    }
                
                }
                //await DisplayAlert("err", $"tappedProductID is : {product.Product}", "ok");
                await Navigation.PushAsync(new ProductPage(list));
            
            }
            else
            {
                await DisplayAlert("err", $"tappedProductID is null", "ok");
            }
        }
    }