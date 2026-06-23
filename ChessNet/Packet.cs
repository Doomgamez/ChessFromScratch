using ChessFromScratch;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessNet
{ 
    public class Move
    {
        public Point From { get; set; }
        public Point To { get; set; }
        public Board.Piece? promotion { get; set; }
    }
    public enum PacketType
    {
        Normal,
        Chat,
        Resign,
        Ping
    }

    public class Packet
    {
        public const ushort ProtocolVersion = 1;
        public UInt32 timeelapsed { get; set; }
        public Move move {get;set;}
        public PacketType packettype { get; set; }
        public string message { get; set; }
        public Game_t.HostType hosttype { get; set; } //client; host if client fakes being host game is dced
        public Game_t.GameState gamestate { get; set; }
    }
}
