using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CommonCSharp.Helpers
{
    public class ConsoleHelper
    {
        static LogHelper l = new LogHelper(typeof(ConsoleHelper));
        public static void ExecuteCMD(string exeFile, string arguments, ref string strOutput, ref string strError)
        {
            l.d(string.Format("\"{0}\" {1}", exeFile, arguments));
            var p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = "cmd.exe";
            p.Start();

            var cmd = string.Format("\"{0}\" {1}"
                , exeFile, arguments);
            p.StandardInput.AutoFlush = true;
            p.StandardInput.WriteLine(cmd);
            p.StandardInput.WriteLine("exit");
            strOutput = p.StandardOutput.ReadToEnd();
            strError = p.StandardError.ReadToEnd();
            p.WaitForExit();
            p.Close();
        }

        public static void ExecuteExe(string exeFile, string arguments, bool waiting = true)
        {
            l.d(string.Format("\"{0}\" {1}", exeFile, arguments));
            using (var process = new Process())
            {
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.FileName = exeFile;
                process.StartInfo.Arguments = arguments;
                process.Start();
                if (waiting)
                {
                    process.WaitForExit();
                }
            }
        }
    }
}
