using ChessLibrary.Board;
using ChessLibrary.Enums;
using ChessLibrary.Factories;
using ChessLibrary.Pieces;
using ChessLibrary.ValueObjects;

namespace ChessLibrary.Moves.PawnMoves
{
    public class PawnPromotion : Move
    {
        public override MoveType Type => MoveType.PawnPromotion;

        public override Position FromPos { get; }
        public override Position ToPos { get; }

        public PieceType PromotionPieceType { get; }

        private readonly IPieceFactory _pieceFactory;

        public PawnPromotion(
            Position fromPos,
            Position toPos,
            PieceType promotionPieceType,
            IPieceFactory pieceFactory)
        {
            FromPos = fromPos;
            ToPos = toPos;
            PromotionPieceType = promotionPieceType;
            _pieceFactory = pieceFactory;
        }

        public override void Execute(IBoard board)
        {
            IPiece pawn = board[FromPos];
            Player color = pawn.Color;

            board[FromPos] = null;
            board[ToPos] = _pieceFactory.CreatePiece(PromotionPieceType, color);
        }
    }
}