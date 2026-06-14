using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessFromScratch
{
    public class Game_t
    {
        public enum matchtype
        {
            Bot,
            Multiplayer
        };
        public enum PlayerColor
        {
            White,
            Black
        };
        public enum HostType
        {
            Host,
            Client
        };
        public enum GameState
        {
            WaitingForOpponent,
            Playing,
            GameOver
        };
    }

    public class Board
    {
        public enum Piece
        {
            W_King,
            W_Queen,
            W_Rook,
            W_Bishop,
            W_Knight,
            W_Pawn,
            B_King,
            B_Queen,
            B_Rook,
            B_Bishop,
            B_Knight,
            B_Pawn,
            Nothing
        };

        public static readonly Dictionary<Point, Piece> Defboard = new Dictionary<Point, Piece>()
        {
            { new Point(1,1), Piece.W_Rook },
            { new Point(2,1), Piece.W_Knight },
            { new Point(3,1), Piece.W_Bishop },
            { new Point(4,1), Piece.W_Queen },
            { new Point(5,1), Piece.W_King },
            { new Point(6,1), Piece.W_Bishop },
            { new Point(7,1), Piece.W_Knight },
            { new Point(8,1), Piece.W_Rook },
        
            { new Point(1,2), Piece.W_Pawn },
            { new Point(2,2), Piece.W_Pawn },
            { new Point(3,2), Piece.W_Pawn },
            { new Point(4,2), Piece.W_Pawn },
            { new Point(5,2), Piece.W_Pawn },
            { new Point(6,2), Piece.W_Pawn },
            { new Point(7,2), Piece.W_Pawn },
            { new Point(8,2), Piece.W_Pawn },
        
            { new Point(1,7), Piece.B_Pawn },
            { new Point(2,7), Piece.B_Pawn },
            { new Point(3,7), Piece.B_Pawn },
            { new Point(4,7), Piece.B_Pawn },
            { new Point(5,7), Piece.B_Pawn },
            { new Point(6,7), Piece.B_Pawn },
            { new Point(7,7), Piece.B_Pawn },
            { new Point(8,7), Piece.B_Pawn },
        
            { new Point(1,8), Piece.B_Rook },
            { new Point(2,8), Piece.B_Knight },
            { new Point(3,8), Piece.B_Bishop },
            { new Point(4,8), Piece.B_Queen },
            { new Point(5,8), Piece.B_King },
            { new Point(6,8), Piece.B_Bishop },
            { new Point(7,8), Piece.B_Knight },
            { new Point(8,8), Piece.B_Rook }
        };

        public static readonly Dictionary<Point, Piece> DefboardWhite = new Dictionary<Point, Piece>()
        {
            { new Point(1,1), Piece.B_Rook },
            { new Point(2,1), Piece.B_Knight },
            { new Point(3,1), Piece.B_Bishop },
            { new Point(4,1), Piece.B_Queen },
            { new Point(5,1), Piece.B_King },
            { new Point(6,1), Piece.B_Bishop },
            { new Point(7,1), Piece.B_Knight },
            { new Point(8,1), Piece.B_Rook },
                                    
            { new Point(1,2), Piece.B_Pawn },
            { new Point(2,2), Piece.B_Pawn },
            { new Point(3,2), Piece.B_Pawn },
            { new Point(4,2), Piece.B_Pawn },
            { new Point(5,2), Piece.B_Pawn },
            { new Point(6,2), Piece.B_Pawn },
            { new Point(7,2), Piece.B_Pawn },
            { new Point(8,2), Piece.B_Pawn },

            { new Point(1,7), Piece.W_Pawn },
            { new Point(2,7), Piece.W_Pawn },
            { new Point(3,7), Piece.W_Pawn },
            { new Point(4,7), Piece.W_Pawn },
            { new Point(5,7), Piece.W_Pawn },
            { new Point(6,7), Piece.W_Pawn },
            { new Point(7,7), Piece.W_Pawn },
            { new Point(8,7), Piece.W_Pawn },

            { new Point(1,8), Piece.W_Rook },
            { new Point(2,8), Piece.W_Knight },
            { new Point(3,8), Piece.W_Bishop },
            { new Point(4,8), Piece.W_Queen },
            { new Point(5,8), Piece.W_King },
            { new Point(6,8), Piece.W_Bishop },
            { new Point(7,8), Piece.W_Knight },
            { new Point(8,8), Piece.W_Rook }
        };

        public static Board Instance { get; } = new Board();

        public void MovePiece(Piece piece,Point from,Point to)
        {

        }
        public Dictionary<Point, Piece> board = new Dictionary<Point, Piece>();
    }

    public static class Client
    {
        public static bool HideMyIp = false; //client side streamer mode

        public static void LoadMyConfig()
        {
            return;
        }
    }

    public class Data
    {
        public Game_t.matchtype matchtype { get; set; }
        public Game_t.HostType hostType { get; set; }
        public Game_t.PlayerColor playerColor { get; set; }
        public string ipv6 { get; set; } = string.Empty;
        public Game_t.PlayerColor CurrentTurn { get; set; } = Game_t.PlayerColor.White;
        public (Point, Board.Piece) CurrentSelectedPiece { get; set; }
        public List<Point> potentialmoves { get; set; } = new List<Point>();
    }
}
