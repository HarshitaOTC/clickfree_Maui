using clickfree_Maui.Helpers;
using clickfree_Maui.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace clickfree_Maui.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public virtual Task OnNavigatingTo(object? parameter)
            => Task.CompletedTask;

        public virtual Task OnNavigatedFrom(bool isForwardNavigation)
            => Task.CompletedTask;

        public virtual Task OnNavigatedTo()
            => Task.CompletedTask;
        public virtual void RaisePropertyChanged([CallerMemberName] string? property = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        
        
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
