using ChessGame.Model;
using ChessGame.Services.Interfaces;

namespace ChessGame.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly INavigationService _navigation;
        private readonly IGameState _gameState;

        public BaseViewModel CurrentView => _navigation.CurrentView;
        public Player CurrentPlayerSide => _gameState.ThisPlayer;

        public MainViewModel(INavigationService navigation, IGameState gameState)
        {
            _gameState = gameState;
            _navigation = navigation;

            _navigation.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(INavigationService.CurrentView))
                {
                    NotifyPropertyChanged(nameof(CurrentView));
                }
            };

            _gameState.ThisPlayerUpdated += (player) =>
            {
                NotifyPropertyChanged(nameof(CurrentPlayerSide));
            };
        }
    }
}
