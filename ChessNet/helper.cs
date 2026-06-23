using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ChessNet
{
    public class helper
    {
        public static DateTime lastping;
        public static DateTime lastpingsent = DateTime.UtcNow;

        public static void SendPacket(TcpClient client, Packet packet)
        {
            string json = JsonConvert.SerializeObject(packet);

            byte[] data = Encoding.UTF8.GetBytes(json);

            client.GetStream().WriteAsync(data, 0, data.Length);
        }

        public static void PingHandler(Packet a)
        {
            if (a.packettype == PacketType.Ping)
            {
                lastping = DateTime.UtcNow;
            }
            return;
        }

        public static void IsConnected(TcpClient client,NetworkStream stream)
        {
            if (DateTime.UtcNow - lastping > TimeSpan.FromSeconds(60))
            {
                stream.Close();
                client.Close();
            }
            else if (DateTime.UtcNow - lastping > TimeSpan.FromSeconds(3)) //garbage piece of shit every 3 seconds if no ping was done sends a packet with a second delay to make sure the clients still are connected
            {
                if (DateTime.UtcNow - lastpingsent > TimeSpan.FromSeconds(1))
                {
                    ChessNet.Client.Logger_h($"Client not responding verifying state time until disconnect : {60 - (DateTime.UtcNow - lastping).TotalSeconds:F0} seconds");
                    SendPacket(client, new Packet
                    {
                        packettype = PacketType.Ping
                    });

                    lastpingsent = DateTime.UtcNow;
                }
            }
        }
    }
}
