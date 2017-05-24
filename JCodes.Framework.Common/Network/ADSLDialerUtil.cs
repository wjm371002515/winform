using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotRas;
using System.Collections.ObjectModel;
using System.Threading;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Entity;

namespace JCodes.Framework.Common.Network
{
    public delegate void ADSLHandleDelegate(string result);

    /// <summary>
    /// ADSL拨号操作辅助类
    /// </summary>
    public class ADSLDialerUtil
    {
        /// <summary>
        /// 提示操作事件
        /// </summary>
        public event ADSLHandleDelegate OnHandle;

        #region 拨号相关操作

        public List<CListItem> InitRAS()
        {
            List<CListItem> list = new List<CListItem>();
            try
            {
                ReadOnlyCollection<RasConnection> ret = RasConnection.GetActiveConnections();
                foreach (RasConnection connection in ret)
                {
                    list.Add(new CListItem(connection.EntryName, connection.EntryId.ToString()));
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(ADSLDialerUtil));
                throw;
            }

            return list;
        }

        /// <summary>
        /// 测试拨号连接
        /// </summary>
        public void Connect(string RasName)
        {
            try
            {
                if (OnHandle != null)
                {
                    OnHandle(string.Format("正在拨号 {0}...", RasName));
                }

                RasDialer dialer = new RasDialer();
                dialer.EntryName = RasName;
                dialer.PhoneNumber = " ";
                dialer.AllowUseStoredCredentials = true;
                dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
                dialer.Timeout = 1000;
                dialer.Dial();
                
                Thread.Sleep(100);

                if (OnHandle != null)
                {
                    OnHandle(string.Format("拨号完成"));
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(ADSLDialerUtil));
                throw;
            }
        }

        /// <summary>
        /// 断开网络连接
        /// </summary>
        public void DisConnect()
        {
            if (OnHandle != null)
            {
                OnHandle(string.Format("正在断开连接..."));
            }

            ReadOnlyCollection<RasConnection> conList = RasConnection.GetActiveConnections();
            foreach (RasConnection con in conList)
            {
                con.HangUp();
            }

            if (OnHandle != null)
            {
                OnHandle(string.Format("断开连接完成"));
            }
        }

        /// <summary>
        /// 重新链接
        /// </summary>
        public void ReConnect(string RasName)
        {
            if (!string.IsNullOrEmpty(RasName))
            {
                if (OnHandle != null)
                {
                    OnHandle(string.Format("正在重新连接..."));
                }

                DisConnect();
                Thread.Sleep(500);
                Connect(RasName);
                Thread.Sleep(500);

                if (OnHandle != null)
                {
                    OnHandle(string.Format("重新连接完成"));
                }
            }
        }


        /// <summary>
        /// 获取最新的IP
        /// </summary>
        /// <param name="EntryId"></param>
        /// <returns></returns>
        public string GetNewIP(string EntryId)
        {
            if (string.IsNullOrEmpty(EntryId)) return "";

            string result = "";
            foreach (RasConnection connection in RasConnection.GetActiveConnections())
            {
                if (connection.EntryId.ToString() == EntryId)
                {
                    RasIPInfo ipAddresses = (RasIPInfo)connection.GetProjectionInfo(RasProjectionType.IP);
                    if (ipAddresses != null)
                    {
                        result = ipAddresses.IPAddress.ToString();
                        break;
                    }
                }
            }
            return result;
        }

        #endregion
    }
}
