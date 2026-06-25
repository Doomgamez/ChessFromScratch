using System;
using System.Windows.Forms;

namespace ChessFromScratch
{
    public partial class InputBox : Form
    {
        public string input = "";

        public InputBox(string message, string buttonText)
        {
            InitializeComponent();

            label1.Text = message;
            button1.Text = buttonText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            input = textBox1.Text;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
