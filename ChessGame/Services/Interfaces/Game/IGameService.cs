using ChessGame.Model;

namespace ChessGame.Services.Interfaces
{
    public interface IGameService
    {
        Player ThisPlayer { get; }

        event Action BoardChanged;
        event Action PlayerChanged;

        void StartGame(Player player);
        void MakeMove(Move move, bool sendToOpponent = false);
        IEnumerable<Move> GetLegalMoves(Position pos);
        bool IsCurrentPlayer();
        Board GetBoard();
    }
}