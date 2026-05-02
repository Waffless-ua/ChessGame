using ChessLibrary.Enums;

namespace ChessLibrary.Game
{
    public record GameResult
    {
        public Player Winner { get; init; }
        public EndGameTypes Type { get; init; }

        public GameResult(Player winner, EndGameTypes type)
        {
            Winner = winner;
            Type = type;
        }
    }
}
