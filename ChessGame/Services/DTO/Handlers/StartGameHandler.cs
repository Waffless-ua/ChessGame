using ChessGame.Model.Abstractions;
using ChessGame.Services.Abstractions;
using ChessGame.Services.DTO.Messages;
using ChessGame.Services.Interfaces;
using ChessGame.ViewModel;

namespace ChessGame.Services.DTO.Handlers
{
    public class StartGameHandler : IMessageHandler<DtoStartGame>
    {
        private readonly IGameService _gameService;
        private readonly INavigationService _navigationService;

        public StartGameHandler(IGameService gameService, INavigationService navigationService)
        {
            _gameService = gameService;
            _navigationService = navigationService;
        }

        public Task HandleAsync(DtoStartGame message)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                _gameService.InitGame(message.StartingSide);
                _navigationService.NavigateTo<GameViewModel>();
            });

            return Task.CompletedTask;
        }
    }
}
