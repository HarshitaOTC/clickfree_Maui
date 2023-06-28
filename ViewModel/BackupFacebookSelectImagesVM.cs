using clickfree_Maui.Facebook;
using clickfree_Maui.Contracts.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace clickfree_Maui.ViewModel
{
    public class BackupFacebookSelectImagesVM : BindableObject, INotifyPropertyChanged
    {
       
        readonly IDataService _dataService;
        readonly INavigationService _navigationService;

        public BackupFacebookSelectImagesVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Activated(null);

        }

       
        public class MediaContainerItem : ViewModelBase, INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            private bool mbIsSelected = false;
            private bool mbIsDownloading = false;
            private bool mbIsFailed = false;           
            //private ImageSource mImage = null;
            private ImageSource mbImage;
            readonly IDataService _dataService;
            readonly INavigationService _navigationService;
            public MediaContainerItem(IDataService dataService, INavigationService navigationService)
            {
                _dataService = dataService;
                _navigationService = navigationService;
            }
            public MediaContainerItem(INavigationService navigationService)
            {
                _navigationService = navigationService;
            }


            #region Fields


            #endregion

            #region Properties
            public Command SelectCommand => new Command(() =>
            {
                IsSelected = !IsSelected;
            });


            public FacebookManager.MediaResult Media { get; private set; }

            public ImageSource ImageSource
            {

                get
                {
                    if (mbImage == null)
                    {
                        IsFailed = false;
                        IsDownloading = true;
                    }
                    else
                    {
                        IsFailed = false;
                        IsDownloading = false;
                    }

                    return mbImage;
                }

                set
                {
                    if (mbImage != value)
                    {
                        mbImage = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImageSource"));
                    }
                }

            }

            public bool IsSelected
            {
                get
                { return mbIsSelected; }
                set
                {
                    if (mbIsSelected != value)
                    {
                        mbIsSelected = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsSelected"));
                    }

                }
            }
            public bool IsDownloading
            {
                get { return mbIsDownloading; }
                set
                {
                    if (mbIsDownloading != value)
                    {
                        mbIsDownloading = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsDownloading"));
                    }

                }
            }
            public bool IsFailed
            {
                get { return mbIsFailed; }
                set
                {
                    if (mbIsFailed != value)
                    {
                        mbIsFailed = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsFailed"));
                    }

                }
            }


            #endregion

            #region Ctor

            public MediaContainerItem(FacebookManager.MediaResult media)
            {
                Media = media;
            }

            #endregion
        }


        #region Fields

        #region Commands

     
        #endregion

        private bool mbLoadingInProgress = false;
        private CancellationTokenSource mCancellationTokenSource = new CancellationTokenSource();
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties

        #region Commands




        public Command SelectAllCommand => new Command(() =>
        {

            foreach (var item in Items)
                item.IsSelected = true;
        });


        public Command DeselectAllCommand => new Command(() =>
        {
            foreach (var item in Items)
                item.IsSelected = false;

        });

        public Command TransferCommand => new Command(async () =>
        { 
           
                await _navigationService.BackupFacebookDestView(Items.Where(i => i.IsSelected).Select(i => i.Media).ToArray());
            
            //}// NavigateTo(NavigateEnum.BackupInstagramDest, Items.Where(i => i.IsSelected).Select(i => i.Media).ToArray());
        });

        #endregion

        public bool LoadingInProgress
        {
            get { return mbLoadingInProgress; }
            set
            {
                if (mbLoadingInProgress != value)
                {
                    mbLoadingInProgress = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LoadingInProgress"));
                }

            }
        }

        public ObservableCollection<MediaContainerItem> Items { get; private set; } = new ObservableCollection<MediaContainerItem>();

        #endregion

        #region Ctor



        #endregion

        #region Overrides
    
        protected internal async void Activated(object parameter)
        {
            try
            {
               // Items.Clear();

                LoadingInProgress = true;

                foreach (var photo in await FacebookManager.LoadALLPhotos(mCancellationTokenSource))
                {
                    Items.Add(new MediaContainerItem(photo)
                    {
                        IsDownloading = true
                    });
                }
                foreach (var video in await FacebookManager.LoadALLVideos(mCancellationTokenSource))
                {
                    Items.Add(new MediaContainerItem(video)
                    {
                        IsDownloading = true
                    });
                }
                await Task.Run(() =>
                {
                    try
                    {
                        foreach (var item in Items)
                        {
                            mCancellationTokenSource.Token.ThrowIfCancellationRequested();

                            using (WebClient wc = new WebClient())
                            {
                                try
                                {
                                    wc.DownloadProgressChanged += (s, e) =>
                                    {
                                        mCancellationTokenSource.Token.ThrowIfCancellationRequested();
                                    };
                                    //var a = ImageSource.FromStream(() => new MemoryStream(wc.DownloadData(new Uri(item.Media.Thumbnail, UriKind.RelativeOrAbsolute))));
                                    //Dispatcher.Dispatch(() => { item.ImageSource = a; }); 
                                    byte[] BytesFile = wc.DownloadData(new Uri(item.Media.Thumbnail, UriKind.RelativeOrAbsolute));

                                    MemoryStream iStream = new MemoryStream(BytesFile);
                                    ImageSource imageSource = ImageSource.FromStream(() => iStream);


                                    // Image a = new Image();
                                    // a.Source = ImageSource.FromStream(() => new MemoryStream(wc.DownloadData(new Uri(item.Media.Thumbnail, UriKind.RelativeOrAbsolute))));         
                                    var a = ImageSource.FromStream(() => new MemoryStream(wc.DownloadData(new Uri(item.Media.Thumbnail, UriKind.RelativeOrAbsolute))));
                                    ImageSource imageSource1 = imageSource;

                                    //Dispatcher.Dispatch(()=>{item.ImageSource = a; });
                                    item.ImageSource = imageSource1;
                                }
                                catch
                                {
                                    item.IsDownloading = false;
                                    item.IsFailed = true;
                                }
                            }
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        /**/
                    }
                    catch
                    {
                        /**/
                    }
                });
            }
            catch (Exception ex)
            {

            }

        }

        protected internal void Deactivated()
        {
            mCancellationTokenSource?.Cancel();

            mCancellationTokenSource = new CancellationTokenSource();

            LoadingInProgress = false;
        }

        
        #endregion
    }
}

