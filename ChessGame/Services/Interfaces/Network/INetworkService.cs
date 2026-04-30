using ChessGame.Model;

namespace ChessGame.Services.Interfaces
{
    public interface INetworkService
    {
        Task<bool> StartServerAsync(int port);
        Task<bool> ConnectAsync(string ip, int port);

        Task SendAsync(DtoType type, IDtoMessage message);
        Task DisconnectAsync();
    }
}