using clickfree_Maui.Contracts.Services;
using clickfree_Maui.Helpers;
using clickfree_Maui.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clickfree_Maui.ViewModel
{

    public class BackupToUSBMainVM : ViewModelBase
    {
        readonly IDataService _dataService;
        readonly INavigationService _navigationService;
        
        public BackupToUSBMainVM(IDataService dataService, INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
        }
        public BackupToUSBMainVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        public Command TransferDefaultCommand
        => new Command(() =>
        {
            if (DriveManager.CheckAccess())
            {
                DateTime dateTime = DateTime.Now;
                string date = dateTime.ToString("dd-MM-yyyy");

                string folder = Path.Combine(DriveManager.SelectedUSBDrive.Name, (Environment.OSVersion).ToString(), Environment.MachineName, Environment.UserName, Constants.Person);
                string toFolder = Path.Combine(DriveManager.SelectedUSBDrive.Name, (Environment.OSVersion).ToString(), Environment.MachineName, Environment.UserName, Constants.WindowsBackupFolderName + "\\Complete Backup - Photos and Videos\\" + date);
                string defaultdirectory = string.Empty;
                var ownerWindow = Application.Current.Windows[Application.Current.Windows.Count - 1];
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                List<string> objList = new List<string>();
                if (settings["DefaultFolders"] != null)
                    objList.Add(settings["DefaultFolders"].Value.ToString());
                else
                    objList = Constants.DefaultBackUpFolders;

                //to foldet is not exists then we need to make this directory
                foreach (var obj in objList)
                {

                    if (obj.Contains("Contacts"))
                    {
                        toFolder = Path.Combine(DriveManager.SelectedUSBDrive.Name, (Environment.OSVersion).ToString(), Environment.MachineName, Environment.UserName, Constants.Person + "\\Contacts\\");

                    }
                }
                if (!Directory.Exists(toFolder))
                {
                    Directory.CreateDirectory(folder);
                    Directory.CreateDirectory(toFolder);

                }
                var secondWindow = new Window
                {

                    Page = new BackupToClickFreeWindow(objList, toFolder)
                    {

                    }

                };

                Application.Current.OpenWindow(secondWindow);
            }
        });
       /* public Command TransferSelectedFilesCommand => new(()=>
        {
            if (DriveManager.CheckAccess())
            {
                _navigationService.BackupToUSBSelectView();
            }              
        });
   */

        public Command TransferSelectedFilesCommand
            => new Command(async () => await _navigationService.BackupToUSBSelectView());




    }
}
