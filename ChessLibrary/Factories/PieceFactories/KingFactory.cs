using ChessLibrary.Enums;
using ChessLibrary.Pieces;

namespace ChessLibrary.Factories.PieceFactories
{
    public class KingFactory : ISubPieceFactory
    {
        public PieceType Type => PieceType.King;

        public IPiece Create(Player color)
            => new King(color);
    }
}
