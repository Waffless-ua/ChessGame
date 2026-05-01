using ChessLibrary.Board;
using ChessLibrary.Enums;
using ChessLibrary.Extensions;
using ChessLibrary.Moves;
using ChessLibrary.Moves.Factories;
using ChessLibrary.Moves.Strategies;
using ChessLibrary.Pieces;
using ChessLibrary.ValueObjects;

namespace ChessLibrary.Rules.Validation
{
    public class ChessValidator : IChessRulesEvaluator
    {
        private readonly IMoveFactory _moveFactory;
        private readonly IMoveService _moveService;

        private static readonly PieceType[] _promotionTypes =
        {
            PieceType.Queen, PieceType.Rook, PieceType.Bishop, PieceType.Knight
        };

        public ChessValidator(IMoveFactory moveFactory, IMoveService moveService)
        {
            _moveFactory = moveFactory;
            _moveService = moveService;
        }

        public IEnumerable<Move> GetLegalMoves(IBoard board, Player player, Position pos)
        {
            if (board.IsEmpty(pos))
                return Enumerable.Empty<Move>();

            var piece = board[pos];

            if (piece.Color != player)
                return Enumerable.Empty<Move>();

            var candidates = _moveService.GetMoves(piece, pos, board);

            var expandedMoves = new List<Move>();

            foreach (var move in candidates)
            {
                if (piece.Type == PieceType.Pawn && IsLastRank(move.ToPos, piece.Color))
                {
                    expandedMoves.AddRange(
                        _moveFactory.CreatePromotionMoves(move.FromPos, move.ToPos, _promotionTypes));
                }
                else
                {
                    expandedMoves.Add(move);
                }
            }

            return expandedMoves.Where(m => IsMoveLegal(board, m)).ToList();
        }

        private bool IsLastRank(Position pos, Player color)
        {
            return color == Player.White ? pos.Row == 0 : pos.Row == 7;
        }

        public bool HasAnyLegalMoves(IBoard board, Player player)
        {
            foreach (var pos in board.PiecePositionsFor(player))
            {
                if (GetLegalMoves(board, player, pos).Any())
                    return true;
            }

            return false;
        }

        public bool IsMoveLegal(IBoard board, Move move)
        {
            var copy = board.Copy();

            var piece = copy[move.FromPos];
            if (piece == null)
                return false;

            copy.SetPawnSkipPosition(piece.Color, null);

            move.Execute(copy);

            if (move.Type == MoveType.DoublePawn && piece is Pawn pawn)
            {
                var skipped = GetSkippedPosition(move.FromPos, move.ToPos);
                copy.SetPawnSkipPosition(pawn.Color, skipped);
            }

            return !IsInCheck(copy, piece.Color);
        }

        private Position GetSkippedPosition(Position from, Position to)
        {
            return new Position((from.Row + to.Row) / 2, from.Column);
        }

        public bool IsInCheck(IBoard board, Player player)
        {
            var kingPos = board.FindKing(player);
            if (kingPos == null)
                return false;

            var opponent = player.Opponent();

            foreach (var pos in board.PiecePositionsFor(opponent))
            {
                var piece = board[pos];

                if (piece.Type == PieceType.Pawn)
                {
                    foreach (var attack in GetPawnAttacks(pos, piece.Color))
                    {
                        if (attack == kingPos)
                            return true;
                    }
                }
                else
                {
                    var attacks = _moveService.GetMoves(piece, pos, board);

                    if (attacks.Any(m => m.ToPos == kingPos))
                        return true;
                }
            }

            return false;
        }

        private IEnumerable<Position> GetPawnAttacks(Position from, Player color)
        {
            int dir = color == Player.White ? -1 : 1;

            yield return new Position(from.Row + dir, from.Column - 1);
            yield return new Position(from.Row + dir, from.Column + 1);
        }

        public Position GetKingInCheck(IBoard board)
        {
            if (IsInCheck(board, Player.White))
                return board.FindKing(Player.White);

            if (IsInCheck(board, Player.Black))
                return board.FindKing(Player.Black);

            return null;
        }
    }
}