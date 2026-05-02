using ChessLibrary.Board;

namespace ChessLibrary.Factories
{
    public interface IBoardFactory
    {
        IBoard CreateInitial();
    }
}
