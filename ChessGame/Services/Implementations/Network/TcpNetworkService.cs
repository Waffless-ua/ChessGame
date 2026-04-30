using ChessGame.Model;
using ChessGame.Model.Moves;
using ChessGame.Services.DTO;
using ChessGame.Services.Interfaces;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Windows;

namespace ChessGame.Services.Implementations
{
    public class TcpNetworkService : INetworkService
    {
        private readonly IDtoResolver _resolver;
        private readonly IMessageDispatcher _dispatcher;

        private TcpListener _listener;
        private TcpClient _client;

        private StreamReader _reader;
        private StreamWriter _writer;

        private CancellationTokenSource _cts;
        private Task _listenTask;

        public TcpNetworkService(IDtoResolver resolver, IMessageDispatcher dispatcher)
        {
            _resolver = resolver;
            _dispatcher = dispatcher;
        }


        public async Task<bool> StartServerAsync(int port)
        {
            try
            {
                _listener?.Stop();

                _listener = new TcpListener(IPAddress.Any, port);
                _listener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

                _listener.Start();

                _client = await _listener.AcceptTcpClientAsync();

                InitStreams();

                if (!await HandshakeAsync(isServer: true))
                    return false;

                StartListening();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> ConnectAsync(string ip, int port)
        {
            try
            {
                _client = new TcpClient();

                var connectTask = _client.ConnectAsync(ip, port);
                var timeoutTask = Task.Delay(5000);

                var completed = await Task.WhenAny(connectTask, timeoutTask);

                if (completed == timeoutTask)
                    return false;

                InitStreams();

                if (!await HandshakeAsync(isServer: false))
                    return false;

                StartListening();
                return true;
            }
            catch
            {
                return false;
            }
        }


        private async Task<bool> HandshakeAsync(bool isServer)
        {
            try
            {
                if (isServer)
                {
                    await SendRawAsync("HELLO_SERVER");
                    var response = await _reader.ReadLineAsync();

                    return response == "HELLO_CLIENT";
                }
                else
                {
                    var request = await _reader.ReadLineAsync();

                    if (request != "HELLO_SERVER")
                        return false;

                    await SendRawAsync("HELLO_CLIENT");
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }


        private void InitStreams()
        {
            var stream = _client.GetStream();
            _reader = new StreamReader(stream);
            _writer = new StreamWriter(stream) { AutoFlush = true };
        }

        private void StartListening()
        {
            _cts = new CancellationTokenSource();

            _listenTask = Task.Run(async () =>
            {
                try
                {
                    while (!_cts.IsCancellationRequested)
                    {
                        var raw = await _reader.ReadLineAsync();
                        if (raw == null) break;

                        var msg = JsonSerializer.Deserialize<NetworkMessage>(raw);
                        if (msg == null) continue;

                        var dto = _resolver.Deserialize(msg);
                        await _dispatcher.DispatchAsync(dto);
                    }
                }
                catch { }
            });
        }


        public async Task SendAsync(DtoType type, IDtoMessage message)
        {
            if (_writer == null) return;

            var payload = JsonSerializer.Serialize(message, message.GetType());

            var envelope = new NetworkMessage
            {
                DtoType = type,
                Payload = payload
            };

            var json = JsonSerializer.Serialize(envelope);

            await _writer.WriteLineAsync(json);
        }

        private async Task SendRawAsync(string msg)
        {
            await _writer.WriteLineAsync(msg);
        }


        public async Task DisconnectAsync()
        {
            try
            {
                _cts?.Cancel();

                _reader?.Dispose();
                _writer?.Dispose();

                _client?.Close();
                _listener?.Stop();

                if (_listenTask != null)
                    await _listenTask;
            }
            catch { }
        }
    }
}