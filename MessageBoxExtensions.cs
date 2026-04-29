using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamResChanger
{
    public static class MessageBoxExtensions
    {
        private static string TakeLines(string text, int lines, string lineBreak = "\n")
        {
            var split = text.Split('\n').Select(s => s.TrimEnd('\r').Trim()).ToArray();
            return string.Join(lineBreak, split.Take(lines));
        }

        public static void ShowErrorMessage(string message, Exception? ex)
        {
            if (ex != null)
            {
                message += $"\n({ex.GetType()}): {ex.Message}";
                message += "\n\nStack trace:\n" + ex.StackTrace;
            }

            MessageBox.Show(TakeLines(message, 30), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowErrorMessage(Exception? ex, bool fatal, bool thread = false)
        {
            string msg;
            if (fatal)
                msg = "Fatal exception occurred. The program will close.";
            else if (thread)
                msg = "Thread exception occurred.";
            else
                msg = "Exception occurred.";

            ShowErrorMessage(msg, ex);
        }

    }
}
