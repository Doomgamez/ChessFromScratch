using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChessNet
{
    internal class Player
    {
        public static TcpListener listener;
        public static TcpClient client;
        public static NetworkStream ns;
    }
}
