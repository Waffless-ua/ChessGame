using ChessLibrary.Enums;
using ChessLibrary.Pieces;

namespace ChessLibrary.Factories.PieceFactories
{
    public class RookFactory : ISubPieceFactory
    {
        public PieceType Type => PieceType.Rook;
        public IPiece Create(Player color)
            => new Rook(color);
    }
}
