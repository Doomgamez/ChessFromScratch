using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChessNet
{
    public class Server
    {
        public static Action<Packet> ReturnFunc_h = null;
        public static Action<string> Logger_h = null;
        public static Task Handler;
        public static CancellationTokenSource cts;
        public static Player instance = new Player();
        public void SetUpClassHandler(Action<Packet> ReturnFunc)
        {
            ReturnFunc_h = ReturnFunc;
            cts = new CancellationTokenSource();
        }

        public void SetUpLogger(Action<string> log)
        {
            Logger_h = log;
        }

        public void DefineGameData(Data gamedata)
        {
            Helper.gamedata = gamedata;
        }

        public void DefineLostConFunc(Action LostConVoid)
        {
            Helper.LostConnection = LostConVoid;
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

            if (Helper.gamedata == null)
            {
                Logger_h("gamedata is undefined");
                throw new NullReferenceException("gamedata is undefined");
            }

            if (Helper.LostConnection == null)
            {
                Logger_h("LostConnection() is undefined");
                throw new NullReferenceException("LostConnection() is undefined");
            }

            instance.listener = new TcpListener(IPAddress.IPv6Any, 36992); //unassigned as of 23/06/2026
            instance.listener.Start();

            Logger_h(instance.listener.LocalEndpoint.ToString());

            while (true)
            {
                TcpClient client = await instance.listener.AcceptTcpClientAsync();
                _ = HandleServer(client, cts.Token);
            }
        }

        private async Task HandleServer(TcpClient client, CancellationToken token)
        {
            NetworkStream ns = client.GetStream();

            using (BinaryReader reader = new BinaryReader(ns, Encoding.UTF8))
            using (BinaryWriter writer = new BinaryWriter(ns, Encoding.UTF8))
            {
                while (client.Connected && !token.IsCancellationRequested)
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
                        Logger_h(ex.ToString());
                        break;
                    }
                }
            }
        }
    }
}
