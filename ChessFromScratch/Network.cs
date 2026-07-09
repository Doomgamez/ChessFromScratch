using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessFromScratch
{
    public class Network
    {
        public static ChessNet.Server server = new ChessNet.Server();
        public static ChessNet.Client client = new ChessNet.Client();
    }
}
