using System;
using System.IO;
using System.Text;
using System.Collections;
using System.ServiceProcess;
using System.Collections.Generic;
using System.Configuration.Install;

using Microsoft.Win32;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.Common.Winform
{
    /// <summary>
    /// window服务辅助类，包括安装、卸载、启动、停止、重新启动、判断服务是否存在等操作。
    /// </summary>
    public class WinServiceHelper
    {
        /// <summary>
        /// 安装Windows服务
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <param name="serviceFileName">服务文件路径</param>
        /// <returns></returns>
        public static bool InstallService(string serviceName, string serviceFileName)
        {
            if (!ServiceIsExisted(serviceName))
            {
                string[] cmdline = { };
                TransactedInstaller transactedInstaller = new TransactedInstaller();
                AssemblyInstaller assemblyInstaller = new AssemblyInstaller(serviceFileName, cmdline);
                transactedInstaller.Installers.Add(assemblyInstaller);
                transactedInstaller.Install(new System.Collections.Hashtable());

                return true;
            }
            else
            {
                throw new Exception(string.Format("{0} 服务已经存在", serviceName));
            }
        }

        /// <summary>
        /// 卸载Windows服务
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <param name="serviceFileName">服务文件路径</param>
        public static bool UnInstallService(string serviceName, string serviceFileName)
        {
            if (ServiceIsExisted(serviceName))
            {
                string[] cmdline = { };
                TransactedInstaller transactedInstaller = new TransactedInstaller();
                AssemblyInstaller assemblyInstaller = new AssemblyInstaller(serviceFileName, cmdline);
                transactedInstaller.Installers.Add(assemblyInstaller);
                transactedInstaller.Uninstall(null);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 另外一种安装、卸载Windows服务的方法
        /// </summary>
        /// <param name="install">安装还是卸载，true为安装，false为卸载</param>
        /// <param name="serviceFileName"></param>
        public static void InstallService2(bool install, string serviceFileName)
        {
            if (install)
            {
                //注册
                AssemblyInstaller Installer = new AssemblyInstaller();
                IDictionary saveState = new Hashtable();
                Installer.UseNewContext = true;
                Installer.Path = serviceFileName;
                saveState.Clear();
                Installer.Install(saveState);
                Installer.Commit(saveState);
                Installer.Dispose();
            }
            else
            {
                //卸载
                AssemblyInstaller Installer = new AssemblyInstaller();
                Installer.UseNewContext = true;
                Installer.Path = serviceFileName;
                Installer.Uninstall(null);
                Installer.Dispose();
            }
        }

        /// <summary>
        /// 判断window服务是否存在
        /// </summary>
        /// <param name="serviceName">window服务名称</param>
        /// <returns></returns>
        public static bool ServiceIsExisted(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController s in services)
            {
                if (s.ServiceName.Equals(serviceName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 等待某种预期的状态（如运行，停止等）
        /// </summary>
        /// <param name="serviceName">window服务名称</param>
        /// <param name="status">预期的状态</param>
        /// <param name="second">如果获取不到预期的状态，则等待多少秒</param>
        /// <returns></returns>
        public static bool WaitForStatus(string serviceName, ServiceControllerStatus status, int second)
        {
            bool result = false;
            bool exist = ServiceIsExisted(serviceName);
            if (exist)
            {
                ServiceController service = new ServiceController(serviceName);
                if (service != null)
                {
                    TimeSpan timeout = TimeSpan.FromMilliseconds(1000 * second);
                    service.WaitForStatus(ServiceControllerStatus.Running, timeout);

                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 启动window服务
        /// </summary>
        /// <param name="serviceName"></param>
        public static bool StartService(string serviceName)
        {
            try
            {
                ServiceController service = new ServiceController(serviceName);
                if (service.Status == ServiceControllerStatus.Running)
                {
                    return true;
                }
                else
                {
                    TimeSpan timeout = TimeSpan.FromMilliseconds(1000 * 10);
                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                }
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(WinServiceHelper));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="serviseName"></param>
        /// <returns></returns>
        public static bool StopService(string serviseName)
        {
            try
            {
                ServiceController service = new ServiceController(serviseName);
                if (service.Status == ServiceControllerStatus.Stopped)
                {
                    return true;
                }
                else
                {
                    TimeSpan timeout = TimeSpan.FromMilliseconds(1000 * 10);
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                }
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(WinServiceHelper));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 修改服务的启动项 2为自动,3为手动
        /// </summary>
        /// <param name="startType"></param>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static bool ChangeServiceStartType(int startType, string serviceName)
        {
            try
            {
                RegistryKey regist = Registry.LocalMachine;
                RegistryKey sysReg = regist.OpenSubKey("SYSTEM");
                RegistryKey currentControlSet = sysReg.OpenSubKey("CurrentControlSet");
                RegistryKey services = currentControlSet.OpenSubKey("Services");
                RegistryKey servicesName = services.OpenSubKey(serviceName, true);
                servicesName.SetValue("Start", startType);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(WinServiceHelper));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取服务启动类型 2为自动 3为手动 4 为禁用
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static string GetServiceStartType(string serviceName)
        {
            try
            {
                RegistryKey regist = Registry.LocalMachine;
                RegistryKey sysReg = regist.OpenSubKey("SYSTEM");
                RegistryKey currentControlSet = sysReg.OpenSubKey("CurrentControlSet");
                RegistryKey services = currentControlSet.OpenSubKey("Services");
                RegistryKey servicesName = services.OpenSubKey(serviceName, true);
                return servicesName.GetValue("Start").ToString();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(WinServiceHelper));
                return string.Empty;
            }
        }

        /// <summary>
        /// 验证服务是否启动
        /// </summary>
        /// <returns></returns>
        public static bool ServiceIsRunning(string serviceName)
        {
            ServiceController service = new ServiceController(serviceName);
            if (service.Status == ServiceControllerStatus.Running)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
