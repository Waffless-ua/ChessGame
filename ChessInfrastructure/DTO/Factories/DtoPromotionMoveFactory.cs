using ChessApplication.DTO;
using ChessApplication.DTO.Messages;
using ChessApplication.Interfaces.Game;
using ChessApplication.Interfaces.Network;
using ChessLibrary.Enums;
using ChessLibrary.Moves;
using ChessLibrary.Moves.PawnMoves;
using ChessLibrary.ValueObjects;

namespace ChessInfrastructure.DTO.Factories
{
    /// <summary>
    /// Фабрика для ходу з перетворенням пішака.
    /// </summary>
    public class DtoPromotionMoveFactory : BaseDtoMoveFactory<DtoPromotionMove>
    {
        public override DtoType TargetDtoType => DtoType.PromotionMove;
        public override MoveType TargetMoveType => MoveType.PawnPromotion;

        public DtoPromotionMoveFactory(IGameService gameService) : base(gameService)
        {
        }

        protected override Position GetFromPosition(DtoPromotionMove dto) => dto.FromPos;

        protected override Move? FindMove(IEnumerable<Move> legalMoves, DtoPromotionMove dto)
        {
            return legalMoves
                .OfType<PawnPromotion>()
                .FirstOrDefault(m =>
                    m.ToPos == dto.ToPos &&
                    m.PromotionPieceType == dto.PromotionPieceType);
        }

        protected override string GetErrorMessage() => "Нелегальний хід з перетворенням пішака.";

        public override IDtoMessage GetMoveToDTO(Move move)
        {
            var promotion = (PawnPromotion)move;
            return new DtoPromotionMove(
                promotion.FromPos,
                promotion.ToPos,
                promotion.PromotionPieceType);
        }
    }
}