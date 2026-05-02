using ChessLibrary.Board;
using ChessLibrary.Pieces;
using ChessLibrary.ValueObjects;

namespace ChessLibrary.Moves.Strategies.RookStrategies
{
    public class RookMoveStrategy : MoveStrategyBase<Rook>
    {
        private static readonly Direction[] dirs =
        {
            Direction.North, Direction.South,
            Direction.West, Direction.East
        };

        protected override IEnumerable<Move> GetMoves(Position from, IBoard board, Rook rook)
        {
            return GetRayFromDirections(from, board, rook, dirs)
                .Select(to => new NormalMove(from, to));
        }
    }
}
