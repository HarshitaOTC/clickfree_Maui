using clickfree_Maui.Contracts.Services;
using clickfree_Maui.Instagram;
using clickfree_Maui.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace clickfree_Maui.ViewModel
{
    public class BackupInstagramMainVM 
    {
        readonly IDataService _dataService;
        readonly INavigationService _navigationService;
        #region Properties
        public BackupInstagramMainVM(IDataService dataService, INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
        }
        public BackupInstagramMainVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public Command BackupEverythingCommand => new Command(async () =>
                    {
                        if (InstagramManager.CheckAuthorization1(_navigationService, false))
                        {
                            await _navigationService.BackupInstagramDestView(null);
                        }
                    });
               
        public ICommand BackupPhotoAndVideoCommand => new Command(async () =>
                    {
                        if (InstagramManager.CheckAuthorization1(_navigationService, false))
                        {
                            await _navigationService.BackupInstagramSelectImagesView();
                        }
                           
                    });
               

        #endregion

        #region Ctor

        public BackupInstagramMainVM()
        {
        }

        #endregion
    }
}
