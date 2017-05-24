using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace JCodes.Framework.Common.Images
{
    /// <summary>
    /// 读取文件夹或者文件的系统图标辅助类
    /// </summary>
    /// <example>
    /// <code>IconReaderHelper.GetFileIcon("c:\\general.xls");</code>
    /// </example>
    public class IconReaderHelper
    {
        /// <summary>
        /// 返回给定文件的图标
        /// </summary>
        /// <param name="name">文件路径名</param>
        /// <param name="size">大图标还是小图标</param>
        /// <param name="linkOverlay">是否包含链接图标</param>
        /// <returns>System.Drawing.Icon</returns>
        public static Icon GetFileIcon(string name, IconSize size, bool linkOverlay)
        {
            uint flags = Const.SHGFI_ICON | Const.SHGFI_USEFILEATTRIBUTES;

            if (true == linkOverlay) flags += Const.SHGFI_LINKOVERLAY;

            if (IconSize.Small == size)
            {
                flags += Const.SHGFI_SMALLICON;
            }
            else
            {
                flags += Const.SHGFI_LARGEICON;
            }

            Shell32.SHFILEINFO shfi = new Shell32.SHFILEINFO();
            Shell32.SHGetFileInfo(name, Const.FILE_ATTRIBUTE_NORMAL, ref shfi, (uint)Marshal.SizeOf(shfi), flags);

            // Copy (clone) the returned icon to a new object, thus allowing us to clean-up properly
            Icon icon = (Icon)Icon.FromHandle(shfi.hIcon).Clone();
            User32.DestroyIcon(shfi.hIcon); // Cleanup
            return icon;
        }

        /// <summary>
        /// 用于访问系统文件夹图标。
        /// </summary>
        /// <param name="size">大图标还是小图标</param>
        /// <param name="folderType">文件夹是打开还是关闭状态</param>
        /// <returns>System.Drawing.Icon</returns>
        public static Icon GetFolderIcon(IconSize size, FolderType folderType)
        {
            // Need to add size check, although errors generated at present!
            uint flags = Const.SHGFI_ICON;// | Shell32.SHGFI_USEFILEATTRIBUTES;

            if (FolderType.Open == folderType)
            {
                flags += Const.SHGFI_OPENICON;
            }

            if (IconSize.Small == size)
            {
                flags += Const.SHGFI_SMALLICON;
            }
            else
            {
                flags += Const.SHGFI_LARGEICON;
            }

            Shell32.SHFILEINFO shfi = new Shell32.SHFILEINFO();
            Shell32.SHGetFileInfo(null, Const.FILE_ATTRIBUTE_DIRECTORY, ref shfi, (uint)Marshal.SizeOf(shfi), flags);

            Icon.FromHandle(shfi.hIcon); // Load the icon from an HICON handle

            // Now clone the icon, so that it can be successfully stored in an ImageList
            Icon icon = (Icon)Icon.FromHandle(shfi.hIcon).Clone();

            User32.DestroyIcon(shfi.hIcon); // Cleanup
            return icon;
        }

        /// <summary>
        /// 获取文件或者文件夹图标的显示名称
        /// </summary>
        /// <param name="name">文件或文件夹路径</param>
        /// <param name="isDirectory">是否为文件夹</param>
        /// <returns></returns>
        public static string GetDisplayName(string name, bool isDirectory)
        {
            uint flags = Const.SHGFI_TYPENAME | Const.SHGFI_USEFILEATTRIBUTES;
            Shell32.SHFILEINFO shfi = new Shell32.SHFILEINFO();
            uint fileType = isDirectory ? Const.FILE_ATTRIBUTE_DIRECTORY : Const.FILE_ATTRIBUTE_NORMAL;
            Shell32.SHGetFileInfo(name, fileType, ref shfi, (uint)Marshal.SizeOf(shfi), flags);
            return shfi.szTypeName;
        }


        #region 后缀名图标操作

        /// <summary>
        /// 添加扩展名小图标对象到ImageList集合中，并返回位置；如果存在，则返回对应的位置。
        /// </summary>
        /// <param name="images"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static int GetIcon(ImageList images, string extension)
        {
            return GetIcon(images, extension, false);
        }

        /// <summary>
        /// 添加扩展名图标对象到ImageList集合中，并返回位置；如果存在，则返回对应的位置。
        /// </summary>
        /// <param name="images">ImageList集合</param>
        /// <param name="extension">扩展名</param>
        /// <param name="largeIcon">是否大图标</param>
        /// <returns></returns>
        public static int GetIcon(ImageList images, string extension, bool largeIcon)
        {
            for (int i = 0; i < images.Images.Count; i++)
            {
                if (images.Images.Keys[i] == extension)
                    return i;
            }

            images.Images.Add(extension, ExtractIconForExtension(extension, largeIcon));
            return images.Images.Count - 1;
        }

        /// <summary>
        /// 获取扩展名的图标
        /// </summary>
        /// <param name="extension">扩展名</param>
        /// <param name="large">是否为大图标</param>
        /// <returns></returns>
        public static Icon ExtractIconForExtension(string extension, bool large)
        {
            if (!string.IsNullOrEmpty(extension))
            {
                if (!extension.Trim().StartsWith("."))
                {
                    extension = "." + extension.Trim();
                }

                //let's just make up a file name with that extension
                string fictitiousFile = "0" + extension;
                //now get the icon for that file
                return GetAssociatedIcon(fictitiousFile, large);
            }
            else
            {
                throw new ArgumentException("Invalid file or extension.", "fileOrExtension");
            }
        }

        /// <summary>
        /// 获取指定文件的关联图标
        /// </summary>
        /// <param name="stubPath">指定的文件路径</param>
        /// <param name="large">是否为大图标</param>
        /// <returns></returns>
        public static Icon GetAssociatedIcon(string stubPath, bool large)
        {
            Shell32.SHFILEINFO info = new Shell32.SHFILEINFO();
            int cbFileInfo = Marshal.SizeOf(info);
            uint flags;

            if (large)
            {
                flags = Const.SHGFI_ICON | Const.SHGFI_LARGEICON | Const.SHGFI_USEFILEATTRIBUTES;
            }
            else
            {
                flags = Const.SHGFI_ICON | Const.SHGFI_SMALLICON | Const.SHGFI_USEFILEATTRIBUTES;
            }

            Shell32.SHGetFileInfo(stubPath, 256, ref info, (uint)cbFileInfo, flags);
            return (Icon)Icon.FromHandle(info.hIcon);
        } 
        #endregion
    }


    // This code has been left largely untouched from that in the CRC example. 
    // The main changes have been moving the icon reading code over to the IconReader type.
    /// <summary>
    /// Wraps necessary Shell32.dll structures and functions required to retrieve Icon Handles using SHGetFileInfo. 
    /// </summary> 
    internal class Shell32
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SHITEMID
        {
            public ushort cb;
            [MarshalAs(UnmanagedType.LPArray)]
            public byte[] abID;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ITEMIDLIST
        {
            public SHITEMID mkid;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BROWSEINFO
        {
            public IntPtr hwndOwner;
            public IntPtr pidlRoot;
            public IntPtr pszDisplayName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpszTitle;
            public uint ulFlags;
            public IntPtr lpfn;
            public int lParam;
            public IntPtr iImage;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);
    }

    /// <summary>
    /// Wraps necessary functions imported from User32.dll. 
    /// </summary>
    internal class User32
    {
        /// <summary>
        /// Provides access to function required to delete handle. This method is used internally
        /// and is not required to be called separately.
        /// </summary>
        /// <param name="hIcon">Pointer to icon handle.</param>
        /// <returns>N/A</returns>
        [DllImport("User32.dll")]
        public static extern int DestroyIcon(IntPtr hIcon);
    }
}
