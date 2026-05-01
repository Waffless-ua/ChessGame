using ChessLibrary.Board;
using ChessLibrary.Enums;
using ChessLibrary.Game;

namespace ChessLibrary.Rules
{
    public class EndGameRulePipeline : IEndGameRulePipeline
    {
        private readonly IEnumerable<IEndGameRule> _rules;

        public EndGameRulePipeline(IEnumerable<IEndGameRule> rules)
        {
            _rules = rules;
        }

        public GameResult Evaluate(IBoard board, Player nextPlayer, IEnumerable<GameStateMemento> history)
        {
            foreach (var rule in _rules)
            {
                var result = rule.Check(board, nextPlayer, history);

                if (result != null)
                    return result;
            }

            return null;
        }
    }
}
