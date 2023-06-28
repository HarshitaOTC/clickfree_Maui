
using clickfree_Maui.Helpers;
using clickfree_Maui.ViewModel;
using clickfree_Maui.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace clickfree_Maui
{
    public class BackupDialogVM :VMBase,INotifyPropertyChanged
    {
        #region Fields

        #region Commands

        private ICommand mCancelCommand = null;

        #endregion

        private TransferManager mTransferManager = null;
        private List<string> mFrom;
        private string mToDir;
        private long mcurrentsize;
        private long mTotalSize;
        private string mStatus;
        public string CurrentDir { get; }

       
        private string toDir;
        private long mtotalSize;
        private string mstatus;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties

        #region Commands

        public ICommand CancelCommand => new Command(async () =>
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
            get
            {
                return mcurrentsize;
            }
            set
            {
                if (mcurrentsize != value)
                {
                    mcurrentsize = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentSize)));
                }
                
                /*string srt = Convert.ToInt64(mcurrentsize).ToString();
                Set(ref srt, value);*/
            }
        }



        public long TotalSize
        {
            get
            {
                return mtotalSize;
            }
            set
            {
                if (mtotalSize != value)
                {
                    mtotalSize = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("TotalSize"));
                }
               
                /*string srt_totalsize = Convert.ToInt64(mTotalSize).ToString();
                Set(ref srt_totalsize, value);*/
            }
        }

        public string Status
        {
            get
            {
                return mstatus;
            }
            set
            {
                if (mstatus != value)
                {
                    mstatus = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Status"));
                }
                
                //Set(ref mStatus, Int64.Parse(value));
            }
        }
        public bool SuccessfullyBackuped { get; private set; }
        
        #endregion

        #region Events

        public event Action Finished;

        #endregion

        #region Ctor

        public BackupDialogVM(List<string> from, string toDir)
        {
            mFrom = from;
            mToDir = toDir;
        }


        public BackupDialogVM(string toDir, List<string> from)
        {
            mFrom = from;
            mToDir = toDir;
        }

        public BackupDialogVM()
        {
        }

        #endregion

        #region Methods

        public async Task<bool> StartBackup()
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

                await mTransferManager.ScanAndBackup(mFrom, mToDir);
            }

            return result;
        }


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

        private void MTransferManager_SearchFinished(FileManager.SearchResult obj)
        {
            Status = $"Scanning is finished";
        }

        private void MTransferManager_SearchStart()
        {
            Status = $"Scanning '{DriveManager.SelectedUSBDrive?.Name}'...";
        }

        private void MTransferManager_Finished(TransferManager.TransferFinishedInfo obj)
        {

            CurrentSize = obj.CurrentSize;
            TotalSize = obj.TotalSize;

            switch (obj.FailedReson)
            {
                case TransferManager.FailedReason.AccessDenied:
                    /*MessageBoxWindow.ShowMessageBox("Backup your photos and videos to PC",
                                                    "Could not backup files. You dont have enought permissions for destination folder. Please restart the app as administrator and try again.",
                                                    MessageBoxWindow.MessageBoxType.Error);*/
                    break;
                case TransferManager.FailedReason.SearchFailed:
                case TransferManager.FailedReason.Other:
                case TransferManager.FailedReason.UsbNotFound:
                case TransferManager.FailedReason.FolderNotFound:
                   /* MessageBoxWindow.ShowMessageBox("Could not establish connection with ClickFree.", "Please connect/ re - connect ClickFree to your computer USB port.",
                        MessageBoxWindow.MessageBoxType.Error);*/
                    break;
                case TransferManager.FailedReason.Cancelled:
                    Status = "Transfer is cancelled";
                    break;
                case TransferManager.FailedReason.None:
                default:
                    /*MessageBoxWindow.ShowMessageBox("Backup Photos and Videos to Clickfree USB",
                                                    $"{obj.CurrentPosition} files were successfully backup to your USB. Now you can view them in Click Free folder.",
                                                    MessageBoxWindow.MessageBoxType.Success,
                                                    () =>
                                                    {
                                                        Process.Start(mToDir);
                                                    });*/
                    Thread.Sleep(1000);
                    Application.Current.Dispatcher.Dispatch((Action)(() => MesaageBox()));
                    SuccessfullyBackuped = true;
                    break;
            }

            Finished?.Invoke();
        }
        public void MesaageBox()
        {
            var secondWindow = new Window
            {
                Page = new MessageBoxWindow("BackupDialog", "Backup Photos and Videos to Clickfree USB", "files were successfully transferred to your PC.", mToDir)
                {
                }
            };
            Application.Current.OpenWindow(secondWindow);
        }
        private void MTransferManager_Progress(TransferManager.TransferProgressInfo obj)
        {
            CurrentSize = obj.CurrentSize;
            TotalSize = obj.TotalSize;

            Status = $"{obj.CurrentPosition} files out of {obj.TotalFiles} were backup. Please wait.";
        }

        private void MTransferManager_Start(TransferManager.TransferStartInfo obj)
        {
            CurrentSize = obj.CurrentSize;
            TotalSize = obj.TotalSize;

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
                mTransferManager.SearchStart -= MTransferManager_SearchStart;
                mTransferManager.SearchFinished -= MTransferManager_SearchFinished;
                mTransferManager = null;
            }
        }

        #endregion
    }
}
