
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
        public bool IsPaused { get; set; } = false;
        public bool IsExiting { get; set; } = false;

        readonly ComboBox[] comboPresets;
        readonly CheckBox[] chkPresetsHDR;

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
            comboPresets = [comboPreset1, comboPreset2, comboPreset3, comboPreset4, comboPreset5];
            chkPresetsHDR = [chkPresetHDR1, chkPresetHDR2, chkPresetHDR3, chkPresetHDR4, chkPresetHDR5];
        }

        private void SetComboBoxItems(ComboBox combo, CheckBox hdr, IEnumerable items, object? selected)
        {
            combo.Items.Clear();
            foreach (var item in items)
                combo.Items.Add(item);

            SetComboBoxSelectedItem(combo, hdr, combo.Items, selected);
        }

        private void SetComboBoxSelectedItem(ComboBox combo, CheckBox hdr, IEnumerable items, object? selected)
        {
            if (selected == null)
            {
                combo.SelectedItem = null;
                return;
            }

            foreach (var item in items)
            {
                if (selected.Equals(item))
                {
                    combo.SelectedItem = item;
                    if (selected is DisplayModeBase dm)
                    {
                        if (!hdr.Visible)
                            hdr.CheckState = CheckState.Indeterminate;
                        else
                            hdr.CheckState = dm.EnableHdrCheckState;
                    }
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

                SetComboBoxItems(comboGameRes, chkGameHDR, _resolutionsWithNone, Config.GameRes);

                chkShowTooltip.Checked = Config.ShowTooltip;
                chkIgnoreVrGames.Checked = Config.IgnoreVrGames;

                for (int i = 0; i < comboPresets.Length; i++)
                    SetComboBoxItems(comboPresets[i], chkPresetsHDR[i], _resolutionsWithNone, i < Config.DesktopRes.Length ? Config.DesktopRes[i] : DisplayMode.None);

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
        }

        private void butApply_Click(object sender, EventArgs e)
        {
            var currentRes = DisplayHelper.GetCurrentResolution();
            var defaultRes = currentRes.Equals(Config.GameRes, WithHdr.IfNotNull) ? Config.ResAtStart : currentRes;

            var gameRes = comboGameRes.SelectedItem as DisplayMode ?? defaultRes;
            Config.GameRes = gameRes.SetHDR(chkGameHDR.CheckState);

            Config.ShowTooltip = chkShowTooltip.Checked;
            Config.IgnoreVrGames = chkIgnoreVrGames.Checked;

            var modes = comboPresets.Select(combo => combo.SelectedItem).ToArray();
            for (int i = 0; i < modes.Length; i++)
                if (modes[i] is DisplayMode dm)
                    modes[i] = dm.SetHDR(chkPresetsHDR[i].CheckState);

            Config.DesktopRes = modes.OfType<DisplayMode>().DefaultIfEmpty(defaultRes).Distinct(DisplayModeComparer.WithHdr).ToArray();

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
