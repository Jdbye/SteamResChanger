using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using static SteamResChanger.DisplayHelperWindows;

namespace SteamResChanger
{
    public static class DisplayHelper
    {
        public static IEnumerable<DisplayMode> GetResolutions()
            => DisplayHelperWindows.GetResolutions(null);

        public static DisplayMode GetCurrentResolution()
            => DisplayHelperWindows.GetCurrentResolution(null);

        public static bool SetResolution(DisplayMode mode, bool showTooltip)
        {
            if (showTooltip)
                TrayApplicationContext.ShowBalloonTip($"Resolution: " + mode.ToString(), "Steam Resolution Changer", ToolTipIcon.Info);

            if (mode is DisplayModeWindows dm)
                return DisplayHelperWindows.SetResolution(null, dm);

            return false;
        }
    }

    public static class DisplayHelperWindows
    {
        [DllImport("user32.dll")]
        private static extern bool EnumDisplaySettings(string? deviceName, DisplaySettingsMode mode, ref DeviceMode devMode);

        [DllImport("user32.dll")]
        private static extern ChangeDisplaySettingsExResults ChangeDisplaySettingsEx(string? deviceName, ref DeviceMode devMode, IntPtr hWnd, ChangeDisplaySettingsFlags flags, IntPtr param);

        public static IEnumerable<DisplayModeWindows> GetResolutions(string? displayName)
        {
            var dm = new DeviceMode();
            int modeIndex = 0;

            while (EnumDisplaySettings(displayName, (DisplaySettingsMode)modeIndex, ref dm))
            {
                if (dm.BitsPerPixel >= 32)
                    yield return new(dm);
                modeIndex++;
            }
        }

        public static DisplayModeWindows GetCurrentResolution(string? displayName)
        {
            var dm = new DeviceMode();

            EnumDisplaySettings(displayName, DisplaySettingsMode.CurrentSettings, ref dm);

            return new(dm);
        }

        public static bool SetResolution(string? displayName, DisplayModeWindows mode, bool? reset = null)
        {
            var dm = GetChangedDeviceMode(mode.DevMode, displayName);

            var flags = ChangeDisplaySettingsFlags.UpdateRegistry | ChangeDisplaySettingsFlags.Global;
            if (reset != null)
                flags |= reset.Value ? ChangeDisplaySettingsFlags.Reset : ChangeDisplaySettingsFlags.NoReset;

            if (dm.Position.X == 0 && dm.Position.Y == 0)
                flags |= ChangeDisplaySettingsFlags.SetPrimary;

            var result = ChangeDisplaySettingsEx(displayName, ref dm, IntPtr.Zero, flags, IntPtr.Zero);

            return result == ChangeDisplaySettingsExResults.Successful;
        }

        private static DeviceMode GetChangedDeviceMode(DeviceMode existingDeviceMode, string? displayName)
        {
            var flags = DisplayFlags.None;

            if (existingDeviceMode.DisplayFlags.HasFlag(DisplayFlags.Interlaced))
                flags |= DisplayFlags.Interlaced;

            var deviceMode = new DeviceMode(
                displayName,
                existingDeviceMode.Position,
                existingDeviceMode.DisplayOrientation,
                existingDeviceMode.DisplayFixedOutput,
                existingDeviceMode.BitsPerPixel,
                existingDeviceMode.PixelsWidth,
                existingDeviceMode.PixelsHeight,
                flags,
                existingDeviceMode.DisplayFrequency
            );

            return deviceMode;
        }
    }

    public class DisplayModeBase
    {
        public virtual uint Width => 0;
        public virtual uint Height => 0;
        public virtual uint Frequency => 0;

        public override bool Equals(object? obj)
        {
            return obj is DisplayModeBase mode &&
                   Width == mode.Width &&
                   Height == mode.Height &&
                   Frequency == mode.Frequency;
        }

        public bool Equals(int width, int height, int frequency)
        {
            return Width == width &&
                   Height == height &&
                   Frequency == frequency;
        }

        public override int GetHashCode()
            => HashCode.Combine(Width, Height, Frequency);

        public override string ToString()
            => ToString(this.Width, this.Height, this.Frequency);

        public static string ToString(int width, int height, int frequency)
            => $"{width} x {height} @ {frequency}Hz";

        public static string ToString(uint width, uint height, uint frequency)
            => $"{width} x {height} @ {frequency}Hz";
    }

    public class DisplayMode : DisplayModeBase
    {
        public static readonly DisplayModeBase None = new();

        public static DisplayMode? TryFromString(string value, ICollection<DisplayMode> supportedResolutions)
        {
            try
            {
                return FromString(value, supportedResolutions);
            }
            catch
            {
                return null;
            }
        }

        public static DisplayMode FromString(string value, IEnumerable<DisplayMode> supportedResolutions)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value cannot be null or empty.", nameof(value));

            // Robust parsing using regex
            var match = Regex.Match(value,
                @"(?<w>\d+)\s*x\s*(?<h>\d+)\s*[@x]\s*(?<f>\d+)\s*(?:Hz)?",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

            if (!match.Success)
                throw new FormatException($"Invalid display mode format: '{value}'");

            int width = int.Parse(match.Groups["w"].Value, CultureInfo.InvariantCulture);
            int height = int.Parse(match.Groups["h"].Value, CultureInfo.InvariantCulture);
            int frequency = int.Parse(match.Groups["f"].Value, CultureInfo.InvariantCulture);

            if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width), "Width must be greater than 0");
            if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height), "Height must be greater than 0");
            if (frequency <= 0) throw new ArgumentOutOfRangeException(nameof(frequency), "Frequency must be greater than 0");

            DisplayMode? parsed = supportedResolutions.FirstOrDefault(res => res.Equals(width, height, frequency));

            if (parsed == null)
                throw new ArgumentException($"Not a supported display mode: '{DisplayMode.ToString(width, height, frequency)}'", nameof(value));

            return parsed;
        }
    }

    public class DisplayModeWindows : DisplayMode
    {
        public DeviceMode DevMode { get; }

        public override uint Width => DevMode.PixelsWidth;
        public override uint Height => DevMode.PixelsHeight;
        public override uint Frequency => DevMode.DisplayFrequency;

        public DisplayModeWindows(DeviceMode dm)
            => DevMode = dm;
    }
}
