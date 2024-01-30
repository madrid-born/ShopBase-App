using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.Json;
using BaseShop.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BaseShop.Pages ;

    public partial class ProductPage : ContentPage
    {
        public static List<Product> CurrentCart = new();

        public ProductPage(List<Product> passedProduct)
        {
            InitializeComponent();
            PImage.Source = passedProduct[0].Picture;
            PNAme.Text= passedProduct[0].ProductName;
            PBrand.Text= passedProduct[0].BrandTitle;
            PDesc.Text= passedProduct[0].ProductDesc;
            GetProducts.ItemsSource = passedProduct;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void addToCart_Clicked(object sender, EventArgs eventArgs)
        {
            if (sender is Button button && button.CommandParameter is Product product)
            {
                //read the data from the previous cart
                string jsonString = Preferences.Get("cart", "");
                CurrentCart = jsonString is "" ? CurrentCart : JsonSerializer.Deserialize<List<Product>>(jsonString);
                
                //check that there is no duplicate of the product in the current cart
                if (CheckProduct(CurrentCart, product))
                {
                    await DisplayAlert("err", $"this item already exist in your cart!", "ok");
                }
                else
                {
                    //add the product to the current cart and then add the cart to the database
                    product.Quantity = 1;
                    CurrentCart.Add(product);
                    jsonString = JsonSerializer.Serialize(CurrentCart);
                    Preferences.Set("cart", jsonString);
                    await DisplayAlert("suc", $"item added successfully", "ok");
                }
            }
        }

        public static bool CheckProduct(List<Product> list, Product product)
        {
            foreach (var productVar in list)
            {
                if (product.ProductID == productVar.ProductID)
                {
                    return true;
                }
            }
            return false;
        }
    }

    