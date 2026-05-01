using ChessLibrary.Enums;
using ChessLibrary.Pieces;
using ChessLibrary.ValueObjects;

namespace ChessLibrary.Extensions
{
    public static class PieceExtensions
    {
        private static readonly Direction[] WhiteCaptures =
{
            Direction.NorthEast, Direction.NorthWest
        };

        private static readonly Direction[] BlackCaptures =
        {
            Direction.SouthEast, Direction.SouthWest
        };

        public static Direction Forward(this Player player)
        {
            return player == Player.White
                ? Direction.North
                : Direction.South;
        }

        public static IReadOnlyList<Direction> CaptureDirections(this Pawn pawn)
        {
            return pawn.Color == Player.White
                ? WhiteCaptures
                : BlackCaptures;
        }
    }
}
