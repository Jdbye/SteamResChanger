using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SteamResChanger
{
    // This code borrowed from https://github.com/jdalley/command-palette-toggle-hdr
    public static class HdrHelper
    {
        [DllImport("user32.dll")]
        internal static extern int GetDisplayConfigBufferSizes(
          QDC flags,
          out uint numPathArrayElements,
          out uint numModeInfoArrayElements);

        [DllImport("user32.dll")]
        internal static extern int QueryDisplayConfig(
            QDC flags,
            ref uint numPathArrayElements,
            [Out] DISPLAYCONFIG_PATH_INFO[] pathArray,
            ref uint numModeInfoArrayElements,
            [Out] DISPLAYCONFIG_MODE_INFO[] modeInfoArray,
            IntPtr currentTopologyId);

        [DllImport("user32.dll")]
        internal static extern int DisplayConfigSetDeviceInfo(
            ref DISPLAYCONFIG_SET_ADVANCED_COLOR_STATE requestPacket);

        [DllImport("user32.dll")]
        internal static extern int DisplayConfigGetDeviceInfo(
            ref DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO requestPacket);

        [DllImport("user32.dll")]
        internal static extern int DisplayConfigGetDeviceInfo(
            ref DISPLAYCONFIG_TARGET_DEVICE_NAME requestPacket);

        internal static class DisplayConfigConstants
        {
            internal const int ERROR_SUCCESS = 0;

            // DisplayConfig device info types
            internal const int DISPLAYCONFIG_DEVICE_INFO_GET_SOURCE_NAME = 1;
            internal const int DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME = 2;
            internal const int DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_PREFERRED_MODE = 3;
            internal const int DISPLAYCONFIG_DEVICE_INFO_GET_ADAPTER_NAME = 4;
            internal const int DISPLAYCONFIG_DEVICE_INFO_SET_TARGET_PERSISTENCE = 5;
            internal const int DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_BASE_TYPE = 6;
            internal const int DISPLAYCONFIG_DEVICE_INFO_GET_SUPPORT_VIRTUAL_RESOLUTION = 7;
            internal const int DISPLAYCONFIG_DEVICE_INFO_SET_SUPPORT_VIRTUAL_RESOLUTION = 8;
            internal const int DISPLAYCONFIG_DEVICE_INFO_GET_ADVANCED_COLOR_INFO = 9;
            internal const int DISPLAYCONFIG_DEVICE_INFO_SET_ADVANCED_COLOR_STATE = 10;
        }

        [Flags]
        internal enum DisplayConfigGetAdvancedColorInfoValue : uint
        {
            AdvancedColorSupported = 0x1,
            AdvancedColorEnabled = 0x2,
            WideColorEnforced = 0x4,
            AdvancedColorForceDisabled = 0x8
        }

        internal enum QDC : uint
        {
            ALL_PATHS = 0x00000001,
            ONLY_ACTIVE_PATHS = 0x00000002,
            DATABASE_CURRENT = 0x00000004,
            VIRTUAL_MODE_AWARE = 0x00000010,
            INCLUDE_HMD = 0x00000020,
            FORCE_UINT32 = 0xFFFFFFFF
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct LUID
        {
            public uint LowPart;
            public int HighPart;

            public override bool Equals(object? obj)
            {
                if (obj is not LUID other)
                {
                    return false;
                }

                return LowPart == other.LowPart && HighPart == other.HighPart;
            }

            public override int GetHashCode()
            {
                return LowPart.GetHashCode() ^ HighPart.GetHashCode();
            }

            public static bool operator ==(LUID left, LUID right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(LUID left, LUID right)
            {
                return !(left == right);
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct DISPLAYCONFIG_TARGET_DEVICE_NAME // Fix for IDE0251: Mark struct as readonly
        {
            public DISPLAYCONFIG_DEVICE_INFO_HEADER header;
            public uint flags;
            public uint outputTechnology;
            public ushort edidManufactureId;
            public ushort edidProductCodeId;
            public uint connectorInstance;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string monitorFriendlyDeviceName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string monitorDevicePath;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_DEVICE_INFO_HEADER
        {
            public uint type;
            public uint size;
            public LUID adapterId;
            public uint id;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO
        {
            public DISPLAYCONFIG_DEVICE_INFO_HEADER header;
            public uint value;
            public uint colorEncoding;
            public uint bitsPerColorChannel;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_SET_ADVANCED_COLOR_STATE
        {
            public DISPLAYCONFIG_DEVICE_INFO_HEADER header;
            public uint value;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_PATH_SOURCE_INFO
        {
            public LUID adapterId;
            public uint id;
            public uint modeInfoIdx;
            public uint statusFlags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_PATH_TARGET_INFO
        {
            public LUID adapterId;
            public uint id;
            public uint modeInfoIdx;
            public uint outputTechnology;
            public uint rotation;
            public uint scaling;
            public DISPLAYCONFIG_RATIONAL refreshRate;
            public uint scanLineOrdering;
            public uint targetAvailable;
            public uint statusFlags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_RATIONAL
        {
            public uint Numerator;
            public uint Denominator;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_PATH_INFO
        {
            public DISPLAYCONFIG_PATH_SOURCE_INFO sourceInfo;
            public DISPLAYCONFIG_PATH_TARGET_INFO targetInfo;
            public uint flags;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct DISPLAYCONFIG_MODE_INFO_UNION
        {
            [FieldOffset(0)]
            public DISPLAYCONFIG_TARGET_MODE targetMode;
            [FieldOffset(0)]
            public DISPLAYCONFIG_SOURCE_MODE sourceMode;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_TARGET_MODE
        {
            public DISPLAYCONFIG_VIDEO_SIGNAL_INFO targetVideoSignalInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_SOURCE_MODE
        {
            public uint width;
            public uint height;
            public uint pixelFormat;
            public POINTL position;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINTL
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_VIDEO_SIGNAL_INFO
        {
            public ulong pixelRate;
            public DISPLAYCONFIG_RATIONAL hSyncFreq;
            public DISPLAYCONFIG_RATIONAL vSyncFreq;
            public DISPLAYCONFIG_2DREGION activeSize;
            public DISPLAYCONFIG_2DREGION totalSize;
            public uint videoStandard;
            public uint scanLineOrdering;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_2DREGION
        {
            public uint cx;
            public uint cy;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_MODE_INFO
        {
            public uint infoType;
            public uint id;
            public LUID adapterId;
            public DISPLAYCONFIG_MODE_INFO_UNION modeInfo;
        }

        /// <summary>
        /// Set the HDR state for a specific display by its index.
        /// </summary>
        /// <param name="displayIndex"></param>
        /// <param name="enable">HDR State - On(true)/Off(false)</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Exception"></exception>
        internal static void SetHDRStateForDisplay(int displayIndex, bool enable)
        {
            var displays = GetDisplays();

            if (displayIndex < 0 || displayIndex >= displays.Count)
            {
                throw new ArgumentException($"Display index {displayIndex} is out of range. Available displays: {displays.Count}");
            }

            var display = displays[displayIndex];

            if (!display.SupportsHDR)
            {
                throw new InvalidOperationException($"Display at index {displayIndex} does not support HDR");
            }

            // Set HDR state for the selected display
            var setAdvancedColorState = new DISPLAYCONFIG_SET_ADVANCED_COLOR_STATE
            {
                header = new DISPLAYCONFIG_DEVICE_INFO_HEADER
                {
                    type = DisplayConfigConstants.DISPLAYCONFIG_DEVICE_INFO_SET_ADVANCED_COLOR_STATE,
                    size = (uint)Marshal.SizeOf<DISPLAYCONFIG_SET_ADVANCED_COLOR_STATE>(),
                    adapterId = display.AdapterId,
                    id = display.DisplayId
                },
                value = enable ? 1u : 0u
            };

            int result = DisplayConfigSetDeviceInfo(ref setAdvancedColorState);
            if (result != DisplayConfigConstants.ERROR_SUCCESS)
            {
                throw new InvalidOperationException($"Failed to set HDR state for display {displayIndex}. Error code: {result}");
            }
        }

        /// <summary>
        /// Get a list of Displays/Monitors connected to this computer, including information on whether they 
        /// support HDR, and whether it's currently enabled.
        /// </summary>
        /// <returns><see cref="List{DisplayInfo}"/></returns>
        /// <exception cref="Exception"></exception>
        internal static List<DisplayInfo> GetDisplays()
        {
            // Get necessary buffer sizes for the display configuration
            int result = GetDisplayConfigBufferSizes(QDC.ONLY_ACTIVE_PATHS, out uint pathCount, out uint modeCount);
            if (result != DisplayConfigConstants.ERROR_SUCCESS)
            {
                throw new  Win32Exception(result, $"Failed to get display config buffer sizes. Error code: {result}");
            }

            // Query the display configuration
            DISPLAYCONFIG_PATH_INFO[] pathInfoArray = new DISPLAYCONFIG_PATH_INFO[pathCount];
            DISPLAYCONFIG_MODE_INFO[] modeInfoArray = new DISPLAYCONFIG_MODE_INFO[modeCount];
            result = QueryDisplayConfig(
                QDC.ONLY_ACTIVE_PATHS,
                ref pathCount,
                pathInfoArray,
                ref modeCount,
                modeInfoArray,
                nint.Zero);

            if (result != DisplayConfigConstants.ERROR_SUCCESS)
            {
                throw new Win32Exception(result, $"Failed to query display configuration. Error code: {result}");
            }

            // Create list of display information
            var displays = new List<DisplayInfo>();

            for (int i = 0; i < pathCount; i++)
            {
                var displayInfo = new DisplayInfo
                {
                    AdapterId = pathInfoArray[i].targetInfo.adapterId,
                    DisplayId = pathInfoArray[i].targetInfo.id,
                };

                // Get the display name
                var displayNameInfo = new DISPLAYCONFIG_TARGET_DEVICE_NAME
                {
                    header = new DISPLAYCONFIG_DEVICE_INFO_HEADER
                    {
                        type = DisplayConfigConstants.DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME,
                        size = (uint)Marshal.SizeOf<DISPLAYCONFIG_TARGET_DEVICE_NAME>(),
                        adapterId = pathInfoArray[i].targetInfo.adapterId,
                        id = pathInfoArray[i].targetInfo.id
                    }
                };

                result = DisplayConfigGetDeviceInfo(ref displayNameInfo);
                if (result == DisplayConfigConstants.ERROR_SUCCESS)
                {
                    displayInfo.DisplayName = displayNameInfo.monitorFriendlyDeviceName;
                }

                // Check HDR capabilities
                var getAdvancedColorInfo = new DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO
                {
                    header = new DISPLAYCONFIG_DEVICE_INFO_HEADER
                    {
                        type = DisplayConfigConstants.DISPLAYCONFIG_DEVICE_INFO_GET_ADVANCED_COLOR_INFO,
                        size = (uint)Marshal.SizeOf<DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO>(),
                        adapterId = pathInfoArray[i].targetInfo.adapterId,
                        id = pathInfoArray[i].targetInfo.id
                    }
                };

                result = DisplayConfigGetDeviceInfo(ref getAdvancedColorInfo);
                if (result == DisplayConfigConstants.ERROR_SUCCESS)
                {
                    displayInfo.SupportsHDR = (getAdvancedColorInfo.value
                        & (uint)DisplayConfigGetAdvancedColorInfoValue.AdvancedColorSupported) != 0;
                    displayInfo.IsHDREnabled = (getAdvancedColorInfo.value
                        & (uint)DisplayConfigGetAdvancedColorInfoValue.AdvancedColorEnabled) != 0;
                }

                displays.Add(displayInfo);
            }

            return displays;
        }

        internal class DisplayInfo
        {
            internal string DisplayName { get; set; }
            internal bool SupportsHDR { get; set; }
            internal bool IsHDREnabled { get; set; }
            internal LUID AdapterId { get; set; }
            internal uint DisplayId { get; set; }

            internal DisplayInfo()
            {
                DisplayName = "Unknown Display";
                SupportsHDR = false;
                IsHDREnabled = false;
            }
        }
    }
}
