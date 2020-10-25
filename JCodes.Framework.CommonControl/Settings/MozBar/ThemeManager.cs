using System;
using System.Runtime.InteropServices;  
using System.Drawing;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.CommonControl.Settings
{	
	[StructLayout(LayoutKind.Sequential)]
	public struct DLLVERSIONINFO
	{
		public int cbSize;
		public int dwMajorVersion;
		public int dwMinorVersion;
		public int dwBuildNumber;
		public int dwPlatformID;
	}
	
	/// <summary>
	/// Summary description for ThemeManager.
	/// </summary>
	public class ThemeManager
	{		
		// Declare functions used in uxTheme.dll and ComCtl32.dll

		[DllImport("uxTheme.dll", EntryPoint="GetThemeColor", ExactSpelling=true, PreserveSig=false, CharSet=CharSet.Unicode )]
		private extern static void GetThemeColor (System.IntPtr hTheme,
			int partID,
			int stateID,
			int propID,
			out int color);

		[DllImport( "uxtheme.dll", CharSet=CharSet.Unicode )]
		private static extern IntPtr OpenThemeData( IntPtr hwnd, string classes );
		
		[DllImport( "uxtheme.dll", EntryPoint="CloseThemeData", ExactSpelling=true, PreserveSig=false, CharSet=CharSet.Unicode) ]
		private static extern int CloseThemeData( IntPtr hwnd );

		[DllImport("uxtheme.dll", EntryPoint="GetWindowTheme", ExactSpelling=true,PreserveSig=false, CharSet=CharSet.Unicode)]
		private static extern int GetWindowTheme(IntPtr hWnd);
		
		[DllImport("uxtheme.dll", EntryPoint="IsThemeActive", ExactSpelling=true,PreserveSig=false, CharSet=CharSet.Unicode)]
		private static extern bool IsThemeActive();
		
		[DllImport("Comctl32.dll", EntryPoint="DllGetVersion", ExactSpelling=true,PreserveSig=false, CharSet=CharSet.Unicode)]
		private static extern int DllGetVersion(ref DLLVERSIONINFO s);

		public ThemeManager()
		{
			  
		}

		public bool _IsAppThemed()
		{
			try
			{
				// Check which version of ComCtl32 thats in use..
				DLLVERSIONINFO version = new DLLVERSIONINFO();
				version.cbSize = Marshal.SizeOf(typeof(DLLVERSIONINFO));
								
				int ret = DllGetVersion(ref version);
				// If MajorVersion > 5 themes are allowed.
				if (version.dwMajorVersion >= 6) 			
					return true;
				else
					return false;
			}
			catch (Exception ex)
			{
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(ThemeManager));
                MessageDxUtil.ShowError(ex.Message); 
				return false;
			}
		}

		public void _CloseThemeData(IntPtr hwnd)
		{
			try
			{
				CloseThemeData(hwnd);
			}
			catch (Exception ex)
			{
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(ThemeManager));
                MessageDxUtil.ShowError(ex.Message); 
			}
		}
		
		public IntPtr _OpenThemeData(IntPtr hwnd, string classes)
		{
			try
			{
				return OpenThemeData(hwnd, classes );
			}
			catch (Exception ex)
			{
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(ThemeManager));
                MessageDxUtil.ShowError(ex.Message); 
				return System.IntPtr.Zero;
			}
		}

		public int _GetWindowTheme(IntPtr hwnd)
		{
			try
			{
				return GetWindowTheme(hwnd);
			}
			catch (Exception ex)
			{
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(ThemeManager));
                MessageDxUtil.ShowError(ex.Message); 
				return -1;
			}

		}

		public bool _IsThemeActive()
		{
			try
			{
				return IsThemeActive();
			}
			catch (Exception ex)
			{
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(ThemeManager));
                MessageDxUtil.ShowError(ex.Message); 
				return false;
			}
		}

		public Color _GetThemeColor ( IntPtr hTheme, int partID, int stateID,int propID )
		{
			int color;

			try 
			{
				GetThemeColor ( hTheme, partID, stateID, propID, out color );
				return ColorTranslator.FromWin32 ( color );
			}
			catch (Exception ex) 
			{
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(ThemeManager));
                MessageDxUtil.ShowError(ex.Message); 
				return Color.Empty;
			}
		}
	}
}
