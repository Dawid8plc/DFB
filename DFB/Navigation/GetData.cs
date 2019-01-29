using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Dawid8.DFB.Navigation.DataLoader
{
    class GetData
    {

        ///<summary>
        ///<para>Detects the installed drives inside the machine</para>
        ///</summary>
        public DriveInfo[] GetDrives()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            return allDrives;
        }

        public DriveInfo[] sGetDrives()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            return allDrives;
        }
        
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SHFILEINFO
    {
        public IntPtr hIcon;
        public IntPtr iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;
    };

    class Win32
    {
        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_LARGEICON = 0x0;    // 'Large icon
        public const uint SHGFI_SMALLICON = 0x1;    // 'Small icon

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath,
                                    uint dwFileAttributes,
                                    ref SHFILEINFO psfi,
                                    uint cbSizeFileInfo,
                                    uint uFlags);
    }
}
