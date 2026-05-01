using ChessLibrary.Enums;

namespace ChessLibrary.Pieces
{
    public class Rook : Piece
    {
        public override PieceType Type => PieceType.Rook;
        public override Player Color { get; }

        public Rook(Player player)
        {
            Color = player;
        }

        public override IPiece Copy()
        {
            return new Rook(Color) { HasMoved = HasMoved };
        }
    }
}