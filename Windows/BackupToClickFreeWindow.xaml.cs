
using clickfree_Maui.Helpers;
using clickfree_Maui.ViewModel;
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

namespace clickfree_Maui.Windows;

public partial class BackupToClickFreeWindow : ContentPage
{
    private EraseDialogVM mEraseDialogVM;
    BackupDialogVM backupDialogVM;
    private bool IsEraseDevice = false;
    private TransferManager mTransferManager = null;
    private List<string> objList;
    private string toFolder;
     BackupDialogVM mTransferDialogVM;
    TransferDialogVM transferDialogVM;
    const int WindowWidth = 450;
    const int WindowHeight = 350;
  
    public BackupToClickFreeWindow(BackupDialogVM backupDialogVM)
    {
        
        BindingContext = backupDialogVM;
        InitializeComponent();
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(BackupToClickFreeWindow), (handler, view) =>
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
    }
   
   
    public BackupToClickFreeWindow(List<string> from, string toDir)
    {
        InitializeComponent();
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(BackupToClickFreeWindow), (handler, view) =>
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
        this.BindingContext = mTransferDialogVM = new BackupDialogVM(from, toDir);
        mTransferDialogVM.Finished += MTransferDialogVM_Finished;
    }


    public BackupToClickFreeWindow(string toDir, List<string> from) 
    {
        InitializeComponent();
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(BackupToClickFreeWindow), (handler, view) =>
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
        IsEraseDevice = true;
        this.BindingContext = mEraseDialogVM = new EraseDialogVM(toDir, from);
        mEraseDialogVM.Finished += mEraseDialogVM_Finished;
    }

    public BackupToClickFreeWindow()
    {

    }

    private async void mEraseDialogVM_Finished()
    {
        await Task.Delay(1000);
        Dispatcher.Dispatch(async () =>
        {
            var window = this.GetParentWindow();
            if (window is not null)
                Application.Current.CloseWindow(window);
        });
    }

    public async void MTransferDialogVM_Finished()
    {
        DriveManager.CheckAccess();
        await Task.Delay(1000);
        Dispatcher.Dispatch(async () =>
        {
            var window = this.GetParentWindow();
            if (window is not null)
                Application.Current.CloseWindow(window);
        });
    }

    public void OnClosed(EventArgs e)
    {
       
        if (mTransferDialogVM != null)
        {
            mTransferDialogVM.Finished -= MTransferDialogVM_Finished;
            
        }
       
    }
    private async void ContentPage_Loaded(object sender, EventArgs e)
    {

        if (mTransferDialogVM != null)
        {
            await mTransferDialogVM.StartBackup();
        }
        if (mEraseDialogVM != null)
        {
            await mEraseDialogVM.StartErase();
           
        }
        /*IsEraseDevice = true;
        if (IsEraseDevice == true)
        {
            //await Task.Delay(1250);
            var window = this.GetParentWindow();
            if (window is not null)
                Application.Current.CloseWindow(window);
        }*/


    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
    public bool SuccessfullyBackuped
    {

        get
        {
            if (!IsEraseDevice)
                return (mTransferDialogVM?.SuccessfullyBackuped).GetValueOrDefault(false);
            else
                return (mEraseDialogVM?.SuccessfullyBackuped).GetValueOrDefault(false);

        }
    }
    private async void Button_Clicked(object sender, EventArgs e)
    {
   
       // await mTransferManager.CancelAsync();
        var window = this.GetParentWindow();
        if (window is not null)
            Application.Current.CloseWindow(window);
    }
}