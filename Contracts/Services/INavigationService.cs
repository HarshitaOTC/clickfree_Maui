using clickfree_Maui.Instagram;

namespace clickfree_Maui.Contracts.Services;

public interface INavigationService
{
    Task NavigateToSecondPage(string id);
    Task TransferView();
    Task NavigateBack();
    Task NavigateToMainPage();

    Task NavigateToMainView();
    Task TransferToPCWindow();
    Task InstagramLoginWindow();
    Task BackupInstagramMainView();
  Task BackupFacebookSelectImagesView();
    Task BackupToClickFreeWindow();
    Task DefaultFolderView();
    Task BackupToUSBSelectView();
    Task EraseDevice();
    Task MessageBoxWindow();

  Task BackupFacebookMainView();
    Task BackupInstagramSelectImagesView();
    Task Continue();
    Task Continue_facebook();
   Task FacebookLoginwindow();
    Task BackupFacebookDestView(object parameter);
    Task BackupInstagramDestView(object parameter);
    Task BackupToUSBMainView();
}

