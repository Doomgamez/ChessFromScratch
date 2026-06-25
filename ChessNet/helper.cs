using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ChessNet
{
    public class Helper
    {
        public static DateTime lastping;
        public static DateTime lastpingsent = DateTime.UtcNow;

        public static void PingHandler(Packet a)
        {
            if (a.packettype == PacketType.Ping)
            {
                lastping = DateTime.UtcNow;
            }
            return;
        }

        public static void IsConnected(TcpClient client, BinaryWriter writer)
        {
            if (DateTime.UtcNow - lastping > TimeSpan.FromSeconds(60))
            {
                // TODO: Close the reader, writer, etc
                //       do this outside of this, in the client/server class
                client.Close();
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
    }
}
