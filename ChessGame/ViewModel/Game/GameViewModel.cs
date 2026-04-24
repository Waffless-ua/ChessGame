using ChessGame.Commands;
using ChessGame.Model;
using ChessGame.Model.Moves;
using ChessGame.Services.Implementations;
using ChessGame.Services.Interfaces;
using ChessGame.ViewModel.Game;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChessGame.ViewModel
{
    public class GameViewModel : BaseViewModel
    {
        protected readonly Dictionary<Position, Move> moveCache = new Dictionary<Position, Move>();
        public CellsViewModel CellsViewModel { get; set; }
        public HighlightsViewModel HighlightsViewModel { get; set; }
        private Position SelectedPos { get; set; }
        private double _boardRotation;
        public double BoardRotation
        {
            get => _boardRotation;
            set
            {
                _boardRotation = value;
                NotifyPropertyChanged();
            }
        }
        private IGameState GameState { get; }

        private INetworkService _networkService;

        public ICommand CellClickCommand { get; }
        public GameViewModel(INetworkService networkService, IGameState gameState)
        {
            GameState = gameState;
            _networkService = networkService;
            IsFlipped(GameState.ThisPlayer);
            InitializeBoard();

            CellClickCommand = new RelayCommand(OnCellClick);

            GameState.BoardUpdated += OnBoardUpdated;

            OnBoardUpdated(GameState.Board);
        }
        private void OnBoardUpdated(Board board)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                SelectedPos = null;
                HighlightsViewModel.HideHighlights();
                CellsViewModel.DrawBoard(board);
            });
        }

        private void IsFlipped(Player player)
        {
            System.Diagnostics.Debug.WriteLine($"Перевірка кольору для повороту: {player}");

            if (player == Player.Black)
            {
                BoardRotation = 180;
            }
            else
            {
                BoardRotation = 0;
            }
        }

        private void InitializeBoard()
        {
            CellsViewModel = new CellsViewModel(moveCache);
            HighlightsViewModel = new HighlightsViewModel(moveCache);
            CellsViewModel.InitializeCells();
            HighlightsViewModel.InitializeHighlights();

            IsFlipped(GameState.ThisPlayer);
        }
        private void CacheMoves(IEnumerable<Move> moves)
        {
            moveCache.Clear();

            foreach (Move move in moves)
            {
                moveCache[move.ToPos] = move;
            }
        }
        private void OnCellClick(object obj)
        {
            if (obj is not Position pos)
            {
                return;
            }

            if (!GameState.IsCurrentPlayer())
            {
                return;
            }

            if (SelectedPos == null)
            {
                OnFromPositionSelected(pos);
            }
            else
            {
                OnToPositionSelected(pos);
            }
        }

        private void OnFromPositionSelected(Position pos)
        {
            IEnumerable<Move> moves = GameState.LegalMovesForPiece(pos);

            if (moves.Any())
            {
                SelectedPos = pos;
                CacheMoves(moves);
                HighlightsViewModel.ShowHighlights();
            }
        }
        private void OnToPositionSelected(Position pos)
        {
            SelectedPos = null;
            HighlightsViewModel.HideHighlights();

            if(moveCache.TryGetValue(pos, out Move move))
            {
                HandleMove(move);
            }
        }

        private void HandleMove(Move move)
        {
            GameState.MakeMove(move);
            _networkService.SendAsync(DtoType.Move, new DtoMove(move));
        }
    }
}
