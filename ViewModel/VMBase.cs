using clickfree_Maui.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clickfree_Maui.ViewModel
{
    public class VMBase : ViewModelBase
    {
        #region Fields

        private bool mbisDisposed = false;

        #endregion

        #region Contructor
        public VMBase()
        {
            DriveManager.DriveStateChanged += DriveManager_DriveStateChanged;
        }

        #endregion
        #region overrides

        #endregion

        #region Event handlers
        public void Cleanup()
        {
           // base.Cleanup();

            Dispose();
        }
        private void DriveManager_DriveStateChanged(DriveState state, UsbDisk disk)
        {
            RaisePropertyChanged("HasUsbDrives");

            OnDriveStateChanged(state, disk);
        }

        #endregion

        #region Protected virtual

        protected virtual void OnDisposeInternal() { }
        protected virtual void OnDriveStateChanged(DriveState state, UsbDisk disk) { }

        #endregion

        #region Implementation of IDisposable
        public void Dispose()
        {
            if (!mbisDisposed)
            {
                OnDisposeInternal();

                mbisDisposed = true;
            }
        }

        #endregion
    }
}
