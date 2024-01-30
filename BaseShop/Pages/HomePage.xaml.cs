using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BaseShop.Models;
using Newtonsoft.Json;

namespace BaseShop.Pages ;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        LoadCart();
        await LoadProductsAsync("GetProductGroups");
        await LoadProductsAsync("GetProducts");
        await LoadProductsAsync("GetBest");
    }

    public static List<Product> FullList = new List<Product>();
    public static List<Product> Cart = new List<Product>();
    public static List<Product> products = new List<Product>();

    private void LoadCart()
    {
        string jsonString = Preferences.Get("Cart", "");
        Cart = JsonConvert.DeserializeObject<List<Product>>(jsonString);
    }
    private async Task LoadProductsAsync(string keyValue)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                // Define the URL of the API
                string apiUrl = "https://apiformul86.toranjapp.ir/api/Values/Post";

                // Define your API credentials
                string apiKey = "aUpMUDdZNVIkRWhUSkA5aDJHQ2k=";

                // Prepare the body data as x-www-form-urlencoded
                var requestBody = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("", $"<root><ReportKey>{keyValue}</ReportKey></root>")
                });

                // Set the Authorization header
                httpClient.DefaultRequestHeaders.Add("AppId", $"api_key {apiKey}");

                // Make an HTTP POST request and read the response
                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, requestBody);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string responseContent = await response.Content.ReadAsStringAsync();
                    ReadData(responseContent, keyValue);
                }
                else
                {
                    await DisplayAlert("err", $"Request failed with status code: {response.StatusCode}", "ok");
                }
            }
            catch (HttpRequestException ex)
            {
                await DisplayAlert("err", $"An error occurred: {ex.Message}", "ok");
            }
        }
    }
    
    private void ReadData(string responseContent, string keyValue){
        
        // according to keyValue it fill the right place in the xaml file
        if (keyValue == "GetProducts")
        {
            ProductsRootObject rootObject = JsonConvert.DeserializeObject<ProductsRootObject>(responseContent);
            FullList = rootObject.Value;
            rootObject.RemoveDuplicateProducts();
            products = PickRandomObjects(rootObject.Value, 10);
            Random random = new Random();
            foreach (var VARIABLE in products)
            {
                int randomInteger = random.Next(4, 9);
                VARIABLE.Picture = $"sample{randomInteger}";
            }
            GetProducts.ItemsSource = products;
        }else if (keyValue == "GetProductGroups")
        {
            ProductGroupRootObject rootObject = JsonConvert.DeserializeObject<ProductGroupRootObject>(responseContent);
            GetProductGroups.ItemsSource = rootObject.Value;
        }else if (keyValue == "GetBest")
        {
            GetBest.ItemsSource = products;
        }
    }
    
    private static List<Product> PickRandomObjects<Product>(List<Product> originalList, int count)
    {
        Random random = new Random();
        int totalObjects = originalList.Count;
        List<Product> randomObjects = new List<Product>();

        // Handle the case when the count exceeds the size of the original list
        count = Math.Min(count, totalObjects);

        while (randomObjects.Count < count)
        {
            int randomIndex = random.Next(totalObjects);
            Product randomObject = originalList[randomIndex];

            if (!randomObjects.Contains(randomObject))
            {
                randomObjects.Add(randomObject);
            }
        }

        return randomObjects;
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
    
    private async void CategoryTapped(object sender, EventArgs e)
    {
        var tappedGroup = (sender as Grid)?.GestureRecognizers
            .OfType<TapGestureRecognizer>()
            .FirstOrDefault()
            ?.CommandParameter;
        List<Product> list = new List<Product>();
        if (tappedGroup is ProductGroup productGroup )
        {
            
            foreach (var productStructure in FullList)
            {
                if (productGroup.GroupID == productStructure.GroupID)
                {
                    list.Add(productStructure);
                }
                
            }
            //await DisplayAlert("err", $"tappedProductID is : {product.Product}", "ok");
            await Navigation.PushAsync(new CategoryPage(list));
            
        }
        else
        {
            await DisplayAlert("err", $"tappedProductID is null", "ok");
        }
    }

    private async void ViewAll(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AllProducts(FullList));
    }
}
