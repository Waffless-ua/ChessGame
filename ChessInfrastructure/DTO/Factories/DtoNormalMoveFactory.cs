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
    /// Фабрика для звичайних ходів.
    /// </summary>
    public class DtoNormalMoveFactory : BaseDtoMoveFactory<DtoNormalMove>
    {
        public override DtoType TargetDtoType => DtoType.NormalMove;
        public override MoveType TargetMoveType => MoveType.NormalMove;

        public DtoNormalMoveFactory(IGameService gameService) : base(gameService)
        {
        }

        protected override Position GetFromPosition(DtoNormalMove dto) => dto.FromPos;

        protected override Move? FindMove(IEnumerable<Move> legalMoves, DtoNormalMove dto)
        {
            return legalMoves.FirstOrDefault(m =>
                m.ToPos == dto.ToPos && m.Type == TargetMoveType);
        }

        protected override string GetErrorMessage() => "Нелегальний звичайний хід.";

        public override IDtoMessage GetMoveToDTO(Move move)
        {
            return new DtoNormalMove(move.FromPos, move.ToPos);
        }
    }
}