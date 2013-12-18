namespace Baseclass.Wpf.Utilities
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Interop;

    /// <summary>
    /// Helper to make window flash using FlashWindowEx from user32.dll
    /// </summary>
    public static class WindowExtensions
    {
        #region Constants

        /// <summary>
        /// Flash both the window caption and taskbar button.
        /// </summary>
        private const uint FlashwAll = 3;

        /// <summary>
        /// Stop flashing. The system restores the window to its original state.
        /// </summary>
        private const uint FlashwStop = 0;

        /// <summary>
        /// Flash continuously, until the FLASHW_STOP flag is set.
        /// </summary>
        private const uint FlashwTimer = 4;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Lets the window flash for a specific amount of times if it's not active.
        /// </summary>
        /// <param name="win">
        /// The window which should flash.
        /// </param>
        /// <param name="count">
        /// The number of times the window should flash.
        /// </param>
        public static void FlashWindow(this Window win, uint count = uint.MaxValue)
        {
            // Don't flash if the window is active
            if (win.IsActive)
            {
                return;
            }

            var h = new WindowInteropHelper(win);

            var info = new Flashwinfo
                           {
                               hwnd = h.Handle, 
                               dwFlags = FlashwAll | FlashwTimer, 
                               uCount = count, 
                               dwTimeout = 0
                           };

            info.cbSize = Convert.ToUInt32(Marshal.SizeOf(info));

            FlashWindowEx(ref info);
        }

        /// <summary>
        /// The stop flashing window.
        /// </summary>
        /// <param name="win">
        /// The win.
        /// </param>
        public static void StopFlashingWindow(this Window win)
        {
            var h = new WindowInteropHelper(win);

            var info = new Flashwinfo
                           {
                               hwnd = h.Handle, 
                               dwFlags = FlashwStop, 
                               uCount = uint.MaxValue, 
                               dwTimeout = 0
                           };

            info.cbSize = Convert.ToUInt32(Marshal.SizeOf(info));
            
            FlashWindowEx(ref info);
        }

        #endregion

        #region Methods

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FlashWindowEx(ref Flashwinfo pwfi);

        #endregion

        /// <summary>
        /// The FlashwInfo struct
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct Flashwinfo
        {
            /// <summary>
            /// The size of the structure in bytes.
            /// </summary>
            public uint cbSize;

            /// <summary>
            /// A Handle to the Window to be Flashed. The window can be either opened or minimized.
            /// </summary>
            public IntPtr hwnd;

            /// <summary>
            /// The Flash Status.
            /// </summary>
            public uint dwFlags;

            /// <summary>
            /// Number of times to flash the window
            /// </summary>
            public uint uCount;

            /// <summary>
            /// The rate at which the Window is to be flashed, in milliseconds. If Zero, the function uses the default cursor blink rate.
            /// </summary>
            public uint dwTimeout;
        }
    }
}