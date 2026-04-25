using ChessGame.Model;
using ChessGame.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Services.Interfaces
{
    public interface IDtoResolver
    {
        IDtoMessage Deserialize(NetworkMessage message);
    }
}
