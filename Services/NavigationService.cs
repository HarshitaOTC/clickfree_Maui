﻿using clickfree_Maui.Contracts.Services;
using clickfree_Maui.Instagram;
using clickfree_Maui.ViewModel;
using clickfree_Maui.Views;
using clickfree_Maui.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clickfree_Maui.Services
{
    public class NavigationService : INavigationService
    {
        readonly IServiceProvider _services;

        protected INavigation Navigation
        {
            get
            {
                INavigation? navigation = Application.Current?.MainPage?.Navigation;
                if (navigation is not null)
                    return navigation;
                else
                {
                    //This is not good!
                    if (Debugger.IsAttached)
                        Debugger.Break();
                    throw new Exception();
                }
            }
        }

        public NavigationService(IServiceProvider services)
            => _services = services;
        public Task BackupToUSBMainView() => NavigateToPage<BackupToUSBMainView>();
        public Task NavigateToMainView()=> NavigateToPage<MainView>();
        public Task TransferToPCWindow() => NavigateToPage<TransferToPCView>();
        public Task InstagramLoginWindow() => NavigateToPage<InstagramLoginWindow>();
        public Task BackupInstagramMainView() => NavigateToPage<BackupInstagramMainView>();
        public Task DefaultFolderView() => NavigateToPage<DefaultFolderView>();
        public Task BackupToUSBSelectView() => NavigateToPage<BackupToUSBSelectView>();
        public Task EraseDevice() => NavigateToPage<EraseDevice>();
        public Task NavigateToSecondPage(string id)
            => NavigateToPage<SecondPage>(id);

        public Task FacebookLoginwindow()
            => NavigateToPage<FacebookLoginwindow>();
        public Task Continue_facebook()
            => NavigateToPage<Continue_facebook>();
        public Task BackupFacebookMainView()=>NavigateToPage<BackupFacebookMainView>();
        public Task BackupFacebookSelectImagesView() => NavigateToPage<BackupFacebookSelectImagesView>();

        public Task BackupFacebookDestView(Object Parameter)
           => NavigateToPage<BackupFacebookDestView>(Parameter);
        public Task TransferView()
            => NavigateToPage<TransferView>();
        public Task BackupToClickFreeWindow()
           => NavigateToPage<BackupToClickFreeWindow>();
        public Task NavigateToMainPage()
            => NavigateToPage<MainPage>();

        public Task Continue()
                => NavigateToPage<Continue>();
        public Task MessageBoxWindow()
               => NavigateToPage<MessageBoxWindow>();
        public Task NavigateBack()
        {
            if (Navigation.NavigationStack.Count > 1)
                return Navigation.PopAsync();

            throw new InvalidOperationException("No pages to navigate back to!");
        }
        public Task BackupInstagramDestView(Object Parameter) => NavigateToPage<BackupInstagramDestView>(Parameter);
        public Task BackupInstagramSelectImagesView() => NavigateToPage<BackupInstagramSelectImagesView>();
       

        private async Task NavigateToPage<T>(object? parameter = null) where T : Page
        {
            var toPage = ResolvePage<T>();

            if (toPage is not null)
            {
                //Subscribe to the toPage's NavigatedTo event
                toPage.NavigatedTo += Page_NavigatedTo;

                //Get VM of the toPage
                var toViewModel = GetPageViewModelBase(toPage);

                //Call navigatingTo on VM, passing in the paramter
                if (toViewModel is not null)
                    await toViewModel.OnNavigatingTo(parameter);

                //Navigate to requested page
                await Navigation.PushAsync(toPage, true);

                //Subscribe to the toPage's NavigatedFrom event
                toPage.NavigatedFrom += Page_NavigatedFrom;
            }
            else
                throw new InvalidOperationException($"Unable to resolve type {typeof(T).FullName}");
        }

        private async void Page_NavigatedFrom(object? sender, NavigatedFromEventArgs e)
        {
            //To determine forward navigation, we look at the 2nd to last item on the NavigationStack
            //If that entry equals the sender, it means we navigated forward from the sender to another page
            bool isForwardNavigation = Navigation.NavigationStack.Count > 1
                && Navigation.NavigationStack[^2] == sender;

            if (sender is Page thisPage)
            {
                if (!isForwardNavigation)
                {
                    thisPage.NavigatedTo -= Page_NavigatedTo;
                    thisPage.NavigatedFrom -= Page_NavigatedFrom;
                }

                await CallNavigatedFrom(thisPage, isForwardNavigation);
            }
        }

        private Task CallNavigatedFrom(Page p, bool isForward)
        {
            var fromViewModel = GetPageViewModelBase(p);

            if (fromViewModel is not null)
                return fromViewModel.OnNavigatedFrom(isForward);
            return Task.CompletedTask;
        }

        private async void Page_NavigatedTo(object? sender, NavigatedToEventArgs e)
            => await CallNavigatedTo(sender as Page);

        private Task CallNavigatedTo(Page? p)
        {
            var fromViewModel = GetPageViewModelBase(p);

            if (fromViewModel is not null)
                return fromViewModel.OnNavigatedTo();
            return Task.CompletedTask;
        }

        private ViewModelBase? GetPageViewModelBase(Page? p)
        {
            return p?.BindingContext as ViewModelBase;
        }

        private T? ResolvePage<T>() where T : Page
            => _services.GetService<T>();

       
    }
}
