using System;
using System.ComponentModel;
using System.Management;
using clickfree_Maui.ViewModel;
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
using static Microsoft.Maui.ApplicationModel.Permissions;
#endif
namespace clickfree_Maui.Windows
{ 

    public partial class ClickFreeFormatProgress : ContentPage
    {
        private EraseDialogVM mEraseDialogVM;
        EraseDialogVM eraseDialogVM;
        const int WindowWidth = 450;
        const int WindowHeight = 300;
        string selectedDriveFormat;
        public int TotalCount = 0;
        public int CurrentCount = 0;
        bool format = false;

        public  ClickFreeFormatProgress(string selecteddrive, int totalCount)
        {
            InitializeComponent();
                Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(ClickFreeFormatProgress), (handler, view) =>
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
            selectedDriveFormat = selecteddrive;
            TotalCount = totalCount;
            progress.Progress = TotalCount;
            var isFormatted = FormatUSB(selectedDriveFormat);
            if (isFormatted)
            {

                Messagebox();


            }
            else
            {

                var secondWindow = new Window
                {
                    Page = new MessageBoxWindow("Format", "Click Free", "Click Free USB Format Failed.", "")
                    {
                    }
                };
                Application.Current.OpenWindow(secondWindow);
            }
            if(format == true)
            {
                var window = this.GetParentWindow();
                if (window is not null)
                    Application.Current.CloseWindow(window);
            }

        }

        public void Messagebox()
        {
            format = true;
            var secondWindow = new Window
            {
                Page = new MessageBoxWindow("Format", "Click Free", "Click Free USB Formatted.", "")
                {
                }
            };
            Application.Current.OpenWindow(secondWindow);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            var window = this.GetParentWindow();
            if (window is not null)
                Application.Current.CloseWindow(window);
            
       
        }

        public bool FormatUSB(string driveLetter, string fileSystem = "FAT32", bool quickFormat = true, int clusterSize = 4096,
            string label = "USB_0000", bool enableCompression = false)
        {
            try
            {
                //add logic to format Usb drive
                //verify conditions for the letter format: driveLetter[0] must be letter. driveLetter[1] must be ":" and all the characters mustn't be more than 2
                if (driveLetter.Length != 2 || driveLetter[1] != ':' || !char.IsLetter(driveLetter[0]))
                    return false;

                //query and format given drive 
                //best option is to use ManagementObjectSearcher

                DirectoryInfo di = new DirectoryInfo(driveLetter);

                foreach (FileInfo file in di.GetFiles())
                {
                    try
                    {
                        file.Delete();
                        TotalCount--;
                  
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    try
                    {
                        dir.Delete(true);
                        TotalCount--;
                   
                    }
                
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                //as per the requirement , we are updating volume label of drive
                //scope start
                DriveInfo objusbdrive = new DriveInfo(driveLetter + "\\");
                objusbdrive.VolumeLabel = "CLICKFREE";

                //scope end
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"select * from Win32_Volume WHERE DriveLetter = '" + driveLetter + "'");
                foreach (ManagementObject vi in searcher.Get())
                {
                    try
                    {
                        var completed = false;
                        var watcher = new ManagementOperationObserver();

                        watcher.Completed += (sender, args) =>
                        {
                           /* var status = Status;
                            Console.WriteLine("USB format completed " + args.Status);*/
                            //Status = "USB format completed ";
                            completed = true;
                           // Status = "Format Completed";

                        };
                        watcher.Progress += (sender, args) =>
                        {
                           // Status = "Format not Completed";
                            /* var status = Status;
                             Console.WriteLine("USB format in progress " + args.Current);*/
                            // Status = "USB format in progress ";

                        };

                        vi.InvokeMethod(watcher, "Format", args: new object[] { fileSystem, quickFormat, clusterSize, label, enableCompression });

                        while (!completed) { System.Threading.Thread.Sleep(1000); }
                    
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async void ContentPage_Loaded(object sender, EventArgs e)
        {
            
            await Task.Delay(5000);
          /*  var secondWindow = new Window
            {
                Page = new MessageBoxWindow()
                {

                }
            };
            Application.Current.OpenWindow(secondWindow);*/
            if (format == true)
            {
                
                var window = this.GetParentWindow();
                if (window is not null)
                    Application.Current.CloseWindow(window);

            }
            // Dispatcher.Dispatch(() => DisplayAlert("Title", "Format is completed", "Ok"));
           
        }
       
    }
   
    
}