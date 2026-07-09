using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessFromScratch
{
    public class Logger
    {
        public static List<string> logs = new List<string>();
        public static ulong starttime;

        public static void log(string a)
        {
            logs.Add(a);
            File.WriteAllLines($"c_log_chessnet_instance{starttime}.log", logs.ToArray());
        }
    }
}
