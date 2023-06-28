using clickfree_Maui.ViewModel;

namespace clickfree_Maui.Views;

public partial class SecondPage : ContentPage
{
	public SecondPage(SecondPageViewModel secondPageViewModel)
	{
		BindingContext= secondPageViewModel;
		InitializeComponent();
	}
}