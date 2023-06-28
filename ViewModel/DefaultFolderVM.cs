using clickfree_Maui.Contracts.Services;
using clickfree_Maui.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clickfree_Maui.ViewModel
{
    public class DefaultFolderVM : ViewModelBase
    {
        private string mCurrentDir;
        public IDataService _dataService;
        readonly INavigationService _navigationService;
        string mCDir;
        public event PropertyChangedEventHandler PropertyChanged;

       
        public Command TransferCommand => new Command(async () =>
        {
            AddUpdateAppSettings("DefaultFolders", CurrentDir);
            await _navigationService.NavigateToMainView();
            });
        public DefaultFolderVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
            // CurrentDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), Constants.ClickFreeFolderName);
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            if (settings["DefaultFolders"] != null)
                CurrentDir = settings["DefaultFolders"].Value;
            
        }
        public string CurrentDir
        {
            get { return mCDir; }
            set
            {
                if (mCDir != value)
                {
                    mCDir = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentDir"));
                }

            }
        }

        static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

    }
}
