
using clickfree_Maui.Helpers;
using clickfree_Maui.Windows;
using System.ComponentModel;
using System.Windows.Input;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace clickfree_Maui.ViewModel;

public partial class TransferDialogVM : VMBase, INotifyPropertyChanged
{

    #region Fields

    #region Commands

     //private Command mCancelCommand = null;
  
    #endregion
    //private DataTransferManager dataTransferManager = null;
    private TransferManager mTransferManager = null;
    private string mToDir;
    private long mcurrentsize;
    private long mTotalSize;
    private string mStatus;
    private string toDir;
    private long mtotalSize;
    private string mstatus;
    public event PropertyChangedEventHandler PropertyChanged;
    #endregion

    #region Properties

    #region Commands
  
    public ICommand CancelCommand => new Command(async () =>
    {
        try
        {
            if (mTransferManager == null)
                Finished?.Invoke();
            else
            {
                Status = "Cancelling";
                await mTransferManager.CancelAsync();
            }
        }
        catch(Exception ex)
        { 
        
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
            if(mcurrentsize != value)
            {
                mcurrentsize = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentSize"));
            }
           
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalSize"));
            }
           
            
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Status"));
            }
           
           
        }
    }
    #endregion

    #region Events

    public event Action Finished;
    public event Func<bool?> AppleFormatDetected;
    #endregion


    #region Ctor
   
    public TransferDialogVM(string toDir)
    {
        mToDir = toDir;
    }
    #endregion

    #region Methods

    public async Task<bool> StartTransfer()
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
                mTransferManager.AppleFormatDetected -= MTransferManager_AppleFormatDetected;
                mTransferManager.SearchStart -= MTransferManager_SearchStart;
                mTransferManager.SearchFinished -= MTransferManager_SearchFinished;
            }

            mTransferManager = new TransferManager();
            mTransferManager.Start += MTransferManager_Start;
            mTransferManager.Progress += MTransferManager_Progress;
            mTransferManager.Finished += MTransferManager_Finished;
            mTransferManager.AppleFormatDetected += MTransferManager_AppleFormatDetected;
            mTransferManager.SearchStart += MTransferManager_SearchStart;
            mTransferManager.SearchFinished += MTransferManager_SearchFinished;

            await mTransferManager.ScanAndTransfer(DriveManager.SelectedUSBDrive.Name, mToDir);
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

    private void MTransferManager_SearchFinished(Helpers.FileManager.SearchResult obj)
    {
        Status = $"Scanning is finished";
    }

    private void MTransferManager_SearchStart()
    {
        Status = $"Scanning '{DriveManager.SelectedUSBDrive?.Name}'...";
    }

    private bool? MTransferManager_AppleFormatDetected(Helpers.FileManager.SearchResult arg)
    {
        if (AppleFormatDetected == null)
            return false;
        else return AppleFormatDetected.Invoke();
    }

    private void MTransferManager_Finished(TransferManager.TransferFinishedInfo obj)
    {
        CurrentSize = obj.CurrentSize;
        TotalSize = obj.TotalSize;

        switch (obj.FailedReson)
        {
            case TransferManager.FailedReason.AccessDenied:
                //MessageBoxWindow.ShowMessageBox("Transfer your photos and videos to PC",
                //                                "Could not transfer files. You dont have enought permissions for destination folder. Please restart the app as administrator and try again.",
                //                                MessageBoxWindow.MessageBoxType.Error);
                break;
            case TransferManager.FailedReason.SearchFailed:
            case TransferManager.FailedReason.Other:
            case TransferManager.FailedReason.UsbNotFound:
            case TransferManager.FailedReason.FolderNotFound:
                //MessageBoxWindow.ShowMessageBox("Could not establish connection with ClickFree.", "Please connect/ re - connect ClickFree to your computer USB port.",
                //    MessageBoxWindow.MessageBoxType.Error);
                break;
            case TransferManager.FailedReason.Cancelled:
                Status = "Transfer is cancelled";
                break;
            case TransferManager.FailedReason.None:
            default:
                /* MessageBoxWindow.ShowMessageBox("Transfer your photos and videos to PC",
                                                 $"{obj.CurrentPosition} files were successfully transferred to your PC. Now you can view them in '{Path.GetFileName(mToDir)}' folder.",
                                                 MessageBoxWindow.MessageBoxType.Success,
                                                 () =>
                                                 {
                                                     Process.Start(mToDir);
                                                 });*/
                Thread.Sleep(1000);
                Application.Current.Dispatcher.Dispatch((Action)(() => MesaageBox()));
                break;
        }

        Finished?.Invoke();
    }
    public void MesaageBox()
    {
        string msg = "files were successfully transferred to your PC";
        var secondWindow = new Window
        {
            Page = new MessageBoxWindow("TransferDialog", "Transfer your photos and videos to PC","files were successfully transferred to your PC.", mToDir)
            {
            }
        };
        Application.Current.OpenWindow(secondWindow);
    }

    private void MTransferManager_Progress(TransferManager.TransferProgressInfo obj)
    {
        CurrentSize = obj.CurrentSize;
        TotalSize = obj.TotalSize;

        Status = $"{obj.CurrentPosition} files out of {obj.TotalFiles} were transferred. Please wait.";
    }

    private void MTransferManager_Start(TransferManager.TransferStartInfo obj)
    {
        CurrentSize = obj.CurrentSize;
        TotalSize = obj.TotalSize;

        Status = $"{obj.CurrentPosition} files out of {obj.TotalFiles} were transferred. Please wait.";
    }

    #endregion

    #region Overrides

    protected async  void OnDisposeInternal()
    {
       // base.OnDisposeInternal();

        if (mTransferManager != null)
        {
            await mTransferManager.CancelAsync();
            mTransferManager.Start -= MTransferManager_Start;
            mTransferManager.Progress -= MTransferManager_Progress;
            mTransferManager.Finished -= MTransferManager_Finished;
            mTransferManager.AppleFormatDetected -= MTransferManager_AppleFormatDetected;
            mTransferManager.SearchStart -= MTransferManager_SearchStart;
            mTransferManager.SearchFinished -= MTransferManager_SearchFinished;
            mTransferManager = null;
        }
    }

    #endregion
}
