using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// 配置文件读取辅助类
    /// </summary>
    public static class ConfigurationUtil
    {
        /// <summary>
        /// 读取指定名称的的配置项的值
        /// </summary>
        /// <typeparam name="T">节点的数据类型</typeparam>
        /// <param name="key">节点名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static T GetValue<T>(string key, T defaultValue)
        {
            string value = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrEmpty(value))
            {
                if (typeof(T).BaseType == typeof(Enum))
                {
                    return (T)Enum.Parse(typeof(T), value, true);
                }

                return (T)Convert.ChangeType(value, typeof(T));
            }
            return defaultValue;
        }

        /// <summary>
        /// 设置配置节点的值
        /// </summary>
        /// <param name="key">节点名称</param>
        /// <param name="value">设置的新值</param>
        public static void SetValue(string key, object value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationElement element = config.AppSettings.Settings[key];
            if (element != null)
            {
                if (!element.Value.Equals(value))
                {
                    element.Value = value.ToString();
                }
            }
            else
            {
                config.AppSettings.Settings.Add(key, value.ToString());
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
