using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Common.Databases;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace JCodes.Framework.SQLServerDAL
{
	/// <summary>
	/// Province 的摘要说明。
	/// </summary>
    public class Sysparameter : BaseDALSQLServer<SysparameterInfo>, ISysparameter
	{
		#region 对象实例及构造函数

		public static Sysparameter Instance
		{
			get
			{
				return new Sysparameter();
			}
		}
        public Sysparameter()
            : base(SQLServerPortal.gc._basicTablePre + "Sysparameter", "ID")
		{
            sortField = "Seq";
            IsDescending = false;
		}

		#endregion

        public List<SysparameterInfo> GetSysparameterBysysId(int sysId)
        {
            string sql = string.Format("select ID, sysId, Name, Value, Control_type, Dic_no, Numlen, Remark, Seq, Editor, EditorTime from {0}Sysparameter where sysId={1}", SQLServerPortal.gc._basicTablePre, sysId);
            return base.GetList(sql, null);
        }

        public int UpdateSysparameter(List<SysparameterInfo> info)
        {
            Int32 row_count = 0;
            Database db = CreateDatabase();

            foreach (var sysparameterInfo in info)
            {
                // 记录修改操作
                OperationLogOfUpdate(sysparameterInfo, sysparameterInfo.Id, null);//根据设置记录操作日志
                string sql = string.Format("UPDATE {0}Sysparameter set value='{1}', Editor='{2}', EditorTime='{3}'  where ID={4} and sysId={5}", SQLServerPortal.gc._basicTablePre, sysparameterInfo.SysValue, sysparameterInfo.EditorId, sysparameterInfo.LastUpdateTime, sysparameterInfo.Id, sysparameterInfo.SysId);
                DbCommand dbCommand = db.GetSqlStringCommand(sql);
                row_count += db.ExecuteNonQuery(dbCommand);
            }
            return row_count;
        }

        /// <summary>
        /// 将DataReader的属性值转化为实体类的属性值，返回实体类
        /// </summary>
        /// <param name="dr">有效的DataReader对象</param>
        /// <returns>实体类对象</returns>
        protected override SysparameterInfo DataReaderToEntity(IDataReader dataReader)
        {
            SysparameterInfo sysparameterInfo = new SysparameterInfo();
            SmartDataReader reader = new SmartDataReader(dataReader);

            // ID, sysId, Name, Value, Control_type, Dic_no, Numlen, Remark, Seq, Editor, EditorTime 
            sysparameterInfo.Id = reader.GetInt32("Id");
            sysparameterInfo.SysId = (short)reader.GetInt32("SysId");
            sysparameterInfo.Name = reader.GetString("Name");
            sysparameterInfo.SysValue = reader.GetString("SysValue");
            sysparameterInfo.ControlType = reader.GetInt16("Control_type");
            sysparameterInfo.DicNo = reader.GetInt32("Dic_no");
            sysparameterInfo.NumLen = reader.GetInt32("NumLen");
            sysparameterInfo.Remark = reader.GetString("Remark");
            sysparameterInfo.Seq = reader.GetString("Seq");
            sysparameterInfo.EditorId = reader.GetInt32("EditorId");
            sysparameterInfo.LastUpdateTime = reader.GetDateTime("LastUpdateTime");

            return sysparameterInfo;
        }

        /// <summary>
        /// 将实体对象的属性值转化为Hashtable对应的键值
        /// </summary>
        /// <param name="obj">有效的实体对象</param>
        /// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(SysparameterInfo obj)
        {
            SysparameterInfo info = obj as SysparameterInfo;
            Hashtable hash = new Hashtable();

            hash.Add("Id", info.Id);
            hash.Add("SysId", info.SysId);
            hash.Add("SysValue", info.SysValue);
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
            // SysId, Name, Value, Remark, Seq, Editor, EditorTime

            Dictionary<string, string> dict = new Dictionary<string, string>();
            #region 添加别名解析
            dict.Add("SysId", "参数类型");
            dict.Add("Name", "参数名称");
            dict.Add("SysValue", "参数值");
            dict.Add("Remark", "备注");
            dict.Add("Seq", "排序");
            dict.Add("EditorId", "编辑人");
            dict.Add("LastUpdateTime", " 更新时间");
            #endregion

            return dict;
        }
    }
}