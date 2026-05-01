using ChessLibrary.Board;
using ChessLibrary.Enums;
using ChessLibrary.Game;
using ChessLibrary.Rules.Validation;

namespace ChessLibrary.Rules.GameEnd
{
    public class StalemateRule : IEndGameRule
    {
        private readonly IChessRulesEvaluator _rules;

        public StalemateRule(IChessRulesEvaluator rules)
        {
            _rules = rules;
        }

        public GameResult? Check(IBoard board, Player nextPlayer, IEnumerable<GameStateMemento> history)
        {
            if (IsStalemate(board, nextPlayer))
            {
                return new GameResult(Player.None, EndGameTypes.Stalemate);
            }

            return null;
        }

        private bool IsStalemate(IBoard board, Player player)
        {
            if (_rules.IsInCheck(board, player)) return false;
            return !_rules.HasAnyLegalMoves(board, player);
        }
    }
}
