using ChessLibrary.Board;
using ChessLibrary.Extensions;
using ChessLibrary.Moves.PawnMoves;
using ChessLibrary.Pieces;
using ChessLibrary.ValueObjects;

namespace ChessLibrary.Moves.Strategies.PawnStrategies
{
    public class PawnEnPassantStrategy : MoveStrategyBase<Pawn>
    {
        protected override IEnumerable<Move> GetMoves(Position from, IBoard board, Pawn pawn)
        {
            var player = pawn.Color;
            var opponent = player.Opponent();
            var forward = player.Forward();

            foreach (Direction dir in new Direction[] { Direction.West, Direction.East })
            {
                Position to = from + forward + dir;

                if (to == board.GetPawnSkipPosition(opponent))
                {
                    yield return new EnPassant(from, to);
                }
            }
        }
    }
}
