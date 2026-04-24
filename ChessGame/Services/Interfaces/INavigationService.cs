using ChessGame.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Services.Interfaces
{
    public interface INavigationService : INotifyPropertyChanged
    {
        BaseViewModel CurrentView { get; }
        void NavigateTo<T>() where T : BaseViewModel;
        void NavigateTo(BaseViewModel viewModel);
    }
}
