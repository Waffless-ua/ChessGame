using ChessApplication.DTO;
using ChessApplication.Interfaces.Game;
using ChessApplication.Interfaces.Network;
using ChessLibrary.Enums;
using ChessLibrary.Moves;
using ChessLibrary.ValueObjects;

namespace ChessInfrastructure.DTO.Factories
{
    /// <summary>
    /// Базовий клас для фабрик перетворення DTO-повідомлень у ходи та назад.
    /// Усуває дублювання коду в конкретних фабриках за допомогою шаблону Template Method.
    /// </summary>
    /// <typeparam name="TDto">Тип DTO-повідомлення, яке обробляє фабрика.</typeparam>
    public abstract class BaseDtoMoveFactory<TDto> : ISpecificDtoMoveFactory
        where TDto : IDtoMessage
    {
        protected readonly IGameService GameService;

        public abstract DtoType TargetDtoType { get; }
        public abstract MoveType TargetMoveType { get; }

        protected BaseDtoMoveFactory(IGameService gameService)
        {
            GameService = gameService ?? throw new ArgumentNullException(nameof(gameService));
        }

        /// <summary>
        /// Отримує хід із DTO-повідомлення, перевіряючи його легальність.
        /// </summary>
        public Move GetMoveFromDTO(IDtoMessage message)
        {
            if (message is not TDto dto)
                throw new ArgumentException($"Очікувався тип {typeof(TDto).Name}", nameof(message));

            var legalMoves = GameService.GetLegalMoves(GetFromPosition(dto));
            var move = FindMove(legalMoves, dto);

            return move ?? throw new InvalidOperationException(GetErrorMessage());
        }

        /// <summary>
        /// Отримує початкову позицію з DTO для пошуку легальних ходів.
        /// </summary>
        protected abstract Position GetFromPosition(TDto dto);

        /// <summary>
        /// Шукає відповідний хід серед легальних.
        /// </summary>
        protected abstract Move? FindMove(IEnumerable<Move> legalMoves, TDto dto);

        /// <summary>
        /// Повідомлення про помилку у випадку нелегального ходу.
        /// </summary>
        protected abstract string GetErrorMessage();

        /// <summary>
        /// Перетворює хід у DTO-повідомлення для передачі мережею.
        /// </summary>
        public abstract IDtoMessage GetMoveToDTO(Move move);
    }
}