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
    /// Фабрика для ходу "взяття на проході" (en passant).
    /// </summary>
    public class DtoEnPasssantMoveFactory : BaseDtoMoveFactory<DtoEnPassantMove>
    {
        public override DtoType TargetDtoType => DtoType.EnPassant;
        public override MoveType TargetMoveType => MoveType.EnPassant;

        public DtoEnPasssantMoveFactory(IGameService gameService) : base(gameService)
        {
        }

        protected override Position GetFromPosition(DtoEnPassantMove dto) => dto.FromPos;

        protected override Move? FindMove(IEnumerable<Move> legalMoves, DtoEnPassantMove dto)
        {
            return legalMoves.FirstOrDefault(m =>
                m.ToPos == dto.ToPos && m.Type == TargetMoveType);
        }

        protected override string GetErrorMessage() => "Нелегальний хід en passant.";

        public override IDtoMessage GetMoveToDTO(Move move)
        {
            return new DtoEnPassantMove(move.FromPos, move.ToPos);
        }
    }
}