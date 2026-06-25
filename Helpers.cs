using ChessNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessFromScratch
{
    public class Helpers
    {
        public static PlayerColor? WhatPlayerColorIsPiece(Piece piece)
        {
            if (Enum.GetName(typeof(Piece), piece).Contains("W_"))
            {
                return PlayerColor.White;
            }
            else if (Enum.GetName(typeof(Piece), piece).Contains("B_"))
            {
                return PlayerColor.Black;
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

        public static void DrawPiece(Graphics g, Piece piece, Point point, Point scale,Image spritesheet,Point Outputscale)
        {
            switch (piece)
            {
                case Piece.W_King:
                    g.DrawImage(
                    spritesheet,
                    new Rectangle(point.X * scale.X, point.Y * scale.Y, Outputscale.X, Outputscale.Y),
                    new Rectangle(160, 0, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Piece.W_Queen:
                    g.DrawImage(
                    spritesheet,
                    new Rectangle(point.X * scale.X, point.Y * scale.Y, Outputscale.X, Outputscale.Y),
                    new Rectangle(128, 0, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Piece.W_Rook:
                    g.DrawImage(
                    spritesheet,
                    new Rectangle(point.X * scale.X, point.Y * scale.Y, Outputscale.X, Outputscale.Y),
                    new Rectangle(32, 0, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Piece.W_Bishop:
                    g.DrawImage(
                    spritesheet,
                    new Rectangle(point.X * scale.X, point.Y * scale.Y, Outputscale.X, Outputscale.Y),
                    new Rectangle(96, 0, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Piece.W_Knight:
                    g.DrawImage(
                    spritesheet,
                    new Rectangle(point.X * scale.X, point.Y * scale.Y, Outputscale.X, Outputscale.Y),
                    new Rectangle(64, 0, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Piece.W_Pawn:
                    g.DrawImage(
                    spritesheet,
                    new Rectangle(point.X * scale.X, point.Y * scale.Y, Outputscale.X, Outputscale.Y),
                    new Rectangle(0, 0, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Piece.B_King:
                    g.DrawImage(
                    spritesheet,
                    new Rectangle(point.X * scale.X, point.Y * scale.Y, Outputscale.X, Outputscale.Y),
                    new Rectangle(160, 32, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Piece.B_Queen:
                    g.DrawImage(
                    spritesheet,
                    new Rectangle(point.X * scale.X, point.Y * scale.Y, Outputscale.X, Outputscale.Y),
                    new Rectangle(128, 32, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Piece.B_Rook:
                    g.DrawImage(
                    spritesheet,
                    new Rectangle(point.X * scale.X, point.Y * scale.Y, Outputscale.X, Outputscale.Y),
                    new Rectangle(32, 32, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Piece.B_Bishop:
                    g.DrawImage(
                    spritesheet,
                    new Rectangle(point.X * scale.X, point.Y * scale.Y, Outputscale.X, Outputscale.Y),
                    new Rectangle(96, 32, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Piece.B_Knight:
                    g.DrawImage(
                    spritesheet,
                    new Rectangle(point.X * scale.X, point.Y * scale.Y, Outputscale.X, Outputscale.Y),
                    new Rectangle(64, 32, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Piece.B_Pawn:
                    g.DrawImage(
                    spritesheet,
                    new Rectangle(point.X * scale.X, point.Y * scale.Y, Outputscale.X, Outputscale.Y),
                    new Rectangle(0, 32, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Piece.Nothing:
                    break;
                default:
                    break;
            }
        }
    }
}
