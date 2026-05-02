using ChessLibrary.Board;
using ChessLibrary.Enums;
using ChessLibrary.ValueObjects;

namespace ChessLibrary.Moves.PawnMoves
{
    public class EnPassant : Move
    {
        public override MoveType Type => MoveType.EnPassant;

        public override Position FromPos { get; }
        public override Position ToPos { get; }

        private readonly Position _capturePosition;
        public EnPassant(Position fromPos, Position toPos)
        {
            FromPos = fromPos;
            ToPos = toPos;
            _capturePosition = new Position(fromPos.Row, toPos.Column);
        }

        public override void Execute(IBoard board)
        {
            new NormalMove(FromPos, ToPos).Execute(board);
            board[_capturePosition] = null;
        }
    }
}
