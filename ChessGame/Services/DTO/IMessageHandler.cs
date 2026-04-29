using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Services.Abstractions
{
    public interface IMessageHandler<T> where T : IDtoMessage
    {
        Task HandleAsync(T message);
    }
}
