using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Model.Abstractions
{
    public interface ICountingPieces
    {
        int TotalCount { get; }

        public void Increment(Player color, PieceType type);

        public int GetWhitePieces(PieceType type);
        public int GetBlackPieces(PieceType type);
    }
}
