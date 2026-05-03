using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        {
            var current = DisplayHelperWindows.GetCurrentResolution(null);
            var display = HdrHelper.GetDisplays().First();
            return current.SetHDR(display.IsHDREnabled, display.SupportsHDR);
        }

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

    public enum WithHdr { Never, IfNotNull, Always }

    public class DisplayModeComparer : IEqualityComparer, IEqualityComparer<DisplayMode?>
    {
        public WithHdr _WithHdr { get; }

        public new bool Equals(object? x, object? y)
        {
            if (x is not DisplayMode && y is not DisplayMode)
                return object.Equals(x, y);

            return Equals(x as DisplayMode, y as DisplayMode);
        }

        public bool Equals(DisplayMode? x, DisplayMode? y)
        {
            if (object.ReferenceEquals(x, y)) return true;
            if (x is DisplayMode dmx) return dmx.Equals(y, _WithHdr);
            if (y is DisplayMode dmy) return dmy.Equals(x, _WithHdr);
            return false;
        }

        public int GetHashCode(object obj)
        {
            if (obj is not DisplayMode dm)
                return obj.GetHashCode();
            return GetHashCode(dm);
        }

        public int GetHashCode([DisallowNull] DisplayMode? obj)
            => obj.GetHashCode(_WithHdr);

        private DisplayModeComparer(WithHdr withHdr)
            => _WithHdr = withHdr;

        public static DisplayModeComparer WithHdrIfNotNull { get; } = new(SteamResChanger.WithHdr.IfNotNull);
        public static DisplayModeComparer WithHdr { get; } = new(SteamResChanger.WithHdr.Always);
    }

    public class DisplayModeBase
    {
        public virtual uint Width => 0;
        public virtual uint Height => 0;
        public virtual uint Frequency => 0;
        public virtual bool? EnableHdr => null;
        public virtual Hotkey? Hotkey { get => null; protected set { } }

        public CheckState EnableHdrCheckState
        {
            get => EnableHdr switch
            {
                false => CheckState.Unchecked,
                true => CheckState.Checked,
                _ => CheckState.Indeterminate,
            };
        }

        protected static bool? EnableHdrFromCheckState(CheckState checkState) => checkState switch
        {
            CheckState.Unchecked => false,
            CheckState.Checked => true,
            _ => null,
        };

        public override bool Equals(object? obj)
            => Equals(obj, WithHdr.Never);

        public bool Equals(object? obj, WithHdr withHdr)
        {
            if (object.ReferenceEquals(this, obj)) return true;
            if (obj is not DisplayModeBase other) return false;

            if (withHdr == WithHdr.Never
                || (withHdr == WithHdr.IfNotNull && (EnableHdr == null || other.EnableHdr == null)))
                return Equals(other.Width, other.Height, other.Frequency);
            return Equals(other.Width, other.Height, other.Frequency, other.EnableHdr);
        }

        public bool Equals(uint width, uint height, uint frequency)
        {
            return Width == width &&
                   Height == height &&
                   Frequency == frequency;
        }

        public bool Equals(int width, int height, int frequency)
        {
            return Width == width &&
                   Height == height &&
                   Frequency == frequency;
        }

        public bool Equals(uint width, uint height, uint frequency, bool? enableHdr)
        {
            return Width == width &&
                   Height == height &&
                   Frequency == frequency &&
                   EnableHdr == enableHdr;
        }

        public bool Equals(int width, int height, int frequency, bool? enableHdr)
        {
            return Width == width &&
                   Height == height &&
                   Frequency == frequency &&
                   EnableHdr == enableHdr;
        }

        public override int GetHashCode()
            => GetHashCode(WithHdr.Never);

        public int GetHashCode(WithHdr withHdr)
        {
            if (withHdr == WithHdr.Never
                || (withHdr == WithHdr.IfNotNull && EnableHdr == null))
                return HashCode.Combine(Width, Height, Frequency);
            return HashCode.Combine(Width, Height, Frequency, EnableHdr);
        }

        public override string ToString()
            => ToString(this.Width, this.Height, this.Frequency, this.EnableHdr, this.Hotkey);

        public static string ToString(int width, int height, int frequency, bool? enableHdr, Hotkey? hotkey)
            => $"{width} x {height} @ {frequency}Hz{(enableHdr == true ? " (HDR)" : enableHdr == false ? " (No HDR)" : "")}{(hotkey != null ? $" [ {hotkey.ToString()} ]" : "")}";

        public static string ToString(uint width, uint height, uint frequency, bool? enableHdr, Hotkey? hotkey)
            => $"{width} x {height} @ {frequency}Hz{(enableHdr == true ? " (HDR)" : enableHdr == false ? " (No HDR)" : "")}{(hotkey != null ? $" [ {hotkey.ToString()} ]" : "")}";
    }

    public class DisplayMode : DisplayModeBase
    {
        public static readonly DisplayModeBase None = new();

        public override bool? EnableHdr => _enableHdr;
        private bool? _enableHdr = null;

        public bool? SupportsHdr { get; } = null;

        public override Hotkey? Hotkey
        {
            get => _hotkey;
            protected set => _hotkey = value;
        }
        private Hotkey? _hotkey;

        public void RegisterHotkey(HotkeyManager manager, Keys defaultKey = Keys.None, ModifierKeys modifiers = ModifierKeys.None)
        {
            if (_hotkey == null && defaultKey != Keys.None)
                _hotkey = new(defaultKey, modifiers);

            if (_hotkey != null && _hotkey.Key != Keys.None)
                _hotkey.Id = manager.Register(_hotkey.Key, _hotkey.Modifiers);
        }

        private void UnregisterHotkey(HotkeyManager manager)
        {
            if (_hotkey != null)
            {
                if (_hotkey.Id >= 0)
                    manager.Unregister(_hotkey.Id);
                _hotkey.Id = -1;
            }
        }

        public DisplayMode ChangeHotkey(Hotkey? oldHotkey, HotkeyManager manager, Keys newKey, ModifierKeys modifiers = ModifierKeys.None)
        {
            var dm = SetHotkey(oldHotkey);
            dm.ChangeHotkey(manager, newKey, modifiers);
            return dm;
        }

        public DisplayMode ChangeHotkey(HotkeyManager manager, Keys newKey, ModifierKeys modifiers = ModifierKeys.None)
        {
            if (newKey != _hotkey?.Key || modifiers != _hotkey?.Modifiers)
            {
                if (_hotkey != null && _hotkey.Id >= 0)
                {
                    if (newKey != Keys.None)
                        manager.ChangeHotkey(_hotkey.Id, _hotkey.Key, _hotkey.Modifiers);
                    else
                        UnregisterHotkey(manager);

                    _hotkey.Key = newKey;
                    _hotkey.Modifiers = modifiers;
                }
                else
                {
                    _hotkey = new(newKey, modifiers);
                    if (newKey != Keys.None)
                        RegisterHotkey(manager);
                }
            }
            return this;
        }

        public virtual DisplayMode SetHotkey(Hotkey? hotkey)
            => new(hotkey, EnableHdr, SupportsHdr);

        public DisplayMode SetHDR(CheckState enableHdrState, bool? supportsHdr = null)
            => SetHDR(EnableHdrFromCheckState(enableHdrState), supportsHdr);

        public virtual DisplayMode SetHDR(bool? enableHdr, bool? supportsHdr = null)
            => new(Hotkey, enableHdr, supportsHdr);

        protected DisplayMode(Hotkey? hotkey, bool? enableHdr, bool? supportsHdr = null)
        {
            _hotkey = hotkey;
            _enableHdr = enableHdr;
            SupportsHdr = supportsHdr;
        }

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
                @"(?<w>\d+)\s*x\s*(?<h>\d+)\s*[@x]\s*(?<f>\d+)\s*(?:Hz)?\s*(?:\(?(?<hdr>HDR)?(?<nohdr>No\s*HDR)?\)?)?\s*(?:\[\s*(?<hotkey>.*)\s*\])?",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

            if (!match.Success)
                throw new FormatException($"Invalid display mode format: '{value}'");

            int width = int.Parse(match.Groups["w"].Value, CultureInfo.InvariantCulture);
            int height = int.Parse(match.Groups["h"].Value, CultureInfo.InvariantCulture);
            int frequency = int.Parse(match.Groups["f"].Value, CultureInfo.InvariantCulture);
            bool? hdr =
                match.Groups.ContainsKey("hdr") && match.Groups["hdr"].Success ? true
                : match.Groups.ContainsKey("nohdr") && match.Groups["nohdr"].Success ? false
                : null;

            Hotkey? hotkey =
                match.Groups.ContainsKey("hotkey") && match.Groups["hotkey"].Success ? Hotkey.FromString(match.Groups["hotkey"].Value) : null;

            if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width), "Width must be greater than 0");
            if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height), "Height must be greater than 0");
            if (frequency <= 0) throw new ArgumentOutOfRangeException(nameof(frequency), "Frequency must be greater than 0");

            DisplayMode? parsed = supportedResolutions.FirstOrDefault(res => res.Equals(width, height, frequency));

            if (parsed == null)
                throw new ArgumentException($"Not a supported display mode: '{DisplayMode.ToString(width, height, frequency, hdr, hotkey)}'", nameof(value));

            if (hdr != null)
                parsed = parsed.SetHDR(hdr.Value);

            parsed.Hotkey = hotkey;

            return parsed;
        }
    }

    public class DisplayModeWindows : DisplayMode
    {
        public DeviceMode DevMode { get; }

        public override uint Width => DevMode.PixelsWidth;
        public override uint Height => DevMode.PixelsHeight;
        public override uint Frequency => DevMode.DisplayFrequency;

        public override DisplayMode SetHotkey(Hotkey? hotkey)
            => new DisplayModeWindows(DevMode, hotkey, EnableHdr, SupportsHdr);

        public override DisplayMode SetHDR(bool? enableHdr, bool? supportsHdr = null)
            => new DisplayModeWindows(DevMode, Hotkey, enableHdr, supportsHdr);

        public DisplayModeWindows(DeviceMode dm, Hotkey? hotkey = null, bool? enableHdr = null, bool? supportsHdr = null) : base(hotkey, enableHdr, supportsHdr)
            => DevMode = dm;
    }
}
