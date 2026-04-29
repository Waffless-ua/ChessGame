using ChessGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Services.Interfaces
{
    public interface IChessRulesService
    {
        IEnumerable<Move> GetLegalMoves(IBoard board, Player player, Position pos);
        public bool HasAnyLegalMoves(IBoard board, Player player);
        bool IsMoveLegal(IBoard board, Move move);
        Position GetKingInCheck(IBoard board);
        bool IsInCheck(IBoard board, Player player);
    }
}
