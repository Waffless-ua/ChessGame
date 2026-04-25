using ChessGame.Commands;
using ChessGame.Services.Interfaces;
using System.Windows.Input;

namespace ChessGame.ViewModel
{
    public class LobbyViewModel : BaseViewModel
    {
        private readonly ILobbyService _lobbyService;
        private readonly INavigationService _navigation;

        public string HeaderText => _lobbyService.HeaderText;
        public bool IsHost => _lobbyService.IsHost;
        public bool IsOtherPlayerConnected => _lobbyService.IsConnected;

        public bool CanStartGame => IsHost && IsOtherPlayerConnected;
        public ICommand StartGameCommand { get; }

        public LobbyViewModel(ILobbyService lobbyService, INavigationService navigation)
        {
            _lobbyService = lobbyService;
            _navigation = navigation;

            _lobbyService.StateChanged += OnStateChanged;

            StartGameCommand = new AsyncRelayCommand(
                StartGameAsync,
                () => CanStartGame
            );
        }

        private void OnStateChanged()
        {
            NotifyPropertyChanged(nameof(HeaderText));
            NotifyPropertyChanged(nameof(IsOtherPlayerConnected));
            NotifyPropertyChanged(nameof(IsHost));
            NotifyPropertyChanged(nameof(CanStartGame));

            (StartGameCommand as AsyncRelayCommand)?.RaiseCanExecuteChanged();
        }

        public Task ConfigureAsync(bool isHost, string ip = null)
            => _lobbyService.InitializeAsync(isHost, ip);

        private async Task StartGameAsync()
        {
            await _lobbyService.StartGameAsync();

            _navigation.NavigateTo<GameViewModel>();
        }

        public void Dispose()
        {
            _lobbyService.StateChanged -= OnStateChanged;
        }
    }
}