using ChessGame.Model;
using ChessGame.Model.DTO;
using ChessGame.Model.Moves;
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

        private TcpListener tcpListener;
        private TcpClient tcpClient;

        private StreamReader reader;
        private StreamWriter writer;

        public bool IsConnected => tcpClient?.Connected == true;

        public TcpNetworkService(IDtoResolver resolver, IMessageDispatcher dispatcher)
        {
            _resolver = resolver;
            _dispatcher = dispatcher;
        }


        public async Task StartServerAsync(int port)
        {
            tcpListener = new TcpListener(IPAddress.Any, port);
            tcpListener.Start();

            tcpClient = await tcpListener.AcceptTcpClientAsync();
            InitStreams();
            StartListening();
        }

        public async Task ConnectAsync(string ip, int port)
        {
            tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(ip, port);

            InitStreams();
            StartListening();
        }

        private void InitStreams()
        {
            var stream = tcpClient.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream) { AutoFlush = true };
        }

        private void StartListening()
        {
            _ = Task.Run(async () =>
            {
                try
                {
                    while (tcpClient?.Connected == true)
                    {
                        var raw = await reader.ReadLineAsync();
                        if (raw == null) break;

                        NetworkMessage msg = null;

                        try
                        {
                            msg = JsonSerializer.Deserialize<NetworkMessage>(raw);
                        }
                        catch
                        {
                            continue;
                        }

                        if (msg == null)
                            continue;

                        var dto = _resolver.Deserialize(msg);

                        await _dispatcher.DispatchAsync(dto);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Network Error] {ex}");
                }
            });
        }
        public async Task SendAsync(DtoType type, IDtoMessage message)
        {
            if (writer == null)
            {
                return;
            }

            try
            {
                var payload = JsonSerializer.Serialize(message, message.GetType());

                var envelope = new NetworkMessage
                {
                    DtoType = type,
                    Payload = payload
                };

                var json = JsonSerializer.Serialize(envelope);

                await writer.WriteLineAsync(json);
                await writer.FlushAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[SEND ERROR]: {ex.Message}");
            }
        }

        public Task DisconnectAsync()
        {
            tcpClient?.Close();
            tcpListener?.Stop();
            return Task.CompletedTask;
        }
    }
}
