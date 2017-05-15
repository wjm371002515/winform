using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// 对业务类进行构造的工厂类
    /// </summary>
    /// <typeparam name="T">业务对象类型</typeparam>
    public class BLLFactory<T> where T : class
    {
        private static Hashtable objCache = new Hashtable();
        private static object syncRoot = new Object();

        /// <summary>
        /// 创建或者从缓存中获取对应业务类的实例
        /// </summary>
        public static T Instance
        {
            get
            {
                string CacheKey = typeof(T).FullName;
                T bll = (T)objCache[CacheKey];　 //从缓存读取  
                if (bll == null)
                {
                    lock (syncRoot)
                    {
                        if (bll == null)
                        {
                            bll = Reflect<T>.Create(typeof(T).FullName, typeof(T).Assembly.GetName().Name); //反射创建，并缓存
                            objCache.Add(typeof(T).FullName, bll);
                        }
                    }
                }
                return bll;
            }
        }
    } 
}
