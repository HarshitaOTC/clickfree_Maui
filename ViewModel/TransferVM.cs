using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using clickfree_Maui.Contracts.Services;
using clickfree_Maui.Helpers;
using clickfree_Maui.Windows;
using Microsoft.VisualBasic;
using Telerik.Windows.Documents.Fixed.Model.Data;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace clickfree_Maui.ViewModel
{
    public class TransferVM : ViewModelBase, INotifyPropertyChanged
    {
        string mCDir;
        public event PropertyChangedEventHandler PropertyChanged;
        private string mCurrentDir;
        readonly INavigationService _navigationService;
        public TransferVM()
        {
            CurrentDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), Constants.ClickFreeFolderName);
        }
        public Command GoBackCommand
            => new Command(async () => await _navigationService.NavigateBack());
        public Command TransferCommand
            => new Command(() =>
            {

                var secondWindow = new Window
                {
                    Page = new TransferToPCView(CurrentDir)
                    {

                    }
                };
                Application.Current.OpenWindow(secondWindow);

            });
         
        public string CurrentDir
        {
            get { return mCDir; }
            set
            {
                if (mCDir != value)
                {
                    mCDir = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentDir"));
                }

            }
        }

      /*  public TransferVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
*/
        public override Task OnNavigatedFrom(bool isForwardNavigation)
        {
            Console.WriteLine($"On {(isForwardNavigation ? "forward" : "backward")} navigated from ThirdPage");
            return base.OnNavigatedFrom(isForwardNavigation);
        }

        public override Task OnNavigatingTo(object? parameter)
        {
            Console.WriteLine($"On navigating to ThirdPage with parameter {parameter}");
            return base.OnNavigatingTo(parameter);
        }

        public override Task OnNavigatedTo()
        {
            Console.WriteLine("On navigated to ThirdPage");
            return base.OnNavigatedTo();
        }
    }
}
