using Microsoft.Maui.Controls;
using System.ComponentModel;
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

namespace clickfree_Maui.Windows;

public partial class ConfirmationWindow : ContentPage, INotifyPropertyChanged
{
    public static string selectedDriveFormat { get; set; }
    public static FormatClickFreeWindow _formatClickFreeWindow { get; set; }
    int fileCount;
    int directoriesCount;
    int TotalCount;
    public bool close= true;
    const int WindowWidth = 350;
    const int WindowHeight = 250;
    bool format = false;
    public ConfirmationWindow(string selectedDrive, FormatClickFreeWindow formatClickFreeWindow)
    {
        InitializeComponent();
        selectedDriveFormat = selectedDrive;
        _formatClickFreeWindow = formatClickFreeWindow;
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(ConfirmationWindow), (handler, view) =>
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
             var presenter = appWindow.Presenter as OverlappedPresenter;
        presenter.IsResizable = false;
        presenter.IsMaximizable = false;
         presenter.SetBorderAndTitleBar(true, true);
#endif
        });
    }
  

    public async void YesButton_Click(object sender, System.EventArgs e)
    {
        try
        {    
           // yesBtn.IsVisible = false;           
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(selectedDriveFormat);
            fileCount = di.GetFiles().Count();
            directoriesCount = di.GetDirectories().Count();
            TotalCount = fileCount + directoriesCount;
            //ClickFreeFormatProgress win = new ClickFreeFormatProgress(selectedDriveFormat, TotalCount);
            var secondWindow = new Window
            {
                Page = new ClickFreeFormatProgress(selectedDriveFormat, TotalCount)
                {

                }
            };
            Application.Current.OpenWindow(secondWindow);
            format = true;

            await Task.Delay(1250);
         
            var window = this.GetParentWindow();
            if (window is not null)
            Application.Current.CloseWindow(window);
           
        }
        catch (Exception ex)
        {
            FileAttributes attributes = File.GetAttributes(selectedDriveFormat);
            if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                attributes &= ~FileAttributes.ReadOnly;
                File.SetAttributes(selectedDriveFormat, attributes);
                File.Delete(selectedDriveFormat);
            }
            else
            {
                throw;
            }
        }
    }

 
    private void NoButton_Click(object sender, EventArgs e)
    {
        var window = this.GetParentWindow();
        if (window is not null)
            Application.Current.CloseWindow(window);
    }

  
}