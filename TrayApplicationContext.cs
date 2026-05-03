using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;

namespace SteamResChanger
{
    public class TrayApplicationContext : ApplicationContext
    {
        private static Form1? _form;
        private static NotifyIcon? _trayIcon;
        private ContextMenuStrip _menu;
        private ToolStripMenuItem _itmPause;
        private ToolStripMenuItem _itmActiveRes;
        private ToolStripMenuItem _itmGameRes;
        private ToolStripMenuItem _itmHdr;
        private ToolStripItem _itmHdrSeparator;
        private ToolStripMenuItem[] _itmPresetRes = Array.Empty<ToolStripMenuItem>();

        public TrayApplicationContext()
        {
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            _form = new Form1();

            _menu = new ContextMenuStrip();
            _menu.Margin = new(4);

            _menu.Items.Add(" &Settings", null, mnuSettings_Click);
            _itmPause = (ToolStripMenuItem)_menu.Items.Add(" &Pause", null, mnuPause_Click);

            _menu.Items.Add(new ToolStripSeparator());

            _itmActiveRes = (ToolStripMenuItem)_menu.Items.Add(" Active Resolution", null, null);
            _itmActiveRes.Enabled = false;
            _itmGameRes = (ToolStripMenuItem)_menu.Items.Add(" &Game Resolution", null, mnuResolution_Click);

            _menu.Items.Add(new ToolStripSeparator());

            _itmHdr = (ToolStripMenuItem)_menu.Items.Add(" &HDR", null, mnuHdr_Click);

            _itmHdrSeparator = new ToolStripSeparator();
            _menu.Items.Add(_itmHdrSeparator);

            _menu.Items.Add(" &Exit", null, mnuExit_Click);

            _menu.Opening += Menu_Opening;

            _trayIcon = new NotifyIcon
            {
                Icon = Icon.ExtractAssociatedIcon(Environment.ProcessPath!),
                Visible = true,
                Text = "Steam Resolution Changer",
                ContextMenuStrip = _menu,
            };

            _trayIcon.MouseDoubleClick += mnuSettings_Click;

            if (_form.Config.DesktopRes.Length == 0 || _form.Config.DesktopRes.All(res => res.Equals(_form.Config.GameRes, WithHdr.IfNotNull)))
            {
                _form.Show();
                MessageBox.Show(_form, "You have not configured the application yet.\n\nYou should set a desired game resolution (that is different from your desktop resolution)\n\nYou can optionally also add multiple desktop resolution presets that can be accessed from the tray icon.", "Steam Resolution Changer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Make absolutely sure the WMI ManagementEventWatcher always gets disposed properly.
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBoxExtensions.ShowErrorMessage(e.ExceptionObject as Exception, e.IsTerminating);

            if (e.IsTerminating)
            {
                HotkeyManager.UnregisterAllHandles();
                SteamHelper.UnmonitorAll();
            }
        }

        private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBoxExtensions.ShowErrorMessage(e.Exception, false, true);

            HotkeyManager.UnregisterAllHandles();
            SteamHelper.UnmonitorAll();
        }

        private void Menu_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_form != null)
            {
                _itmPause.Text = _form.IsPaused ? " &Paused" : " &Pause";
                _itmPause.Checked = _form.IsPaused;

                var activeRes = DisplayHelper.GetCurrentResolution();
                _itmActiveRes.Text = " Active Resolution: " + activeRes.ToString();
                _itmActiveRes.Tag = activeRes;

                _itmGameRes.Text = " &Game Resolution: " + _form.Config.GameRes.ToString();
                _itmGameRes.Checked = _form.Config.GameRes.Equals(activeRes, WithHdr.IfNotNull);
                _itmGameRes.Tag = _form.Config.GameRes;

                try
                {
                    var primaryDisplay = HdrHelper.GetDisplays().First();
                    _itmHdr.Visible = primaryDisplay.SupportsHDR;
                    _itmHdrSeparator.Visible = primaryDisplay.SupportsHDR;
                    _itmHdr.Checked = primaryDisplay.IsHDREnabled;
                }
                catch
                {
                    _itmHdr.Visible = false;
                    _itmHdrSeparator.Visible = false;
                }

                foreach (var itm in _itmPresetRes)
                    _menu.Items.Remove(itm);

                _itmPresetRes = _form.Config.DesktopRes.Select((res, index) => new ToolStripMenuItem($" &Preset {index + 1}: {res.ToString()}", null, mnuResolution_Click) { Checked = res.Equals(activeRes, WithHdr.IfNotNull), Tag = res }).ToArray();

                var startIndex = _menu.Items.IndexOf(_itmGameRes) + 1;

                foreach (var itm in _itmPresetRes)
                    _menu.Items.Insert(startIndex++, itm);
            }
        }

        private void mnuSettings_Click(object? sender, EventArgs e)
        {
            if (_form == null || _form.IsDisposed)
                _form = new();

            _form.Visible = !_form.Visible;
        }

        private void mnuPause_Click(object? sender, EventArgs e)
        {
            if (_form != null)
                _form.IsPaused = !_form.IsPaused;
        }

        private void mnuHdr_Click(object? sender, EventArgs e)
        {
            var enableHdr = !((ToolStripMenuItem)sender!).Checked;
            try { HdrHelper.SetHDRStateForDisplay(0, enableHdr); }
            catch (Exception ex)
            {
                MessageBoxExtensions.ShowErrorMessage($"Failed to set HDR to {enableHdr.ToString()}", ex);
            }
        }

        private void mnuResolution_Click(object? sender, EventArgs e)
        {
            var res = (DisplayMode)((ToolStripMenuItem)sender!).Tag!;
            DisplayHelper.SetResolution(res, _form?.Config.ShowTooltip ?? true);

            if (res.EnableHdr != null)
            {
                try { HdrHelper.SetHDRStateForDisplay(0, res.EnableHdr.Value); }
                catch (Exception ex)
                {
                    MessageBoxExtensions.ShowErrorMessage($"Failed to set HDR to {res.EnableHdr.Value.ToString()}", ex);
                }
            }
        }

        private void mnuExit_Click(object? sender, EventArgs e)
        {
            if (_form != null)
            {
                _form.IsExiting = true;
                if (!_form.IsDisposed)
                {
                    _form.Close();
                    _form.Dispose();
                }
                _form = null;
            }

            _trayIcon?.Dispose();
            _trayIcon = null;

            HotkeyManager.UnregisterAllHandles();
            SteamHelper.UnmonitorAll();

            ExitThread(); // this actually stops Application.Run()
        }

        public static void ShowBalloonTip(string message, string title, ToolTipIcon icon = ToolTipIcon.None, int timeout = 5000)
            => _trayIcon?.ShowBalloonTip(timeout, title, message, icon);
    }
}
