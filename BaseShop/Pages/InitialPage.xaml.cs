using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Views;

namespace BaseShop.Pages ;

    public partial class InitialPage : ContentPage
    {
        public InitialPage()
        {
            InitializeComponent();
        }
        

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            // Navigate to the Login page
            
            await Navigation.PushAsync(new LoginPage());
        }
        
        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            // Navigate to the Login page
            await Navigation.PushAsync(new RegisterPage());
        }
    }
    
    
    