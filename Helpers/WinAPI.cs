using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace clickfree_Maui.Helpers
{
    public class WinAPI
    {
        #region Constants

        public const int GWL_STYLE = -16;
        public const int WS_MAXIMIZEBOX = 0x10000;
        public const int WS_MINIMIZEBOX = 0x20000;
        public const int WS_SYSMENU = 0x80000;

        #endregion

        #region winapi
        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        #endregion

        #region Methods

        public static void HideSysMENU(IntPtr windowHandle)
        {
            if (windowHandle == null)
                throw new InvalidOperationException("The window has not yet been completely initialized");

            SetWindowLong(windowHandle, GWL_STYLE, GetWindowLong(windowHandle, GWL_STYLE) & ~WS_SYSMENU);
        }

        #endregion
    }
}
