using ChessGame.Services.Interfaces;

namespace ChessGame.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly INavigationService _navigation;

        public BaseViewModel CurrentView => _navigation.CurrentView;

        public MainViewModel(INavigationService navigation)
        {
            _navigation = navigation;

            _navigation.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(INavigationService.CurrentView))
                {
                    NotifyPropertyChanged(nameof(CurrentView));
                }
            };
        }
    }
}
