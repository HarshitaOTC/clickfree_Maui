

using clickfree_Maui;
using clickfree_Maui.Helpers;
using clickfree_Maui.Windows;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Reflection;
using System.Windows.Input;

namespace  clickfree_Maui.ViewModel
{

    public partial class BackupToUSBSelectVM : INotifyPropertyChanged
    {



        public class IOInfoBase
        {


            public string Name { get; set; }
            public string Path { get; set; }

            
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


            public BackInfo()
            {
                Name = "...";
            }

        }

        public class RetryInfo : IOInfoBase
        {


            public RetryInfo()
            {
                Name = "Retry";
            }


        }

        public class HeaderInfo
        {


            public string Name { get; set; }
            public string Path { get; set; }
            public bool IsStart { get; set; }


        }





        private Command mGoToFolderCommand = null;
        private Command mSelectAllCommand = null;
        private Command mClearAllCommand = null;
        private Command mBackupCommand = null;
        private Command mDropCommand = null;
        private Command mDoDragCommand = null;
        private Command mDeleteFromSelectedCommand = null;
        private Command mHeaderClickCommand = null;
        private IOInfoBase _draggedItem;

        public event PropertyChangedEventHandler PropertyChanged;

        [RelayCommand]
        public void DragStarted(IOInfoBase task)
        {
            _draggedItem = task;
        }

        [RelayCommand]
        public  void TaskDroped()
        {
           
           
            if (_draggedItem != null)
            {
               

                AddToSelected(_draggedItem);
            }
           
        }
        public Command AddToSelectedCommand
           => new Command<IOInfoBase>(o =>
           {


               AddToSelected(o);



           });

       

        public ObservableCollection<IOInfoBase> AddToSelected1(IOInfoBase data)
        {
            if(data != null)
            {
                IOInfoBase iOInfoBase = new IOInfoBase();
                _ = iOInfoBase.Name;
            }
            return CurrentDirList;
            
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

        // public Command GoToFolderCommand => new Command<IOInfoBase>(folder  =>
        //{
        //    if (folder is DirectoryInfo || folder is DiskInfo)
        //    {
        //        Navigate(folder.Path);
        //    }
        //    else if (folder is BackInfo)
        //    {
        //        Navigate(Path.GetDirectoryName(folder.Path));
        //    }
        //    else
        //    {
        //        AddToSelected(folder);
        //    }
        //});





        public ICommand SelectAllCommand

           => new Command(() =>
                    {
                        foreach (var item in CurrentDirList)
                            AddToSelected(item);
                    });






        public ICommand ClearAllCommand

            => new Command(() =>
            {
                SelectedDirList.Clear();
            });





        public Command BackupCommand
        {
            get
            {
                if (mBackupCommand == null)
                {
                    mBackupCommand = new Command(() =>
                    {
                        if (DriveManager.CheckAccess())
                        {
                            DateTime dateTime = DateTime.Now;
                            string toFolder;
                            string date = dateTime.ToString("dd-MM-yyyy");
                            string album = Path.Combine(DriveManager.SelectedUSBDrive.Name, (Environment.OSVersion).ToString(), Environment.MachineName, Environment.UserName, Constants.Person);
                            Directory.CreateDirectory(album);
                            string folder = Path.Combine(DriveManager.SelectedUSBDrive.Name, (Environment.OSVersion).ToString(), Environment.MachineName, Environment.UserName, Constants.Person);
                            toFolder = Path.Combine(DriveManager.SelectedUSBDrive.Name, (Environment.OSVersion).ToString(), Environment.MachineName, Environment.UserName, Constants.WindowsBackupFolderName + "\\Selected Backups - Photos and Videos\\" + date);
                            var ownerWindow = Application.Current.Windows[Application.Current.Windows.Count - 1];

                            var secondWindow = new Microsoft.Maui.Controls.Window
                            {

                                Page = new BackupToClickFreeWindow(SelectedDirList.Select(s => s.Path).ToList(), toFolder)
                                {

                                }

                            };

                            Application.Current.OpenWindow(secondWindow);





                            SelectedDirList.Clear();


                        }
                    },
                    () =>
                    {
                        return SelectedDirList.Count > 0;
                    });
                }

                return mBackupCommand;
            }
        }




       /* public Command DropCommand
        {
            get
            {
                if (mDropCommand == null)
                {
                    mDropCommand = new Command<DropEventArgs>(o =>
                    {
                        
                        var model = o.Data.Properties["Text"].ToString();

                        if (model is IOInfoBase)
                        {
                            AddToSelected();
                        }
                    },
                    o =>
                    {
                        return true;
                    });
                }

                return mDropCommand;
            }
        }*/

        /* public Command  DoDragCommand => new Command<DragEventArgs>(o =>
                     {
                         var args = o as DragEventArgs;


                         var lb = args.Data.Properties.Values as ListView;

                             if (lb != null && lb.SelectedItem != null && !(lb.SelectedItem is BackInfo))
                             {
                            *//* DropGestureRecognizer.DragLeaveCommandProperty(lb, lb.SelectedItem);
                                 DragDrop.DoDragDrop(lb, lb.SelectedItem, DragDropEffects.All);*//*
                             }
                  });
          */
        public ICommand DeleteFromSelectedCommand
        {
            get
            {
                if (mDeleteFromSelectedCommand == null)
                {
                    mDeleteFromSelectedCommand = new Command<IOInfoBase>(o =>
                    {
                        SelectedDirList.Remove(o);
                    });
                }

                return mDeleteFromSelectedCommand;
            }
        }

        public Command HeaderClickCommand


                 => new Command<EventCommandEventArgs>(args =>
                    {
                        HeaderInfo hi = new();

                        {
                            if (hi.IsStart)
                                Navigate(null);
                            else
                            {
                                Navigate(hi.Path);
                            }
                        }
                    });



        public BackupToUSBSelectVM()
        {
            AddDefaultDirectory();
            Navigate(null);

            //source = new List<IOInfoBase>();
            //this.source = source;
        }



        //IList<IOInfoBase> source;
        public ObservableCollection<HeaderInfo> Headers { get; set; } = new ObservableCollection<HeaderInfo>();
        public ObservableCollection<IOInfoBase> CurrentDirList { get; private set; } = new ObservableCollection<IOInfoBase>();
        public ObservableCollection<IOInfoBase> SelectedDirList { get; private set; } = new ObservableCollection<IOInfoBase>();
        public IOInfoBase CurrentDirSelectedItem { get; set; }
        public IOInfoBase SelectedDirSelectedItem { get; set; }



        /*public BackupToUSBSelectVM()

        {
            //here we will add default folders in selected directory list
            //AddDefaultDirectory();

        }*/

        public void AddDefaultDirectory()
        {
            string selectedpath = string.Empty;
            //for pictures
            selectedpath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Pictures";
            if (Directory.Exists(selectedpath))
                SelectedDirList.Add(new DirectoryInfo { Name = "Pictures", Path = selectedpath });
            //for videos
            selectedpath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Videos";
            if (Directory.Exists(selectedpath))
                SelectedDirList.Add(new DirectoryInfo { Name = "Videos", Path = selectedpath });
            //for Contacts
            selectedpath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Contacts";
            if (Directory.Exists(selectedpath))
                SelectedDirList.Add(new DirectoryInfo { Name = "Contacts", Path = selectedpath });
            //for Contacts
            selectedpath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Icloud Photos";
            if (Directory.Exists(selectedpath))
                SelectedDirList.Add(new DirectoryInfo { Name = "Icloud Photos", Path = selectedpath });
        }



        public void AddToSelected(IOInfoBase info)
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

            //Command.InvalidateRequerySuggested();
        }

        public void Navigate(string path)
        {
            CurrentDirList.Clear();
            Headers.Clear();

            Headers.Add(new HeaderInfo()
            {
                Name = "This PC",
                IsStart = true
            });

            if (string.IsNullOrWhiteSpace(path))
            {
                try
                {
                    foreach (var drive in DriveInfo.GetDrives())
                    {
                        if (drive.DriveType == DriveType.Fixed)
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
                }, new CancellationToken());

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




        public class CurrentDirectoiesHeaderTemplate : DataTemplateSelector
        {
            protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
            {

                BoxView element = container as BoxView;

                ScrollView sv = GetParent<ScrollView>(container);

                if (element != null && item is HeaderInfo hi)
                {

                    if (hi.IsStart)
                    {
                        return element.FindByName("first") as DataTemplate;
                    }
                    else
                    {
                        return element.FindByName("middle") as DataTemplate;
                    }

                    /* finally
                     {
                         sv?.ScrollToRight();
                     }*/


                }

                return null;
            }
        }
        private static T GetParent<T>(BindableObject currentObj) where T : BindableObject
        {
            T result = null;

            if (currentObj != null)
            {
                var parent = currentObj;

                if (parent is T res)
                {
                    result = res;
                }
                else
                {
                    result = GetParent<T>(parent);
                }
            }

            return result;
        }

        private static ScrollView FindScrollViewer(BindableObject root)
        {
            var queue = new Queue<BindableObject>(new[] { root });

            do
            {
                var item = queue.Dequeue();

                if (item is ScrollView)
                    return (ScrollView)item;

                /* for (var i = 0; i < item; i++)
                     queue.Enqueue(ITreeDesigner.Equals(item, i));*/
            } while (queue.Count > 0);

            return null;
        }





        
    }

}






