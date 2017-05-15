using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// Has suppress unmanaged code attribute. These methods are safe and can be 
    /// used fairly safely and the caller is not needed to do full security reviews 
    /// even though no stack walk will be performed.
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    internal static class SafeNativeMethods
    {
        #region Methods

        /// <summary>
        /// obtains extended information about the version of the operating system that is currently running.
        /// </summary>
        /// <param name="ver">data structure that the function fills with operating system version information</param>
        /// <returns>true if the function succeeds, else false.</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [SuppressMessage("Microsoft.Usage", "CA2205:UseManagedEquivalentsOfWin32Api")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetVersionEx([In, Out] OSVERSIONINFO ver);

        /// <summary>
        /// obtains extended information about the version of the operating system that is currently running.
        /// </summary>
        /// <param name="ver">data structure that the function fills with operating system version information</param>
        /// <returns>true if the function succeeds, else false.</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [SuppressMessage("Microsoft.Usage", "CA2205:UseManagedEquivalentsOfWin32Api")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetVersionEx([In, Out] OSVERSIONINFOEX ver);

        /// <summary>
        /// determines whether the specified window is enabled for mouse and keyboard input. 
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool IsWindowEnabled(IntPtr hWnd);


  
        /// <summary>
        /// determines whether the specified window is minimized (iconic). 
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool IsIconic(IntPtr hWnd);


        /// <summary>
        /// sets the specified window's show state. 
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
 

        #endregion

        #region OSVERSIONINFO

        /// <summary>
        /// data structure contains operating system version information.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class OSVERSIONINFO
        {
            /// <summary>
            /// Size of this data structure, in bytes. Set this member to sizeof(OSVERSIONINFO) before calling the GetVersionEx function.
            /// </summary>
            public int OSVersionInfoSize;
            /// <summary>
            /// Major version number of the operating system.
            /// </summary>
            public int MajorVersion;
            /// <summary>
            /// Minor version number of the operating system.
            /// </summary>
            public int MinorVersion;
            /// <summary>
            /// Build number of the operating system.
            /// </summary>
            /// <remarks>Windows Me/98/95:  The low-order word contains the build number of the operating. The high-order word contains the major and minor version numbers.</remarks>
            public int BuildNumber;
            /// <summary>
            /// Operating system platform
            /// </summary>
            public OSPlatformID PlatformId;
            /// <summary>
            /// Pointer to a null-terminated string, such as "Service Pack 3", that indicates the latest Service Pack installed on the system. If no Service Pack has been installed, the string is empty.
            /// </summary>
            /// <remarks>Windows Me/98/95:  Pointer to a null-terminated string that indicates additional version information. For example, " C" indicates Windows 95 OSR2 and " A" indicates Windows 98 Second Edition.</remarks>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string CSDVersion;

            /// <summary>
            /// Initializes a new instance of the <see cref="T:OSVERSIONINFO"/> class.
            /// </summary>
            public OSVERSIONINFO()
            {
                this.OSVersionInfoSize = Marshal.SizeOf(this);
            } // OSVERSIONINFO
        } // class OSVERSIONINFO
        #endregion

        #region OSVERSIONINFOEX

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class OSVERSIONINFOEX
        {
            /// <summary>
            /// Size of this data structure, in bytes. Set this member to sizeof(OSVERSIONINFO) before calling the GetVersionEx function.
            /// </summary>
            public int OSVersionInfoSize;
            /// <summary>
            /// Major version number of the operating system.
            /// </summary>
            public int MajorVersion;
            /// <summary>
            /// Minor version number of the operating system.
            /// </summary>
            public int MinorVersion;
            /// <summary>
            /// Build number of the operating system.
            /// </summary>
            /// <remarks>Windows Me/98/95:  The low-order word contains the build number of the operating. The high-order word contains the major and minor version numbers.</remarks>
            public int BuildNumber;
            /// <summary>
            /// Operating system platform
            /// </summary>
            public OSPlatformID PlatformId;
            /// <summary>
            /// Pointer to a null-terminated string, such as "Service Pack 3", that indicates the latest Service Pack installed on the system. If no Service Pack has been installed, the string is empty.
            /// </summary>
            /// <remarks>Windows Me/98/95:  Pointer to a null-terminated string that indicates additional version information. For example, " C" indicates Windows 95 OSR2 and " A" indicates Windows 98 Second Edition.</remarks>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string CSDVersion;

            /// <summary>
            /// Major version number of the latest Service Pack installed on the system
            /// </summary>
            public ushort ServicePackMajor;

            /// <summary>
            /// Minor version number of the latest Service Pack installed on the system
            /// </summary>
            public ushort ServicePackMinor;

            /// <summary>
            /// Bit flags that identify the product suites available on the system
            /// </summary>
            public OSSuiteMask SuiteMask;

            /// <summary>
            /// Additional information about the system
            /// </summary>
            public OSProductType ProductType;

            /// <summary>
            /// Reserved for future use
            /// </summary>
            public byte Reserved;

            /// <summary>
            /// Initializes a new instance of the <see cref="T:OSVERSIONINFO"/> class.
            /// </summary>
            public OSVERSIONINFOEX()
            {
                this.OSVersionInfoSize = Marshal.SizeOf(this);
            } // OSVERSIONINFOEX
        } // class OSVERSIONINFOEX
        #endregion


    }
}
