using ChessGame.Commands;
using ChessGame.Model;
using ChessGame.Model.Data;
using ChessGame.Services.Implementations;
using ChessGame.Services.Interfaces;
using ChessGame.Services.Interfaces.Factories;
using System.Windows;
using System.Windows.Input;

namespace ChessGame.ViewModel
{
    public class SearchGameViewModel : BaseViewModel
    {
        private readonly INavigationService _navigation;
        private readonly IViewModelFactory<LobbyParams> _lobbyFactory;
        private readonly ILobbyService _lobbyService;

        private string _ipAddress = "127.0.0.1";

        public string IpAddress
        {
            get => _ipAddress;
            set
            {
                _ipAddress = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand JoinCommand { get; }
        public ICommand MenuCommand { get; }

        public SearchGameViewModel(
            INavigationService navigation,
            IViewModelFactory<LobbyParams> lobbyFactory,
            ILobbyService lobbyService)
        {
            _navigation = navigation;
            _lobbyFactory = lobbyFactory;
            _lobbyService = lobbyService;

            JoinCommand = new AsyncRelayCommand(JoinGame);
            MenuCommand = new RelayCommand(ReturnToMenu);
        }

        private async Task JoinGame()
        {
            if (string.IsNullOrWhiteSpace(IpAddress))
            {
                MessageBox.Show("Будь ласка, введіть IP адресу хоста.");
                return;
            }

            var param = new LobbyParams(isHost: false, IpAddress);

            var connected = await _lobbyService.InitializeAsync(param);

            if (!connected)
            {
                MessageBox.Show("Не вдалося підключитися до хоста.");
                return;
            }

            var lobbyVM = _lobbyFactory.CreateViewModelWithParams(param);

            _navigation.NavigateTo(lobbyVM);
        }

        private void ReturnToMenu(object parameter)
        {
            _navigation.NavigateTo<MenuViewModel>();
        }
    }
}