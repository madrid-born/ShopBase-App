using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace BaseShop.Pages ;

    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            if (true)
            {
                await Navigation.PushModalAsync(new LoginPage());
            }
        }
    }