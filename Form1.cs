
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using YamlDotNet.Core.Tokens;

namespace SteamResChanger
{
    public partial class Form1 : Form
    {
        const string appName = "SteamResChanger";

        public Config Config { get; private set; }
        public HotkeyManager Hotkeys { get; private set; }
        public bool IsPaused { get; set; } = false;
        public bool IsExiting { get; set; } = false;

        readonly ComboBox[] comboPresets;
        readonly CheckBox[] chkPresetsHDR;
        readonly HotkeyBox[] hotkeyPresets;
        readonly Keys[] defaultHotkeys;

        public SteamGameState steamGameState { get; private set; }

        public DisplayModeBase[] _none = [DisplayMode.None];

        public DisplayMode? resBeforeGameStarted;

#if DEBUG
        Stopwatch watch;
#endif

        public Form1()
        {
            Visible = false;
            //ShowInTaskbar = false;

            var _resolutions = DisplayHelper.GetResolutions().ToArray()
                .OrderByDescending(m => m.Height * m.Width).ThenBy(m => m.Frequency).Distinct().ToArray();

            Config = Config.Load(_resolutions);
            var currentRes = Config.ResAtStart;

            steamGameState = SteamHelper.GetSteamGameState(Config.IgnoreVrGames);
#if DEBUG
            KeyPreview = true;
            watch = Stopwatch.StartNew();
#endif

            if (!IsPaused && !steamGameState.IsRunning() && currentRes.Equals(Config.GameRes, WithHdr.IfNotNull) && !Config.DesktopRes.Any(res => currentRes.Equals(res, WithHdr.IfNotNull)))
            {
                var res = Config.DesktopRes.First();
                DisplayHelper.SetResolution(res, Config.ShowTooltip);

                if (res.EnableHdr != null && currentRes.SupportsHdr == true)
                {
                    try { HdrHelper.SetHDRStateForDisplay(0, res.EnableHdr.Value); }
                    catch (Exception ex)
                    {
                        MessageBoxExtensions.ShowErrorMessage($"Failed to set HDR to {res.EnableHdr.Value.ToString()}", ex);
                    }
                }
            }

            InitializeComponent();
            comboPresets = [comboPreset1, comboPreset2, comboPreset3, comboPreset4, comboPreset5, comboPreset6, comboPreset7, comboPreset8, comboPreset9, comboPreset0];
            chkPresetsHDR = [chkPresetHDR1, chkPresetHDR2, chkPresetHDR3, chkPresetHDR4, chkPresetHDR5, chkPresetHDR6, chkPresetHDR7, chkPresetHDR8, chkPresetHDR9, chkPresetHDR0];
            hotkeyPresets = [hotkeyPreset1, hotkeyPreset2, hotkeyPreset3, hotkeyPreset4, hotkeyPreset5, hotkeyPreset6, hotkeyPreset7, hotkeyPreset8, hotkeyPreset9, hotkeyPreset0];
            defaultHotkeys = [Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9, Keys.D0];

            Hotkeys = new(this.Handle);
            Hotkeys.HotkeyPressed += Hotkeys_HotkeyPressed;

            Config.GameRes.RegisterHotkey(Hotkeys, Keys.G, SteamResChanger.ModifierKeys.ControlShift);

            for (int i = 0; i < Config.DesktopRes.Length; i++)
                Config.DesktopRes[i].RegisterHotkey(Hotkeys, defaultHotkeys[i], SteamResChanger.ModifierKeys.ControlShift);

            if (Config.HdrToggle.Key != Keys.None && currentRes.SupportsHdr == true)
                Config.HdrToggle.Id = Hotkeys.Register(Config.HdrToggle.Key, Config.HdrToggle.Modifiers);
        }

        protected override void WndProc(ref Message m)
        {
            Hotkeys?.ProcessHotkey(m);
            base.WndProc(ref m);
        }

        private void Hotkeys_HotkeyPressed(object? sender, HotkeyEventArgs e)
        {
            DisplayMode? dm = null;

            if (e.Hotkey.Id == Config.GameRes.Hotkey?.Id)
                dm = Config.GameRes;
            else
            {
                foreach (var res in Config.DesktopRes)
                {
                    if (e.Hotkey.Id == res.Hotkey?.Id)
                    {
                        dm = res;
                        break;
                    }
                }
            }

            if (dm != null)
            {
                DisplayHelper.SetResolution(dm, Config.ShowTooltip);

                if (dm.EnableHdr != null)
                {
                    try { HdrHelper.SetHDRStateForDisplay(0, dm.EnableHdr.Value); }
                    catch (Exception ex)
                    {
                        MessageBoxExtensions.ShowErrorMessage($"Failed to set HDR to {dm.EnableHdr.Value.ToString()}", ex);
                    }
                }
            }
            else if (e.Hotkey.Id == Config.HdrToggle.Id)
            {
                var enableHdr = !HdrHelper.GetDisplays().First().IsHDREnabled;
                try { HdrHelper.SetHDRStateForDisplay(0, enableHdr); }
                catch (Exception ex)
                {
                    MessageBoxExtensions.ShowErrorMessage($"Failed to set HDR to {enableHdr.ToString()}", ex);
                }
            }
        }

        private void SetComboBoxItems(ComboBox combo, CheckBox hdr, HotkeyBox hotkeyBox, Keys defaultHotkey, IEnumerable items, object? selected)
        {
            combo.Items.Clear();
            foreach (var item in items)
                combo.Items.Add(item);

            if (selected == null)
            {
                combo.SelectedItem = null;
                return;
            }

            if (selected is DisplayModeBase dm)
            {
                if (!hdr.Visible)
                    hdr.CheckState = CheckState.Indeterminate;
                else
                    hdr.CheckState = dm.EnableHdrCheckState;

                if (dm.Hotkey != null)
                    hotkeyBox.SetHotkey(dm.Hotkey.Key, dm.Hotkey.Modifiers);
                else
                    hotkeyBox.SetHotkey(defaultHotkey, SteamResChanger.ModifierKeys.ControlShift);
            }

            foreach (var item in items)
            {
                if (selected.Equals(item))
                {
                    combo.SelectedItem = item;
                    return;
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsExiting)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                Hide();
        }

        private void Form1_VisibleChanged(object sender, EventArgs e)
        {
            //ShowInTaskbar = Visible;
            if (Visible)
            {
                DisplayMode[] _resolutions = DisplayHelper.GetResolutions().ToArray()
                    .OrderByDescending(m => m.Height * m.Width).ThenBy(m => m.Frequency).Distinct().ToArray();
                Config.SupportedResolutions = _resolutions;

                DisplayModeBase[] _resolutionsWithNone = _none.Concat(_resolutions).ToArray();

                SetComboBoxItems(comboGameRes, chkGameHDR, hotkeyGame, Keys.G, _resolutionsWithNone, Config.GameRes);

                chkShowTooltip.Checked = Config.ShowTooltip;
                chkIgnoreVrGames.Checked = Config.IgnoreVrGames;

                for (int i = 0; i < comboPresets.Length; i++)
                    SetComboBoxItems(comboPresets[i], chkPresetsHDR[i], hotkeyPresets[i], defaultHotkeys[i], _resolutionsWithNone, i < Config.DesktopRes.Length ? Config.DesktopRes[i] : DisplayMode.None);

                hotkeyHDR.SetHotkey(Config.HdrToggle.Key, Config.HdrToggle.Modifiers);

                chkRunOnStartup.Checked = StartupHelper.IsInStartup(appName);

                ShowHDR(HdrHelper.GetDisplays().First().SupportsHDR);

                Show();
                WindowState = FormWindowState.Normal;
                Activate();
            }
            else
            {
                if (Config.ShowTooltip)
                    TrayApplicationContext.ShowBalloonTip("Program is still running in the tray.\nYou can right click the tray icon any time to change the resolution or settings.", "Steam Resolution Changer", ToolTipIcon.Info, 3000);
            }
        }

        public void ShowHDR(bool show)
        {
            var combo = comboGameRes;
            var chk = chkGameHDR;

            if (chk.Visible != show)
            {
                chk.Visible = show;
                if (show)
                    combo.Width -= 104;
                else
                    combo.Width += 104;
            }

            for (int i = 0; i < comboPresets.Length; i++)
            {
                combo = comboPresets[i];
                chk = chkPresetsHDR[i];

                if (chk.Visible != show)
                {
                    chk.Visible = show;
                    if (show)
                        combo.Width -= 104;
                    else
                        combo.Width += 104;
                }
            }

            if (hotkeyHDR.Visible != show)
            {
                labelHotkeyHDR.Visible = show;
                hotkeyHDR.Visible = show;

                if (show)
                    this.Height += 47;
                else
                    this.Height -= 47;
            }
        }

        private void butApply_Click(object sender, EventArgs e)
        {
            var currentRes = DisplayHelper.GetCurrentResolution();
            var defaultRes = currentRes.Equals(Config.GameRes, WithHdr.IfNotNull) ? Config.ResAtStart : currentRes;

            Config.GameRes = (comboGameRes.SelectedItem as DisplayMode ?? defaultRes)
                .SetHDR(chkGameHDR.CheckState)
                .ChangeHotkey(Config.GameRes.Hotkey, Hotkeys, hotkeyGame.Key, hotkeyGame.Modifiers);

            Config.ShowTooltip = chkShowTooltip.Checked;
            Config.IgnoreVrGames = chkIgnoreVrGames.Checked;

            var modes = comboPresets.Select(combo => combo.SelectedItem).ToArray();
            for (int i = 0; i < modes.Length; i++)
            {
                if (modes[i] is DisplayMode dm)
                {
                    var hotkeyBox = hotkeyPresets[i];
                    modes[i] = dm
                        .SetHDR(chkPresetsHDR[i].CheckState)
                        .ChangeHotkey(i < Config.DesktopRes.Length ? Config.DesktopRes[i].Hotkey : null, Hotkeys, hotkeyBox.Key, hotkeyBox.Modifiers);
                }
            }

            Config.DesktopRes = modes.OfType<DisplayMode>().DefaultIfEmpty(defaultRes).Distinct(DisplayModeComparer.WithHdr).ToArray();

            if (!hotkeyHDR.Key.Equals(Config.HdrToggle.Key) || !hotkeyHDR.Modifiers.Equals(Config.HdrToggle.Modifiers))
            {
                Config.HdrToggle.Key = hotkeyHDR.Key;
                Config.HdrToggle.Modifiers = hotkeyHDR.Modifiers;

                if (Config.HdrToggle.Key != Keys.None && currentRes.SupportsHdr == true)
                {
                    if (Config.HdrToggle.Id >= 0)
                        Hotkeys.ChangeHotkey(Config.HdrToggle.Id, Config.HdrToggle.Key, Config.HdrToggle.Modifiers);
                    else
                        Config.HdrToggle.Id = Hotkeys.Register(Config.HdrToggle.Key, Config.HdrToggle.Modifiers);
                }
                else if (Config.HdrToggle.Id >= 0)
                {
                    Hotkeys.Unregister(Config.HdrToggle.Id);
                    Config.HdrToggle.Id = -1;
                }
            }

            Config.Save();

            if (chkRunOnStartup.Checked != StartupHelper.IsInStartup(appName))
            {
                if (chkRunOnStartup.Checked)
                    StartupHelper.AddToStartup(appName);
                else
                    StartupHelper.RemoveFromStartup(appName);
            }
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            butApply_Click(sender, e);
            Hide();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void comboRes_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem == null || DisplayMode.None.Equals(e.ListItem))
                e.Value = "(None)";
        }

        private void timerCheckGame_Tick(object sender, EventArgs e)
        {
            if (IsPaused) return;

            var oldState = steamGameState;
            var newState = SteamHelper.GetSteamGameState(Config.IgnoreVrGames);

            if (oldState != newState)
            {
#if DEBUG
                var elapsed = watch.Elapsed;
                Debug.WriteLine($"State: {(oldState < 0 ? oldState.ToString() : "AppID: " + (int)oldState)} -> {(newState < 0 ? newState.ToString() : "AppID: " + (int)newState)} ({elapsed.FormatTimeSpan()})");
                watch = Stopwatch.StartNew();
#endif

                if (SteamGameStateExtensions.IsChanged(oldState, newState))
                {
                    var currentRes = DisplayHelper.GetCurrentResolution();
                    DisplayMode res;

                    if (newState.IsRunning())
                    {
                        res = Config.GameRes;

                        if (!currentRes.Equals(Config.GameRes, WithHdr.IfNotNull))
                            resBeforeGameStarted = currentRes;
                        else
                            resBeforeGameStarted = null;
                    }
                    else
                    {
                        res = resBeforeGameStarted ?? Config.DesktopRes.First();
                    }

                    DisplayHelper.SetResolution(res, Config.ShowTooltip);

                    if (res.EnableHdr != null && currentRes.SupportsHdr == true)
                    {
                        try { HdrHelper.SetHDRStateForDisplay(0, res.EnableHdr.Value); }
                        catch (Exception ex)
                        {
                            MessageBoxExtensions.ShowErrorMessage($"Failed to set HDR to {res.EnableHdr.Value.ToString()}", ex);
                        }
                    }
                }

                steamGameState = newState;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
#if DEBUG
            if ((e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2) && e.Modifiers == Keys.Shift)
            {
                e.Handled = true;
                ShowHDR(e.KeyCode == Keys.F1);
            }
#endif
        }
    }
}
