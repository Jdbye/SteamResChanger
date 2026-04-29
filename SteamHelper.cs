using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win32Process;

namespace SteamResChanger
{
    public enum SteamGameState { NotRunning = -2, Overlay = -1 }
    public static class SteamGameStateExtensions
    {
        public static bool IsRunning(this SteamGameState state)
            => state != SteamGameState.NotRunning;

        public static bool IsChanged(this SteamGameState oldState, SteamGameState newState)
            => oldState.IsRunning() != newState.IsRunning();
    }


    public static class SteamHelper
    {
        public static Dictionary<string, ProcessInfo> monitoredProcesses { get; private set; } = new(StringComparer.OrdinalIgnoreCase);

        public static bool IsSteamOverlayRunning()
            => Process.GetProcessesByName("GameOverlayUI").Length > 0 || Process.GetProcessesByName("GameOverlayUI64").Length > 0;

        public static bool IsSteamVrRunning()
            => Process.GetProcessesByName("vrserver").Length > 0;

        public static int GetRunningSteamAppId()
            => Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Valve\Steam")?.GetValue("RunningAppID", 0) as int? ?? 0;

        public static int IsSteamGameRunning(bool ignoreVr = true)
        {
            if (ignoreVr && IsSteamVrRunning()) return -1;
            return GetRunningSteamAppId();
        }

        // Prefer detecting overlay process first, as this is faster than waiting for the RunningAppID registry key to update
        public static SteamGameState GetSteamGameState(bool ignoreVr = true)
        {
            if (IsSteamOverlayRunning())
                return SteamGameState.Overlay;

            var appId = IsSteamGameRunning(ignoreVr);
            if (appId > 0)
                return (SteamGameState)appId;

            return SteamGameState.NotRunning;
        }

        public static void MonitorProcess(string processName, ProcessInfo.StartedEventHandler startedEvent, ProcessInfo.TerminatedEventHandler terminatedEvent)
        {
            if (!monitoredProcesses.TryGetValue(processName, out var processInfo))
            {
                processInfo = new(processName);
                monitoredProcesses.Add(processName, processInfo);
            }

            processInfo.Started += startedEvent;
            processInfo.Terminated += terminatedEvent;
        }

        public static bool UnmonitorProcess(string processName)
        {
            if (!monitoredProcesses.Remove(processName, out var processInfo)) return false;
            processInfo.Dispose();
            return true;
        }

        public static bool UnmonitorAll()
        {
            bool unmonitoredAny = false;
            foreach (var kvp in monitoredProcesses.ToArray())
            {
                try
                {
                    unmonitoredAny |= UnmonitorProcess(kvp.Key);
                }
                catch { }
            }
            return unmonitoredAny;
        }
    }
}
