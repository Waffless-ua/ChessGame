using ChessLibrary.Board;
using ChessLibrary.Enums;

namespace ChessLibrary.Game
{
    public record GameStateMemento
    {
        public string PositionHash { get; init; }
        public IBoard SavedBoard { get; init; }
        public Player CurrentPlayer { get; init; }
    }
}
