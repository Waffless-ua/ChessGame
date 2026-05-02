namespace ChessLibrary.Board
{
    public interface IPieceCounterStrategy
    {
        ICountingPieces Count(Board board);
    }
}
