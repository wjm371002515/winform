using System.Collections;
using System.Data;
using System.Collections.Generic;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Databases;

namespace JCodes.Framework.OracleDAL
{
    /// <summary>
    /// 用户对指定内容的操作状态记录
    /// </summary>
    public class InformationStatus : BaseDALOracle<InformationStatusInfo>, IInformationStatus
	{
		#region 对象实例及构造函数

		public static InformationStatus Instance
		{
			get
			{
				return new InformationStatus();
			}
		}
		public InformationStatus() : base("TB_InformationStatus","ID")
        {
            this.SeqName = "";//由于字符型组件，不需要序列
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override InformationStatusInfo DataReaderToEntity(IDataReader dataReader)
		{
			InformationStatusInfo info = new InformationStatusInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			
			info.ID = reader.GetString("ID");
			info.Category = reader.GetString("Category");
			info.Information_ID = reader.GetString("Information_ID");
			info.Status = reader.GetInt32("Status");
			info.User_ID = reader.GetString("User_ID");
			
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(InformationStatusInfo obj)
		{
		    InformationStatusInfo info = obj as InformationStatusInfo;
			Hashtable hash = new Hashtable(); 
			
			hash.Add("ID", info.ID);
 			hash.Add("Category", info.Category);
 			hash.Add("Information_ID", info.Information_ID);
 			hash.Add("Status", info.Status);
 			hash.Add("User_ID", info.User_ID);
 				
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
            //dict.Add("ID", "编号");
            dict.Add("ID", "");
             dict.Add("Category", "信息类型");
             dict.Add("Information_ID", "信息ID");
             dict.Add("Status", "阅读状态");
             dict.Add("User_ID", "用户ID");
             #endregion

            return dict;
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="InfoType">信息类型</param>
        /// <param name="InfoID">信息主键ID</param>
        /// <param name="Status">状态</param>
        public void SetStatus(string UserID, InformationCategory InfoType, string InfoID, int Status)
        {
            if (!IsExistRecord(string.Format("USER_ID='{0}' and Category='{1}' and Information_ID='{2}'", UserID, InfoType, InfoID)))
            {
                InformationStatusInfo info = new InformationStatusInfo();
                info.User_ID = UserID;
                info.Category = InfoType.ToString();
                info.Information_ID = InfoID;
                info.Status = Status;
                base.Insert(info);
            }
            else
            {
                InformationStatusInfo info = FindSingle(string.Format("USER_ID='{0}' and Category='{1}' and Information_ID='{2}'", UserID, InfoType, InfoID));
                info.Status = Status;
                base.Update(info, info.ID);
            }
        }

        /// <summary>
        /// 匹配状态
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="InfoType">信息类型</param>
        /// <param name="InfoID">信息主键ID</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public bool CheckStatus(string UserID, InformationCategory InfoType, string InfoID, int Status)
        {
            return base.IsExistRecord(string.Format("USER_ID='{0}' and Category='{1}' and Information_ID='{2}' and STATUS={3}",
                UserID, InfoType.ToString(), InfoID, Status));
        }
    }
}