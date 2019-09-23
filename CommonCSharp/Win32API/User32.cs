using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CommonCSharp.Win32API
{
    public class User32
    {
        [DllImport("user32", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string cls, string win);
        [DllImport("user32")]
        public static extern IntPtr SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32")]
        public static extern bool IsIconic(IntPtr hWnd);
        [DllImport("user32")]
        public static extern bool OpenIcon(IntPtr hWnd);


        public const uint WM_DROPFILES = 0x0233;
        public const uint WM_COPYDATA = 0x4A;
        public const uint MSGFLT_ADD = 1;
        [DllImport("user32", SetLastError = true)]
        public static extern IntPtr ChangeWindowMessageFilter(uint message, uint dwFlag);
    }
}
