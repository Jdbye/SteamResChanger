using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

// This code borrowed from WindowsDisplayAPI (https://github.com/falahati/WindowsDisplayAPI)
namespace SteamResChanger
{    // https://msdn.microsoft.com/en-us/library/windows/desktop/dd183565(v=vs.85).aspx
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
    public struct DeviceMode
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        [FieldOffset(0)]
        public readonly string? DeviceName;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(32)]
        public readonly ushort SpecificationVersion;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(34)]
        public readonly ushort DriverVersion;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(36)]
        public readonly ushort Size;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(38)]
        public readonly ushort DriverExtra;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(40)]
        public readonly DeviceModeFields Fields;

        [MarshalAs(UnmanagedType.Struct)]
        [FieldOffset(44)]
        public readonly PointL Position;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(52)]
        public readonly DisplayOrientation DisplayOrientation;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(56)]
        public readonly DisplayFixedOutput DisplayFixedOutput;

        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(60)]
        public readonly short Color;

        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(62)]
        public readonly short Duplex;

        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(64)]
        public readonly short YResolution;

        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(66)]
        public readonly short TrueTypeOption;

        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(68)]
        public readonly short Collate;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        [FieldOffset(72)]
        private readonly string? FormName;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(102)]
        public readonly ushort LogicalInchPixels;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(104)]
        public readonly uint BitsPerPixel;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(108)]
        public readonly uint PixelsWidth;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(112)]
        public readonly uint PixelsHeight;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(116)]
        public readonly DisplayFlags DisplayFlags;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(120)]
        public readonly uint DisplayFrequency;

        public DeviceMode() : this(DeviceModeFields.None) { }

        public DeviceMode(DeviceModeFields fields)
        {
            SpecificationVersion = 0x0320;
            Size = (ushort)Marshal.SizeOf(GetType());
            Fields = fields;
        }

        public DeviceMode(string? deviceName, DeviceModeFields fields) : this(fields)
        {
            DeviceName = deviceName;
        }

        public DeviceMode(
            string? deviceName,
            PointL position,
            DisplayOrientation orientation,
            DisplayFixedOutput fixedOutput,
            uint bpp,
            uint width,
            uint height,
            DisplayFlags displayFlags,
            uint displayFrequency)
            : this(deviceName, DeviceModeFields.AllDisplayExceptYResolution)
        {
            Position = position;
            DisplayOrientation = orientation;
            DisplayFixedOutput = fixedOutput;
            BitsPerPixel = bpp;
            PixelsWidth = width;
            PixelsHeight = height;
            DisplayFlags = displayFlags;
            DisplayFrequency = displayFrequency;
        }

        // These do not work (resolution doesn't change correctly, at least on my setup...)

        /*
        public DeviceMode(
            string? deviceName,
            PointL position,
            DisplayOrientation orientation,
            uint bpp,
            uint width,
            uint height,
            DisplayFlags displayFlags,
            uint displayFrequency)
            : this(deviceName, DeviceModeFields.AllDisplayExceptYResolutionAndFixedOutput)
        {
            Position = position;
            DisplayOrientation = orientation;
            BitsPerPixel = bpp;
            PixelsWidth = width;
            PixelsHeight = height;
            DisplayFlags = displayFlags;
            DisplayFrequency = displayFrequency;
        }
        */

        /*
        public DeviceMode(string? deviceName, PointL position, uint bpp, uint width, uint height, uint displayFrequency)
            : this(deviceName, DeviceModeFields.ResolutionFrequencyAndPosition)
        {
            Position = position;
            BitsPerPixel = bpp;
            PixelsWidth = width;
            PixelsHeight = height;
            DisplayFrequency = displayFrequency;
        }

        public DeviceMode(string? deviceName, uint bpp, uint width, uint height, uint displayFrequency)
            : this(deviceName, DeviceModeFields.ResolutionAndFrequency)
        {
            BitsPerPixel = bpp;
            PixelsWidth = width;
            PixelsHeight = height;
            DisplayFrequency = displayFrequency;
        }
        */
    }

    [Flags]
    public enum DeviceModeFields : uint
    {
        None = 0,

        Position = 0x20,

        DisplayOrientation = 0x80,

        Color = 0x800,

        Duplex = 0x1000,

        YResolution = 0x2000,

        TtOption = 0x4000,

        Collate = 0x8000,

        FormName = 0x10000,

        LogPixels = 0x20000,

        BitsPerPixel = 0x40000,

        PelsWidth = 0x80000,

        PelsHeight = 0x100000,

        DisplayFlags = 0x200000,

        DisplayFrequency = 0x400000,

        DisplayFixedOutput = 0x20000000,

        ResolutionAndFrequency =
            BitsPerPixel | PelsWidth | PelsHeight | DisplayFrequency,

        ResolutionFrequencyAndPosition =
            ResolutionAndFrequency | Position,

        AllDisplayExceptYResolutionAndFixedOutput =
            ResolutionFrequencyAndPosition | DisplayOrientation | DisplayFlags,

        AllDisplayExceptYResolution =
            AllDisplayExceptYResolutionAndFixedOutput | DisplayFixedOutput,

        AllDisplay = AllDisplayExceptYResolution
            | YResolution,
    }

    // https://msdn.microsoft.com/en-us/library/vs/alm/dd162807(v=vs.85).aspx
    [StructLayout(LayoutKind.Sequential)]
    public struct PointL : IEquatable<PointL>
    {
        [MarshalAs(UnmanagedType.I4)] public readonly int X;
        [MarshalAs(UnmanagedType.I4)] public readonly int Y;

        [Pure]
        public Point ToPoint()
        {
            return new Point(X, Y);
        }

        [Pure]
        public Size ToSize()
        {
            return new Size(X, Y);
        }

        public PointL(Point point) : this(point.X, point.Y) { }

        public PointL(Size size) : this(size.Width, size.Height) { }

        public PointL(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(PointL other)
            => X == other.X && Y == other.Y;

        public override bool Equals(object? obj)
            => obj != null && obj is PointL point && Equals(point);

        public override int GetHashCode()
            => HashCode.Combine(X, Y);

        public static bool operator ==(PointL left, PointL right)
        {
            return Equals(left, right) || left.Equals(right);
        }

        public static bool operator !=(PointL left, PointL right)
        {
            return !(left == right);
        }
    }

    /// <summary>
    ///     Contains possible values for the display orientation
    /// </summary>
    public enum DisplayOrientation : uint
    {
        /// <summary>
        ///     No rotation
        /// </summary>
        Identity = 0,

        /// <summary>
        ///     90 degree rotation
        /// </summary>
        Rotate90Degree = 1,

        /// <summary>
        ///     180 degree rotation
        /// </summary>
        Rotate180Degree = 2,

        /// <summary>
        ///     270 degree rotation
        /// </summary>
        Rotate270Degree = 3
    }

    /// <summary>
    ///     Contains possible values for the display fixed output
    /// </summary>
    public enum DisplayFixedOutput : uint
    {
        /// <summary>
        ///     Default behavior
        /// </summary>
        Default = 0,

        /// <summary>
        ///     Stretches the output to fit to the display
        /// </summary>
        Stretch = 1,

        /// <summary>
        ///     Centers the output in the middle of the display
        /// </summary>
        Center = 2
    }

    [Flags]
    public enum DisplayFlags : uint
    {
        None = 0,
        Grayscale = 1,
        Interlaced = 2
    }

    internal enum DisplaySettingsMode
    {
        CurrentSettings = -1,

        RegistrySettings = -2
    }

    [Flags]
    public enum ChangeDisplaySettingsFlags : uint
    {
        UpdateRegistry = 0x00000001,

        Fullscreen = 0x00000004,

        Global = 0x00000008,

        SetPrimary = 0x00000010,

        Reset = 0x40000000,

        NoReset = 0x10000000
    }

    /// <summary>
    ///     Contains possible values for the result of mode change request
    /// </summary>
    public enum ChangeDisplaySettingsExResults
    {
        /// <summary>
        ///     Completed successfully
        /// </summary>
        Successful = 0,

        /// <summary>
        ///     Changes needs restart
        /// </summary>
        Restart = 1,

        /// <summary>
        ///     Failed to change and save setings
        /// </summary>
        Failed = -1,

        /// <summary>
        ///     Invalid data provide
        /// </summary>
        BadMode = -2,

        /// <summary>
        ///     Changes not updated
        /// </summary>
        NotUpdated = -3,

        /// <summary>
        ///     Invalid flags provided
        /// </summary>
        BadFlags = -4,

        /// <summary>
        ///     Bad parameters provided
        /// </summary>
        BadParam = -5,

        /// <summary>
        ///     Bad Dual View mode used with mode
        /// </summary>
        BadDualView = -6
    }
}
