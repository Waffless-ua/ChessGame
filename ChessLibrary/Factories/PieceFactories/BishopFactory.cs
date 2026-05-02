using ChessLibrary.Enums;
using ChessLibrary.Pieces;

namespace ChessLibrary.Factories.PieceFactories
{
    public class BishopFactory : ISubPieceFactory
    {
        public PieceType Type => PieceType.Bishop;

        public IPiece Create(Player color)
            => new Bishop(color);
    }
}
