
using clickfree_Maui.Contracts.Services;
using clickfree_Maui.Facebook;
using clickfree_Maui.Instagram;
using clickfree_Maui.Properties;
using clickfree_Maui.ViewModel;
using clickfree_Maui.Windows;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

namespace clickfree_Maui;

public partial class Continue : ContentPage
{
    const int WindowWidth = 450;
    const int WindowHeight = 350;
    WebView webview1;
    private bool showLogin;
    InstagramLoginDialogVM instagramLoginDialogVM;
    private bool logout;
    INavigationService _navigationService;
    private InstagramLoginDialogVM mInstagramLoginDialogVM;
 
    bool login;
    public Continue(INavigationService navigationService,bool Login)
    {
        _navigationService = navigationService;
        InitializeComponent();
        
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(Continue), (handler, view) =>
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
        this.BindingContext = mInstagramLoginDialogVM = new InstagramLoginDialogVM()
        {
            BrowserIsVisible = showLogin
        };

        String TEST = ("You have already logged in as " +  UserName + " : Would you like to continue");
        userLabel.Text = TEST;
          login = Login;
        this.mInstagramLoginDialogVM.Continue += MInstagramLoginDialogVM_Continue;
        this.mInstagramLoginDialogVM.Logout += MInstagramLoginDialogVM_Logout;
        

    }

    public string UserName
    {
        get
        {
            return Settings.Default.InstagramUserName;
        }
    }


    private bool mbLoggout = false;
    private void MInstagramLoginDialogVM_Logout()
    {
        logout = true;
        mbLoggout = true;
        InstagramManager.Logout();
        InstagramManager.CheckAuthorization1(_navigationService, false, true);
    
             var secondWindow = new Window
             {

                 Page = new InstagramLoginWindow(login)
                 {


                 }

             };
             Application.Current.OpenWindow(secondWindow);
        var window = this.GetParentWindow();
        if (window is not null)
            Application.Current.CloseWindow(window);
        //_navigationService.NavigateToMainView();
    }

    public void MInstagramLoginDialogVM_Continue()
    {

        var secondWindow = new Window
        {
            Page = new InstagramLoginWindow(login)
            {


            }

        };
        Application.Current.OpenWindow(secondWindow);
        var window = this.GetParentWindow();
        if (window is not null)
            Application.Current.CloseWindow(window);

    }

   /* private void Button_Clicked(object sender, EventArgs e)
    {
        var secondWindow = new Window
        {
            Page = new InstagramLoginWindow(login)
            {


            }

        };
        Application.Current.OpenWindow(secondWindow);
        var window = this.GetParentWindow();
        if (window is not null)
            Application.Current.CloseWindow(window);
    }*/
}