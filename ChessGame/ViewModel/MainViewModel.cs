using ChessGame.Model;
using ChessGame.Services;
using ChessGame.Services.Interfaces;

namespace ChessGame.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly INavigationService _navigation;
        private readonly IGameService _gameService;

        public BaseViewModel CurrentView => _navigation.CurrentView;
        public Player CurrentPlayerSide => _gameService.ThisPlayer;

        public MainViewModel(INavigationService navigation, IGameService gameService)
        {
            _gameService = gameService;
            _navigation = navigation;

            _navigation.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(INavigationService.CurrentView))
                {
                    NotifyPropertyChanged(nameof(CurrentView));
                }
            };

            _gameService.PlayerChanged += OnPlayerChanged;
        }
        private void OnPlayerChanged()
        {
            NotifyPropertyChanged(nameof(CurrentPlayerSide));
        }
    }
}
