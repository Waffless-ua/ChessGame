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
        public Board Board { get; }
        public event Action<Board> BoardUpdated;
        public void Initialize(Player player);
        public bool IsCurrentPlayer();
        public IEnumerable<Move> LegalMovesForPiece(Position pos);
        public void MakeMove(Move move);
    }
}
