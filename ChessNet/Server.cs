using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ChessNet
{
    public class Server
    {
        public static Action<Packet> ReturnFunc_h = null;
        public static Action<string> Logger_h = null;
        private TcpListener listener;
        public void SetUpClassHandler(Action<Packet> ReturnFunc)
        {
            ReturnFunc_h = ReturnFunc;
        }

        public void SetUpLogger(Action<string> log)
        {
            Logger_h = log;
        }
        public async void StartServer()
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

            listener = new TcpListener(IPAddress.IPv6Any, 36992); //unassigned as of 23/06/2026
            listener.Start();

            Logger_h(listener.LocalEndpoint.ToString());

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                _ = HandleServer(client);
            }
        }

        private async Task HandleServer(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            BinaryReader reader = new BinaryReader(stream, Encoding.UTF8);
            BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8);

            while (true)
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
