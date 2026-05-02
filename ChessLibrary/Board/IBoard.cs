using ChessLibrary.Enums;
using ChessLibrary.Pieces;
using ChessLibrary.ValueObjects;

namespace ChessLibrary.Board
{
    public interface IBoard
    {
        IPiece this[int row, int col] { get; set; }
        IPiece this[Position pos] { get; set; }

        bool IsEmpty(Position pos);
        bool IsInside(Position pos);
        Position GetPawnSkipPosition(Player player);
        void SetPawnSkipPosition(Player player, Position pos);
        IEnumerable<Position> PiecePositions();
        IEnumerable<Position> PiecePositionsFor(Player player);
        Position FindKing(Player player);
        IBoard Copy();
        string GeneratePositionHash();
        ICountingPieces CountPieces();
    }
}