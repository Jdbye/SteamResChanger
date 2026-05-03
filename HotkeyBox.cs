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
            Modifiers = modifiers;
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

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (!_isRecording)
                return;

            e.SuppressKeyPress = true;
            e.Handled = true;

            // Cancel
            if (e.KeyCode == Keys.Escape)
            {
                StopRecording(cancel: true);
                return;
            }

            var win = IsWinPressed();

            // Clear
            if ((e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete) && !e.Control && !e.Shift && !e.Alt && !win)
            {
                Key = Keys.None;
                Modifiers = SteamResChanger.ModifierKeys.None;
                UpdateText();
                return;
            }

            // Capture modifiers
            Modifiers = SteamResChanger.ModifierKeys.None;

            if (e.Control) Modifiers |= SteamResChanger.ModifierKeys.Control;
            if (e.Shift) Modifiers |= SteamResChanger.ModifierKeys.Shift;
            if (e.Alt) Modifiers |= SteamResChanger.ModifierKeys.Alt;
            if (win) Modifiers |= SteamResChanger.ModifierKeys.Win;

            Key = IsModifierKey(e.KeyCode) ? Keys.None : e.KeyCode;

            UpdateText();
            if (Key != Keys.None)
                HotkeyChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Prevent normal textbox shortcuts like Ctrl+C, Ctrl+V
            if (_isRecording)
                return true;

            return base.ProcessCmdKey(ref msg, keyData);
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
            return (Control.ModifierKeys & Keys.LWin) == Keys.LWin ||
                   (Control.ModifierKeys & Keys.RWin) == Keys.RWin;
        }
    }
}
