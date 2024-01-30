using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseShop.Pages ;

    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            if (true)
            {
                Preferences.Set("MainPage", "HomePage");
                Application.Current.MainPage = new NavigationPage(new CustomTabbedPage());
            }
            else
            {
                //await DisplayAlert("error", "oops something is wrong", "ok");
            }
        }
    }