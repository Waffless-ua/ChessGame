using ChessLibrary.Enums;

namespace ChessLibrary.Board
{
    public class CountingPieces : ICountingPieces
    {
        private readonly Dictionary<PieceType, int> whiteCount = new Dictionary<PieceType, int>();
        private readonly Dictionary<PieceType, int> blackCount = new Dictionary<PieceType, int>();

        public int TotalCount { get; private set; }

        public CountingPieces()
        {
            TotalCount = 0;

            foreach (PieceType type in Enum.GetValues(typeof(PieceType)))
            {
                whiteCount[type] = 0;
                blackCount[type] = 0;
            }
        }
        public void Increment(Player color, PieceType type)
        {
            if (color == Player.White)
            {
                whiteCount[type]++;
            }
            else if (color == Player.Black)
            {
                blackCount[type]++;
            }

            TotalCount++;
        }
        public int GetBlackPieces(PieceType type)
        {
            return blackCount[type];
        }

        public int GetWhitePieces(PieceType type)
        {
            return whiteCount[type];
        }

    }
}
