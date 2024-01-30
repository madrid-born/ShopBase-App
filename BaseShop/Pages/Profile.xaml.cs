using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BaseShop.Models;

namespace BaseShop.Pages ;

    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void PurchaseHistory(object sender, TappedEventArgs e)
        {
            await Navigation.PushAsync(new PurchaseHistory());
        }

        private async void FavoriteList(object sender, TappedEventArgs e)
        {
            await Navigation.PushAsync(new FavoritePage());
        }

        private async void AccountInformation(object sender, TappedEventArgs e)
        {
            await Navigation.PushAsync(new AccountInformationPage());
        }

        private async void Signout(object sender, TappedEventArgs e)
        {
            Preferences.Set("MainPage", "InitialPage");
            Application.Current.MainPage = new NavigationPage( new InitialPage());
        }
    }