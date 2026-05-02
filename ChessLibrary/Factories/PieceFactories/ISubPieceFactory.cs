using ChessLibrary.Enums;
using ChessLibrary.Pieces;

namespace ChessLibrary.Factories.PieceFactories
{
    public interface ISubPieceFactory
    {
        PieceType Type { get; }
        IPiece Create(Player color);
    }
}
