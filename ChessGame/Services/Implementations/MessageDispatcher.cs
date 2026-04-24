using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGame.Model;
using ChessGame.Model.DTO.Handlers;
using ChessGame.Model.DTO.Messages;
using ChessGame.Services.Interfaces;

namespace ChessGame.Services.Implementations
{
    public class MessageDispatcher : IMessageDispatcher
    {
        private readonly IServiceProvider _provider;

        public MessageDispatcher(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task DispatchAsync(IDtoMessage message)
        {
            var messageType = message.GetType();

            var handlerType = typeof(IMessageHandler<>).MakeGenericType(messageType);

            var handler = _provider.GetService(handlerType);

            if (handler == null)
                throw new Exception($"No handler for {messageType.Name}");

            var method = handlerType.GetMethod("HandleAsync");

            await (Task)method.Invoke(handler, new object[] { message })!;
        }
    }
}
