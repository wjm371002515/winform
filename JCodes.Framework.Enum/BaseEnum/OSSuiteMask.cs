using System;

namespace JCodes.Framework.jCodesenum.BaseEnum
{
    [Flags()]
    public enum OSSuiteMask : ushort
    {
        /// <summary>
        /// Terminal Services is installed.
        /// </summary>
        VER_SUITE_TERMINAL = 16,
        /// <summary>
        /// Microsoft Small Business Server is installed with the restrictive client license in force
        /// </summary>
        VER_SUITE_SMALLBUSINESS_RESTRICTED = 32,
        /// <summary>
        /// Microsoft Small Business Server was once installed on the system, but may have been upgraded to another version of Windows
        /// </summary>
        VER_SUITE_SMALLBUSINESS = 1,
        /// <summary>
        /// Terminal Services is installed, but only one interactive session is supported.
        /// </summary>
        VER_SUITE_SINGLEUSERTS = 256,
        /// <summary>
        /// Windows XP Home Edition is installed.
        /// </summary>
        VER_SUITE_PERSONAL = 512,
        /// <summary>
        /// Windows XP Embedded is installed
        /// </summary>
        VER_SUITE_EMBEDDEDNT = 64,
        /// <summary>
        /// Windows Server 2003, Enterprise Edition, Windows 2000 Advanced Server, or Windows NT 4.0 Enterprise Edition.
        /// </summary>
        VER_SUITE_ENTERPRISE = 2,
        /// <summary>
        /// Windows Server 2003, Datacenter Edition or Windows 2000 Datacenter Server is installed.
        /// </summary>
        VER_SUITE_DATACENTER = 128,
        /// <summary>
        /// Windows Server 2003, Web Edition is installed.
        /// </summary>
        VER_SUITE_BLADE = 1024,
        /// <summary>
        /// Microsoft BackOffice components are installed.
        /// </summary>
        VER_SUITE_BACKOFFICE = 4,
        /// <summary>
        /// Unknown Suite.
        /// </summary>
        VER_UNKNOWN = 0,
        /// <summary>
        /// Windows Storage Server 2003 R2 is installed.
        /// </summary>
        VER_SUITE_STORAGE_SERVER = 8192,
        /// <summary>
        /// Windows Server 2003, Compute Cluster Edition is installed.
        /// </summary>
        VER_SUITE_COMPUTE_SERVER = 16384,
    }
}
