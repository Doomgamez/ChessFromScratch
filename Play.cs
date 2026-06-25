using ChessNet;
using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ChessFromScratch
{
    public partial class Play : Form
    {
        private Random random;
        private ToolTip domaintip = new ToolTip();
        public Play()
        {
            random = new Random();
            domaintip.InitialDelay = 0;
            domaintip.ReshowDelay = 0;
            domaintip.AutoPopDelay = 5000;
            InitializeComponent();
        }

        private void Play_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int i = 0; i < 1000; i = i + 10)
            {
                g.DrawLine(Pens.Black, 0, 0, Width, Height - i);
            }
            for (int i = 0; i < 1000; i = i + 10)
            {
                g.DrawLine(Pens.DarkGray, 0, 0, Width - i, Height);
            }
            g.DrawString("Chess From Scratch", new Font("Arial", 24), Brushes.Black, new PointF(Width / 2 - 150, Height / 2 - 50));
        }

        private void Play_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Data structs = new Data();
            structs.matchtype = MatchType.Bot;
            structs.ipv6 = string.Empty;
            structs.playerColor = (PlayerColor)Enum.GetValues(typeof(PlayerColor)).GetValue(random.Next(Enum.GetValues(typeof(PlayerColor)).Length)); // https://stackoverflow.com/questions/3132126/how-do-i-select-a-random-value-from-an-enumeration
            Game game = new Game(structs);
            this.Hide();
            game.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InputBox box = new InputBox(
                "Enter IPv6\n\n(dont input anything if host)",
                "Connect"
            );

            if (box.ShowDialog() == DialogResult.OK)
            {
                Data structs = new Data();

                structs.matchtype = MatchType.Multiplayer;
                if (box.input == string.Empty)
                {
                    structs.hostType = HostType.Host;
                    structs.playerColor = (PlayerColor)Enum.GetValues(typeof(PlayerColor)).GetValue(random.Next(Enum.GetValues(typeof(PlayerColor)).Length));
                }
                else
                {
                    if (IPAddress.TryParse(box.input, out IPAddress address) && address.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        structs.hostType = HostType.Client;
                        //get playercolor from host
                    }
                    else
                    {
                        MessageBox.Show("Invalid Host Ipv6", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                structs.ipv6 = box.input;

                Game game = new Game(structs);

                this.Hide();
                game.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            ClientData.HideMyIp = checkBox2.Checked;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://doomgames.cc");
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            domaintip.SetToolTip(pictureBox1, "click to open Doomgames.cc");
        }
    }
}
