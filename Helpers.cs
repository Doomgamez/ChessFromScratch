using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChessFromScratch.Board;

namespace ChessFromScratch
{
    public class Helpers
    {
        public static Game_t.PlayerColor? WhatPlayerColorIsPiece(Piece piece)
        {
            if (Enum.GetName(typeof(Piece), piece).Contains("W_"))
            {
                return Game_t.PlayerColor.White;
            }
            else if (Enum.GetName(typeof(Piece), piece).Contains("B_"))
            {
                return Game_t.PlayerColor.Black;
            }
            return null;
        }

        public static Piece GetPieceByCell(Point point)
        {
            if (Board.Instance.board.TryGetValue(new Point(point.X, point.Y), out Piece value))
            {
                return value;
            }
            return default;
        }

        public static void TryAddPotentialMove(Point position)
        {
            Game.gamedata.potentialmoves.Add(position);
        }
    }
}
