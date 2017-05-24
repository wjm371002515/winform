using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace JCodes.Framework.Common.Network
{
    /// <summary>
    /// 基于Newtonsoft.Json.dll的Json转换辅助类
    /// </summary>
    public class NewtonsoftJsonHelper
    {
        /// <summary>
        /// 把对象为json字符串
        /// </summary>
        /// <param name="obj">待序列号对象</param>
        /// <returns></returns>
        public string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        } 

        /// <summary>
        /// 返回处理过的时间（处理后格式yyyy-MM-dd HH:mm:ss）的Json字符串
        /// </summary>
        /// <param name="date">包含日期的类对象实例</param>
        /// <returns></returns>
        public string JsonDate(object date)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            return JsonConvert.SerializeObject(date, Formatting.Indented, timeConverter);
        }
    }
}
