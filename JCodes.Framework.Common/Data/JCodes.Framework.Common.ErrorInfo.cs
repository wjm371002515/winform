using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Office;
using JCodes.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using JCodes.Framework.Common.Extension;

namespace JCodes.Framework.Common
{
    public class ErrorInfo
    {
        private Dictionary<String, ErrornoInfo> dicErrInfo = new Dictionary<String, ErrornoInfo>();

        public ErrorInfo()
        {
        }

        private Dictionary<String, ErrornoInfo> InitErrorInfo()
        {
            Dictionary<String, ErrornoInfo> dicErrInfo = Cache.Instance["DicErrInfo"] as Dictionary<String, ErrornoInfo>;

            if (dicErrInfo == null)
            {
                string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + @"Data\error.xml";
                // 如果文件不存在，则返回空
                if (!FileUtil.FileIsExist(filePath)) return null;

                // 读取Icon文件
                XmlHelper xmlHelper = new XmlHelper(filePath);
                XmlNodeList nodeLst = xmlHelper.Read("errors");

                dicErrInfo = new Dictionary<String, ErrornoInfo>();
                dicErrInfo.Clear();

                for (var i = 0; i < nodeLst.Count; i++)
                {
                    ErrornoInfo errornoInfo = new ErrornoInfo();
                    var node = nodeLst[i];
                    // <error id="E100001" name="ERR_TEST" chinesename="" loglevel="" remark="" />
                    if (node.Attributes["id"] != null) errornoInfo.Gid = node.Attributes["id"].Value;
                    if (node.Attributes["name"] != null) errornoInfo.Name = node.Attributes["name"].Value;
                    if (node.Attributes["chinesename"] != null) errornoInfo.ChineseName = "[" + node.Attributes["chinesename"].Value + "]";
                    if (node.Attributes["loglevel"] != null) errornoInfo.LogLevel = Convert.ToInt16(node.Attributes["loglevel"].Value);
                    if (node.Attributes["remark"] != null) errornoInfo.Remark = node.Attributes["remark"].Value;

                    dicErrInfo.Add(errornoInfo.Name, errornoInfo);
                }
                Cache.Instance["DicErrInfo"] = dicErrInfo;
            }

            return dicErrInfo;
        }

        /// <summary>
        /// 获取某个具体的错误信息
        /// </summary>
        /// <param name="key">错误号KEY值</param>
        /// <returns>返回错误信息</returns>
        public ErrornoInfo GetErrorInfo(String key) { 
            if (string.IsNullOrEmpty(key))
                return null;

            if (Cache.Instance["DicErrInfo"] as Dictionary<String, ErrornoInfo> == null)
            {
                dicErrInfo = InitErrorInfo();
            }
            else
            {
                dicErrInfo = Cache.Instance["DicErrInfo"] as Dictionary<String, ErrornoInfo>;
            }

            if (!dicErrInfo.Keys.Contains(key))
                return null;

            return dicErrInfo[key];
        }
        
        /// <summary>
        /// 重新初始化错误信息
        /// </summary>
        public void ReInitErrorInfo() {
            Cache.Instance["DicErrInfo"] = null;

            InitErrorInfo();
        }

        public Dictionary<String, ErrornoInfo> GetAllErrorInfo() {
            return InitErrorInfo();
        }
    }
}
