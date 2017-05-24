using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Caching;
using System.Linq;

namespace JCodes.Framework.Common.Office
{
    /// <summary>
    /// 基于MemoryCache的缓存辅助类
    /// </summary>
    public static class MemoryCacheHelper
	{
	    private static readonly Object locker = new object();
	 
        /// <summary>
        /// 创建一个缓存的键值，并指定响应的时间范围，如果失效，则自动获取对应的值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">对象的键</param>
        /// <param name="cachePopulate">获取缓存值的操作</param>
        /// <param name="slidingExpiration">失效的时间范围</param>
        /// <param name="absoluteExpiration">失效的绝对时间</param>
        /// <returns></returns>
	    public static T GetCacheItem<T>(String key, Func<T> cachePopulate, TimeSpan? slidingExpiration = null, DateTime? absoluteExpiration = null)
	    {
	        if(String.IsNullOrWhiteSpace(key)) throw new ArgumentException("Invalid cache key");
	        if(cachePopulate == null) throw new ArgumentNullException("cachePopulate");
	        if(slidingExpiration == null && absoluteExpiration == null) throw new ArgumentException("Either a sliding expiration or absolute must be provided");
	 
	        if(MemoryCache.Default[key] == null)
	        {
	            lock(locker)
	            {
	                if(MemoryCache.Default[key] == null)
	                {
	                    var item = new CacheItem(key, cachePopulate());
	                    var policy = CreatePolicy(slidingExpiration, absoluteExpiration);
	 
	                    MemoryCache.Default.Add(item, policy);
	                }
	            }
	        }
	 
	        return (T)MemoryCache.Default[key];
	    }
	 
	    private static CacheItemPolicy CreatePolicy(TimeSpan? slidingExpiration, DateTime? absoluteExpiration)
	    {
	        var policy = new CacheItemPolicy();
	 
	        if(absoluteExpiration.HasValue)
	        {
	            policy.AbsoluteExpiration = absoluteExpiration.Value;
	        }
	        else if(slidingExpiration.HasValue)
	        {
	            policy.SlidingExpiration = slidingExpiration.Value;
	        }
	 
	        policy.Priority = CacheItemPriority.Default;
	 
	        return policy;
	    }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public static void ClearCache()
        {
            List<string> cacheKeys = MemoryCache.Default.Select(kvp => kvp.Key).ToList();
            foreach (string cacheKey in cacheKeys)
            {
                MemoryCache.Default.Remove(cacheKey);
            }
        }

        /// <summary>
        /// 永久放置键值到缓存里面
        /// </summary>
        /// <param name="key">缓存的键</param>
        /// <param name="value">缓存的值</param>
        public static void AddItem(string key, object value)
        {
            lock (locker)
            {
                MemoryCache.Default.Add(key, value, DateTimeOffset.MaxValue);
            }
        }

        /// <summary>
        /// 在缓存里面移除对应的键值
        /// </summary>
        /// <param name="key">缓存的键</param>
        public static void RemoveItem(string key)
        {
            lock (locker)
            {
                if (MemoryCache.Default.Contains(key))
                {
                    MemoryCache.Default.Remove(key);
                }
            }
        }

        /// <summary>
        /// 在缓存里面获取指定的键值，如果键不存在，则返回null
        /// </summary>
        /// <param name="key">缓存的键</param>
        /// <returns></returns>
        public static object GetItem(string key)
        {
            lock (locker)
            {
                object result = null;
                if (MemoryCache.Default.Contains(key))
                {
                    result = MemoryCache.Default[key];
                }
                return result;
            }
        }
	}
}
