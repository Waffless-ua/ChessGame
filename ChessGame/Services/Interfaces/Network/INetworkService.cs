using ChessGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Services.Interfaces
{
    public interface INetworkService
    {
        Task StartServerAsync(int port);
        Task ConnectAsync(string ip, int port);

        Task SendAsync(DtoType type, IDtoMessage message);

        bool IsConnected { get; }

        Task DisconnectAsync();
    }
}
