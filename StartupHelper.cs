using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamResChanger
{
    public static class StartupHelper
    {
        private const string RunKey =
            @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

        /// <summary>
        /// Adds the current application to Windows startup.
        /// </summary>
        public static void AddToStartup(string appName)
        {
            using var key = Registry.CurrentUser.OpenSubKey(RunKey, writable: true);

            if (key == null)
                throw new InvalidOperationException("Cannot open startup registry key.");

            key.SetValue(appName, $"\"{GetExecutablePath()}\"");
        }

        /// <summary>
        /// Removes the application from Windows startup.
        /// </summary>
        public static void RemoveFromStartup(string appName)
        {
            using var key = Registry.CurrentUser.OpenSubKey(RunKey, writable: true);

            if (key == null)
                return;

            if (key.GetValue(appName) != null)
                key.DeleteValue(appName, throwOnMissingValue: false);
        }

        /// <summary>
        /// Checks if the application is set to run at startup.
        /// </summary>
        public static bool IsInStartup(string appName)
        {
            using var key = Registry.CurrentUser.OpenSubKey(RunKey, writable: false);

            if (key == null)
                return false;

            var value = key.GetValue(appName);

            return value is string s && s.Equals($"\"{GetExecutablePath()}\"", StringComparison.InvariantCultureIgnoreCase);
        }

        private static string GetExecutablePath()
        {
            return Process.GetCurrentProcess().MainModule!.FileName!;
        }
    }
}
