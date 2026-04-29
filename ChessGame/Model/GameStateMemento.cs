using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Model
{
    public record GameStateMemento
    {
        public string PositionHash { get; init; }
        public IBoard SavedBoard { get; init; }
        public Player CurrentPlayer { get; init; }
    }
}
