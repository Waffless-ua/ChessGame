using ChessGame.Model;
using ChessGame.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChessGame.Services.Implementations.Game
{
    public class GameHistoryService : IGameHistoryService
    {
        private readonly List<GameStateMemento> _history = new List<GameStateMemento>();

        public void AddSnapshot(GameStateMemento memento)
        {
            _history.Add(memento);
        }

        public GameStateMemento UndoMove()
        {
            if (_history.Count > 1)
            {
                _history.RemoveAt(_history.Count - 1);
                return _history.Last();
            }
            return null;
        }

        public bool IsThreefoldRepetition()
        {
            if (_history.Count < 8) return false;

            string currentHash = _history.Last().PositionHash;
            int count = _history.Count(m => m.PositionHash == currentHash);

            return count >= 3;
        }

        public string ExportMatchData()
        {
            return JsonSerializer.Serialize(_history);
        }

        public void ImportMatchData(string jsonData)
        {
            _history.Clear();
            var importedHistory = JsonSerializer.Deserialize<List<GameStateMemento>>(jsonData);
            if (importedHistory != null)
            {
                _history.AddRange(importedHistory);
            }
        }

        public GameStateMemento GetCurrentState() => _history.LastOrDefault();
    }
}
