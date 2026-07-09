using System;
using System.Windows.Forms;

namespace ChessFromScratch
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Logger.starttime = (ulong)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            Application.Run(new Play());
        }
    }
}
