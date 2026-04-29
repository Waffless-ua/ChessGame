using ChessGame.Services.Abstractions;
using ChessGame.Services.DTO.Messages;
using ChessGame.Services.Interfaces;


namespace ChessGame.Services.DTO.Handlers
{
    public class PromotionMoveHandler : IMessageHandler<DtoPromotionMove>
    {
        private readonly IGameService _gameService;
        private readonly IDtoMoveFactory _moveFactory;

        public PromotionMoveHandler(IDtoMoveFactory moveFactory, IGameService gameService)
        {
            _moveFactory = moveFactory;
            _gameService = gameService;
        }

        public Task HandleAsync(DtoPromotionMove message)
        {
            var move = _moveFactory.GetMoveFromDTO(message);
            _gameService.TryMakeMove(move);
            return Task.CompletedTask;
        }
    }
}
