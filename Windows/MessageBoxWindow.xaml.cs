#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

using clickfree_Maui.Helpers;

namespace clickfree_Maui.Windows;

public partial class MessageBoxWindow : ContentPage
{
    #region Nested types
    const int WindowWidth = 550;
    const int WindowHeight = 250;
    public enum MessageBoxType
    {
        Success,
        Information,
        Warning,
        Error
    }
    string dir;
    #endregion

    #region Ctor

    public MessageBoxWindow(string Type,string Title, string Msg, string Path)
    {
       
        InitializeComponent();
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(MessageBoxWindow), (handler, view) =>
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
            nativeWindow.ExtendsContentIntoTitleBar = false;           
        var presenter = appWindow.Presenter as OverlappedPresenter;
        presenter.IsResizable = false;
        presenter.IsMaximizable = false;
         presenter.SetBorderAndTitleBar(true, false);
      
#endif
        });
        switch (Type)
        {
            case "Eject":
                {
                    btnCancel.HorizontalOptions = LayoutOptions.Center;
                    btnViewFolder.IsVisible = false;
                }
                break;
            case "Error":
                {
                    btnCancel.HorizontalOptions = LayoutOptions.Center;
                    btnViewFolder.IsVisible = false;
                }
                break;

            case "BackupDialog":
                {
                    btnViewFolder.IsVisible = true;
                }
                break;
            case "BackupFromFacebook":
                {
                    btnViewFolder.IsVisible = true;
                }
                break;
            case "BackupFromInstagram":
                {
                    btnViewFolder.IsVisible = true;
                }
                break;
            case "EraseDialog":
                {
                    btnViewFolder.IsVisible = true;
                }
                break;
            case "TransferDialog":
                {
                    btnViewFolder.IsVisible = true;
                }
                break;

        }
        txtTitle.Text = Title;
        tbMessage.Text = Msg;
        dir = Path;
        this.BindingContext = this;
       
       
    }
 
    private void btnViewFolder_Clicked(object sender, EventArgs e)
    {
      // string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), Constants.ClickFreeFolderName);
        if (DriveManager.CheckAccess())
        {
            string ss = DriveManager.SelectedUSBDrive.Name;
            System.Diagnostics.Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", dir);
         }
        //if (btnViewFolder.Tag is Action action)
        //    action.BeginInvoke(null, this);
        var window = this.GetParentWindow();
        if (window is not null)
            Application.Current.CloseWindow(window);
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        var window = this.GetParentWindow();
        if (window is not null)
            Application.Current.CloseWindow(window);
    }
    #endregion  

}