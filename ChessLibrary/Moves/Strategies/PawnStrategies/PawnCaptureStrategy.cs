using ChessLibrary.Board;
using ChessLibrary.Extensions;
using ChessLibrary.Pieces;
using ChessLibrary.ValueObjects;

namespace ChessLibrary.Moves.Strategies.PawnStrategies
{
    public class PawnCaptureStrategy : MoveStrategyBase<Pawn>
    {
        protected override IEnumerable<Move> GetMoves(Position from, IBoard board, Pawn pawn)
        {
            var player = pawn.Color;

            foreach (var dir in pawn.CaptureDirections())
            {
                Position target = from + dir;

                if (!board.IsInside(target))
                    continue;

                var targetPiece = board[target];

                if (targetPiece != null && targetPiece.Color != pawn.Color)
                {
                    yield return new NormalMove(from, target);
                }
            }
        }
    }
}
