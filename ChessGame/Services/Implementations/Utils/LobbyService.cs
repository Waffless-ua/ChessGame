using ChessGame.Model;
using ChessGame.Model.Data;
using ChessGame.Services.DTO.Messages;
using ChessGame.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ChessGame.Services.Implementations
{
    public class LobbyService : ILobbyService
    {
        private readonly INetworkService _networkService;
        private readonly IGameService _gameService;

        public event Action<bool> IsConnected;

        private const int _port = 55555;

        public LobbyService(INetworkService networkService, IGameService gameService)
        {
            _networkService = networkService;
            _gameService = gameService;
        }

        public async Task<bool> InitializeAsync(LobbyParams lobbyParams)
        {
            bool success;
            bool isHost = lobbyParams.IsHost;
            string ip = lobbyParams.IpAdress;

            if (isHost)
            {
                success = await _networkService.StartServerAsync(_port);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(ip))
                    return false;

                success = await _networkService.ConnectAsync(ip, _port);
            }

            IsConnected?.Invoke(success);

            return success;
        }

        public async Task StartGameAsync()
        {
            await _networkService.SendAsync(
                DtoType.StartGame,
                new DtoStartGame(Player.Black)
            );

            _gameService.InitGame(Player.White);
        }
    }
}