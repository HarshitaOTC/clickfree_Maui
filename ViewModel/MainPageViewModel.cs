using clickfree_Maui.Contracts.Services;
using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.Input;
using clickfree_Maui.Facebook;

namespace clickfree_Maui.ViewModel
{

    public class MainPageViewModel :ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        readonly IDataService _dataService;
        readonly INavigationService _navigationService;
        private Command mNextPageCommand = null;
        private Command mPrevPageCommand = null;
        private int mCurrentPage = 0;
        private Command mSubmitEmailCommand = null;
        private Command mSendEmailCommand = null;
        private string mEmail;
        private bool mEmailSubmitted = false;
        public Command NavigateCommand
            => new Command(async () => await _navigationService.NavigateToMainView());

        public MainPageViewModel(IDataService dataService, INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
        }
        public Command SubmitEmailCommand => new Command(() =>
        {
            //get
            {
                //if (mSubmitEmailCommand == null)
                //{
                //    mSubmitEmailCommand = new Command(() =>
                //    {
                if (FacebookManager.CheckNetworkConnection())
                {
                    try
                    {
                        HttpWebRequest request = WebRequest.CreateHttp("https://api.sendgrid.com/v3/marketing/contacts");
                        request.ContentType = "application/json";
                        request.Method = "PUT";
                        request.Headers.Add("authorization", "Bearer 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");
                        using (var rStream = request.GetRequestStream())
                        {
                            var rString = "{\"list_ids\":[\"3f131b5e-5d77-4dff-bc80-bb2ee76119d7\"],\"contacts\":[{\"email\":\"" + Email + "\"}]}";
                            var rStringBytes = Encoding.UTF8.GetBytes(rString);

                            rStream.Write(rStringBytes, 0, rStringBytes.Length);
                        }
                        using (var response = request.GetResponse())
                        {
                            using (var responseStream = response.GetResponseStream())
                            {
                                StreamReader sr = new StreamReader(responseStream);

                                var text = sr.ReadToEnd();
                            }
                        }

                        EmailSubmitted = true;
                        
                    }
                    catch
                    {
                    }
                }
                //    },
                //    () =>
                //    {
                //        return IsValidEmail(Email);
                //    });
                //}
                //return mSubmitEmailCommand;
            }
        });




        public string Email
        {
            get { return mEmail; }
            set
            {
                if (mEmail != value)
                {
                    mEmail = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Email"));
                    // CommandManager.InvalidateRequerySuggested();
                }

            }

        }



        #region Methods

        public  bool IsValidEmail(string email)
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

        public Command SendEmailCommand
        {
            get
            {
                if (mSendEmailCommand == null)
                {
                    mSendEmailCommand = new Command(() =>
                    {
                        System.Diagnostics.Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", "mailto:wecare@clickfreebackup.com?subject=Click Free app question (Windows)");

                    });
                }

                return mSendEmailCommand;
            }
        }

        #endregion

        public bool EmailSubmitted
        {
            get { return mEmailSubmitted; }
            set
            {
                if (mEmailSubmitted != value)
                {
                    mEmailSubmitted = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EmailSubmitted"));
                    // CommandManager.InvalidateRequerySuggested();
                }

            }
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



        public override Task OnNavigatedFrom(bool isForwardNavigation)
        {
            Console.WriteLine($"On {(isForwardNavigation ? "forward" : "backward")} navigated from MainPage");
            return base.OnNavigatedFrom(isForwardNavigation);
        }

        public override Task OnNavigatingTo(object? parameter)
        {
            Console.WriteLine($"On navigating to MainPage with parameter {parameter}");
            return base.OnNavigatingTo(parameter);
        }

        public override Task OnNavigatedTo()
        {
            Console.WriteLine("On navigated to MainPage");
            return base.OnNavigatedTo();
        }

    }
}

