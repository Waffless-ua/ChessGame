using ChessLibrary.Board;
using ChessLibrary.Pieces;
using ChessLibrary.ValueObjects;

namespace ChessLibrary.Moves.Strategies
{
    public abstract class MoveStrategyBase<TPiece> : IMoveStrategy where TPiece : IPiece
    {
        public bool CanHandle(IPiece piece) => piece is TPiece;

        public IEnumerable<Move> GetMoves(Position from, IBoard board, IPiece piece)
        {
            return GetMoves(from, board, (TPiece)piece);
        }

        protected abstract IEnumerable<Move> GetMoves(Position from, IBoard board, TPiece piece);


        protected virtual IEnumerable<Position> GetRayFromDirections(Position from, IBoard board, Piece piece, Direction[] dirs)
        {
            foreach (var dir in dirs)
            {
                for (var to = from + dir; board.IsInside(to); to += dir)
                {
                    if (board.IsEmpty(to))
                    {
                        yield return to;
                        continue;
                    }

                    if (board[to].Color != piece.Color)
                    {
                        yield return to;
                    }

                    break;
                }
            }
        }
    }
}
