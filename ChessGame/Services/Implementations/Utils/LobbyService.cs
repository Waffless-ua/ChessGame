using ChessGame.Model;
using ChessGame.Model.DTO.Messages;
using ChessGame.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Services.Implementations
{
    public class LobbyService : ILobbyService
    {
        private readonly INetworkService _networkService;
        private readonly IGameService _gameService;

        public bool IsHost { get; private set; }
        public bool IsConnected { get; private set; }
        public string HeaderText { get; private set; }

        public event Action StateChanged;

        public LobbyService(INetworkService networkService, IGameService gameService)
        {
            _networkService = networkService;
            _gameService = gameService;
        }

        public async Task InitializeAsync(bool isHost, string ip = null)
        {
            IsHost = isHost;
            IsConnected = false;
            UpdateHeader();

            if (isHost)
            {
                await _networkService.StartServerAsync(55555);
                IsConnected = true;
            }
            else
            {
                await _networkService.ConnectAsync(ip, 55555);
                IsConnected = true;
            }

            UpdateHeader();
            StateChanged?.Invoke();
        }

        public async Task StartGameAsync()
        {
            if (!IsHost) return;

            await _networkService.SendAsync(
                DtoType.StartGame,
                new DtoStartGame(Player.Black)
            );

            _gameService.StartGame(Player.White);
        }

        private void UpdateHeader()
        {
            HeaderText = IsHost
                ? (IsConnected ? "Суперник приєднався" : "Очікування суперника")
                : "Очікування початку гри";
        }
    }
}
