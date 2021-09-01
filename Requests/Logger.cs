using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requests
{
    class Logger
    {
        public void LogIt(string message)
        {
            string file = @"C:\Logs\log.txt";
            StringBuilder sb = new StringBuilder();
            sb.Append(message + "\n");
            // flush every 20 seconds as you do it
            File.AppendAllText(file, sb.ToString());
            Console.WriteLine(message);
            sb.Clear();
        }
    }
}
