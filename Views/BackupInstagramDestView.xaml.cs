using clickfree_Maui.Contracts.Services;
using clickfree_Maui.ViewModel;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

namespace clickfree_Maui.Views;

public partial class BackupInstagramDestView : ContentPage
{
    const int WindowWidth = 450;
    const int WindowHeight = 350;
    // public BackupInstagramDestVM backupInstagramDestVM;
    public BackupInstagramDestView(BackupInstagramDestVM backupInstagramDestVM)
	{

      
        BindingContext= backupInstagramDestVM;  
		

        InitializeComponent();
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(BackupInstagramDestView), (handler, view) =>
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
}