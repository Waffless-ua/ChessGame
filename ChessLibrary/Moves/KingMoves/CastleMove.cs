using ChessLibrary.Board;
using ChessLibrary.Enums;
using ChessLibrary.ValueObjects;

namespace ChessLibrary.Moves.KingMoves
{
    public class CastleMove : Move
    {
        public override MoveType Type { get; }

        public override Position FromPos { get; }

        public override Position ToPos { get; }

        private readonly Direction kingMoveDir;
        private readonly Position rookFromPos;
        private readonly Position rookToPos;

        public CastleMove(MoveType type, Position kingPos)
        {
            Type = Type;
            FromPos = kingPos;

            if (type == MoveType.CastleKS)
            {
                kingMoveDir = Direction.East;
                ToPos = new Position(kingPos.Row, 6);
                rookFromPos = new Position(kingPos.Row, 7);
                rookToPos = new Position(kingPos.Row, 5);

            }
            else if (type == MoveType.CastleQS)
            {
                kingMoveDir = Direction.West;
                ToPos = new Position(kingPos.Row, 2);
                rookFromPos = new Position(kingPos.Row, 0);
                rookToPos = new Position(kingPos.Row, 3);

            }
        }

        public override void Execute(IBoard board)
        {
            new NormalMove(FromPos, ToPos).Execute(board);
            new NormalMove(rookFromPos, rookToPos).Execute(board);
        }
    }
}
