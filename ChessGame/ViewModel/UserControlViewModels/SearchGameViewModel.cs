using ChessGame.Commands;
using ChessGame.Model;
using ChessGame.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Input;

namespace ChessGame.ViewModel
{
    public class SearchGameViewModel : BaseViewModel
    {
        private readonly INavigationService _navigation;
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

        public SearchGameViewModel(INavigationService navigation)
        {
            _navigation = navigation;

            JoinCommand = new RelayCommand(JoinGame);
            MenuCommand = new RelayCommand(ReturnToMenu);
        }

        private void JoinGame(object parameter)
        {
            if (string.IsNullOrWhiteSpace(IpAddress))
            {
                MessageBox.Show("Будь ласка, введіть IP адресу хоста.");
                return;
            }

            var sp = ((App)Application.Current).ServiceProvider;

            var lobbyVM = sp.GetRequiredService<LobbyViewModel>();

            lobbyVM.Configure(isHost: false, ip: IpAddress);

            _navigation.NavigateTo(lobbyVM);
        }

        private void ReturnToMenu(object parameter)
        {
            _navigation.NavigateTo<MenuViewModel>();
        }
    }
}