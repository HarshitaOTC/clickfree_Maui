using clickfree_Maui.Facebook;
using clickfree_Maui.Helpers;
using clickfree_Maui.ViewModel;
using static clickfree_Maui.Instagram.InstagramManager;
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

namespace clickfree_Maui.Windows;

public partial class BackupFromInstagramWindow : ContentPage
{
    BackupFromInstagramDialogVM mBackupFromInstagramDialogVM;
    private List<FacebookManager.MediaResult> mediaResults;
    private string pickedFolder;
    const int WindowWidth = 450;
    const int WindowHeight = 350;
    TransferManager TransferManager;
    public List<MediaResult> MediaResults { get; }
    public Task<string> PickedFolder { get; }

    public BackupFromInstagramWindow()
	{
        BindingContext = mBackupFromInstagramDialogVM;
		InitializeComponent();
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(BackupFromInstagramWindow), (handler, view) =>
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
    }



    public BackupFromInstagramWindow(List<MediaResult> from, string toDir)
    {
      
        InitializeComponent();

        this.BindingContext = mBackupFromInstagramDialogVM = new BackupFromInstagramDialogVM(from, toDir);
        mBackupFromInstagramDialogVM.Finished += MTransferDialogVM_Finished;
    }

    public BackupFromInstagramWindow(List<MediaResult> mediaResults)
    {
        MediaResults = mediaResults;
        
    }

    public BackupFromInstagramWindow(List<FacebookManager.MediaResult> mediaResults, string pickedFolder)
    {
        this.mediaResults = mediaResults;
        this.pickedFolder = pickedFolder;
    }


    #region Event handlers

    private async void MTransferDialogVM_Finished()
    {
        
        await Task.Delay(2000);
        Dispatcher.Dispatch(() =>
        {

            /* while (grid.Children.Count > 0)
             {
                 await Task.Delay(500);
             }
             await Task.Delay(2000);*/

            // Application.Current.CloseWindow(GetParentWindow());
            var window = this.GetParentWindow();
            if (window is not null)
                Application.Current.CloseWindow(window);
        });
    }

    #endregion

    #region Overrides

  

    protected  void OnClosed(EventArgs e)
    {
        //base.OnClosed(e);

        if (this.mBackupFromInstagramDialogVM != null)
        {
            this.mBackupFromInstagramDialogVM.Finished -= MTransferDialogVM_Finished;
            this.mBackupFromInstagramDialogVM.Dispose();
        }
    }

    #endregion

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        if(mBackupFromInstagramDialogVM != null)
        {
            await mBackupFromInstagramDialogVM.StartBackup();
        }
       
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var window = this.GetParentWindow();
        if (window is not null)
            Application.Current.CloseWindow(window);

    }
}