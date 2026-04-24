using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Model
{
    public class GameState : IGameState
    {
        public Player ThisPlayer { get; private set; }
        public Player CurrentPlayer { get; private set; }
        public Board Board { get; private set; }
        public event Action<Board> BoardUpdated;

        public GameState() { }

        public void Initialize(Player player)
        {
            ThisPlayer = player;
            Board = Board.Initial();

            CurrentPlayer = Player.White;
            BoardUpdated?.Invoke(Board);
        }

        public bool IsCurrentPlayer()
        {
            return ThisPlayer == CurrentPlayer;
        }

        public IEnumerable<Move> LegalMovesForPiece(Position pos)
        {
            if (Board.IsEmpty(pos) || Board[pos].Color != CurrentPlayer)
            {
                return Enumerable.Empty<Move>();
            }

            Piece piece = Board[pos];
            return piece.GetMoves(pos, Board);
        }
        public void MakeMove(Move move)
        {
            move.Execute(Board);
            BoardUpdated?.Invoke(this.Board);
            CurrentPlayer = CurrentPlayer.Opponent();
        }

    }
}
