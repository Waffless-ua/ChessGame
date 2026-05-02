using ChessLibrary.Enums;
using ChessLibrary.Pieces;

namespace ChessLibrary.Factories
{
    public interface IPieceFactory
    {
        IPiece CreatePiece(PieceType type, Player color);
    }
}
