using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChessNet
{
    public class Client
    {
        public static Action<Packet> ReturnFunc_h = null;
        public static Action<string> Logger_h = null;
        private TcpClient _client;
        public void SetUpClassHandler(Action<Packet> ReturnFunc)
        {
            ReturnFunc_h = ReturnFunc;
        }

        public void SetUpLogger(Action<string> log)
        {
            Logger_h = log;
        }

        public async void StartClient(string ip)
        {
            if (Logger_h == null)
            {
                throw new NullReferenceException("Logger_h() is undefined");
            }

            if (ReturnFunc_h == null)
            {
                Logger_h("ReturnFunc_h() is undefined");
                throw new NullReferenceException("ReturnFunc_h() is undefined");
            }

            _client = new TcpClient(AddressFamily.InterNetworkV6);

            await _client.ConnectAsync(IPAddress.Parse(ip), 36992);

            _ = HandleClient(_client);
        }

        private async Task HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            BinaryReader reader = new BinaryReader(stream, Encoding.UTF8);
            BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8);

            while (client.Connected)
            {
                Helper.IsConnected(client, writer);
                try
                {
                    Packet packet = Packet.Read(reader);
                    Helper.PingHandler(packet);
                    ReturnFunc_h(packet);
                }
                catch (Exception ex)
                {
                    Logger_h("invalid packet " + ex.Message);
                    ReturnFunc_h(null);
                }
            }
        }
    }
}
