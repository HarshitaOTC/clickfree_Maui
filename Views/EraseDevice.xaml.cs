using clickfree_Maui.ViewModel;
using static clickfree_Maui.ViewModel.EraseDeviceVM;
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif
namespace clickfree_Maui.Views;





public partial class EraseDevice : ContentPage
{
    const int WindowWidth = 450;
    const int WindowHeight = 350;
    EraseDeviceVM eraseDeviceVM;
	public EraseDevice()
	{
		BindingContext = eraseDeviceVM;
      
        InitializeComponent(); 
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(EraseDevice), (handler, view) =>
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
        if(lbSelected.SelectedItem != null)
        {
            eraseBtn.IsEnabled = true;
        }
    }

    private void lbSource_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var d1 = e.SelectedItem;  
        var view = new EraseDeviceVM();
        lbSource.ItemsSource = view.GoToFolderCommand((IOInfoBase)d1);
    }
}