using JCodes.Framework.jCodesenum;
using System;
using System.ServiceModel;

namespace JCodes.Framework.Common.Network
{
    /// <summary>
    /// WCF服务包装类，避免使用Using等方式导致服务出错的问题
    /// </summary>
    public static class WcfExtensions
    {
        public static void Using<T>(this T client, Action<T> work)
            where T : ICommunicationObject
        {
            try
            {
                work(client);
                client.Close();
            }
            catch (CommunicationException ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(WcfExtensions));
                client.Abort();
            }
            catch (TimeoutException ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(WcfExtensions));
                client.Abort();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(WcfExtensions));
                client.Abort();
                throw;
            }
        }
    } 
}
