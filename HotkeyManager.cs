using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace SteamResChanger
{
    public class HotkeyManager : IDisposable
    {
        private readonly IntPtr _handle;
        private int _currentId = 0;

        private readonly Dictionary<int, Hotkey> _hotkeys = new();
        public ReadOnlyDictionary<int, Hotkey> Hotkeys => _hotkeys.AsReadOnly();

        public event EventHandler<HotkeyEventArgs>? HotkeyPressed;

        [YamlIgnore]
        public const string YamlPath = "hotkeys.yml";

        private static readonly Dictionary<IntPtr, HotkeyManager> _instances = new();

        public bool Any() => _hotkeys.Any();

        public HotkeyManager(IntPtr handle)
        {
            _handle = handle;
            _instances.Add(handle, this);
        }

        #region WinAPI

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        #endregion

        #region Public API

        public int Register(Keys key, ModifierKeys modifiers)
        {
            int id = _currentId++;

            if (!RegisterHotKey(_handle, id, (uint)modifiers, (uint)key))
                throw new InvalidOperationException("Failed to register hotkey.");

            _hotkeys[id] = new Hotkey(id, key, modifiers);
            return id;
        }

        public bool Unregister(int id)
        {
            if (!_hotkeys.ContainsKey(id)) return false;
            UnregisterHotKey(_handle, id);
            _hotkeys.Remove(id);
            return true;
        }

        public bool UnregisterAll()
        {
            bool any = false;
            foreach (var hotkey in _hotkeys.ToArray())
                any |= Unregister(hotkey.Key);
            return any;
        }

        public static bool UnregisterAllHandles()
        {
            bool any = false;
            foreach (var inst in _instances)
                any |= inst.Value.UnregisterAll();
            return any;
        }

        public int ChangeHotkey(int id, Keys newKey, ModifierKeys newModifiers)
        {
            if (!_hotkeys.ContainsKey(id))
                throw new ArgumentException("Hotkey ID not found.");

            // Remove old
            UnregisterHotKey(_handle, id);
            _hotkeys.Remove(id);

            // Register new
            if (!RegisterHotKey(_handle, id, (uint)newModifiers, (uint)newKey))
                throw new InvalidOperationException("Failed to register new hotkey.");

            _hotkeys[id] = new Hotkey(id, newKey, newModifiers);

            return id;
        }

        public void ProcessHotkey(Message m)
        {
            const int WM_HOTKEY = 0x0312;

            if (m.Msg != WM_HOTKEY)
                return;

            int id = m.WParam.ToInt32();

            if (_hotkeys.TryGetValue(id, out var hotkey))
            {
                HotkeyPressed?.Invoke(this, new HotkeyEventArgs(hotkey));
            }
        }

        public void Dispose()
        {
            foreach (var id in _hotkeys.Keys)
                UnregisterHotKey(_handle, id);

            _hotkeys.Clear();
        }

        #endregion
    }

    [Flags]
    public enum ModifierKeys
    {
        None = 0x0000,
        Alt = 0x0001,
        Control = 0x0002,
        Shift = 0x0004,
        Win = 0x0008,

        ControlShift = Control | Shift,
        ControlAlt = Control | Alt,
        ShiftAlt = Shift | Alt,
        ControlShiftAlt = Control | Shift | Alt,
    }

    public class Hotkey
    {
        [YamlIgnore]
        public int Id { get; set; } = -1;
        public Keys Key { get; set; }
        public ModifierKeys Modifiers { get; set; }

        public Hotkey() { }

        public Hotkey(Keys key, ModifierKeys modifiers)
        {
            Key = key;
            Modifiers = modifiers;
        }

        public Hotkey(int id, Keys key, ModifierKeys modifiers) : this(key, modifiers)
        {
            Id = id;
        }

        public override string ToString()
            => GetFriendlyName(this.Key, this.Modifiers);

        public static string GetFriendlyName(Keys key, ModifierKeys modifiers)
        {
            var sb = new StringBuilder();

            if (modifiers.HasFlag(ModifierKeys.Control))
                sb.Append("Ctrl + ");

            if (modifiers.HasFlag(ModifierKeys.Shift))
                sb.Append("Shift + ");

            if (modifiers.HasFlag(ModifierKeys.Alt))
                sb.Append("Alt + ");

            if (modifiers.HasFlag(ModifierKeys.Win))
                sb.Append("Win + ");

            if (key != Keys.None)
                sb.Append(GetKeyName(key));

            return sb.ToString();
        }

        private static string GetKeyName(Keys key) => key switch
        {
            Keys.ControlKey => "Ctrl",
            Keys.Menu => "Alt",
            Keys.ShiftKey => "Shift",
            Keys.Return => "Enter",
            Keys.Escape => "Esc",
            Keys.Space => "Space",
            Keys.Prior => "Page Up",
            Keys.Next => "Page Down",
            Keys.Capital => "Caps Lock",
            Keys.Back => "Backspace",
            Keys.LWin or Keys.RWin => "Win",
            >= Keys.D0 and <= Keys.D9 => ((char)key).ToString(),

            _ => key.ToString()
        };

        public static Hotkey FromString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input cannot be null or empty.");

            ModifierKeys modifiers = ModifierKeys.None;
            Keys key = Keys.None;

            var parts = input.Split('+', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            foreach (var rawPart in parts)
            {
                var part = rawPart.ToLowerInvariant();

                switch (part)
                {
                    case "ctrl":
                    case "control":
                        modifiers |= ModifierKeys.Control;
                        continue;

                    case "shift":
                        modifiers |= ModifierKeys.Shift;
                        continue;

                    case "alt":
                        modifiers |= ModifierKeys.Alt;
                        continue;

                    case "win":
                    case "windows":
                        modifiers |= ModifierKeys.Win;
                        continue;
                }

                // Not a modifier → must be the key
                key = ParseKey(part);
            }

            if (key == Keys.None)
                throw new FormatException("No valid key found in hotkey string.");

            return new(key, modifiers);
        }

        private static Keys ParseKey(string key) => key switch
        {
            "enter" => Keys.Return,
            "esc" or "escape" => Keys.Escape,
            "space" => Keys.Space,
            "page up" => Keys.Prior,
            "page down" => Keys.Next,
            "caps lock" => Keys.Capital,
            "backspace" => Keys.Back,
            "win" or "windows" => Keys.LWin,
            "none" or "(none)" => Keys.None,

            _ => TryParseEnum(key)
        };

        private static Keys TryParseEnum(string key)
        {
            if (key.Length == 1 && key[0] >= '0' && key[0] <= '9')
                return (Keys)key[0];

            if (Enum.TryParse<Keys>(key, ignoreCase: true, out var result))
                return result;

            throw new FormatException($"Unknown key: {key}");
        }

        public override bool Equals(object? obj)
            => obj is Hotkey other && Equals(other.Key, other.Modifiers);

        public bool Equals(Keys key, ModifierKeys modifiers)
            => Key == key && Modifiers == modifiers;

        public override int GetHashCode()
        {
            return HashCode.Combine(Key, Modifiers);
        }
    }

    public class HotkeyEventArgs : EventArgs
    {
        public Hotkey Hotkey { get; }

        public HotkeyEventArgs(Hotkey hotkey)
        {
            Hotkey = hotkey;
        }
    }
}
