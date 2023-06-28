using clickfree_Maui.Helpers;
using clickfree_Maui.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace clickfree_Maui.ViewModel 
{
    public class EraseDialogVM : INotifyPropertyChanged
    {
        private Command mCancelCommand = null;

        public event PropertyChangedEventHandler PropertyChanged;
        private TransferManager mTransferManager = null;
        private List<string> mFrom;
        private string mToDir;
        private long mCurrentSize;
        private long mTotalSize;
        private string mStatus;
        private string toDir;
        private bool hasmessagebox = true;



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

        public EraseDialogVM(List<string> from, string toDir)
        {
            mFrom = from;
            mToDir = toDir;
        }


        public EraseDialogVM(string toDir, List<string> from)
        {
            mFrom = from;
            mToDir = toDir;
        }

        public EraseDialogVM(string toDir)
        {
            this.toDir = toDir;
        }

      
        #endregion

        #region Methods

        public async Task<bool> StartErase()
        {
            bool result = DriveManager.CheckAccess();

            if (result)
            {
                if (mTransferManager != null)
                {
                    await mTransferManager.CancelAsync();
                    mTransferManager.Start -= MTransferManager_Start;
                    mTransferManager.Progress -= MTransferManager_Progress;
                    mTransferManager.Finished -= MTransferManager_Finished;
                    mTransferManager.SearchStart -= MTransferManager_SearchStart;
                    mTransferManager.SearchFinished -= MTransferManager_SearchFinished;
                }

                mTransferManager = new TransferManager();
                mTransferManager.Start += MTransferManager_Start;
                mTransferManager.Progress += MTransferManager_Progress;
                mTransferManager.Finished += MTransferManager_Finished;
                mTransferManager.SearchStart += MTransferManager_SearchStart;
                mTransferManager.SearchFinished += MTransferManager_SearchFinished;

                await mTransferManager.Eraseprocess(mFrom);
            }

            return result;
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
        private void MTransferManager_Progress(TransferManager.TransferProgressInfo obj)
        {
            CurrentSize = obj.CurrentSize;
            TotalSize = obj.TotalSize;

            Status = $"{obj.CurrentPosition} files out of {obj.TotalFiles} were Erased. Please wait.";
        }

        private void MTransferManager_Start(TransferManager.TransferStartInfo obj)
        {
            CurrentSize = obj.CurrentSize;
            TotalSize = obj.TotalSize;

            Status = $"{obj.CurrentPosition} files out of {obj.TotalFiles} were Erased. Please wait.";
        }

        private void MTransferManager_SearchFinished(FileManager.SearchResult obj)
        {
            Status = $"Erasing is finished";
        }

        private void MTransferManager_SearchStart()
        {
            Status = $"Erasing '{DriveManager.SelectedUSBDrive?.Name}'...";
        }

        private void MTransferManager_Finished(TransferManager.TransferFinishedInfo obj)
        {
            CurrentSize = obj.CurrentSize;
            TotalSize = obj.TotalSize;

            switch (obj.FailedReson)
            {
                case TransferManager.FailedReason.AccessDenied:
                   // MessageBoxWindow.ShowMessageBox("Erase your photos and videos",
                                                   // "Could not Erase files. You dont have enought permissions for Folder. Please restart the app as administrator and try again."
                                                   // MessageBoxWindow.MessageBoxType.Error);
                    break;
                case TransferManager.FailedReason.SearchFailed:
                case TransferManager.FailedReason.Other:
                case TransferManager.FailedReason.UsbNotFound:
                case TransferManager.FailedReason.FolderNotFound:
                   // MessageBoxWindow.ShowMessageBox("Could not establish connection with ClickFree.", "Please connect/ re - connect ClickFree to your computer USB port.",
                      //  MessageBoxWindow.MessageBoxType.Error);
                    break;
                case TransferManager.FailedReason.Cancelled:
                    Status = "Transfer is cancelled";
                    break;
                case TransferManager.FailedReason.None:
                default:
                    // MessageBoxWindow.ShowMessageBox("Erase your photos and video",
                    //   $"{obj.CurrentPosition} files were successfully Erased in your USB. Now you can view them in Click Free.",
                    // MessageBoxWindow.MessageBoxType.Success,
                    //() =>
                    //{
                    //    Process.Start(mToDir);
                    //});
                    Thread.Sleep(1000);
                    if (hasmessagebox==true)
                    {
                        Application.Current.Dispatcher.Dispatch((Action)(() => Messagebox()));

                    }
                   
                   
                    break;
            }

            Finished?.Invoke();
        }
        public void Messagebox()
        {
           
            var secondWindow = new Window
            {
                Page = new MessageBoxWindow("EraseDialog", "Erase your photos and video", "files were successfully backup.", mToDir)
                {
                }
            };
            Application.Current.OpenWindow(secondWindow);
            hasmessagebox = false;
        }
        
        #endregion

        #region Overrides

        protected async void OnDisposeInternal()
        {
           // base.OnDisposeInternal();

            if (mTransferManager != null)
            {
                await mTransferManager.CancelAsync();
                mTransferManager.Start -= MTransferManager_Start;
                mTransferManager.Progress -= MTransferManager_Progress;
                mTransferManager.Finished -= MTransferManager_Finished;
                mTransferManager.SearchStart -= MTransferManager_SearchStart;
                mTransferManager.SearchFinished -= MTransferManager_SearchFinished;
                mTransferManager = null;
            }
        }

        #endregion
    }

}

