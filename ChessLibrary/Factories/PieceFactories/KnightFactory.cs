using ChessLibrary.Enums;
using ChessLibrary.Pieces;

namespace ChessLibrary.Factories.PieceFactories
{
    public class KnightFactory : ISubPieceFactory
    {
        public PieceType Type => PieceType.Knight;

        public IPiece Create(Player color)
            => new Knight(color);
    }
}
