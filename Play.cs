using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessFromScratch
{
    public partial class Play : Form
    {
        public Play()
        {
            InitializeComponent();
        }

        private void Play_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int i = 0; i < 1000; i=i+10)
            {
                g.DrawLine(Pens.Black, 0, 0, Width, Height-i);
            }
            for (int i = 0; i < 1000; i = i + 10)
            {
                g.DrawLine(Pens.DarkGray, 0, 0, Width-i, Height);
            }
            g.DrawString("Chess From Scratch", new Font("Arial", 24), Brushes.Black, new PointF(Width / 2 - 150, Height / 2 - 50));
            g.Dispose();
        }

        private void Play_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Structs structs = new Structs();
            structs.matchtype = matchtype.Bot;
            structs.ipv6 = string.Empty;
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
                Structs structs = new Structs();

                structs.matchtype = matchtype.Multiplayer;
                if (box.input == string.Empty)
                {
                    structs.hostType = HostType.Host;
                }else
                {
                    if (IPAddress.TryParse(box.input, out IPAddress address) && address.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        structs.hostType = HostType.Client;
                    }
                    else
                    {
                        MessageBox.Show("Invalid Host Ipv6", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                structs.ipv6 = box.input;

                Game game = new Game(structs);

                Hide();
                game.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
