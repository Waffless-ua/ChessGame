using ChessGame.Model;
using ChessGame.Model.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Services.Interfaces
{
    public interface IDtoMoveFactory
    {
        public Move GetMoveFromDTO(DtoMove dtoMove);
        public DtoMove GetMoveToDTO(Move Move);
    }
}
