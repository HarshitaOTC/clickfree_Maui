using clickfree_Maui.Facebook;
using clickfree_Maui.Contracts.Services;
using clickfree_Maui.Helpers;
using clickfree_Maui.Properties;
using clickfree_Maui.Windows;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;

namespace clickfree_Maui.ViewModel
{
    public class BackupFacebookDestVM : ViewModelBase, INotifyPropertyChanged
    {
        public IDataService _dataService;
        readonly INavigationService _navigationService;



        public BackupFacebookDestVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private FacebookManager.MediaResult[] mSelectedImages = null;
        public IFolderPicker _folderPicker;
        public async void FolderPickerMainAsync()
        {
            var pickedFolder = await _folderPicker.PickFolder();

            /*Path.Text = pickedFolder;

            SemanticScreenReader.Announce(Path.Text);*/
        }
        public BackupFacebookDestVM(IFolderPicker folderPicker, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _folderPicker = folderPicker;
        }



       
        #region Properties

        public Command TransferToUSBCommand => new Command(() =>
        {

            if (FacebookManager.CheckNetworkConnection() && DriveManager.CheckAccess())
            {
                DateTime dateTime = DateTime.Now;
                string date = dateTime.ToString("dd-MM-yyyy");
                string album = Environment.MachineName + " " + date;
                string folder = Path.Combine(DriveManager.SelectedUSBDrive.Name, (Environment.OSVersion).ToString(), Environment.MachineName, Environment.UserName, Constants.Person);
                string toFolder;
                Directory.CreateDirectory(folder);
                if (mSelectedImages != null)
                {
                    toFolder = Path.Combine(DriveManager.SelectedUSBDrive.Name, (Environment.OSVersion).ToString(), Environment.MachineName, Environment.UserName, Constants.FacebookFolderName + "\\Selected Photos and Videos\\" + album, Settings.Default.FacebookUserName);
                }
                else
                    //string toFolder = Path.Combine(DriveManager.SelectedUSBDrive.Name, Constants.WindowsBackupFolderName + "\\Complete backup\\" + date, Environment.MachineName);
                    toFolder = Path.Combine(DriveManager.SelectedUSBDrive.Name, (Environment.OSVersion).ToString(), Environment.MachineName, Environment.UserName, Constants.FacebookFolderName + "\\Complete Backups\\" + album, Settings.Default.FacebookUserName);
                var ownerWindow = Application.Current.Windows[Application.Current.Windows.Count - 1];

                var secondWindow = new Window
                {
                    Page = new BackupFromFacebookWindow(mSelectedImages?.ToList(), toFolder)
                    {

                    }
                };
                Application.Current.OpenWindow(secondWindow);

            }
        });



        public Command TransferClickFreePCFolderCommand => new Command(() =>
        {
            if (FacebookManager.CheckNetworkConnection())
            {
                string toFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), Constants.ClickFreeFolderName, Constants.FacebookFolderName);

                var ownerWindow = Application.Current.Windows[Application.Current.Windows.Count - 1];
                var secondWindow = new Window
                {
                    Page = new BackupFromFacebookWindow(mSelectedImages?.ToList(), toFolder)
                    {

                    }
                };
                Application.Current.OpenWindow(secondWindow);


            }
        });



        public Command TransferToSelectedFolderCommand => new Command(async () =>
        {
            if (FacebookManager.CheckNetworkConnection())
            {
               
                try
                {
                    var pickedFolder = await _folderPicker.PickFolder();
                    var secondWindow = new Window
                    {
                        Page = new BackupFromFacebookWindow(mSelectedImages?.ToList(), pickedFolder)
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
        });



        #endregion


        public void Activated(object parameter)
        {
            if (parameter is FacebookManager.MediaResult[] selectedImages && selectedImages.Length > 0)
            {
                mSelectedImages = selectedImages;
            }
        }

        protected internal void Deactivated()
        {
            mSelectedImages = null;
        }
        public override Task OnNavigatingTo(object? parameter)
        {
            Console.WriteLine($"On navigating to SecondPage with parameter {parameter}");
            Activated(parameter);
            return base.OnNavigatingTo(parameter);

        }











    }
}
