using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ChessFromScratch.Board;
using static ChessFromScratch.Helpers;

namespace ChessFromScratch
{
    public partial class Game : Form
    {
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        public static Data gamedata;
        private Bitmap bgimgcache;
        private Bitmap boardcache;

        private Image spriteSheet;

        private ToolTip chessTip = new ToolTip();

        public Game(Data gamestruct)
        {
            chessTip.InitialDelay = 0;
            chessTip.ReshowDelay = 50;
            chessTip.AutoPopDelay = 200;
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
                Instance.board = Defboard;
            }else
            {
                Instance.board = DefboardWhite;
            }

            Console.WriteLine(gamedata.playerColor); // yes it is random just not enough

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

        void RenderPotentialMove(Graphics g)
        {
            using (Brush b = new SolidBrush(
                Color.FromArgb(128, Color.Lime)))
            {
                foreach (Point move in gamedata.potentialmoves)
                {
                    int x = (move.X - 1) * 100;
                    int y = (move.Y - 1) * 100;

                    g.FillEllipse(
                        b,
                        x + 25,
                        y + 25,
                        50,
                        50);
                }
            }
        }

        void ShowPotentialMoves()
        {
            Piece value;
            gamedata.potentialmoves.Clear();
            Point position = gamedata.CurrentSelectedPiece.Item1;
            Piece piece = gamedata.CurrentSelectedPiece.Item2;
            switch (piece)
            {
                case Piece.W_King:
                    break;
                case Piece.W_Queen:
                    break;
                case Piece.W_Rook:
                    break;
                case Piece.W_Bishop:
                    break;
                case Piece.W_Knight:
                    break;
                case Piece.W_Pawn:
                    {
                        if (position.Y == 7)
                        {
                            if (GetPieceByCell(new Point(position.X, 5)) == default)
                            {
                                TryAddPotentialMove(new Point(position.X, 5));
                            }
                            //todo add el pasante
                        }
                        if (GetPieceByCell(new Point(position.X, position.Y - 1)) == default)
                        {
                            TryAddPotentialMove(new Point(position.X, position.Y - 1));
                        }

                        value = GetPieceByCell(new Point(position.X - 1, position.Y - 1));
                        if (WhatPlayerColorIsPiece(value) == Game_t.PlayerColor.Black)
                        {
                            TryAddPotentialMove(new Point(position.X - 1, position.Y - 1));
                        }

                        value = GetPieceByCell(new Point(position.X + 1, position.Y - 1));
                        if (WhatPlayerColorIsPiece(value) == Game_t.PlayerColor.Black)
                        {
                            TryAddPotentialMove(new Point(position.X + 1, position.Y - 1));
                        }
                    }
                    break;
                case Piece.B_King:
                    break;
                case Piece.B_Queen:
                    break;
                case Piece.B_Rook:
                    break;
                case Piece.B_Bishop:
                    break;
                case Piece.B_Knight:
                    break;
                case Piece.B_Pawn:
                    {
                        if (position.Y == 7)
                        {
                            if (GetPieceByCell(new Point(position.X,5)) == default)
                            {
                                TryAddPotentialMove(new Point(position.X, 5));
                            }
                            //todo add el pasante
                        }
                        if (GetPieceByCell(new Point(position.X, position.Y - 1)) == default)
                        {
                            TryAddPotentialMove(new Point(position.X, position.Y - 1));
                        }

                        value = GetPieceByCell(new Point(position.X - 1, position.Y - 1));
                        if (WhatPlayerColorIsPiece(value) == Game_t.PlayerColor.White)
                        {
                            TryAddPotentialMove(new Point(position.X - 1, position.Y - 1));
                        }

                        value = GetPieceByCell(new Point(position.X + 1, position.Y - 1));
                        if (WhatPlayerColorIsPiece(value) == Game_t.PlayerColor.White)
                        {
                            TryAddPotentialMove(new Point(position.X + 1, position.Y - 1));
                        }
                    }
                    break;
                case Piece.Nothing:
                    break;
                default:
                    break;
            }
        }

        void PotentialMoveClicked(Point target)
        {
            if (!gamedata.potentialmoves.Contains(target))
                return;

            Point from = gamedata.CurrentSelectedPiece.Item1;
            Piece piece = gamedata.CurrentSelectedPiece.Item2;

            Instance.board.Remove(from);

            Instance.board[target] = piece;

            gamedata.CurrentSelectedPiece = default;
            gamedata.potentialmoves.Clear();

            boardcache?.Dispose();
            boardcache = null;

            if (piece == Piece.W_Pawn || piece == Piece.B_Pawn)
            {
                if (target.Y == 1)
                {
                    promotion pr = new promotion();
                    pr.ShowDialog();
                }
            }

            panel1.Invalidate();
        }

        void SelectPieceToMove(Point p)
        {
            if (!Instance.board.TryGetValue(p, out Piece value))
            {
                return;
            }
            if (gamedata.playerColor != WhatPlayerColorIsPiece(value))
            {
                return;
            }
            gamedata.CurrentSelectedPiece = (p, value);

            ShowPotentialMoves();

            panel1.Invalidate();
            boardcache = null;
            Console.WriteLine(gamedata.CurrentSelectedPiece);
            return;
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
                case Piece.W_King:
                    g.DrawImage(
                    spriteSheet,
                    new Rectangle(point.X * 100, point.Y * 100, 100, 100),
                    new Rectangle(160, 0, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Piece.W_Queen:
                    g.DrawImage(
                    spriteSheet,
                    new Rectangle(point.X * 100, point.Y * 100, 100, 100),
                    new Rectangle(128, 0, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Piece.W_Rook:
                    g.DrawImage(
                    spriteSheet,
                    new Rectangle(point.X * 100, point.Y * 100, 100, 100),
                    new Rectangle(32, 0, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Piece.W_Bishop:
                    g.DrawImage(
                    spriteSheet,
                    new Rectangle(point.X * 100, point.Y * 100, 100, 100),
                    new Rectangle(96, 0, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Piece.W_Knight:
                    g.DrawImage(
                    spriteSheet,
                    new Rectangle(point.X * 100, point.Y * 100, 100, 100),
                    new Rectangle(64, 0, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Piece.W_Pawn:
                    g.DrawImage(
                    spriteSheet,
                    new Rectangle(point.X * 100, point.Y * 100, 100, 100),
                    new Rectangle(0, 0, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Piece.B_King:
                    g.DrawImage(
                    spriteSheet,
                    new Rectangle(point.X * 100, point.Y * 100, 100, 100),
                    new Rectangle(160, 32, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Piece.B_Queen:
                    g.DrawImage(
                    spriteSheet,
                    new Rectangle(point.X * 100, point.Y * 100, 100, 100),
                    new Rectangle(128, 32, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Piece.B_Rook:
                    g.DrawImage(
                    spriteSheet,
                    new Rectangle(point.X * 100, point.Y * 100, 100, 100),
                    new Rectangle(32, 32, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Piece.B_Bishop:
                    g.DrawImage(
                    spriteSheet,
                    new Rectangle(point.X * 100, point.Y * 100, 100, 100),
                    new Rectangle(96, 32, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Piece.B_Knight:
                    g.DrawImage(
                    spriteSheet,
                    new Rectangle(point.X * 100, point.Y * 100, 100, 100),
                    new Rectangle(64, 32, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Piece.B_Pawn:
                    g.DrawImage(
                    spriteSheet,
                    new Rectangle(point.X * 100, point.Y * 100, 100, 100),
                    new Rectangle(0, 32, 32, 32),
                    GraphicsUnit.Pixel);
                    break;
                case Piece.Nothing:
                    break;
                default:
                    break;
            }
        }

        void RenderBoard(Graphics g)
        {
            foreach (var item in Instance.board)
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
                    RenderPotentialMove(g);
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
            if (e.Button != MouseButtons.Left)
                return;

            Point clicked = new Point(
                e.X / 100 + 1,
                e.Y / 100 + 1);

            if (gamedata.CurrentSelectedPiece != default &&
                gamedata.potentialmoves.Contains(clicked))
            {
                PotentialMoveClicked(clicked);
                return;
            }

            SelectPieceToMove(clicked);
        }

        private void KeyInput_Tick(object sender, EventArgs e)
        {
            if ((GetAsyncKeyState((int)Keys.Escape) & 0x8000) != 0)
            {
                gamedata.CurrentSelectedPiece = default;
                panel1.Invalidate();
                boardcache = null;
            }
        }
    }
}
