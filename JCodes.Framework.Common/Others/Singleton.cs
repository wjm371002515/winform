using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// 创建一个类对象的单件实例，类对象的构造函数不能为Public修饰符的，一般为private。
    /// </summary>
    /// <remarks>http://www.codeproject.com/KB/architecture/GenericSingletonPattern.aspx</remarks>
    /// <typeparam name="T">待创建的对象类</typeparam>
    public static class Singleton<T> where T : class
    {
        static volatile T _instance;
        static object _lock = new object();

        static Singleton()
        {
        }

        /// <summary>
        /// 创建/获取一个可以new的类对象的单件实例
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            ConstructorInfo constructor = null;

                            try
                            {
                                // 构造函数不包含public修饰符的
                                constructor = typeof(T).GetConstructor(BindingFlags.Instance |
                                              BindingFlags.NonPublic, null, new Type[0], null);
                            }
                            catch (Exception exception)
                            {
                                throw new InvalidOperationException(exception.Message, exception);
                            }

                            if (constructor == null || constructor.IsAssembly)
                            {
                                throw new InvalidOperationException(string.Format("在'{0}'里面没有找到private或者protected的构造函数。", typeof(T).Name));
                            }

                            _instance = (T)constructor.Invoke(null);
                        }
                    }

                return _instance;
            }
        }
    }
}
