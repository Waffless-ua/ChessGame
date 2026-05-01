using ChessLibrary.Board;
using ChessLibrary.Pieces;
using ChessLibrary.ValueObjects;

namespace ChessLibrary.Moves.Strategies.BishopStrategies
{
    public class BishopMoveStrategy : MoveStrategyBase<Bishop>
    {
        private static readonly Direction[] dirs =
        {
            Direction.NorthEast,
            Direction.NorthWest,
            Direction.SouthEast,
            Direction.SouthWest
        };

        protected override IEnumerable<Move> GetMoves(Position from, IBoard board, Bishop piece)
        {
            return GetRayFromDirections(from, board, piece, dirs)
                    .Select(to => new NormalMove(from, to));
        }
    }
}
