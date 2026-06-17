using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ChessFromScratch.Board;
using static ChessFromScratch.Helpers;

namespace ChessFromScratch
{
    public partial class promotion : Form
    {
        private Image spriteSheet;
        private ToolTip promotiontip = new ToolTip();
        private Point CellToReplace;
        public promotion(Point Cell)
        {
            CellToReplace = Cell;
            InitializeComponent();
        }

        private void promotion_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            if (Game.gamedata.playerColor == Game_t.PlayerColor.White)
            {
                DrawPiece(g, Piece.W_Rook, new Point(0, 0), new Point(128, 128), spriteSheet, new Point(128, 128));
                DrawPiece(g, Piece.W_Knight, new Point(1, 0), new Point(128, 128), spriteSheet, new Point(128, 128));
                DrawPiece(g, Piece.W_Bishop, new Point(2, 0), new Point(128, 128), spriteSheet, new Point(128, 128));
                DrawPiece(g, Piece.W_Queen, new Point(3, 0), new Point(128, 128), spriteSheet, new Point(128, 128));
            }
            else if (Game.gamedata.playerColor == Game_t.PlayerColor.Black)
            {
                DrawPiece(g, Piece.B_Rook, new Point(0, 0), new Point(128, 128), spriteSheet, new Point(128, 128));
                DrawPiece(g, Piece.B_Knight, new Point(1, 0), new Point(128, 128), spriteSheet, new Point(128, 128));
                DrawPiece(g, Piece.B_Bishop, new Point(2, 0), new Point(128, 128), spriteSheet, new Point(128, 128));
                DrawPiece(g, Piece.B_Queen, new Point(3, 0), new Point(128, 128), spriteSheet, new Point(128, 128));
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        private void promotion_MouseDown(object sender, MouseEventArgs e)
        {
            int x = e.X / 128;

            switch (x)
            {
                case 0:
                    {
                        if (Game.gamedata.playerColor == Game_t.PlayerColor.White)
                        {
                            Instance.PromotePiece(CellToReplace, Piece.W_Rook);
                        }
                        else if (Game.gamedata.playerColor == Game_t.PlayerColor.Black)
                        {
                            Instance.PromotePiece(CellToReplace, Piece.B_Rook);
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    break;
                case 1:
                    {
                        if (Game.gamedata.playerColor == Game_t.PlayerColor.White)
                        {
                            Instance.PromotePiece(CellToReplace, Piece.W_Knight);
                        }
                        else if (Game.gamedata.playerColor == Game_t.PlayerColor.Black)
                        {
                            Instance.PromotePiece(CellToReplace, Piece.B_Knight);
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    break;
                case 2:
                    {
                        if (Game.gamedata.playerColor == Game_t.PlayerColor.White)
                        {
                            Instance.PromotePiece(CellToReplace, Piece.W_Bishop);
                        }
                        else if (Game.gamedata.playerColor == Game_t.PlayerColor.Black)
                        {
                            Instance.PromotePiece(CellToReplace, Piece.B_Bishop);
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    break;
                case 3:
                    {
                        if (Game.gamedata.playerColor == Game_t.PlayerColor.White)
                        {
                            Instance.PromotePiece(CellToReplace, Piece.W_Queen);
                        }
                        else if (Game.gamedata.playerColor == Game_t.PlayerColor.Black)
                        {
                            Instance.PromotePiece(CellToReplace, Piece.B_Queen);
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    break;
                default:
                    break;
            }
            return;
        }

        private void promotion_Load(object sender, EventArgs e)
        {
            var asm = Assembly.GetExecutingAssembly();

            using (var stream = asm.GetManifestResourceStream(
                "ChessFromScratch.emb.spritesheetchess.png"))
            {
                spriteSheet = Image.FromStream(stream);
            }
        }

        void RenderTooltip(Point loc)
        {
            switch (loc.X)
            {
                case 0:
                    {
                        if (Game.gamedata.playerColor == Game_t.PlayerColor.White)
                        {
                            promotiontip.SetToolTip(this, "White Rook");
                        }
                        else if (Game.gamedata.playerColor == Game_t.PlayerColor.Black)
                        {
                            promotiontip.SetToolTip(this, "Black Rook");
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                    }
                    break;
                case 1:
                    {
                        if (Game.gamedata.playerColor == Game_t.PlayerColor.White)
                        {
                            promotiontip.SetToolTip(this, "White Knight");
                        }
                        else if (Game.gamedata.playerColor == Game_t.PlayerColor.Black)
                        {
                            promotiontip.SetToolTip(this, "Black Knight");
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                    }
                    break;
                case 2:
                    {
                        if (Game.gamedata.playerColor == Game_t.PlayerColor.White)
                        {
                            promotiontip.SetToolTip(this, "White Bishop");
                        }
                        else if (Game.gamedata.playerColor == Game_t.PlayerColor.Black)
                        {
                            promotiontip.SetToolTip(this, "Black Bishop");
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                    }
                    break;
                case 3:
                    {
                        if (Game.gamedata.playerColor == Game_t.PlayerColor.White)
                        {
                            promotiontip.SetToolTip(this, "White Queen");
                        }
                        else if (Game.gamedata.playerColor == Game_t.PlayerColor.Black)
                        {
                            promotiontip.SetToolTip(this, "Black Queen");
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                    }
                    break;
                default:
                    break;
            }
            return;
        }

        private void promotion_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X / 128;
            int y = e.Y / 128;

            RenderTooltip(new Point(x, y));
        }
    }
}
