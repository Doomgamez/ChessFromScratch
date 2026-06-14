using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessFromScratch
{
    public partial class Game : Form
    {
        Structs gamedata;
        private Bitmap bgimgcache;
        private Bitmap boardcache;

        public Game(Structs gamestruct)
        {
            gamedata = gamestruct;
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
                    DrawLayout(g);
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

    }
}
