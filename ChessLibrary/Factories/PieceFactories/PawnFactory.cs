using ChessLibrary.Enums;
using ChessLibrary.Pieces;

namespace ChessLibrary.Factories.PieceFactories
{
    public class PawnFactory : ISubPieceFactory
    {
        public PieceType Type => PieceType.Pawn;

        public IPiece Create(Player color)
            => new Pawn(color);
    }
}
