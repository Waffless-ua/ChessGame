using ChessApplication.DTO;
using ChessApplication.DTO.Messages;
using ChessApplication.Interfaces.Game;
using ChessApplication.Interfaces.Network;
using ChessLibrary.Enums;
using ChessLibrary.Moves;
using ChessLibrary.ValueObjects;

namespace ChessInfrastructure.DTO.Factories
{
    /// <summary>
    /// Фабрика для подвійного ходу пішака.
    /// </summary>
    public class DtoDoubleMoveFactory : BaseDtoMoveFactory<DtoDoubleMove>
    {
        public override DtoType TargetDtoType => DtoType.DoubleMove;
        public override MoveType TargetMoveType => MoveType.DoublePawn;

        public DtoDoubleMoveFactory(IGameService gameService) : base(gameService)
        {
        }

        protected override Position GetFromPosition(DtoDoubleMove dto) => dto.FromPos;

        protected override Move? FindMove(IEnumerable<Move> legalMoves, DtoDoubleMove dto)
        {
            return legalMoves.FirstOrDefault(m =>
                m.ToPos == dto.ToPos && m.Type == TargetMoveType);
        }

        protected override string GetErrorMessage() => "Нелегальний подвійний хід пішака.";

        public override IDtoMessage GetMoveToDTO(Move move)
        {
            return new DtoDoubleMove(move.FromPos, move.ToPos);
        }
    }
}