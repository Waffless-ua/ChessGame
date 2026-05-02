using ChessLibrary.Enums;

namespace ChessLibrary.Pieces
{
    public interface IPiece
    {
        PieceType Type { get; }
        Player Color { get; }
        bool HasMoved { get; set; }
        IPiece Copy();
    }
}