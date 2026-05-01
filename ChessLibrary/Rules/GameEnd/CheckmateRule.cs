using ChessLibrary.Board;
using ChessLibrary.Enums;
using ChessLibrary.Extensions;
using ChessLibrary.Game;
using ChessLibrary.Rules.Validation;

namespace ChessLibrary.Rules.GameEnd
{
    public class CheckmateRule : IEndGameRule
    {
        private readonly IChessRulesEvaluator _rules;
        public CheckmateRule(IChessRulesEvaluator rules)
        {
            _rules = rules;
        }

        public GameResult? Check(IBoard board, Player nextPlayer, IEnumerable<GameStateMemento> history)
        {
            if (IsCheckmate(board, nextPlayer))
            {
                return new GameResult(nextPlayer.Opponent(), EndGameTypes.Checkmate);
            }

            return null;
        }

        private bool IsCheckmate(IBoard board, Player player)
        {
            if (!_rules.IsInCheck(board, player)) return false;

            return !_rules.HasAnyLegalMoves(board, player);
        }
    }
}
