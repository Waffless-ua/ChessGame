using ChessLibrary.Board;
using ChessLibrary.Extensions;
using ChessLibrary.Moves.PawnMoves;
using ChessLibrary.Pieces;
using ChessLibrary.ValueObjects;

namespace ChessLibrary.Moves.Strategies.PawnStrategies
{
    public class PawnDoubleStrategy : MoveStrategyBase<Pawn>
    {
        protected override IEnumerable<Move> GetMoves(Position from, IBoard board, Pawn pawn)
        {
            var player = pawn.Color;
            var forward = player.Forward();

            Position twoStep = from + forward + forward;

            if (!board.IsInside(twoStep) || !board.IsEmpty(twoStep))
                yield break;

            if (!pawn.HasMoved)
                yield return new DoublePawn(from, twoStep);
        }
    }
}
