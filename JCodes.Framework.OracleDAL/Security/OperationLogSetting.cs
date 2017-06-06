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

namespace JCodes.Framework.OracleDAL
{
    /// <summary>
    /// 记录操作日志的数据表配置
    /// </summary>
    public class OperationLogSetting : BaseDALOracle<OperationLogSettingInfo>, IOperationLogSetting
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
            : base(OraclePortal.gc._securityTablePre+"OperationLogSetting", "ID")
        {
            this.SeqName = "";//由于字符型组件，不需要序列
            this.SortField = "CreateTime";
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

            info.ID = reader.GetString("ID");
            info.Forbid = reader.GetInt32("Forbid") > 0;
            info.TableName = reader.GetString("TableName");
            info.InsertLog = reader.GetInt32("InsertLog") > 0;
            info.DeleteLog = reader.GetInt32("DeleteLog") > 0;
            info.UpdateLog = reader.GetInt32("UpdateLog") > 0;
            info.Note = reader.GetString("Note");
            info.Creator = reader.GetString("Creator");
            info.Creator_ID = reader.GetString("Creator_ID");
            info.CreateTime = reader.GetDateTime("CreateTime");
            info.Editor = reader.GetString("Editor");
            info.Editor_ID = reader.GetString("Editor_ID");
            info.EditTime = reader.GetDateTime("EditTime");

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

            hash.Add("ID", info.ID);
            hash.Add("Forbid", info.Forbid ? 1 : 0);
            hash.Add("TableName", info.TableName);
            hash.Add("InsertLog", info.InsertLog ? 1 : 0);
            hash.Add("DeleteLog", info.DeleteLog ? 1 : 0);
            hash.Add("UpdateLog", info.UpdateLog ? 1 : 0);
            hash.Add("Note", info.Note);
            hash.Add("Creator", info.Creator);
            hash.Add("Creator_ID", info.Creator_ID);
            hash.Add("CreateTime", info.CreateTime);
            hash.Add("Editor", info.Editor);
            hash.Add("Editor_ID", info.Editor_ID);
            hash.Add("EditTime", info.EditTime);

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
            dict.Add("ID", "编号");
            dict.Add("Forbid", "是否禁用");
            dict.Add("TableName", "数据库表");
            dict.Add("InsertLog", "记录插入日志");
            dict.Add("DeleteLog", "记录删除日志");
            dict.Add("UpdateLog", "记录更新日志");
            dict.Add("Note", "备注");
            dict.Add("Creator", "创建人");
            dict.Add("Creator_ID", "创建人ID");
            dict.Add("CreateTime", "创建时间");
            dict.Add("Editor", "编辑人");
            dict.Add("Editor_ID", "编辑人ID");
            dict.Add("EditTime", "编辑时间");
            #endregion

            return dict;
        }

    }
}