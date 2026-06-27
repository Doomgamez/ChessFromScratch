using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChessNet
{
    public class Client
    {
        public static Action<Packet> ReturnFunc_h = null;
        public static Action<string> Logger_h = null;
        public static Task Handler;
        public static CancellationTokenSource cts;

        public void SetUpClassHandler(Action<Packet> ReturnFunc)
        {
            ReturnFunc_h = ReturnFunc;
            cts = new CancellationTokenSource();
            Handler = HandleClient(cts.Token);
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

            Player.client = new TcpClient(AddressFamily.InterNetworkV6);

            await Player.client.ConnectAsync(IPAddress.Parse(ip), 36992);

            _ = HandleClient(cts.Token);
        }

        private async Task HandleClient(CancellationToken cts)
        {
            Player.ns = Player.client.GetStream();
            BinaryReader reader = new BinaryReader(Player.ns, Encoding.UTF8);
            BinaryWriter writer = new BinaryWriter(Player.ns, Encoding.UTF8);

            while (Player.client.Connected && !cts.IsCancellationRequested)
            {
                Helper.IsConnected(Player.client, writer);
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
