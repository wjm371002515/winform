using System;
using System.Collections.Generic;
using System.Text;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// 提供用户硬件唯一信息的辅助类
    /// </summary>
    public class FingerprintHelper
    {
        /// <summary>
        /// 根据用户各种硬件（CPU、网卡、磁盘、主板等）标识信息，
        /// 获取一个8位唯一标识数字
        /// </summary>
        public static string Value()
        {
            return pack(cpuId()
                    + biosId()
                    + diskId()
                    + baseId()
                    + videoId()
                    + macId());
        }

        /// <summary>
        /// 获取硬件的标识信息
        /// </summary>
        private static string identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result="";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString()=="True")
                {
                    //Only get the first one
                    if (result=="")
                    {
                        try
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }

                }
            }
            return result;
        }

        /// <summary>
        /// 获取硬件的标识信息
        /// </summary>
        private static string identifier(string wmiClass, string wmiProperty)
        {
            string result="";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                //Only get the first one
                if (result=="")
                {
                    try
                    {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch
                    {
                    }
                }

            }
            return result;
        }

        /// <summary>
        /// 获取CPU信息
        /// </summary>
        private static string cpuId()
        {
            //Uses first CPU identifier available in order of preference
            //Don't get all identifiers, as very time consuming
            string retVal = identifier("Win32_Processor", "UniqueId");
            if (retVal=="") //If no UniqueID, use ProcessorID
            {
                retVal = identifier("Win32_Processor", "ProcessorId");
                if (retVal=="") //If no ProcessorId, use Name
                {
                    retVal = identifier("Win32_Processor", "Name");
                    if (retVal=="") //If no Name, use Manufacturer
                    {
                        retVal = identifier("Win32_Processor", "Manufacturer");
                    }

                    //Add clock speed for extra security
                    retVal +=identifier("Win32_Processor", "MaxClockSpeed");
                }
            }

            return retVal;
        }

        /// <summary>
        /// 获取BOIS主板标识信息
        /// </summary>
        private static string biosId()
        {
            return identifier("Win32_BIOS", "Manufacturer")
                    + identifier("Win32_BIOS", "SMBIOSBIOSVersion")
                    + identifier("Win32_BIOS", "IdentificationCode")
                    + identifier("Win32_BIOS", "SerialNumber")
                    + identifier("Win32_BIOS", "ReleaseDate")
                    + identifier("Win32_BIOS", "Version");
        }

        /// <summary>
        /// 获取主硬盘的标识信息
        /// </summary>
        /// <returns></returns>
        private static string diskId()
        {
            return identifier("Win32_DiskDrive", "Model")
                    + identifier("Win32_DiskDrive", "Manufacturer")
                    + identifier("Win32_DiskDrive", "Signature")
                    + identifier("Win32_DiskDrive", "TotalHeads");
        }

        /// <summary>
        /// 获取主板的标识信息
        /// </summary>
        /// <returns></returns>
        private static string baseId()
        {
            return identifier("Win32_BaseBoard", "Model")
                    + identifier("Win32_BaseBoard", "Manufacturer")
                    + identifier("Win32_BaseBoard", "Name")
                    + identifier("Win32_BaseBoard", "SerialNumber");
        }

        /// <summary>
        /// 获取主视频控制器标识信息
        /// </summary>
        /// <returns></returns>
        private static string videoId()
        {
            return identifier("Win32_VideoController", "DriverVersion")
                    + identifier("Win32_VideoController", "Name");
        }

        /// <summary>
        /// 获取第一个可用网卡信息
        /// </summary>
        /// <returns></returns>
        private static string macId()
        {
            return identifier("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled");
        }

        /// <summary>
        /// 转换内容为8位数字
        /// </summary>
        private static string pack(string text)
        {
            string retVal;
            int x = 0;
            int y = 0;
            foreach (char n in text)
            {
                y++;
                x += (n*y);
            }
            retVal = x.ToString() + "00000000";

            return retVal.Substring(0, 8);
        }
    }
}
