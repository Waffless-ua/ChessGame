using ChessGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Services.Interfaces
{
    public interface IGameHistoryService
    {
        public GameStateMemento GetCurrentState();
        public void AddSnapshot(GameStateMemento memento);
        public GameStateMemento UndoMove();
        public bool IsThreefoldRepetition();
        public string ExportMatchData();
        public void ImportMatchData(string jsonData);
    }
}
