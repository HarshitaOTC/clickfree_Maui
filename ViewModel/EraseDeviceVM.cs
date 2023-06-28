using clickfree_Maui.Helpers;
using clickfree_Maui.Windows;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace clickfree_Maui.ViewModel
{
    public class EraseDeviceVM : BindableObject
    {
        EraseDialogVM mEraseDialogVM;
        BackupToClickFreeWindow backupToClickFreeWindow = new BackupToClickFreeWindow();
        public EraseDeviceVM()
        {
            Navigate(null);
            // AddToSelected(null);
        }
        public abstract class IOInfoBase
        {
            #region Properties
            public string Name { get; set; }
            public string Path { get; set; }
            #endregion
        }
        public class DiskInfo : IOInfoBase
        {
        }
        public class DirectoryInfo : IOInfoBase
        {
        }
        public class ImageFileInfo : IOInfoBase
        {
        }
        public class VideoFileInfo : IOInfoBase
        {
        }
        public class BackInfo : IOInfoBase
        {
            #region Ctor
            public BackInfo()
            {
                Name = "...";
            }
            #endregion
        }
        public class RetryInfo : IOInfoBase
        {
            #region Ctor
            public RetryInfo()
            {
                Name = "Retry";
            }
            #endregion
        }
        public class HeaderInfo
        {
            #region Properties
            public string Name { get; set; }
            public string Path { get; set; }
            public bool IsStart { get; set; }
            #endregion
        }
        public ObservableCollection<HeaderInfo> Headers { get; set; } = new ObservableCollection<HeaderInfo>();
        public ObservableCollection<IOInfoBase> CurrentDirList { get; private set; } = new ObservableCollection<IOInfoBase>();
        public ObservableCollection<IOInfoBase> SelectedDirList { get; private set; } = new ObservableCollection<IOInfoBase>();
        public IOInfoBase CurrentDirSelectedItem { get; set; }
        public IOInfoBase SelectedDirSelectedItem { get; set; }
        public Command AddToSelectedCommand
#pragma warning disable CA1416 // Validate platform compatibility
          => new Command<IOInfoBase>(o =>
          {
              AddToSelected(o);

          });
#pragma warning restore CA1416 // Validate platform compatibility
        /*public Command AddToSelectedCommand
       => new Command(() =>
       {
           AddToSelected(SelectedDirSelectedItem);
       });*/
#pragma warning disable CA1416 // Validate platform compatibility
        public Command SelectAllCommand => new Command(() =>
        {
            foreach (var item in CurrentDirList)
                AddToSelected(item);
        });
#pragma warning restore CA1416 // Validate platform compatibility
        private void AddToSelected(IOInfoBase info)
        {
            if (info != null && !(info is BackInfo))
            {
                bool skip = false;
                foreach (var item in SelectedDirList)
                {
                    if (info.Path.StartsWith(item.Path, StringComparison.InvariantCultureIgnoreCase))
                    {
                        skip = true;
                        break;
                    }
                }
                if (!skip)
                {
                    foreach (var item in SelectedDirList.ToArray())
                    {
                        if (item.Path.StartsWith(info.Path, StringComparison.InvariantCultureIgnoreCase))
                        {
                            SelectedDirList.Remove(item);
                        }
                    }
                    SelectedDirList.Add(info);
                }
            }
            // CommandManager.InvalidateRequerySuggested();
        }
        public ObservableCollection<IOInfoBase> GoToFolderCommand(IOInfoBase folder)
        {
            if (folder is DirectoryInfo || folder is DiskInfo)
            {
                Navigate(folder.Path);
            }
            else if (folder is BackInfo)
            {
                Navigate(Path.GetDirectoryName(folder.Path));
            }
            else
            {
                AddToSelected(folder);
            }

            return CurrentDirList;
        }
        public ICommand ClearAllCommand

#pragma warning disable CA1416 // Validate platform compatibility
           => new Command(() =>
           {
               SelectedDirList.Clear();
           });
#pragma warning restore CA1416 // Validate platform compatibility

#pragma warning disable CA1416 // Validate platform compatibility
        public ICommand EraseCommand => new Command(() =>
        {
                if (DriveManager.CheckAccess())
                {
                    string toFolder = Path.Combine(DriveManager.SelectedUSBDrive.Name);

                    var ownerWindow = Application.Current.Windows[Application.Current.Windows.Count - 1];
                    var secondWindow = new Window
                    {
                        Page = new BackupToClickFreeWindow(toFolder, SelectedDirList.Select(s => s.Path).ToList())
                        {

                        }
                         
                    };
                Application.Current.OpenWindow(secondWindow);
               /* if(backupToClickFreeWindow.SuccessfullyBackuped)
                {
                    SelectedDirList.Clear();

                    Navigate(null);
                }*/
            }
        });


        public void Navigate(string path)
        {
            CurrentDirList.Clear();
            Headers.Clear();
            Headers.Add(new HeaderInfo()
            {
                Name = "",
                IsStart = true
            });
            if (string.IsNullOrWhiteSpace(path))
            {
                try
                {
                    foreach (var drive in DriveInfo.GetDrives())
                    {
                        if (drive.DriveType == DriveType.Removable)
                        {
                            CurrentDirList.Add(new DiskInfo()
                            {
                                Name = drive.Name,
                                Path = drive.RootDirectory.FullName
                            });
                        }
                    }
                }
                catch (Exception)
                {
                    CurrentDirList.Clear();
                    CurrentDirList.Add(new RetryInfo()
                    {
                        Path = path
                    });
                }
            }
            else
            {
                CurrentDirList.Add(new BackInfo()
                {
                    Path = path
                });
                var result = FileManager.Search(path, new FileManager.SearchParameters(FileManager.SearchFilterEnum.ALL)
                {
                    IncludeSubFolders = false
                }, new System.Threading.CancellationToken());
                if (result != null)
                {
                    foreach (var dir in result.Directories)
                    {
                        CurrentDirList.Add(new DirectoryInfo()
                        {
                            Name = Path.GetFileName(dir),
                            Path = dir
                        });
                    }
                    var videos = FileManager.SearchParameters.GetFilters(FileManager.SearchFilterEnum.Videos, ".");
                    foreach (var file in result.Files)
                    {
                        if (videos.Contains(Path.GetExtension(file), StringComparer.InvariantCultureIgnoreCase))
                        {
                            CurrentDirList.Add(new VideoFileInfo()
                            {
                                Name = Path.GetFileNameWithoutExtension(file),
                                Path = file
                            });
                        }
                        else
                        {
                            CurrentDirList.Add(new ImageFileInfo()
                            {
                                Name = Path.GetFileNameWithoutExtension(file),
                                Path = file
                            });
                        }
                    }
                }
                string headerPath = null;
                foreach (var item in path.Split(new[] { Path.DirectorySeparatorChar, Path.VolumeSeparatorChar }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (headerPath == null)
                        headerPath = item + Path.VolumeSeparatorChar + Path.DirectorySeparatorChar;
                    else
                        headerPath = Path.Combine(headerPath, item);
                    Headers.Add(new HeaderInfo()
                    {
                        Name = item,
                        Path = headerPath,
                        IsStart = false
                    });
                }
            }
        }
    }
}