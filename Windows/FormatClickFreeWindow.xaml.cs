using clickfree_Maui.Helpers;
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using System.Runtime.InteropServices;
using Windows.Graphics;
#endif
namespace clickfree_Maui.Windows;

public partial class FormatClickFreeWindow : ContentPage
{
    List<UsbDisk> disks = new List<UsbDisk>();
    List<string> USBNameList = new List<string>();
    const int WindowWidth = 450;
    const int WindowHeight = 350;

   
    public FormatClickFreeWindow()
	{
		InitializeComponent();
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(FormatClickFreeWindow), (handler, view) =>
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
            var presenter = appWindow.Presenter as OverlappedPresenter;
        presenter.IsResizable = false;
        presenter.IsMaximizable = false;
         presenter.SetBorderAndTitleBar(true, true);
#endif
        });


    }

    private void Window_Loaded(object sender, EventArgs e)
    {
        disks = DriveManager.GetAvailableDisks();
        foreach (var disk in disks)
        {
            var name = disk.Name.Split('\\')[0] + "-Clickfree";
            USBNameList.Add(name);
            UsbListComboBox.SelectedItem = USBNameList.FirstOrDefault();
            UsbListComboBox.ItemsSource = USBNameList;
        }
       
    }

    public void FormatBtn_Clicked(object sender, EventArgs e)
    {
        try
        {
            Dispatcher.Dispatch(() =>
            {
                this.FormatBtn.IsEnabled = false;
            });

            string selectedDrive = Convert.ToString(UsbListComboBox.SelectedItem.ToString().Split('-')[0]);

            //ConfirmationWindow confirmation = new ConfirmationWindow(selectedDrive, this);
            var secondWindow = new Window
            {

                Page = new ConfirmationWindow(selectedDrive, this)
                {

                }
            };

            Application.Current.OpenWindow(secondWindow);

          

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void CloseWindow_Clicked(object sender, EventArgs e)
    {
        
        var window = this.GetParentWindow();
        if (window is not null)
            Application.Current.CloseWindow(window);
    }

    private void UsbListComboBox_SelectionChanged(object sender, Telerik.Maui.Controls.ComboBoxSelectionChangedEventArgs e)
    {

    }
}