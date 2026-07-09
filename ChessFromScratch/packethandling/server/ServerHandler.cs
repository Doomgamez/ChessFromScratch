using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessNet;

namespace ChessFromScratch
{
    public class ServerHandler
    {
        public GameState gameState = GameState.WaitingForOpponent;
        public void PacketHandler(Packet packet)
        {
            return;
        }

        public void LostConnection()
        {
            if (gameState == GameState.WaitingForOpponent)
            {
                return;
            }
        }
        public static ServerHandler serverhandler = new ServerHandler();
    }
}
