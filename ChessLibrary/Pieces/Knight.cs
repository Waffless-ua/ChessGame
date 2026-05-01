using ChessLibrary.Enums;

namespace ChessLibrary.Pieces
{
    public class Knight : Piece
    {
        public override PieceType Type => PieceType.Knight;
        public override Player Color { get; }

        public Knight(Player player)
        {
            Color = player;
        }

        public override IPiece Copy()
        {
            return new Knight(Color) { HasMoved = HasMoved };
        }
    }
}