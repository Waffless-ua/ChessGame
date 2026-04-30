using ChessGame.Model;
using ChessGame.Model.Data;
using System;
using System.Threading.Tasks;

namespace ChessGame.Services.Interfaces
{
    public interface ILobbyService
    {
        event Action<bool> IsConnected;
        Task<bool> InitializeAsync(LobbyParams lobbyParams);
        Task StartGameAsync();
    }
}