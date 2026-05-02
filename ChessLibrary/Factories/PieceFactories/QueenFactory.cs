using ChessLibrary.Enums;
using ChessLibrary.Pieces;

namespace ChessLibrary.Factories.PieceFactories
{
    public class QueenFactory : ISubPieceFactory
    {
        public PieceType Type => PieceType.Queen;
        public IPiece Create(Player color)
            => new Queen(color);
    }
}
