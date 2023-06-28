using clickfree_Maui.Views;

namespace clickfree_Maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("MainPage", typeof(MainPage));

    }
}
