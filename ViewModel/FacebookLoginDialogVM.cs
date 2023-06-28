using clickfree_Maui.Properties;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace clickfree_Maui.ViewModel
{
    public class FacebookLoginDialogVM : VMBase, INotifyPropertyChanged
    {
        #region Fields

        #region Commands
        public event PropertyChangedEventHandler PropertyChanged;
        private Command mContinueCommand;
        private Command mLogoutCommand;

        #endregion

        private bool mbBrowserIsVisible = true;

        #endregion

        #region Events

        public event Action Continue;
        public event Action Logout;

        #endregion

        #region Properties

        public Command ContinueCommand=>
        
             new Command(() =>
                    {
                        Continue?.Invoke();
                    });
                

        public Command LogoutCommand
        {
            get
            {
                if (mLogoutCommand == null)
                {
                    mLogoutCommand = new Command(() =>
                    {
                        Logout?.Invoke();
                    });
                }

                return mLogoutCommand;
            }
        }

        public bool BrowserIsVisible
        {
            get { return mbBrowserIsVisible; }
            set
            {
                if (mbBrowserIsVisible != value)
                {
                    mbBrowserIsVisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BrowserIsVisible)));
                }

            }
        }

        public string UserName
        {
            get
            {
                return Settings.Default.FacebookUserName;
            }
        }

        #endregion

        #region Ctor

        public FacebookLoginDialogVM()
        {

        }

        #endregion
    }
}
