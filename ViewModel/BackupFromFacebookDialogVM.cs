using clickfree_Maui.Contracts.Services;
using clickfree_Maui.Helpers;
using clickfree_Maui.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static clickfree_Maui.Facebook.FacebookManager;

namespace clickfree_Maui.ViewModel
{
    public class BackupFromFacebookDialogVM :VMBase, INotifyPropertyChanged
    {

        #region Fields

        #region Commands

        private ICommand mCancelCommand = null;

        #endregion

        private TransferManager mTransferManager = null;
        private List<MediaResult> mFrom;
        private string mToDir;
        private long mCurrentSize;
        private long mCurrentPosition;
        private long mTotalSize;
        private long mTotalFiles;
        private string mStatus;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties

        #region Commands

        public Command CancelCommand => new Command(async () =>
                    {
                        if (mTransferManager == null)
                            Finished?.Invoke();
                        else
                        {
                            Status = "Cancelling";
                            await mTransferManager.CancelAsync();
                        }

                    });
                
        #endregion

        public long CurrentSize
        {
            get { return mCurrentSize; }
            set
            {
                if (mCurrentSize != value)
                {
                    mCurrentSize = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentSize"));
                }

            }
        }

        public long CurrentPosition
        {
            get { return mCurrentPosition; }
            set
            {
                if (mCurrentPosition != value)
                {
                    mCurrentPosition = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPosition"));
                }

            }

        }
        public long TotalSize
        {
            get { return mTotalSize; }
            set
            {
                if (mTotalSize != value)
                {
                    mTotalSize = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalSize"));
                }

            }

        }
        public long TotalFiles
        {
            get { return mTotalFiles; }
            set
            {
                if (mTotalFiles != value)
                {
                    mTotalFiles = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalFiles"));
                }

            }

        }
        public string Status
        {
            get { return mStatus; }
            set
            {
                if (mStatus != value)
                {
                    mStatus = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Status"));
                }

            }

        }

        public bool SuccessfullyBackuped { get; private set; }
       
        #endregion

        #region Events

        public event Action Finished;

        #endregion

        #region Ctor

        public BackupFromFacebookDialogVM(List<MediaResult> from, string toDir)
        {
            mFrom = from;
            mToDir = toDir;
        }

        #endregion

        #region Methods

        public async Task<bool> StartBackup()
        {
            if (mTransferManager != null)
            {
                await mTransferManager.CancelAsync();
                mTransferManager.Start -= MTransferManager_Start;
                mTransferManager.Progress -= MTransferManager_Progress;
                mTransferManager.Finished -= MTransferManager_Finished;
            }

            mTransferManager = new TransferManager();
            mTransferManager.Start += MTransferManager_Start;
            mTransferManager.Progress += MTransferManager_Progress;
            mTransferManager.Finished += MTransferManager_Finished;

            this.Status = "Initializing...";

            await mTransferManager.BackupFromFacebookToSelectedPath(mFrom, mToDir);

            return true;
        }

        public async Task CancelTransfer()
        {
            if (mTransferManager != null)
            {
                await mTransferManager.CancelAsync();
            }
        }

        #endregion

        #region Event handlers

        private async void MTransferManager_Finished(TransferManager.TransferFinishedInfo obj)
        {
            CurrentSize = obj.CurrentSize;
            CurrentPosition = obj.CurrentPosition;
            TotalSize = obj.TotalSize;

            switch (obj.FailedReson)
            {
                case TransferManager.FailedReason.AccessDenied:
                    
                    break;
                case TransferManager.FailedReason.SearchFailed:
                case TransferManager.FailedReason.Other:
                case TransferManager.FailedReason.UsbNotFound:
                case TransferManager.FailedReason.FolderNotFound:
                    
                    break;
                case TransferManager.FailedReason.NoInternet:
                   
                    break;
                case TransferManager.FailedReason.Cancelled:
                    Status = "Transfer is cancelled";
                    break;
                case TransferManager.FailedReason.None:
                default:
                    Thread.Sleep(1000);
                    Application.Current.Dispatcher.Dispatch((Action)(() => Messagebox()));
                    SuccessfullyBackuped = true;
                    break;
            }

            Finished?.Invoke();
        }
        public void Messagebox()
        {
            
            var secondWindow = new Window
            {
                Page = new MessageBoxWindow("BackupFromFacebook", "Backup your photos and videos", "files were successfully backup.", mToDir)
                {
                }
            };
            Application.Current.OpenWindow(secondWindow);
        }

        private void MTransferManager_Progress(TransferManager.TransferProgressInfo obj)
        {
            CurrentSize = obj.CurrentSize;
            TotalSize = obj.TotalSize;
            CurrentPosition = obj.CurrentPosition;

            Status = $"{obj.CurrentPosition} files out of {obj.TotalFiles} were backup. Please wait.";
        }

        private void MTransferManager_Start(TransferManager.TransferStartInfo obj)
        {
            CurrentSize = obj.CurrentSize;
            TotalSize = obj.TotalSize;
            TotalFiles = obj.TotalFiles;
            CurrentPosition = obj.CurrentPosition;

            Status = $"{obj.CurrentPosition} files out of {obj.TotalFiles} were backup. Please wait.";
        }

        #endregion

        #region Overrides

        protected async override void OnDisposeInternal()
        {
            base.OnDisposeInternal();

            if (mTransferManager != null)
            {
                await mTransferManager.CancelAsync();
                mTransferManager.Start -= MTransferManager_Start;
                mTransferManager.Progress -= MTransferManager_Progress;
                mTransferManager.Finished -= MTransferManager_Finished;
                mTransferManager = null;
            }
        }

        #endregion
    }
}
