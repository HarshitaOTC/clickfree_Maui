using clickfree_Maui.Contracts.Services;
using clickfree_Maui.ViewModel;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

namespace clickfree_Maui.Views;

public partial class TransferView : ContentPage
{
    const int WindowWidth = 750;
    const int WindowHeight = 550;
    public IFolderPicker _folderPicker;
    private readonly INavigation navigation;
    TransferVM thirdPageViewModel;
    public async void FolderPickerMainAsync()
    {
        var pickedFolder = await _folderPicker.PickFolder();

        Path.Text = pickedFolder;

        SemanticScreenReader.Announce(Path.Text);
    }

    public TransferView(IFolderPicker folderPicker)
    {
        thirdPageViewModel = new TransferVM();
        BindingContext = thirdPageViewModel;
        InitializeComponent();
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(TransferView), (handler, view) =>
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
        _folderPicker = folderPicker;
       
    }



    private async void Folderpicker_Clicked(object sender, EventArgs e)
    {
        try
        {
            var pickedFolder = await _folderPicker.PickFolder();
            var path = Path.Text;
            Path.Text = pickedFolder;
            if(pickedFolder == null)
            {
                Path.Text = path;
               // SemanticScreenReader.Announce(path);
            }
            SemanticScreenReader.Announce(Path.Text);

        }
        catch (Exception ex)
        {
            throw ex;
        }
      
    }

   
}