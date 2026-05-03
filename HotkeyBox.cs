using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamResChanger
{
    public class HotkeyBox : TextBox
    {
        public Keys Key { get; private set; }
        public ModifierKeys Modifiers { get; private set; }

        public event EventHandler? HotkeyChanged;

        private bool _isRecording;

        public HotkeyBox()
        {
            ReadOnly = true;
            TabStop = true;
            Text = "Click to set hotkey ...";
        }

        public void SetHotkey(Keys key, ModifierKeys modifiers)
        {
            Key = key;
            Modifiers = key != Keys.None ? modifiers : SteamResChanger.ModifierKeys.None;
            UpdateText();
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            StartRecording();
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            StopRecording(cancel: true);
        }

        protected override bool IsInputKey(Keys keyData)
            => true;

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (_isRecording)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                HandleKey(e.KeyData);
                return;
            }

            base.OnKeyDown(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Prevent normal textbox shortcuts like Ctrl+C, Ctrl+V
            if (_isRecording)
            {
                HandleKey(keyData);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected void HandleKey(Keys keyData)
        {
            Keys key = keyData & Keys.KeyCode;

            bool ctrl = keyData.HasFlag(Keys.Control);
            bool shift = keyData.HasFlag(Keys.Shift);
            bool alt = keyData.HasFlag(Keys.Alt);

            // Cancel
            if (key == Keys.Escape)
            {
                StopRecording(cancel: true);
                return;
            }

            var win = IsWinPressed();

            // Clear
            if ((key == Keys.Back || key == Keys.Delete || key == Keys.Escape) && !ctrl && !shift && !alt && !win)
            {
                Key = Keys.None;
                Modifiers = SteamResChanger.ModifierKeys.None;
                UpdateText();
                return;
            }

            // Capture modifiers
            Modifiers = SteamResChanger.ModifierKeys.None;

            if (ctrl) Modifiers |= SteamResChanger.ModifierKeys.Control;
            if (shift) Modifiers |= SteamResChanger.ModifierKeys.Shift;
            if (alt) Modifiers |= SteamResChanger.ModifierKeys.Alt;
            if (win) Modifiers |= SteamResChanger.ModifierKeys.Win;

            Key = IsModifierKey(key) ? Keys.None : key;

            UpdateText();
            if (Key != Keys.None)
                HotkeyChanged?.Invoke(this, EventArgs.Empty);
        }

        private void StartRecording()
        {
            _isRecording = true;
            Text = "Press hotkey ...";
        }

        private void StopRecording(bool cancel)
        {
            _isRecording = false;

            if (cancel)
                UpdateText();
        }

        private void UpdateText()
        {
            if (Key == Keys.None && Modifiers == SteamResChanger.ModifierKeys.None)
            {
                Text = "(None)";
                return;
            }

            Text = Hotkey.GetFriendlyName(Key, Modifiers);
        }

        private static bool IsModifierKey(Keys key)
        {
            return key == Keys.ControlKey ||
                   key == Keys.ShiftKey ||
                   key == Keys.Menu ||
                   key == Keys.LWin ||
                   key == Keys.RWin;
        }

        private static bool IsWinPressed()
        {
            return Control.ModifierKeys.HasFlag(Keys.LWin)
                || Control.ModifierKeys.HasFlag(Keys.RWin);
        }
    }
}
