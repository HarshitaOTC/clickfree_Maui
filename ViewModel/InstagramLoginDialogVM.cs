using clickfree_Maui.Instagram;
using clickfree_Maui.Properties;
using Microsoft.VisualStudio.Services.OAuth;
using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace clickfree_Maui.ViewModel
{
    class InstagramLoginDialogVM : VMBase, INotifyPropertyChanged
    {
        #region Fields

        #region Commands
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private bool mbBrowserIsVisible = true;

        #endregion

        #region Events

        public event Action Continue;
        public event Action Logout;

        #endregion

        #region Properties

        public Command ContinueCommand => new Command(() =>
                    {
                        Continue?.Invoke();
                        
                    });
                

        public Command  LogoutCommand => new Command(() =>
                    {
                        
                        Settings.Default.InstagramAccessToken = null;
                        Settings.Default.InstagramCode = null;
                        Settings.Default.InstagramUserName = null;
                       Settings.Default.Save(); 
                        Logout?.Invoke();
                    });


        public bool BrowserIsVisible
        {
            get { return mbBrowserIsVisible; }
            set
            {  
                    mbBrowserIsVisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BrowserIsVisible"));
                

            }
        }
      

        public string UserName
        {
            get
            {
                return Settings.Default.InstagramUserName;
            }
        }

        #endregion

        #region ctor
        public InstagramLoginDialogVM()
        {

        }
        #endregion
    }
}
