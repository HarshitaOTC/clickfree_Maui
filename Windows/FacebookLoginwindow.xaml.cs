using clickfree_Maui.Facebook;
using clickfree_Maui.Contracts.Services;
using clickfree_Maui.ViewModel;
using System.Runtime.InteropServices;
using System.Web;
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif
namespace clickfree_Maui.Windows;

public partial class FacebookLoginwindow : ContentPage
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
    const int WindowHeight = 550;
    public bool dialogResult;
    public static INavigationService _navigationService;
    private FacebookLoginDialogVM mFacebookLoginDialogVM;

    public static LoginState state;


    WebView webview;

    private bool mbLoggout = false;
    private static EventHandler webview_Loaded;

    public FacebookLoginwindow(bool showLogin = true)
    {
        // _navigationService = navigationService;
        SuppressWininetBehavior();

        InitializeComponent();

        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(FacebookLoginwindow), (handler, view) =>
        {
  #if WINDOWS
            var mauiWindow = handler.VirtualView;
            var nativeWindow = handler.PlatformView;
            nativeWindow.Activate();
            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
            WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
            AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            //appWindow.Resize(new SizeInt32(WindowWidth, WindowHeight));
            appWindow.MoveAndResize(new RectInt32(1920 / 2 - WindowWidth / 2, 1080 / 2 - WindowHeight / 2, WindowWidth, WindowHeight));
#endif
        });
        this.BindingContext = mFacebookLoginDialogVM = new FacebookLoginDialogVM()
        {
            BrowserIsVisible = showLogin
        };

        this.mFacebookLoginDialogVM.Continue += MFacebookLoginDialogVM_Continue;
        this.mFacebookLoginDialogVM.Logout += MFacebookLoginDialogVM_Logout;

        
        if (showLogin)
        {

        }
        else
        {

        }
    }
    private void MFacebookLoginDialogVM_Logout()
    {
        FacebookManager.Logout();

        mbLoggout = true;

    }

    private void MFacebookLoginDialogVM_Continue()
    {
        //DialogResult = true;

    }
    public static LoginState Show1(bool login, INavigationService _navigationService1, bool logout = false)
    {
        _navigationService = _navigationService1;


        if (!logout && login == false)
        {
            var secondWindow = new Window
            {
                Page = new Continue_facebook(_navigationService, login)
                {
                }
            };
            Application.Current.OpenWindow(secondWindow);
        }
        else if (login == true && !logout)
        {
            var secondWindow = new Window
            {
                Page = new FacebookLoginwindow(login)
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
            _ = Task.Run(async () => { return await FacebookManager.GetAccessCode(code); }).Result;

            _ = _navigationService.BackupFacebookMainView();
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
        //await Launcher.OpenAsync("https://facebook.com/dialog/oauth?client_id=2798931540324100&redirect_uri=https://www.datalogixxmemory.com/&scope=user_photos,user_videos&display=popup");

        if (Properties.Settings.Default.FacebookAccessToken == "" || Properties.Settings.Default.FacebookAccessToken == null)
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
            webview = new WebView { Source = new UrlWebViewSource { Url = "https://facebook.com/accounts/logout/" } };
            webview.HeightRequest = 600;
            webview.Margin = 10;
            grid.Children.Add(webview);
        }
       
        
        grid.Padding= 5;
        webview = new WebView { Source = new UrlWebViewSource { Url = FacebookManager.OAuthURL } };
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

}