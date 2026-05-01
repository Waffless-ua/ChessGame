using ChessLibrary.Pieces;
using ChessLibrary.ValueObjects;

namespace ChessLibrary.Board
{
    public class StandardPieceCounter : IPieceCounterStrategy
    {
        public ICountingPieces Count(Board board)
        {
            var counter = new CountingPieces();

            foreach (Position pos in board.PiecePositions())
            {
                IPiece piece = board[pos];
                counter.Increment(piece.Color, piece.Type);
            }

            return counter;
        }
    }
}
