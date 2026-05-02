using ChessLibrary.Board;
using ChessLibrary.Pieces;
using ChessLibrary.ValueObjects;

namespace ChessLibrary.Moves.Strategies
{
    public interface IMoveStrategy
    {
        bool CanHandle(IPiece piece);
        IEnumerable<Move> GetMoves(Position from, IBoard board, IPiece piece);
    }
}
