using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChessNet
{
    public class Player
    {
        public TcpListener listener;
        public TcpClient client;
        public NetworkStream ns;
    }
}
