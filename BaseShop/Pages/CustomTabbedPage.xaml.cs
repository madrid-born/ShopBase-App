using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using TabbedPage = Microsoft.Maui.Controls.TabbedPage;

namespace BaseShop.Pages ;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomTabbedPage : TabbedPage
    {
        public CustomTabbedPage()
        {
            InitializeComponent();
            CurrentPage = Children[1];
            On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().SetIsSwipePagingEnabled(false); // Disable swipe gesture
        }
    }
    