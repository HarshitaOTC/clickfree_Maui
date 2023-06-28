using clickfree_Maui.Contracts.Services;
using clickfree_Maui.Instagram;
using clickfree_Maui.Services;
using ImageMagick;
using Microsoft.Maui.Controls;

using Microsoft.VisualStudio.Services.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Image = Microsoft.Maui.Controls.Image;



namespace clickfree_Maui.ViewModel;

public class BackupInstagramSelectImagesVM : BindableObject, INotifyPropertyChanged
{
    readonly IDataService _dataService;
    readonly INavigationService _navigationService;

    public BackupInstagramSelectImagesVM(INavigationService navigationService)
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
        public Command SelectCommand => new Command(() =>
                    {
                        IsSelected = !IsSelected;
                    });


        public InstagramManager.MediaResult Media { get; private set; }


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


        public MediaContainerItem(InstagramManager.MediaResult media)
        {
            Media = media;
        }

    }


    private bool mbLoadingInProgress = false;
    private CancellationTokenSource mCancellationTokenSource = new CancellationTokenSource();
   

    public event PropertyChangedEventHandler PropertyChanged;

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

                    await _navigationService.BackupInstagramDestView(Items.Where(i => i.IsSelected).Select(i => i.Media).ToArray());

                    // NavigateTo(NavigateEnum.BackupInstagramDest, Items.Where(i => i.IsSelected).Select(i => i.Media).ToArray());
                });
   
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


    protected internal async void Activated(object parameter)
    {

        try
        {

            // Items.Clear();

            LoadingInProgress = true;

            foreach (var photo in await InstagramManager.LoadALLPhotos(mCancellationTokenSource))
            {
                Items.Add(new MediaContainerItem(photo)
                {
                    IsDownloading = true
                });
            }
            await Task.Run(async () =>
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

                                byte[] BytesFile = wc.DownloadData(new Uri(item.Media.Thumbnail, UriKind.RelativeOrAbsolute));

                                MemoryStream iStream = new MemoryStream(BytesFile);
                                ImageSource imageSource = ImageSource.FromStream(() => iStream);


                                // Image a = new Image();
                                // a.Source = ImageSource.FromStream(() => new MemoryStream(wc.DownloadData(new Uri(item.Media.Thumbnail, UriKind.RelativeOrAbsolute))));         
                                var a = ImageSource.FromStream(() => new MemoryStream(wc.DownloadData(new Uri(item.Media.Thumbnail, UriKind.RelativeOrAbsolute))));
                                ImageSource imageSource1 = imageSource;

                                //Dispatcher.Dispatch(()=>{item.ImageSource = a; });
                                item.ImageSource = imageSource1;

                                // Thread.Sleep(1000);
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
}
