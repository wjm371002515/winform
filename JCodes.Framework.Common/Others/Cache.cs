using System;
using System.Collections.Generic;
using System.Text;

namespace JCodes.Framework.Common.Office
{
    /// <summary>
    /// 全局统一的缓存类
    /// </summary>
    public class Cache
    {
        private SortedDictionary<string, object> dic = new SortedDictionary<string, object>();
        private static volatile Cache instance = null;
        private static object lockHelper = new object();

        private Cache()
        {

        }

        /// <summary>
        /// 添加指定的键值元素
        /// </summary>
        /// <param name="key">元素的键</param>
        /// <param name="value">元素的值对象</param>
        public void Add(string key, object value)
        {
            dic.Add(key, value);
        }

        /// <summary>
        /// 移除指定的键
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            dic.Remove(key);
        }

        /// <summary>
        /// 获取索引的对象
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public object this[string index]
        {
            get
            {
                if (dic.ContainsKey(index))
                    return dic[index];
                else
                    return null;
            }
            set { dic[index] = value; }
        }

        /// <summary>
        /// 单件实例
        /// </summary>
        public static Cache Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new Cache();
                        }
                    }
                }
                return instance;
            }
        }
    }
}