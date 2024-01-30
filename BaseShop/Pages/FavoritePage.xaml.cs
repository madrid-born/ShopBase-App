using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseShop.Models;

namespace BaseShop.Pages ;

    public partial class FavoritePage : ContentPage
    {
        public FavoritePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }