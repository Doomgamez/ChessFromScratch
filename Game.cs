using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ChessFromScratch.Board;

namespace ChessFromScratch
{
    public partial class Game : Form
    {
        Data gamedata;
        private Bitmap bgimgcache;
        private Bitmap boardcache;

        private Image spriteSheet;

        private ToolTip chessTip = new ToolTip();

        public Game(Data gamestruct)
        {
            gamedata = gamestruct;
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var asm = Assembly.GetExecutingAssembly();

            using (var stream = asm.GetManifestResourceStream(
                "ChessFromScratch.emb.spritesheetchess.png"))
            {
                spriteSheet = Image.FromStream(stream);
            }

            if (gamedata.playerColor == Game_t.PlayerColor.Black)
            {
                Board.Instance.board = Board.Defboard;
            }else
            {
                Board.Instance.board = Board.DefboardWhite;
            }

        }

        void DrawLayout(Graphics g)
        {
            for (float x = 0; x <= panel1.Width; x += 100)
            {
                g.DrawLine(
                    Pens.Black,
                    x,
                    0,
                    x,
                    panel1.Height);
            }

            for (float y = 0; y <= panel1.Height; y += 100)
            {
                g.DrawLine(
                    Pens.Black,
                    0,
                    y,
                    panel1.Width,
                    y);
            }
        }

        void RenderTooltip(Point loc)
        {
            if (!Board.Instance.board.TryGetValue(loc, out Piece value))
            {
                return;
            }
            chessTip.SetToolTip(panel1, Enum.GetName(typeof(Piece), value).Replace("W_","White ").Replace("B_","Black "));
            return;
        }

        void DrawPiece(Graphics g,Board.Piece piece,Point point)
        {
            switch (piece)
            {
                case Board.Piece.W_King:
                    g.DrawImage(
                    spriteSheet,
                    new Rectangle(point.X * 100, point.Y * 100, 100, 100),
                    new Rectangle(160, 0, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Board.Piece.W_Queen:
                    g.DrawImage(
                    spriteSheet,
                    new Rectangle(point.X * 100, point.Y * 100, 100, 100),
                    new Rectangle(128, 0, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Board.Piece.W_Rook:
                    g.DrawImage(
                    spriteSheet,
                    new Rectangle(point.X * 100, point.Y * 100, 100, 100),
                    new Rectangle(32, 0, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Board.Piece.W_Bishop:
                    g.DrawImage(
                    spriteSheet,
                    new Rectangle(point.X * 100, point.Y * 100, 100, 100),
                    new Rectangle(96, 0, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Board.Piece.W_Knight:
                    g.DrawImage(
                    spriteSheet,
                    new Rectangle(point.X * 100, point.Y * 100, 100, 100),
                    new Rectangle(64, 0, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Board.Piece.W_Pawn:
                    g.DrawImage(
                    spriteSheet,
                    new Rectangle(point.X * 100, point.Y * 100, 100, 100),
                    new Rectangle(0, 0, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Board.Piece.B_King:
                    g.DrawImage(
                    spriteSheet,
                    new Rectangle(point.X * 100, point.Y * 100, 100, 100),
                    new Rectangle(160, 32, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Board.Piece.B_Queen:
                    g.DrawImage(
                    spriteSheet,
                    new Rectangle(point.X * 100, point.Y * 100, 100, 100),
                    new Rectangle(128, 32, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Board.Piece.B_Rook:
                    g.DrawImage(
                    spriteSheet,
                    new Rectangle(point.X * 100, point.Y * 100, 100, 100),
                    new Rectangle(32, 32, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Board.Piece.B_Bishop:
                    g.DrawImage(
                    spriteSheet,
                    new Rectangle(point.X * 100, point.Y * 100, 100, 100),
                    new Rectangle(96, 32, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Board.Piece.B_Knight:
                    g.DrawImage(
                    spriteSheet,
                    new Rectangle(point.X * 100, point.Y * 100, 100, 100),
                    new Rectangle(64, 32, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Board.Piece.B_Pawn:
                    g.DrawImage(
                    spriteSheet,
                    new Rectangle(point.X * 100, point.Y * 100, 100, 100),
                    new Rectangle(0, 32, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Board.Piece.Nothing:
                    break;
                default:
                    break;
            }
        }

        void RenderBoard(Graphics g)
        {
            foreach (var item in Board.Instance.board)
            {
                DrawPiece(g, item.Value, new Point(item.Key.X - 1, item.Key.Y - 1));
            }
            return;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (boardcache == null)
            {
                boardcache = new Bitmap(
                    panel1.Width,
                    panel1.Height);

                using (Graphics g =
                    Graphics.FromImage(boardcache))
                {

                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

                    DrawLayout(g);
                    RenderBoard(g);
                }
            }

            e.Graphics.DrawImage(
                boardcache,
                0,
                0);
        }
        

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            if (bgimgcache == null)
            {
                bgimgcache = new Bitmap(
                    panel2.Width,
                    panel2.Height);

                using (Graphics g =
                    Graphics.FromImage(bgimgcache))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

                    for (int i = 0; i < 1000; i += 10)
                    {
                        g.DrawLine(
                            Pens.Black,
                            0,
                            0,
                            panel2.Width,
                            panel2.Height - i);
                    }

                    for (int i = 0; i < 1000; i += 10)
                    {
                        g.DrawLine(
                            Pens.DarkGray,
                            0,
                            0,
                            panel2.Width - i,
                            panel2.Height);
                    }
                }
            }

            e.Graphics.DrawImage(
                bgimgcache,
                0,
                0);
        }

        private void leave_Click(object sender, EventArgs e)
        {
            this.Hide();
            foreach (Form form in Application.OpenForms)
            {
                if (form is Play)
                {
                    form.Show();
                    form.Activate();
                    break;
                }
            }
        }

        private void manual_Click(object sender, EventArgs e)
        {
            string tmp = Path.Combine(Path.GetTempPath(), "manual-ChessFromScratch.txt");

            if (!File.Exists(tmp))
            {
                using (Stream res = Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("ChessFromScratch.emb.manual.txt"))
                {
                    using (FileStream file = File.Create(tmp))
                    {
                        res.CopyTo(file);
                    }
                }
            }

            Process.Start("notepad.exe", tmp);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X / 100;
            int y = e.Y / 100;

            RenderTooltip(new Point(x + 1, y + 1));
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
