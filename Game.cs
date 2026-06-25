using ChessNet;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using static ChessFromScratch.Helpers;

namespace ChessFromScratch
{
    public partial class Game : Form
    {
        public static Data gamedata;
        private Bitmap bgimgcache;
        private Bitmap boardcache;
        private Image spriteSheet;
        private readonly ToolTip chessTip = new ToolTip();

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
            Assembly asm = Assembly.GetExecutingAssembly();

            using (var stream = asm.GetManifestResourceStream(
                "ChessFromScratch.emb.spritesheetchess.png"))
            {
                spriteSheet = Image.FromStream(stream);
            }

            if (gamedata.playerColor == PlayerColor.Black)
            {
                Board.Instance.board = Board.Defboard;
            }
            else
            {
                Board.Instance.board = Board.DefboardWhite;
            }

            Console.WriteLine(gamedata.playerColor); // yes it is random just not enough

        }

        private void DrawLayout(Graphics g)
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

        private void RenderPotentialMove(Graphics g)
        {
            bool ismove = false;
            using (Brush b = new SolidBrush(
                Color.FromArgb(128, Color.Lime)))
            {
                foreach (Point move in gamedata.potentialmoves)
                {
                    ismove = true;
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
            if (ismove)
            {
                g.DrawString("ESC to cancel", new Font("Arial", 24), Brushes.Cyan, new PointF(0, 0));
            }
        }

        private void ShowPotentialMoves()
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
                        if (WhatPlayerColorIsPiece(value) == PlayerColor.Black)
                        {
                            TryAddPotentialMove(new Point(position.X - 1, position.Y - 1));
                        }

                        value = GetPieceByCell(new Point(position.X + 1, position.Y - 1));
                        if (WhatPlayerColorIsPiece(value) == PlayerColor.Black)
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
                        if (WhatPlayerColorIsPiece(value) == PlayerColor.White)
                        {
                            TryAddPotentialMove(new Point(position.X - 1, position.Y - 1));
                        }

                        value = GetPieceByCell(new Point(position.X + 1, position.Y - 1));
                        if (WhatPlayerColorIsPiece(value) == PlayerColor.White)
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

        private void PotentialMoveClicked(Point target)
        {
            if (!gamedata.potentialmoves.Contains(target))
                return;

            Point from = gamedata.CurrentSelectedPiece.Item1;
            Piece piece = gamedata.CurrentSelectedPiece.Item2;

            Board.Instance.board.Remove(from);

            Board.Instance.board[target] = piece;

            gamedata.CurrentSelectedPiece = default;
            gamedata.potentialmoves.Clear();

            boardcache?.Dispose();
            boardcache = null;

            if (piece == Piece.W_Pawn || piece == Piece.B_Pawn)
            {
                if (target.Y == 1)
                {
                    Promotion pr = new Promotion(target);
                    pr.ShowDialog(this);
                }
            }

            panel1.Invalidate();
        }

        private void SelectPieceToMove(Point p)
        {
            if (!Board.Instance.board.TryGetValue(p, out Piece value))
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

        private void RenderTooltip(Point loc)
        {
            if (!Board.Instance.board.TryGetValue(loc, out Piece value))
            {
                return;
            }
            chessTip.SetToolTip(panel1, Enum.GetName(typeof(Piece), value).Replace("W_", "White ").Replace("B_", "Black "));
            return;
        }

        private void RenderBoard(Graphics g)
        {
            foreach (var item in Board.Instance.board)
            {
                DrawPiece(g, item.Value, new Point(item.Key.X - 1, item.Key.Y - 1), new Point(100, 100), spriteSheet, new Point(100, 100));
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

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                gamedata.CurrentSelectedPiece = default;
                gamedata.potentialmoves.Clear();
                panel1.Invalidate();
                boardcache = null;
            }
        }
    }
}
