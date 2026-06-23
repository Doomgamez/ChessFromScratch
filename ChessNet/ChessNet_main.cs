using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Principal;
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
                return;
            }

            if (ReturnFunc_h == null)
            {
                Logger_h("ReturnFunc_h() is undefined");
                throw new NullReferenceException("ReturnFunc_h() is undefined");
                return;
            }

            _client = new TcpClient(AddressFamily.InterNetworkV6);

            await _client.ConnectAsync(IPAddress.Parse(ip), 36992);

            _ = HandleClient(_client);
        }

        private async Task HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[4096];

            while (client.Connected)
            {
                int bytesRead = await stream.ReadAsync(
                    buffer,
                    0,
                    buffer.Length);

                helper.IsConnected(client, stream);

                string json = Encoding.UTF8.GetString(
                        buffer,
                        0,
                        bytesRead);

                Packet a = new Packet();

                try
                {
                    a = JsonConvert.DeserializeObject<Packet>(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                    helper.PingHandler(a);
                }
                catch (JsonException ex)
                {
                    Logger_h("invalid packet " + ex.Message);
                }finally
                {

                }

                ReturnFunc_h(a);
            }
    }
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
                return;
            }

            if (ReturnFunc_h == null)
            {
                Logger_h("ReturnFunc_h() is undefined");
                throw new NullReferenceException("ReturnFunc_h() is undefined");
                return;
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

            byte[] buffer = new byte[4096];

                while (true)
                {
                    helper.IsConnected(client, stream);

                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                    Packet a = new Packet();

                    try
                    {
                        a = JsonConvert.DeserializeObject<Packet>(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                        helper.PingHandler(a);
                    }
                    catch (JsonException ex)
                    {
                        Logger_h("invalid packet " + ex.Message);
                    }finally
                    {

                    }

                    ReturnFunc_h(a);
                }
            }
        }
    }
}
