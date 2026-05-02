using ChessApplication.DTO;

namespace ChessApplication.Interfaces.Network
{
    public interface INetworkService : IDisposable
    {
        Task<bool> StartServerAsync(int port);
        Task<bool> ConnectAsync(string ip, int port);

        Task SendAsync(DtoType type, IDtoMessage message);
        Task DisconnectAsync();

        event Action OnDisconnected;
    }
}