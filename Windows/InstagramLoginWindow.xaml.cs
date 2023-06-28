using clickfree_Maui;
using clickfree_Maui.Helpers;
using clickfree_Maui.Instagram;
using clickfree_Maui.ViewModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Web;
using Microsoft.Maui.Authentication;
using Microsoft.Maui.Controls;
using System;
using System.Runtime.CompilerServices;
using clickfree_Maui.Contracts.Services;
using clickfree_Maui.Services;
using clickfree_Maui.Views;
using static clickfree_Maui.Instagram.InstagramManager;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Web.Http;
using System.Net;
using System.Drawing;
using Microsoft.Maui.Controls.Compatibility;
using StackLayout = Microsoft.Maui.Controls.Compatibility.StackLayout;
using clickfree_Maui.Properties;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;






#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

namespace clickfree_Maui.Windows;

public partial class InstagramLoginWindow : ContentPage
{
    #region WINAPI

    [DllImport("wininet.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
    public static extern bool InternetSetOption(int hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);

    private static unsafe void SuppressWininetBehavior()
    {

        int option = (int)3/* INTERNET_SUPPRESS_COOKIE_PERSIST*/;
        int* optionPtr = &option;

        bool success = InternetSetOption(0, 81/*INTERNET_OPTION_SUPPRESS_BEHAVIOR*/, new IntPtr(optionPtr), sizeof(int));
    }
    readonly IDataService _dataService;
   // readonly INavigationService _navigationService;
    #endregion
    

    #region Nested types

    public enum LoginState
    {
        Success,
        Failed,
        LoggedOut
    }

    #endregion
    const int WindowWidth = 650;
    const int WindowHeight = 650;
    public bool dialogResult;
    public static INavigationService _navigationService;
    private InstagramLoginDialogVM mInstagramLoginDialogVM;
    public static LoginState state = LoginState.Failed;    
    Microsoft.Maui.Controls.WebView webview;
    

    private bool mbLoggout = false;
    private Button button;

    public InstagramLoginWindow(bool showLogin = true)
    {
       
        SuppressWininetBehavior();

        InitializeComponent();
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(InstagramLoginWindow), (handler, view) =>
        {
#if WINDOWS
            var mauiWindow = handler.VirtualView;
            var nativeWindow = handler.PlatformView;
            nativeWindow.Activate();
            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
            WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
            AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            appWindow.MoveAndResize(new RectInt32(1920 / 2 - WindowWidth / 2, 1080 / 2 - WindowHeight / 2, WindowWidth, WindowHeight));
            // appWindow.Resize(new SizeInt32(WindowWidth, WindowHeight));

#endif
        });
         this.BindingContext = mInstagramLoginDialogVM = new InstagramLoginDialogVM()
         {
             BrowserIsVisible = showLogin
         };
      if(showLogin)
      {
           
      }
  

    }
  
    public static  LoginState Show1(bool login, INavigationService _navigationService1,bool logout=false)
   {
        _navigationService = _navigationService1;
      
    
        if(!logout && login == false)
        { 
        var secondWindow = new Window
        {
            Page = new Continue(_navigationService, login)
            {
            }  
        };
        Application.Current.OpenWindow(secondWindow);
        }
        else if(login == true && !logout)
        {
            var secondWindow = new Window
            {
                Page = new InstagramLoginWindow(login)
                {
                }
            };
            Application.Current.OpenWindow(secondWindow);
        }
        if (logout == true)
        {
           
            state = LoginState.LoggedOut;
        }
        else
        state = LoginState.Success;
        return state;

    }

    private async void webview_Navigated(object sender, WebNavigatedEventArgs e)
    {
        
        Uri obj = new Uri(e.Url);
        var code = HttpUtility.ParseQueryString(obj.Query)["code"];
        if (!string.IsNullOrWhiteSpace(code))
        {  
                _= Task.Run(async () => { return await InstagramManager.GetAccessCode(code); }).Result;

            _ = _navigationService.BackupInstagramMainView();
            grid.Clear();
            while (grid.Children.Count > 0)
            {
                await Task.Delay(500);
            }
            await Task.Delay(1250);

            // Application.Current.CloseWindow(GetParentWindow());
            var window = this.GetParentWindow();
            if (window is not null)
                Application.Current.CloseWindow(window);
        }

       
    }


    private async void Webview_Loaded(object sender, EventArgs e)
    {
        //await Launcher.OpenAsync("https://instagram.com/oauth/authorize?client_id=884907049081721&redirect_uri=https://www.datalogixxmemory.com&response_type=code&scope=user_profile,user_media");
        if (Properties.Settings.Default.InstagramAccessToken == "" || Properties.Settings.Default.InstagramAccessToken == null)
        {
            /* webview = new WebView
             {
                 Source = new UrlWebViewSource
                 {
                     Url = "https://www.instagram.com/accounts/logout/"

                 },
                 Margin = 10,
                 VerticalOptions = LayoutOptions.FillAndExpand
             };

             this.Content = new StackLayout
             {
                 Children =
                 {
                    webview
                 }
             };*/
            webview = new WebView { Source = new UrlWebViewSource { Url = "https://www.instagram.com/accounts/logout/" } };
            webview.HeightRequest = 600;
            webview.Margin = 10;
            grid.Children.Add(webview);
        }

        webview = new WebView { Source = new UrlWebViewSource { Url = InstagramManager.OAuthURL } };
        webview.HeightRequest = 600;
        webview.Margin = 10;
        grid.Children.Add(webview);
        webview.Navigated += webview_Navigated;


        /* webview = new WebView
                {
                    Source = new UrlWebViewSource
                    {
                        Url = InstagramManager.OAuthURL

                    },
                    Margin = 10,

                    VerticalOptions = LayoutOptions.FillAndExpand
                };*/
        //this.Content = new StackLayout
        //{

        //    Children =
        //        {
        //           webview

        //        }

        //};

    }

 
    //private async void Button_Clicked(object sender, EventArgs e)
    //{
    //    grid.Clear();
    //    while (grid.Children.Count > 0)
    //    {
    //        await Task.Delay(500);
    //    }
    //    await Task.Delay(1250);

    //   // Application.Current.CloseWindow(GetParentWindow());
    //    var window = this.GetParentWindow();
    //    if (window is not null)
    //        Application.Current.CloseWindow(window);
    //}
}







