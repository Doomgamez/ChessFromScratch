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

namespace ChessFromScratch
{
    public partial class promotion : Form
    {
        public Piece selectedpiece;
        private Image spriteSheet;
        public promotion()
        {
            InitializeComponent();
        }

        private void promotion_Paint(object sender, PaintEventArgs e)
        {

        }

        private void promotion_MouseDown(object sender, MouseEventArgs e)
        {

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
    }
}
