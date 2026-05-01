using ChessLibrary.Board;
using ChessLibrary.Extensions;
using ChessLibrary.Pieces;
using ChessLibrary.ValueObjects;

namespace ChessLibrary.Moves.Strategies.PawnStrategies
{
    public class PawnForwardStrategy : MoveStrategyBase<Pawn>
    {
        protected override IEnumerable<Move> GetMoves(Position from, IBoard board, Pawn pawn)
        {
            var moves = new List<Move>();
            var player = pawn.Color;
            var forward = player.Forward();

            var oneStep = from + forward;

            if (board.IsEmpty(oneStep))
            {
                moves.Add(new NormalMove(from, oneStep));
            }

            return moves;
        }
    }
}
