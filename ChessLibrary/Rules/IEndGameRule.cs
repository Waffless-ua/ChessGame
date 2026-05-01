using ChessLibrary.Board;
using ChessLibrary.Enums;
using ChessLibrary.Game;

namespace ChessLibrary.Rules
{
    public interface IEndGameRule
    {
        GameResult? Check(IBoard board, Player nextPlayer, IEnumerable<GameStateMemento> history);
    }
}
