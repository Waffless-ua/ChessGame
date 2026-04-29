using System.Collections.Generic;
using System.Linq;
using ChessGame.Model;
using ChessGame.Model.Moves;
using ChessGame.Services.Interfaces;

namespace ChessGame.Services
{
    public class ChessRulesService : IChessRulesService
    {
        public IEnumerable<Move> GetLegalMoves(Board board, Player player, Position pos)
        {
            if (board.IsEmpty(pos)) return Enumerable.Empty<Move>();

            var piece = board[pos];
            if (piece.Color != player) return Enumerable.Empty<Move>();

            var candidates = piece.GetMoves(pos, board);
            return candidates.Where(m => IsMoveLegal(board, m));
        }
        public bool HasAnyLegalMoves(Board board, Player player)
        {
            foreach (var pos in board.PiecePositionsFor(player))
            {
                if (GetLegalMoves(board, player, pos).Any())
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsMoveLegal(Board board, Move move)
        {
            Board copy = board.Copy();

            var piece = copy[move.FromPos];

            move.Execute(copy);

            bool isCheck = IsInCheck(copy, piece.Color);

            return !isCheck;
        }

        public bool IsInCheck(Board board, Player player)
        {
            var kingPos = board.FindKing(player);
            if (kingPos == null) return false;

            var opponent = player.Opponent();

            foreach (var pos in board.PiecePositionsFor(opponent))
            {
                var piece = board[pos];
                var attacks = piece.GetMoves(pos, board);

                if (attacks.Any(m => m.ToPos == kingPos))
                    return true;
            }

            return false;
        }

        public Position GetKingInCheck(Board board)
        {
            if (IsInCheck(board, Player.White)) return board.FindKing(Player.White);
            if (IsInCheck(board, Player.Black)) return board.FindKing(Player.Black);
            return null;
        }
    }
}