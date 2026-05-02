using ChessLibrary.Enums;

namespace ChessLibrary.Pieces
{
    public class Bishop : Piece
    {
        public override PieceType Type => PieceType.Bishop;
        public override Player Color { get; }

        public Bishop(Player player)
        {
            Color = player;
        }

        public override IPiece Copy()
        {
            return new Bishop(Color) { HasMoved = HasMoved };
        }
    }
}