using clickfree_Maui.Contracts.Services;
using clickfree_Maui.Facebook;
using clickfree_Maui.Helpers;
using clickfree_Maui.Instagram;
using clickfree_Maui.Windows;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace clickfree_Maui.ViewModel
{
    public class MainVM : ViewModelBase
    {
        readonly IDataService _dataService;
        readonly INavigationService _navigationService;

        public Command NextCommand
            => new Command(async () => await _navigationService.NavigateToMainView());
        public Command TransferToPC => new Command(async () => await _navigationService.TransferView());
        public Command BackupToUSBCommand
         => new Command(async () => await _navigationService.BackupToUSBMainView());
        public Command ViewClickFreeCommand
           => new Command(async () =>
           {
               if (DriveManager.CheckAccess())
               {
                   System.Diagnostics.Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", DriveManager.SelectedUSBDrive.Name);
               }

           });
        public Command BackupFromInstagramCommand
        => new Command(async () =>
        {

            InstagramManager.CheckAuthorization1(_navigationService);


        });

        public Command TransferSelectedFilesCommand
        => new Command(async () =>
        {
            if (DriveManager.CheckAccess())
            {
                await _navigationService.EraseDevice();
            }

        });
        public ICommand EailSupportCommand=> new RelayCommand(() =>
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", "http://download.metoosoftware.com/sendemail.aspx");
                          //  Process.Start("http://download.metoosoftware.com/sendemail.aspx");
                            //Process.Start("mailto:wecare@clickfreebackup.com?subject=Click Free (Windows)");
                        }
                        catch { }
                    });
        public ICommand ChatSupportCommand => new RelayCommand(() =>
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", "https://download.metoosoftware.com/chatintegration.htm");
                           // Process.Start("https://download.metoosoftware.com/chatintegration.htm");
                        }
                        catch { }
                    });
                
        public Command FAQCommand => new Command(() =>
        {
            System.Diagnostics.Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", "https://easycustomersupport.com/175677-ClickFree-Backup");
            // Process.Start("https://easycustomersupport.com/");
            //Process.Start("https://www.datalogixxmemory.com/faq");
            //Process.Start("https://clickfreebackup.com/pages/technical-support");
        });

        public Command GetHelpCommand => new Command(() =>
        {
            System.Diagnostics.Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", "https://bit.ly/3nDc4YK");
            // Process.Start("https://easycustomersupport.com/");
            //Process.Start("https://www.datalogixxmemory.com/faq");
            //Process.Start("https://clickfreebackup.com/pages/technical-support");
        });

        public Command ContactUSCommand => new Command(() =>
        {
            var secondWindow = new Window
            {
                Page = new ContactusWindow()
                {

                }
            };
            Application.Current.OpenWindow(secondWindow);
        });
        public Command DefaultFoldersCommand
        => new Command(async () =>
        {

            await _navigationService.DefaultFolderView();


        });
        public Command HowtovideoCommand
        => new Command(async () =>
        {
            System.Diagnostics.Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", "https://www.youtube.com/channel/UCv-vJ0GsXsTLNRsiwSjVCZQ");

        });
		 public Command BackupFromFacebookCommand
        => new Command(async () =>
        {
            FacebookManager.CheckAuthorization1(_navigationService);
            {
                //await _navigationService.FacebookLoginwindow();
            }

        });
        public Command FormatClickFreeCommand
        => new Command(async () =>
        {
            var secondWindow = new Window
            {
                Page = new FormatClickFreeWindow()
                {

                }
            };
            Application.Current.OpenWindow(secondWindow);

        });
        public MainVM(IDataService dataService, INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
        }
        public MainVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override Task OnNavigatedFrom(bool isForwardNavigation)
        {
            Console.WriteLine($"On {(isForwardNavigation ? "forward" : "backward")} navigated from SecondPage");
            return base.OnNavigatedFrom(isForwardNavigation);
        }

        public override Task OnNavigatingTo(object? parameter)
        {
            Console.WriteLine($"On navigating to SecondPage with parameter {parameter}");
            return base.OnNavigatingTo(parameter);
        }

        public override Task OnNavigatedTo()
        {
            Console.WriteLine("On navigated to SecondPage");
            return base.OnNavigatedTo();
        }

    }
}
