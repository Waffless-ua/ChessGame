using System;
using System.Threading.Tasks;
using ChessGame.Model;

namespace ChessGame.Services.Interfaces
{
    public interface ILobbyService
    {
        bool IsHost { get; }
        bool IsConnected { get; }
        string HeaderText { get; }

        event Action StateChanged;

        Task InitializeAsync(bool isHost, string ip = null);
        Task StartGameAsync();
    }
}