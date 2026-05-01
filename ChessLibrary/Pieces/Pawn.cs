using ChessLibrary.Enums;

namespace ChessLibrary.Pieces
{
    public class Pawn : Piece
    {
        public override PieceType Type => PieceType.Pawn;
        public override Player Color { get; }

        public Pawn(Player player)
        {
            Color = player;
        }

        public override IPiece Copy()
        {
            return new Pawn(Color) { HasMoved = HasMoved };
        }
    }
}