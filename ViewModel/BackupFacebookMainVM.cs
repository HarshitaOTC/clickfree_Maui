using clickfree_Maui.Facebook;
using clickfree_Maui.Contracts.Services;
using System.ComponentModel;
using System.Windows.Input;

namespace clickfree_Maui.ViewModel
{
    public class BackupFacebookMainVM 
    {
        readonly IDataService _dataService;
        readonly INavigationService _navigationService;
       

        public BackupFacebookMainVM(IDataService dataService, INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
        }
        public BackupFacebookMainVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        public Command BackupEverythingCommand => new Command(async () =>
        {
            if (FacebookManager.CheckAuthorization1(_navigationService, false))
            {
                await _navigationService.BackupFacebookDestView(null);
            }
        });

        public ICommand BackupPhotoAndVideoCommand => new Command(async () =>
        {
            if (FacebookManager.CheckAuthorization1(_navigationService, false))
            {
                await _navigationService.BackupFacebookSelectImagesView();
            }

        });

       


    }
        
    
}

