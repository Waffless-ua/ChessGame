using ChessLibrary.Board;
using ChessLibrary.Enums;

using _Board = ChessLibrary.Board.Board;

namespace ChessLibrary.Factories
{
    public class BoardFactory : IBoardFactory
    {
        private readonly IPieceFactory _pieceFactory;

        public BoardFactory(IPieceFactory pieceFactory)
        {
            _pieceFactory = pieceFactory;
        }

        public IBoard CreateInitial()
        {
            IBoard board = new _Board();

            PlaceBackRank(board, 0, Player.Black);
            PlaceBackRank(board, 7, Player.White);

            for (int i = 0; i < 8; i++)
            {
                board[1, i] = _pieceFactory.CreatePiece(PieceType.Pawn, Player.Black);
                board[6, i] = _pieceFactory.CreatePiece(PieceType.Pawn, Player.White);
            }

            return board;
        }

        private void PlaceBackRank(IBoard board, int row, Player player)
        {
            board[row, 0] = _pieceFactory.CreatePiece(PieceType.Rook, player);
            board[row, 1] = _pieceFactory.CreatePiece(PieceType.Knight, player);
            board[row, 2] = _pieceFactory.CreatePiece(PieceType.Bishop, player);
            board[row, 3] = _pieceFactory.CreatePiece(PieceType.Queen, player);
            board[row, 4] = _pieceFactory.CreatePiece(PieceType.King, player);
            board[row, 5] = _pieceFactory.CreatePiece(PieceType.Bishop, player);
            board[row, 6] = _pieceFactory.CreatePiece(PieceType.Knight, player);
            board[row, 7] = _pieceFactory.CreatePiece(PieceType.Rook, player);
        }
    }
}