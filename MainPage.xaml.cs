using clickfree_Maui.Helpers;
using clickfree_Maui.ViewModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Globalization;
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif


namespace clickfree_Maui;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    const int WindowWidth = 750;
    const int WindowHeight = 550;
    private int mCurrentPage;
    public event PropertyChangedEventHandler PropertyChanged;
    
    public MainPage(MainPageViewModel viewModel)
    {
        
       BindingContext = viewModel;
        InitializeComponent();
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(MainPage), (handler, view) =>
        {
#if WINDOWS
            var mauiWindow = handler.VirtualView;
            var nativeWindow = handler.PlatformView;
            nativeWindow.Activate();
            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
            WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
            AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            // appWindow.Resize(new SizeInt32(WindowWidth, WindowHeight));
             
            appWindow.MoveAndResize(new RectInt32(1920 / 2 - WindowWidth / 2, 1080 / 2 - WindowHeight / 2, WindowWidth, WindowHeight));
            nativeWindow.ExtendsContentIntoTitleBar = false;
            //for remove title bar
            //    if(appWindow.Presenter is OverlappedPresenter p)
            //            {
            //                p.SetBorderAndTitleBar(false, true);
            //            }
            
           
            var presenter = appWindow.Presenter as OverlappedPresenter;
        presenter.IsResizable = false;
        presenter.IsMaximizable = false;
        //presenter.IsResizable = true;
#endif
        });
        previous_page.IsVisible = false;
        submit.IsEnabled = false;
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        bool ifDrive = DriveManager.HasUsbDrives;
        if (ifDrive == true)
        {
            DisplayAlert("ClickFree", "Welcome , Dear User Thanks for using Click Free, your USB device was Connected successfully","OK");
           // MessageBoxWindow.ShowMessageBox("ClickFree", "Welcome , Dear User Thanks for using Click Free, your USB device was Connected successfully ", MessageBoxWindow.MessageBoxType.Information);
        }
        else
            DisplayAlert("ClickFree", "Welcome , Dear User Thanks for using Click Free, Please Connect USB ", "OK");
        //   MessageBoxWindow.ShowMessageBox("ClickFree", "Welcome , Dear User Thanks for using Click Free, Please Connect USB ", MessageBoxWindow.MessageBoxType.Warning);
       
    }
    public int CurrentPage
    {
        get { return mCurrentPage; }
        set
        {
            if (mCurrentPage != value)
            {
                mCurrentPage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPage"));
            }

        }
    }
    private void Button_Clicked(object sender, EventArgs e)
    {
        
       
      
        CurrentPage++;
       if(CurrentPage ==1)
        {
            panel.IsVisible = false;
            submitEmail.IsVisible = true;
            contact.IsVisible = false;
            previous_page.IsVisible = true;
            suceessEmail.IsVisible = false;
        }
        else if(CurrentPage ==2)
        {
            panel.IsVisible = false;
            submitEmail.IsVisible = false;
            contact.IsVisible = true;
            previous_page.IsVisible = true;
            suceessEmail.IsVisible = false;
        }
 
    }



    private void previous_page_Clicked(object sender, EventArgs e)
    {
        CurrentPage--;
        if (CurrentPage == 1)
        {
            panel.IsVisible = false;
            submitEmail.IsVisible = true;
            contact.IsVisible = false;
            suceessEmail.IsVisible = false;
        }
        else if (CurrentPage == 0)
        {

            submitEmail.IsVisible = false;
            contact.IsVisible = false;
            panel.IsVisible = true;
            previous_page.IsVisible = false;
            suceessEmail.IsVisible = false;
        }
    }

    private void submit_Clicked(object sender, EventArgs e)
    {
           submitEmail.IsVisible = false;
           contact.IsVisible = false;
           panel.IsVisible = false;
           suceessEmail.IsVisible = true;
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        string text = e.NewTextValue;
        if(text=="")
        {
            submit.IsEnabled = false;
        }
        else
        {
            submit.IsEnabled = true;
            IsValidEmail(text);
        }
       
       
       

    }

    public bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                  RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examines the domain part of the email and normalizes it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                string domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException e)
        {
            return false;
        }
        catch (ArgumentException e)
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }



    //public void test()
    //{
    //    
    //    Application.Current.CloseWindow(GetParentWindow());

    //}


}

