using ChessLibrary.Enums;

namespace ChessLibrary.Pieces
{
    public class King : Piece
    {
        public override PieceType Type => PieceType.King;
        public override Player Color { get; }

        public King(Player player)
        {
            Color = player;
        }

        public override IPiece Copy()
        {
            return new King(Color) { HasMoved = HasMoved };
        }
    }
}