using clickfree_Maui.Facebook;
using clickfree_Maui.ViewModel;
using static clickfree_Maui.Facebook.FacebookManager;
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

namespace clickfree_Maui.Windows;

public partial class BackupFromFacebookWindow : ContentPage
{
    #region Fields

    private BackupFromFacebookDialogVM mBackupFromFacebookDialogVM;
    const int WindowWidth = 450;
    const int WindowHeight = 350;
    #endregion

    #region Ctor

    public BackupFromFacebookWindow(List<MediaResult> from, string toDir)
    {
        InitializeComponent();
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(BackupFromFacebookWindow), (handler, view) =>
        {
#if WINDOWS
            var mauiWindow = handler.VirtualView;
            var nativeWindow = handler.PlatformView;
            nativeWindow.Activate();
            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
            WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
            AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
           
            appWindow.MoveAndResize(new RectInt32(1920 / 2 - WindowWidth / 2, 1080 / 2 - WindowHeight / 2, WindowWidth, WindowHeight));
           //appWindow.Resize(new SizeInt32(WindowWidth, WindowHeight));
#endif
        });
        this.BindingContext = this.mBackupFromFacebookDialogVM = new BackupFromFacebookDialogVM(from, toDir);
        this.mBackupFromFacebookDialogVM.Finished += this.MTransferDialogVM_Finished;
    }

    public BackupFromFacebookWindow()
    {
    }
    #endregion

    #region Event handlers

    private async void MTransferDialogVM_Finished()
     {
        await Task.Delay(1000);
        Dispatcher.Dispatch(() =>
        {
            var window = this.GetParentWindow();
            if (window is not null)
                Application.Current.CloseWindow(window);
        });

    }
    
    #endregion

    #region Overrides

    private async void OnLoaded(object sender, EventArgs e)
    {
        //base.OnLoaded();

        await mBackupFromFacebookDialogVM.StartBackup();
    }

    public void OnClosed(EventArgs e)
    {
        //base.OnClosed(e);

        if (this.mBackupFromFacebookDialogVM != null)
        {
            this.mBackupFromFacebookDialogVM.Finished -= MTransferDialogVM_Finished;
            this.mBackupFromFacebookDialogVM.Dispose();
        }
    }

    #endregion

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await mBackupFromFacebookDialogVM.StartBackup();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var window = this.GetParentWindow();
        if (window is not null)
            Application.Current.CloseWindow(window);
    }
}
