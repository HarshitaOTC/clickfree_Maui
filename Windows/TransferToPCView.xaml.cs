using clickfree_Maui.Helpers;
using clickfree_Maui.ViewModel;
using Microsoft.VisualStudio.Services.Profile;
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

namespace clickfree_Maui.Windows;

public partial class TransferToPCView : ContentPage
{
    const int WindowWidth = 450;
    const int WindowHeight = 350;
    private TransferDialogVM mTransferDialogVM;
    TransferDialogVM transferDialogVM;
    public bool isrunning = false;

    public TransferToPCView()
    {
        InitializeComponent();
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(TransferToPCView), (handler, view) =>
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

     
    }
    
    
    public TransferToPCView(string toDir) : this()
    {   
        this.BindingContext = mTransferDialogVM = new TransferDialogVM(toDir);
        
        mTransferDialogVM.Finished += MTransferDialogVM_Finished;
      
    }
    
   
    private  async void MTransferDialogVM_Finished()
    {
        await Task.Delay(1000);
        Dispatcher.Dispatch(async () =>
        {
            await Task.Delay(1500);
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


    private async void ContentPage_Loaded(object sender, EventArgs e)
    {

      
        if (mTransferDialogVM != null)
        {
            await mTransferDialogVM.StartTransfer();
           

        }
  
       /* grid.Clear();
        while (grid.Children.Count > 0)
        {
            await Task.Delay(500);
        }
        await Task.Delay(1250);

        // Application.Current.CloseWindow(GetParentWindow());
        var window = this.GetParentWindow();
        if (window is not null)
            Application.Current.CloseWindow(window);*/

    }
  
    protected  void OnClosed(EventArgs e)
    {
        OnClosed(e);

        if (mTransferDialogVM != null)
        {
            mTransferDialogVM.Finished -= MTransferDialogVM_Finished;  // mTransferDialogVM.AppleFormatDetected -= MTransferDialogVM_AppleFormatDetected;
            mTransferDialogVM.Dispose();
            mTransferDialogVM = null;
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var window = this.GetParentWindow();
        if (window is not null)
            Application.Current.CloseWindow(window);
    }
}