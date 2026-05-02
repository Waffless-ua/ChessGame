using ChessLibrary.Board;
using ChessLibrary.Enums;

namespace ChessLibrary.Game
{
    public class GameState : IGameState
    {
        public Player ThisPlayer { get; private set; }
        public Player CurrentPlayer { get; set; }
        public IBoard Board { get; private set; }

        public void Initialize(Player player, IBoard board)
        {
            ThisPlayer = player;
            CurrentPlayer = Player.White;
            Board = board;
        }

        public bool IsInitialized => Board != null;

        public GameStateMemento SaveState()
        {
            return new GameStateMemento
            {
                PositionHash = Board.GeneratePositionHash(),
                SavedBoard = Board.Copy(),
                CurrentPlayer = CurrentPlayer
            };
        }
        public void RestoreState(GameStateMemento memento)
        {
            Board = memento.SavedBoard.Copy();
            CurrentPlayer = memento.CurrentPlayer;
        }
    }
}