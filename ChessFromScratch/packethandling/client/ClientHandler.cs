using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessNet;

namespace ChessFromScratch
{
    public class ClientHandler
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
        public static ClientHandler clienthandler = new ClientHandler();
    }
}
