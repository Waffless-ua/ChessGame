using ChessLibrary.Enums;

namespace ChessLibrary.Pieces
{
    public abstract class Piece : IPiece
    {
        public abstract PieceType Type { get; }
        public abstract Player Color { get; }
        public bool HasMoved { get; set; }

        public abstract IPiece Copy();
    }
}
