using ChessLibrary.Enums;

namespace ChessLibrary.Board
{
    public interface ICountingPieces
    {
        int TotalCount { get; }

        public void Increment(Player color, PieceType type);

        public int GetWhitePieces(PieceType type);
        public int GetBlackPieces(PieceType type);
    }
}
