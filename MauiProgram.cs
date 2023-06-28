using clickfree_Maui.Contracts.Services;
using clickfree_Maui.Services;
using clickfree_Maui.ViewModel;
using clickfree_Maui.Views;
using clickfree_Maui.Windows;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Telerik.Maui.Controls.Compatibility;

namespace clickfree_Maui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
            
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
   
            .ConfigureMauiHandlers(x =>
            {
#if ANDROID
    //x.AddHandler<WebView, Platforms.Android.Controls.AndroidWebViewHandler>();
#endif
            });
#if WINDOWS
        builder.Services.AddTransient<IFolderPicker, Platforms.Windows.FolderPicker>();
#elif MACCATALYST
		builder.Services.AddTransient<IFolderPicker, Platforms.MacCatalyst.FolderPicker>();
#endif
        builder.Services.AddTransient<Windows.FacebookLoginwindow>();
        builder.Services.AddTransient<FacebookLoginDialogVM>();
        builder.Services.AddTransient<BackupFacebookMainVM>();
		   builder.Services.AddTransient<BackupFacebookDestView>();
		      builder.Services.AddTransient<BackupFacebookDestVM>();
			     builder.Services.AddTransient <BackupFacebookMainView>();
        builder.Services.AddTransient<BackupFacebookMainVM>();
        builder.Services.AddTransient<BackupFacebookSelectImagesVM>();
        builder.Services.AddTransient<BackupFacebookSelectImagesView>();
        builder.Services.AddTransient<MainPage>();
     
        builder.Services.AddTransient<App>();
        builder.Services.AddTransient<MainPageViewModel>();

        builder.Services.AddTransient<SecondPage>();
        builder.Services.AddTransient<SecondPageViewModel>();

        builder.Services.AddTransient<MainView>();
        builder.Services.AddTransient<MainVM>();

        builder.Services.AddTransient<TransferView>();
        builder.Services.AddTransient<TransferVM>();

        builder.Services.AddTransient<TransferToPCView>();

        builder.Services.AddTransient<InstagramLoginDialogVM>();
        builder.Services.AddTransient<InstagramLoginWindow>();
        builder.Services.AddTransient<Continue_facebook>();
        builder.Services.AddTransient<BackupInstagramMainView>();
        builder.Services.AddTransient<BackupInstagramMainVM>();

        builder.Services.AddTransient<BackupToUSBSelectView>();
        builder.Services.AddTransient<EraseDevice>();

        builder.Services.AddTransient<BackupToUSBMainView>();
        builder.Services.AddTransient<BackupToUSBMainVM>();
        builder.Services.AddTransient<DefaultFolderView>();
        builder.Services.AddTransient<DefaultFolderVM>();
        builder.Services.AddTransient<MessageBoxWindow>();


        builder.Services.AddTransient<BackupInstagramDestView>();
        builder.Services.AddTransient<BackupInstagramDestVM>();
        builder.Services.AddTransient<BackupFromInstagramWindow>();
        builder.Services.AddTransient<BackupInstagramSelectImagesView>();
        builder.Services.AddTransient<BackupInstagramSelectImagesVM>();

        builder.Services.AddTransient<FormatClickFreeWindow>();
        builder.Services.AddTransient<Continue>();
       
        builder.Services.AddTransient<ConfirmationWindow>();
        builder.Services.AddTransient<ClickFreeFormatProgress>();

        builder.Services.AddSingleton<IDataService, DataService>();
        builder.Services.AddSingleton<INavigationService, NavigationService>();

        return builder.Build();
	}
}
