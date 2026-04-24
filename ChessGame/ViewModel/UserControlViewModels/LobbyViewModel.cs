using ChessGame.Commands;
using ChessGame.Model;
using ChessGame.Model.DTO;
using ChessGame.Model.DTO.Messages;
using ChessGame.Services.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChessGame.ViewModel
{
    public class LobbyViewModel : BaseViewModel
    {
        private readonly INavigationService _navigation;
        private readonly INetworkService _networkService;
        private readonly IGameState _gameState;

        private string _headerText;
        public string HeaderText
        {
            get => _headerText;
            set { _headerText = value; NotifyPropertyChanged(); }
        }

        private bool _isHost;
        public bool IsHost
        {
            get => _isHost;
            private set { _isHost = value; NotifyPropertyChanged(); }
        }

        private bool _isOtherPlayerConnected;
        public bool IsOtherPlayerConnected
        {
            get => _isOtherPlayerConnected;
            set
            {
                _isOtherPlayerConnected = value;
                NotifyPropertyChanged();
                SetHeaderText();

                CommandManager.InvalidateRequerySuggested();
            }
        }

        public ICommand StartGameCommand { get; }

        public LobbyViewModel(
            INavigationService navigation,
            INetworkService networkService,
            IGameState gameState)
        {
            _navigation = navigation;
            _networkService = networkService;
            _gameState = gameState;

            StartGameCommand = new RelayCommand(StartGame, _ => IsHost && IsOtherPlayerConnected);
        }

        public void Configure(bool isHost, string ip = null)
        {
            IsHost = isHost;
            IsOtherPlayerConnected = false;

            InitializeNetworkService(ip);
            SetHeaderText();
        }

        public void SetHeaderText()
        {
            if (IsHost)
            {
                HeaderText = IsOtherPlayerConnected ? "Суперник приєднався" : "Очікування суперника";
            }
            else
            {
                HeaderText = "Очікування початку гри";
            }
        }

        private async void InitializeNetworkService(string ip = null)
        {
            try
            {
                if (ip != null)
                {
                    await _networkService.ConnectAsync(ip, 55555);
                    IsOtherPlayerConnected = true;
                }
                else
                {
                    _ = Task.Run(async () =>
                    {
                        try
                        {
                            await _networkService.StartServerAsync(55555);

                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                IsOtherPlayerConnected = true;
                                CommandManager.InvalidateRequerySuggested();
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Server Error: {ex.Message}");
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                HeaderText = "Помилка мережі";
                Console.WriteLine($"Network Init Error: {ex.Message}");
            }
        }

        private async void StartGame(object obj)
        {
            if (!IsHost)
            {
                return;
            }

            try
            {
                Player hostColor = Player.White;
                Player clientColor = Player.Black;

                await _networkService.SendAsync(DtoType.StartGame, new DtoStartGame(clientColor));

                _gameState.Initialize(hostColor);
                _navigation.NavigateTo<GameViewModel>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Хост: Помилка при запуску гри: {ex.Message}", "Host Error");
            }
        }
    }
}