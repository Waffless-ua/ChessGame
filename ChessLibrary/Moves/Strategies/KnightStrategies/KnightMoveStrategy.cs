using ChessLibrary.Board;
using ChessLibrary.Pieces;
using ChessLibrary.ValueObjects;

namespace ChessLibrary.Moves.Strategies.KnightStrategies
{
    public class KnightMoveStrategy : MoveStrategyBase<Knight>
    {
        protected override IEnumerable<Move> GetMoves(Position from, IBoard board, Knight knight)
        {
            foreach (var pos in PotentialPositions(from))
            {
                if (!board.IsInside(pos))
                    continue;

                if (board.IsEmpty(pos) || board[pos].Color != knight.Color)
                    yield return new NormalMove(from, pos);
            }
        }

        private IEnumerable<Position> PotentialPositions(Position from)
        {
            foreach (Direction vDir in new[] { Direction.North, Direction.South })
            {
                foreach (Direction hDir in new[] { Direction.West, Direction.East })
                {
                    yield return from + 2 * vDir + hDir;
                    yield return from + 2 * hDir + vDir;
                }
            }
        }
    }
}
