using ChessLibrary.Board;
using ChessLibrary.Enums;

namespace ChessLibrary.Game
{
    public interface IGameState
    {
        Player ThisPlayer { get; }
        Player CurrentPlayer { get; set; }
        IBoard Board { get; }

        void Initialize(Player thisPlayer, IBoard board);
        GameStateMemento SaveState();
        void RestoreState(GameStateMemento memento);
    }
}
