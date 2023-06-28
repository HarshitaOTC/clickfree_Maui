using clickfree_Maui.Helpers;
using clickfree_Maui.ViewModel;
using Microsoft.Maui.Graphics;

using System.Reflection;
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

namespace clickfree_Maui.Views;

public partial class MainView : ContentPage
{
    List<UsbDisk> disks = new List<UsbDisk>();
    string USBName;
    string formatResult;
    const int WindowWidth = 750;
    const int WindowHeight = 550;


    public MainView(MainVM mainVM)
	{
		BindingContext= mainVM;
		InitializeComponent();
        DriveManager.MenuName = "Main";
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(MainView), (handler, view) =>
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
    private void MainBtn_Clicked(object sender, EventArgs e)
    {
        bool ifDrive = DriveManager.HasUsbDrives;
        if (ifDrive == true)
        {
            MainPanel.IsVisible = true;
        }
        else
        {
            MainPanel.IsVisible = false;
        }
        DriveManager.MenuName = "Main";
        SettingsPanel.IsVisible = false;
        AboutPanel.IsVisible = false;
    }

    public void SettingsBtn(object sender, System.EventArgs e)
    {
        bool ifDrive = DriveManager.HasUsbDrives;
        if (ifDrive == true)
        {

            SettingsPanel.IsVisible = true;
        }
        else
        {
            SettingsPanel.IsVisible = false;
        }
        DriveManager.MenuName = "Setting";
        MainPanel.IsVisible = false;
        AboutPanel.IsVisible = false;
    }
    private void AboutBtnClick(object sender, EventArgs e)
    {
        bool ifDrive = DriveManager.HasUsbDrives;
        if (ifDrive == true)
        {
            AboutPanel.IsVisible = true;
        }
        else
        {
            AboutPanel.IsVisible = false;
        }
        DriveManager.MenuName = "About";
        MainPanel.IsVisible = false;
        SettingsPanel.IsVisible = false;
        //firstBorder.Background = Brushes.Transparent;
        var disks = DriveManager.GetAvailableDisks();
        var disk = disks.FirstOrDefault();
        FirmwareVersionlbl.Text = (string)disk.FirmwareRevision;
        ApplicationVersion();
        Storagelbl.Text = Math.Floor(disk.Size / 1024.0 / 1024.0 / 1024.0) + " GB";
        StorageAvailablelbl.Text = Math.Floor(disk.FreeSpace / 1024.0 / 1024.0 / 1024.0) + " GB";
        Yearlbl.Text = "© " + DateTime.Now.Year + " Me Too Software, Inc. All rights reserved.";
        lblFileSystem.Text = disk.FileSystem;
       // AppVersionlbl.Text = "1.1.1.112";
       // Yearlbl.Text = "© " + DateTime.Now.Year + " Me Too Software, Inc. All rights reserved.";
    }
    public void ApplicationVersion()
    {
        AppVersionlbl.Text = "Version " + GetRunningVersion();
    }
    private string GetRunningVersion()
    {
        try
        {
            /*if (!VersionTracking.Default.IsFirstLaunchEver)
            {
                return VersionTracking.Default.IsFirstLaunchEver.ToString();
            }
            else
                return VersionTracking.Default.IsFirstLaunchEver.ToString();*/
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
        catch (Exception ex)
        {

            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
    private void dispatcherTimer_Tick(object sender, EventArgs e)
    {
        DriveManager.CheckAccess();
        bool ifDrive = DriveManager.HasUsbDrives;
        if (ifDrive == true)
        {
            promtUserAboutDevice.IsVisible = false;
            AboutBtn.IsVisible = true;
            var DiskInfo = DriveManager.GetAvailableDisks().FirstOrDefault();
            if (DiskInfo != null)
            {
                double bytesFs = DiskInfo.FreeSpace;
                double kilobyteFs = bytesFs / 1024;
                double megabyteFs = kilobyteFs / 1024;
                double gigabyteFs = megabyteFs / 1024;

                double bytesS = DiskInfo.Size;
                double kilobyteS = bytesS / 1024;
                double megabyteS = kilobyteS / 1024;
                double gigabyteS = megabyteS / 1024;

                usbButton.BackgroundColor = Color.FromRgb(19, 136, 8);
                //usbButton.HeightRequest = 35;
                //usbButton.WidthRequest = 40;
                //usbButton.ImageSource = "usb.png";

                //usbButton.Background = Green;
                //connection.Content = "Connected";
                //space.Content = (float)Math.Round(gigabyteFs, 1) + " GB available out of " + (float)Math.Round(gigabyteS, 1) + " GB";


                switch (DriveManager.MenuName)
                {
                    case "Main":
                        {
                            MainBtn_Clicked(null, null);
                        }
                        break;

                    case "Setting":
                        {
                            SettingsBtn(null, null);
                        }
                        break;
                    case "About":
                        {
                            AboutBtnClick(null, null);
                        }
                        break;
                    default:
                        {
                            MainBtn_Clicked(null, null);
                        }
                        break;
                }

            }

        }
        else
        {
            promtUserAboutDevice.IsVisible = true;
            AboutBtn.IsVisible = false;
            //usbButton.ImageSource = "usb.png";
            //usbButton.HeightRequest = 35;
            //usbButton.WidthRequest = 40;
            usbButton.BackgroundColor = Color.FromRgb(204, 27, 14);

            //connection.Content = "Disconnected";
            //space.Content = "";
            MainPanel.IsVisible = false;
            SettingsPanel.IsVisible = false;

        }

    }


    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        IDispatcherTimer timer;
        timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromMilliseconds(1000);
        timer.Tick += new EventHandler(dispatcherTimer_Tick);
        timer.Start();
      
    }

}