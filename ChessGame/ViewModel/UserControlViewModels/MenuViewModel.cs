using ChessGame.Commands;
using ChessGame.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Input;

namespace ChessGame.ViewModel
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly INavigationService _navigation;

        public ICommand CreateGameCommand { get; }
        public ICommand SearchGameCommand { get; }
        public ICommand SettingsCommand { get; }
        public ICommand ExitCommand { get; }

        public MenuViewModel(INavigationService navigation)
        {
            _navigation = navigation;

            CreateGameCommand = new RelayCommand(CreateGame);
            SearchGameCommand = new RelayCommand(SearchGame);
            SettingsCommand = new RelayCommand(Settings);
            ExitCommand = new RelayCommand(Exit);
        }

        public void CreateGame(object obj)
        {
            var app = (App)Application.Current;
            var serviceProvider = app.ServiceProvider;

            var lobbyVM = serviceProvider.GetRequiredService<LobbyViewModel>();

            lobbyVM.ConfigureAsync(isHost: true);

            _navigation.NavigateTo(lobbyVM);
        }

        public void SearchGame(object obj)
        {
            _navigation.NavigateTo<SearchGameViewModel>();
        }

        public void Settings(object obj)
        {
            _navigation.NavigateTo<SettingsViewModel>();
        }

        public void Exit(object obj)
        {
            Application.Current.Shutdown();
        }
    }
}