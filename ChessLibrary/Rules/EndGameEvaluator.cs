using ChessLibrary.Board;
using ChessLibrary.Enums;
using ChessLibrary.Game;
using ChessLibrary.Rules.GameEnd;
using ChessLibrary.Rules.Validation;

namespace ChessLibrary.Rules
{
    public class EndGameEvaluator : IEndGameRulePipeline
    {
        private readonly IEnumerable<IEndGameRule> _rules;

        public EndGameEvaluator(IEnumerable<IEndGameRule> rules)
        {
            _rules = rules;
        }

        public GameResult? Evaluate(IBoard board, Player player, IEnumerable<GameStateMemento> history)
        {
            foreach (var rule in _rules)
            {
                var result = rule.Check(board, player, history);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }
    }
}
