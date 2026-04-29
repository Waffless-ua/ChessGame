using ChessGame.Model.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Model.Abstractions
{
    public interface IPieceCounterStrategy
    {
        ICountingPieces Count(Board board);
    }
}
