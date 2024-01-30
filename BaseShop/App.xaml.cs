using BaseShop.Pages;
namespace BaseShop;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		string main = Preferences.Get("MainPage", "");
        		if (main == "HomePage")
        		{
        			MainPage = new NavigationPage(new CustomTabbedPage());
        
        		}
        		else
        		{
        			MainPage = new NavigationPage(new InitialPage());
        		}
	}
}
