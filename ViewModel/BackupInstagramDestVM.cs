using clickfree_Maui.Contracts.Services;
using clickfree_Maui.Helpers;
using clickfree_Maui.Instagram;
using clickfree_Maui.Windows;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection.Metadata;
using static clickfree_Maui.ViewModel.BackupInstagramSelectImagesVM;

namespace clickfree_Maui.ViewModel
{
    public class BackupInstagramDestVM : ViewModelBase, INotifyPropertyChanged
    {
        public IDataService _dataService;
        readonly INavigationService _navigationService;
        Object parameter = null;




        public BackupInstagramDestVM(INavigationService navigationService, IFolderPicker folderPicker)
        {

            _navigationService = navigationService;
            _folderPicker = folderPicker;
          
        }


        private InstagramManager.MediaResult[] mSelectedImages = null;
        public IFolderPicker _folderPicker;


        public event PropertyChangedEventHandler PropertyChanged;

        public async void FolderPickerMainAsync()
        {
            var pickedFolder = await _folderPicker.PickFolder();
        }




        public Command TransferToUSBCommand => new Command(() =>
        {

            if (InstagramManager.CheckNetworkConnection() && DriveManager.CheckAccess())
            {
                DateTime dateTime = DateTime.Now;
                string date = dateTime.ToString("dd-MM-yyyy");
                string album = Environment.MachineName + " " + date;

                string folder = Path.Combine(DriveManager.SelectedUSBDrive.Name, (Environment.OSVersion).ToString(), Environment.MachineName, Environment.UserName, Constants.Person);
                Directory.CreateDirectory(folder);
                string toFolder;

                if (mSelectedImages != null)
                {
                    toFolder = Path.Combine(DriveManager.SelectedUSBDrive.Name, (Environment.OSVersion).ToString(), Environment.MachineName, Environment.UserName, Constants.InstagramFolderName + "\\Selected Photos and Videos\\" + album);
                }
                else
                    //string toFolder = Path.Combine(DriveManager.SelectedUSBDrive.Name, Constants.WindowsBackupFolderName + "\\Complete backup\\" + date, Environment.MachineName);
                    toFolder = Path.Combine(DriveManager.SelectedUSBDrive.Name, (Environment.OSVersion).ToString(), Environment.MachineName, Environment.UserName, Constants.InstagramFolderName + "\\Complete Backups\\" + album);

                //  string toFolder = Path.Combine(DriveManager.SelectedUSBDrive.Name, Constants.WindowsBackupFolderName, Environment.MachineName, Constants.FacebookFolderName);
                /// string toFolder = Path.Combine(DriveManager.SelectedUSBDrive.Name, Constants.InstagramFolderName, Settings.Default.InstagramUserName);
                var ownerWindow = Application.Current.Windows[Application.Current.Windows.Count - 1];

                var secondWindow = new Window
                {
                    Page = new BackupFromInstagramWindow(mSelectedImages?.ToList(), toFolder)
                    {

                    }
                };
                Application.Current.OpenWindow(secondWindow);

            }
        });



        public Command TransferClickFreePCFolderCommand => new Command(() =>
        {
            if (InstagramManager.CheckNetworkConnection())
            {
                string toFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), Constants.ClickFreeFolderName, Constants.InstagramFolderName);

                var ownerWindow = Application.Current.Windows[Application.Current.Windows.Count - 1];
                var secondWindow = new Window
                {
                    Page = new BackupFromInstagramWindow(mSelectedImages?.ToList(), toFolder)
                    {

                    }
                };
                Application.Current.OpenWindow(secondWindow);


            }
        });



        public Command TransferToSelectedFolderCommand => new Command(async () =>
        {
            if (InstagramManager.CheckNetworkConnection())
            {

                try
                {
                    var pickedFolder = await _folderPicker.PickFolder();
                    // SemanticScreenReader.Announce(pickedFolder);
                    //using (var pickedFolder =  _folderPicker.PickFolder())
                    //{
                    var secondWindow = new Window
                    {
                        Page = new BackupFromInstagramWindow(mSelectedImages?.ToList(), pickedFolder)
                        {

                        }
                    };
                    Application.Current.OpenWindow(secondWindow);
                    // }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                /* string toFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), Constants.ClickFreeFolderName, Constants.InstagramFolderName);
                 */
            }
        });



        public void Activated(object parameter)
        {
            if (parameter is InstagramManager.MediaResult[] selectedImages && selectedImages.Length > 0)
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


