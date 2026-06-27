using ChessNet;
using System;
using System.Drawing;

namespace ChessFromScratch
{
    public static class BoardEx
    {
        public static void MovePiece(this Board board, Piece piece, Point from, Point to)
        {

        }

        public static void PromotePiece(this Board b, Point Cell, Piece PromoteTo)
        {
            if (Helpers.GetPieceByCell(Cell) != Piece.W_Pawn && Helpers.GetPieceByCell(Cell) != Piece.B_Pawn)
            {
                ChessNet.Helper.Disconnect();
            }

            if (Game.gamedata.playerColor != Helpers.WhatPlayerColorIsPiece(PromoteTo))
            {
                ChessNet.Helper.Disconnect();
            }

            b.board[Cell] = PromoteTo;
        }
    }
}
