using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// 根据业务对象的类型进行反射操作辅助类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Reflect<T> where T : class 
    {
        private static Hashtable ObjCache = new Hashtable();
        private static object syncRoot = new Object();

        /// <summary>
        /// 根据参数创建对象实例
        /// </summary>
        /// <param name="sName">对象全局名称</param>
        /// <param name="sFilePath">文件路径</param>
        /// <returns></returns>
        public static T Create(string sName, string sFilePath)
        {
            return Create(sName, sFilePath, true);
        }

        /// <summary>
        /// 根据参数创建对象实例
        /// </summary>
        /// <param name="sName">对象全局名称</param>
        /// <param name="sFilePath">文件路径</param>
        /// <param name="bCache">缓存集合</param>
        /// <returns></returns>
        public static T Create(string sName, string sFilePath, bool bCache)
        {
            string CacheKey = sName;
            T objType = null;
            if (bCache)
            {
                objType = (T)ObjCache[CacheKey];    //从缓存读取 
                if (!ObjCache.ContainsKey(CacheKey))
                {
                    lock (syncRoot)
                    {
                        objType = CreateInstance(CacheKey, sFilePath);
                        ObjCache.Add(CacheKey, objType);//缓存数据访问对象
                    }
                }
            }
            else
            {
                objType = CreateInstance(CacheKey, sFilePath);
            }

            return objType;
        }

        /// <summary>
        /// 根据全名和路径构造对象
        /// </summary>
        /// <param name="sName">对象全名</param>
        /// <param name="sFilePath">程序集路径</param>
        /// <returns></returns>
        private static T CreateInstance(string sName, string sFilePath)
        {
            Assembly assemblyObj = Assembly.Load(sFilePath);
            
            if (assemblyObj == null) 
            {
                throw new ArgumentNullException("sFilePath", string.Format("无法加载sFilePath={0} 的程序集", sFilePath));
            }

            T obj = (T)assemblyObj.CreateInstance(sName); //反射创建 
            return obj;
        }
    }
}
