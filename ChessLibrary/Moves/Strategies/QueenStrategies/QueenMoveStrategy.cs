using ChessLibrary.Board;
using ChessLibrary.Pieces;
using ChessLibrary.ValueObjects;

namespace ChessLibrary.Moves.Strategies.QueenStrategies
{
    public class QueenMoveStrategy : MoveStrategyBase<Queen>
    {
        private static readonly Direction[] dirs =
        {
            Direction.North, Direction.South,
            Direction.West, Direction.East,
            Direction.NorthEast, Direction.NorthWest,
            Direction.SouthEast, Direction.SouthWest
        };

        protected override IEnumerable<Move> GetMoves(Position from, IBoard board, Queen queen)
        {
            return GetRayFromDirections(from, board, queen, dirs)
                    .Select(to => new NormalMove(from, to));
        }
    }
}
