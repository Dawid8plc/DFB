using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Dawid8.DFB.Navigation.DataLoader;

namespace Dawid8.DFB.Navigation
{
    class Navigator
    {
        GetData getdata = new GetData();

        public string CurDir = @"C:\";

        public TextBox PathDisplay;
        public ListView FilesDisplay;
        public ListView DrivesDisplay;

        public ImageList Icons = new ImageList();


        ///<summary>
        ///<para>Adds the detected drives to the FilesDisplay</para>
        ///</summary>
        public void SetDrives()
        {
            DriveInfo[] drives = getdata.GetDrives();

            foreach (DriveInfo d in drives)
            {
                ListViewItem test = new ListViewItem();
                test.Text = d.Name;

                DrivesDisplay.Items.Add(test);
            }
        }

        ///<summary>
        ///<para>Refreshes the File Display</para>
        ///</summary>
        public void RefreshFileDisplay()
        {
            Icons.ColorDepth = ColorDepth.Depth32Bit;
            Icons.ImageSize = new Size(32, 32);

            FilesDisplay.Clear();
            Icons.Images.Clear();

            foreach (string f in Directory.GetDirectories(CurDir))
            {



                IntPtr hImgSmall;    //the handle to the system image list
                IntPtr hImgLarge;    //the handle to the system image list
                string fName;        // 'the file name to get icon from
                SHFILEINFO shinfo = new SHFILEINFO();


                fName = f;
                //Use this to get the small Icon
                hImgSmall = Win32.SHGetFileInfo(fName, 0, ref shinfo,
                                               (uint)Marshal.SizeOf(shinfo),
                                                Win32.SHGFI_ICON |
                                                Win32.SHGFI_SMALLICON);

                //Use this to get the large Icon
                //hImgLarge = SHGetFileInfo(fName, 0,
                //ref shinfo, (uint)Marshal.SizeOf(shinfo),
                //Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON);
                //The icon is returned in the hIcon member of the shinfo
                //struct
                System.Drawing.Icon myIcon =
                       System.Drawing.Icon.FromHandle(shinfo.hIcon);

                Icons.Images.Add("folder:" + f, myIcon);








                //Icon ico;
                //ico = Icon.ExtractAssociatedIcon(f);
                //imageList1.Images.Add("folder:" + f, ico);
                var listviewitem = FilesDisplay.Items.Add(Path.GetFileName(f));
                string[] TagContents = new string[2];
                TagContents[0] = f;
                TagContents[1] = "Folder";
                listviewitem.Tag = TagContents;
                listviewitem.ImageKey = "folder:" + f;
            }

            foreach (string f in Directory.GetFiles(CurDir))
            {
                //listView2.Items.Add(f);
                Icon ico;
                ico = Icon.ExtractAssociatedIcon(f);
                Icons.Images.Add(f, ico);
                FilesDisplay.LargeImageList = Icons;

                var listviewitem = FilesDisplay.Items.Add(Path.GetFileName(f));
                string[] TagContents = new string[2];
                TagContents[0] = f;
                TagContents[1] = "File";
                listviewitem.Tag = TagContents;
                listviewitem.ImageKey = f;

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
}