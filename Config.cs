using System.Diagnostics.CodeAnalysis;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace SteamResChanger
{
    public class Config
    {
        [YamlMember(Alias = "game_resolution")]
        public string GameResString
        {
            get => GameRes.ToString();
            set => GameRes = DisplayMode.TryFromString(value, SupportedResolutions) ?? ResAtStart;
        }

        [YamlMember(Alias = "desktop_resolutions")]
        public string[] DesktopResString
        {
            get => DesktopRes.Select(dm => dm.ToString()).ToArray();
            set
            {
                DesktopRes = value.Select(s => DisplayMode.TryFromString(s, SupportedResolutions)).OfType<DisplayMode>().ToArray()
                    .DefaultIfEmpty(ResAtStart).Distinct(DisplayModeComparer.WithHdr).ToArray();
            }
        }

        public bool ShowTooltip { get; set; } = true;

        public bool IgnoreVrGames { get; set; } = true;

        [YamlIgnore]
        public DisplayMode GameRes { get; set; }

        [YamlIgnore]
        public DisplayMode[] DesktopRes { get; set; }

        [YamlIgnore]
        public DisplayMode ResAtStart { get; } = DisplayHelper.GetCurrentResolution();

        [YamlIgnore]
        public static DisplayMode[] SupportedResolutions { get; set; } = Array.Empty<DisplayMode>();

        public bool DebugMode { get; set; } = false;

        [YamlIgnore]
        public const string YamlPath = "config.yml";

        public Config()
        {
            DesktopRes = [ResAtStart];
            GameRes = ResAtStart;
        }

        public static Config Load(IEnumerable<DisplayMode> resolutions)
        {
            SupportedResolutions = resolutions.ToArray();
            return YamlHelpers.LoadYaml<Config>(YamlPath) ?? YamlHelpers.CreateYaml<Config>(YamlPath);
        }

        public bool Save()
        {
            return YamlHelpers.WriteYaml<Config>(YamlPath, this);
        }
    }
}
