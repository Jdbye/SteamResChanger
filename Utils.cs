using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamResChanger
{
    public static class Utils
    {
        public static string FormatTimeSpan(this TimeSpan ts)
        {
            var days = (int)ts.TotalDays;
            var hours = ts.Hours;
            var mins = ts.Minutes;
            var secs = ts.Seconds;

            if (days > 0)
                return $"{days}d {hours}h {mins}m {secs}s";
            if (hours > 0)
                return $"{hours}h {mins}m {secs}s";
            if (mins > 0)
                return $"{mins}m {secs}s";
            if (secs > 0)
                return $"{secs}s";

            return $"{ts.TotalMilliseconds}ms";
        }
    }
}
