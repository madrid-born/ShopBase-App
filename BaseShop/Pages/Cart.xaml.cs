using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using BaseShop.Models;
using Preferences = Xamarin.Essentials.Preferences;


namespace BaseShop.Pages ;

    public partial class Cart : ContentPage
    {
        private void IncrementButton_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (button?.BindingContext is Product{Quantity: < 100 } product)
            {
                string jsonString = Preferences.Get("cart", "");
                List<Product> CurrentCart = JsonSerializer.Deserialize<List<Product>>(jsonString);
                foreach (var productVar in CurrentCart)
                {
                    if (product.ProductID == productVar.ProductID)
                    {
                        productVar.Quantity++;
                        product.Quantity++;
                    }
                }
                jsonString = JsonSerializer.Serialize(CurrentCart);
                Preferences.Set("cart", jsonString);
            }
        }

        private void DecrementButton_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (button?.BindingContext is Product{Quantity: > 1 } product)
            {
                string jsonString = Preferences.Get("cart", "");
                List<Product> CurrentCart = JsonSerializer.Deserialize<List<Product>>(jsonString);
                foreach (var productVar in CurrentCart)
                {
                    if (product.ProductID == productVar.ProductID)
                    {
                        productVar.Quantity--;
                        product.Quantity--;
                    }
                }
                jsonString = JsonSerializer.Serialize(CurrentCart);
                Preferences.Set("cart", jsonString);
            }
        }

        private void RemoveProduct_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (button?.BindingContext is Product product)
            {
                string jsonString = Preferences.Get("cart", "");
                List<Product> CurrentCart = JsonSerializer.Deserialize<List<Product>>(jsonString);
                CurrentCart.RemoveAll(removeProduct => removeProduct.ProductID == product.ProductID);
                jsonString = JsonSerializer.Serialize(CurrentCart);
                Preferences.Set("cart", jsonString);
                Refresh(product);
            }
        }
        
        // Command to handle the refresh action
        public ICommand RefreshCommand { get; }
        
        // Variable to track if the refresh is in progress
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }
        public Cart()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            
            // Initialize the RefreshCommand
            RefreshCommand = new Command(Refresh);
            
            // Set the BindingContext for data binding
            BindingContext = this;
            Refresh(new Product());
        }
        private async void Refresh(object o)
        {
            // Simulating a delay for demonstration purposes then refresh the page
            Device.StartTimer(TimeSpan.FromSeconds(0.5), () =>
            {
                //read data from previous cart
                string jsonString = null;
                List<Product> CurrentCart = new List<Product>();
                try
                {
                    jsonString = Preferences.Get("cart", "");
                    CurrentCart = JsonSerializer.Deserialize<List<Product>>(jsonString);
                }
                catch (Exception e)
                {

                }
                
                // Update the total fee
                double sum = 0;
                try
                {
                    foreach (var product in CurrentCart)
                    {
                        sum += product.SalePrice * product.Quantity;
                    }
                }
                catch (Exception e)
                {
                    DisplayAlert("err", $"error : {e.Message}", "ok");
                    throw;
                }
                
                //update product section
                CartProduct.ItemsSource = CurrentCart;
                //CartFees.ItemsSource = CurrentCart;
                TotalFee.Text = "ریال" + sum;
                IsRefreshing = false; // Set IsRefreshing to false to indicate that the refresh is completed
                return false;
            });
        }
        
        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            double value = e.NewValue;
        }
        private void ProceedButton_Clicked(object sender, EventArgs e)
        {
            
        }
    }