using ChessGame.Model;
using ChessGame.Model.Moves;
using ChessGame.Services.Interfaces;

namespace ChessGame.Services
{
    public class GameService : IGameService
    {
        private readonly IGameState _state;
        private readonly IBoardFactory _boardFactory;
        private readonly INetworkService _networkService;
        public Player ThisPlayer => _state.ThisPlayer;

        public event Action BoardChanged;
        public event Action PlayerChanged;

        public GameService(IGameState state, IBoardFactory boardFactory, INetworkService networkService)
        {
            _state = state;
            _boardFactory = boardFactory;
            _networkService = networkService;
        }

        public void StartGame(Player player)
        {
            var board = _boardFactory.CreateInitial();
            _state.Initialize(player, board);

            BoardChanged?.Invoke();
            PlayerChanged?.Invoke();
        }

        public bool IsCurrentPlayer()
        {
            return _state.ThisPlayer == _state.CurrentPlayer;
        }
        public Board GetBoard()
        {
            return _state.Board;
        }

        public Position GetKingInCheck()
        {
            var board = _state.Board;

            if (board.IsInCheck(Player.White))
            {
                return board.FindKing(Player.White);
            }

            if (board.IsInCheck(Player.Black))
            {
                return board.FindKing(Player.Black);
            }

            return null;
        }

        public IEnumerable<Move> GetLegalMoves(Position pos)
        {
            if (_state.Board.IsEmpty(pos))
                return Enumerable.Empty<Move>();

            var piece = _state.Board[pos];

            if (piece.Color != _state.CurrentPlayer)
                return Enumerable.Empty<Move>();

            IEnumerable<Move> moveCandidates = piece.GetMoves(pos, _state.Board);

            return moveCandidates.Where(move => move.IsLegal(_state.Board));
        }

        public void MakeMove(Move move, bool sendToOpponent = false)
        {
            if (sendToOpponent && !IsCurrentPlayer())
                return;

            move.Execute(_state.Board);

            if (sendToOpponent)
            {
                _networkService.SendAsync(DtoType.Move, new DtoMove(move));
            }

            _state.CurrentPlayer = _state.CurrentPlayer.Opponent();

            BoardChanged?.Invoke();
            PlayerChanged?.Invoke();
        }
    }
}