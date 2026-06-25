using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessNet
{
    public class Data
    {
        public MatchType matchtype { get; set; }
        public HostType hostType { get; set; }
        public PlayerColor playerColor { get; set; }
        public string ipv6 { get; set; } = string.Empty;
        public PlayerColor CurrentTurn { get; set; } = PlayerColor.White;
        public (Point, Piece) CurrentSelectedPiece { get; set; }
        public List<Point> potentialmoves { get; set; } = new List<Point>();
    }
}
