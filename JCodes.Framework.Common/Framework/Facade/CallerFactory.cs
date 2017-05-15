using System;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using JCodes.Framework.Common;

namespace JCodes.Framework.Common.Facade
{
    /// <summary>
    /// 混合式框架或WCF框架中针对不同调用方式的工厂类（WCF或者Win调用）
    /// </summary>
    /// <typeparam name="T">接口类型</typeparam>
    public class CallerFactory<T>
    {
        private static Hashtable objCache = new Hashtable();
        private static object syncRoot = new Object();
        private static string callerNamespace = null;//Facade接口实现类的命名空间

        /// <summary>
        /// 创建或者从缓存中获取对应接口的实例
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
                            bll = CreateObject(); //反射创建，并缓存
                            if (bll != null)
                            {
                                objCache.Add(typeof(T).FullName, bll); //缓存BLL业务对象
                            }
                        }
                    }
                }
                return bll;
            }
        }

        /// <summary>
        /// 根据配置参数CallerType的值，创建不同的调用方式实现接口
        /// </summary>
        /// <returns></returns>
        private static T CreateObject()
        {      
            T objInterface = default(T);
                        
            Assembly tempAssembly = LoadAssembly();
            if (tempAssembly != null)
            {
                foreach (Type objType in tempAssembly.GetTypes())
                {
                    //IsAssignableFrom判断objType是否是继承了接口T
                    if (objType != null && typeof(T).IsAssignableFrom(objType) && !objType.IsInterface)
                    {
                        //分开两个if方便调试跟踪
                        if (objType.FullName.Contains(callerNamespace))
                        {
                            //使用objType.FullName.Contains来判断对象的名称，是因为可能存在多个Caller的情况
                            //如WCFService和LocalService同时实现的情况
                            //使用前缀限定，则只能有一个Caller对象被创建
                            objInterface = (T)Activator.CreateInstance(objType);
                            break;
                        }
                    }
                }
            }

            return objInterface;
        }

        /// <summary>
        /// 创建程序集对象
        /// </summary>
        private static Assembly LoadAssembly()
        {
            #region 获取配置类型及构建对象的命名空间，决定是WCF（通过WCF服务访问数据库）还是Win（传统方式访问数据库），默认为Win
            AppConfig config = new AppConfig();
            string callerType = config.AppConfigGet("CallerType");
            bool isWCF = !string.IsNullOrEmpty(callerType) && callerType.ToLower() == "wcf";

            //构建对象（Facade接口）所在的命名空间，如"WHC.WareHouseMis.Facade";            
            string defaultNamespace = typeof(T).Namespace;

            //Facade接口实现的程序集命名根据WCF或者Win约定名称方式构建，默认遵循WCF以ServiceCaller结尾，Winform以WinformCaller结尾
            //如WCF的实现命名空间可能为WHC.WareHouseMis.ServiceCaller；Winform的实现命名空间可能为WHC.WareHouseMis.WinformCaller。
            string rootNamespace = defaultNamespace.Substring(0, defaultNamespace.LastIndexOf('.'));
            callerNamespace = isWCF ? (rootNamespace + ".ServiceCaller") : (rootNamespace + ".WinformCaller");

            #endregion

            string assemblyName = typeof(T).Assembly.GetName().Name;//约定Facade接口和实现是同一个程序集
            Assembly tempAssembly = Assembly.Load(assemblyName);
            return tempAssembly;
        }

    }
}
