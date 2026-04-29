using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Model
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
