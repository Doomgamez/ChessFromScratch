using System;
using System.IO;
using System.Net.Sockets;

namespace ChessNet
{
    public class Helper
    {
        public static Data gamedata;
        public static DateTime lastping;
        public static DateTime lastpingsent = DateTime.UtcNow;

        public static Action LostConnection;

        public static void PingHandler(Packet a)
        {
            if (a.packettype == PacketType.Ping)
            {
                lastping = DateTime.UtcNow;
            }
            return;
        }

        public static void Disconnect()
        {
            if (gamedata.hostType == HostType.Host)
            {
                if (Server.instance.client != null)
                {
                    Server.instance.client.Close();
                    Server.instance.ns.Close();
                }
            }
            else
            {
                if (Client.instance.client != null)
                {
                    Client.instance.client.Close();
                    Client.instance.ns.Close();

                    Client.cts.Cancel();
                }
            }
        }

        public static void IsConnected(TcpClient client, BinaryWriter writer)
        {
            if (DateTime.UtcNow - lastping > TimeSpan.FromSeconds(60))
            {
                LostConnection();
            }
            else if (DateTime.UtcNow - lastping > TimeSpan.FromSeconds(3)) //garbage piece of shit every 3 seconds if no ping was done sends a packet with a second delay to make sure the clients still are connected
            {
                if (DateTime.UtcNow - lastpingsent > TimeSpan.FromSeconds(1))
                {
                    Client.Logger_h($"Client not responding verifying state time until disconnect : {60 - (DateTime.UtcNow - lastping).TotalSeconds:F0} seconds");
                    new Packet() { packettype = PacketType.Ping }.Write(writer);
                    lastpingsent = DateTime.UtcNow;
                }
            }
        }

        public static void CloseServer()
        {
            Disconnect();
        }
    }
}
