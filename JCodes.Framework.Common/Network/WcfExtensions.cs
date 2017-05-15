using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace JCodes.Framework.Common
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
            catch (CommunicationException e)
            {
                LogTextHelper.WriteLine(e.ToString());
                client.Abort();
            }
            catch (TimeoutException e)
            {
                LogTextHelper.WriteLine(e.ToString());
                client.Abort();
            }
            catch (Exception e)
            {
                LogTextHelper.WriteLine(e.ToString());
                client.Abort();
                throw;
            }
        }
    } 
}
