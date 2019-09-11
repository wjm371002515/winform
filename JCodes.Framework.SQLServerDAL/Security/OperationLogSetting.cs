using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Common.Databases;

namespace JCodes.Framework.SQLServerDAL
{
    /// <summary>
    /// 记录操作日志的数据表配置
    /// </summary>
    public class OperationLogSetting : BaseDALSQLServer<OperationLogSettingInfo>, IOperationLogSetting
    {
        #region 对象实例及构造函数

        public static OperationLogSetting Instance
        {
            get
            {
                return new OperationLogSetting();
            }
        }
        public OperationLogSetting()
            : base(SQLServerPortal.gc._securityTablePre + "OperationLogSetting", "ID")
        {
            this.SortField = "CreatorTime";
            this.IsDescending = true;
        }

        #endregion

        /// <summary>
        /// 将DataReader的属性值转化为实体类的属性值，返回实体类
        /// </summary>
        /// <param name="dr">有效的DataReader对象</param>
        /// <returns>实体类对象</returns>
        protected override OperationLogSettingInfo DataReaderToEntity(IDataReader dataReader)
        {
            OperationLogSettingInfo info = new OperationLogSettingInfo();
            SmartDataReader reader = new SmartDataReader(dataReader);

            info.Id = reader.GetInt32("Id");
            info.IsForbid = reader.GetInt32("IsForbid");
            info.TableName = reader.GetString("TableName");
            info.IsInsertLog = reader.GetInt32("IsInsertLog");
            info.IsDeleteLog = reader.GetInt32("IsDeleteLog");
            info.IsUpdateLog = reader.GetInt32("IsUpdateLog");
            info.Remark = reader.GetString("Remark");
            //info.Creator = reader.GetString("Creator");
            info.CreatorId = reader.GetInt32("CreatorId");
            info.CreatorTime = reader.GetDateTime("CreatorTime");
            //info.Editor = reader.GetString("Editor");
            info.EditorId = reader.GetInt32("EditorId");
            info.LastUpdateTime = reader.GetDateTime("LastUpdateTime");

            return info;
        }

        /// <summary>
        /// 将实体对象的属性值转化为Hashtable对应的键值
        /// </summary>
        /// <param name="obj">有效的实体对象</param>
        /// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(OperationLogSettingInfo obj)
        {
            OperationLogSettingInfo info = obj as OperationLogSettingInfo;
            Hashtable hash = new Hashtable();

            hash.Add("Id", info.Id);
            hash.Add("IsForbid", info.IsForbid);
            hash.Add("TableName", info.TableName);
            hash.Add("IsInsertLog", info.IsInsertLog);
            hash.Add("IsDeleteLog", info.IsDeleteLog);
            hash.Add("IsUpdateLog", info.IsUpdateLog);
            hash.Add("Remark", info.Remark);
            //hash.Add("Creator", info.Creator);
            hash.Add("CreatorId", info.CreatorId);
            hash.Add("CreatorTime", info.CreatorTime);
            //hash.Add("Editor", info.Editor);
            hash.Add("EditorId", info.EditorId);
            hash.Add("LastUpdateTime", info.LastUpdateTime);

            return hash;
        }

        /// <summary>
        /// 获取字段中文别名（用于界面显示）的字典集合
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, string> GetColumnNameAlias()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            #region 添加别名解析
            dict.Add("Id", "编号");
            dict.Add("IsForbid", "是否禁用");
            dict.Add("TableName", "数据库表");
            dict.Add("IsInsertLog", "记录插入日志");
            dict.Add("IsDeleteLog", "记录删除日志");
            dict.Add("IsUpdateLog", "记录更新日志");
            dict.Add("Remark", "备注");
            //dict.Add("Creator", "创建人");
            dict.Add("CreatorId", "创建人ID");
            dict.Add("CreatorTime", "创建时间");
            //dict.Add("Editor", "编辑人");
            dict.Add("EditorId", "编辑人ID");
            dict.Add("LastUpdateTime", "编辑时间");
            #endregion

            return dict;
        }

    }
}