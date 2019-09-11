using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.SQLServerDAL
{
    /// <summary>
    /// 政策法规公告动态
    /// </summary>
    public class Information : BaseDALSQLServer<InformationInfo>, IInformation
	{
		#region 对象实例及构造函数

		public static Information Instance
		{
			get
			{
				return new Information();
			}
		}
		public Information() : base("TB_Information","ID")
		{
            this.sortField = "EditTime";
            this.IsDescending = true;
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override InformationInfo DataReaderToEntity(IDataReader dataReader)
		{
			InformationInfo info = new InformationInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);

            info.Id = reader.GetInt32("Id");
			info.Title = reader.GetString("Title");
			info.Content = reader.GetString("Content");
			info.Attachment_GUID = reader.GetString("Attachment_GUID");
            info.Category = ConvertCategory(reader.GetString("CATEGORY"));
			info.SubType = reader.GetString("SubType");
            info.EditorId = reader.GetInt32("EditorId");
            info.LastUpdateTime = reader.GetDateTime("LastUpdateTime");
			info.IsChecked = reader.GetInt32("IsChecked");
			info.CheckUser = reader.GetString("CheckUser");
			info.CheckTime = reader.GetDateTime("CheckTime");
			info.ForceExpire = reader.GetInt32("ForceExpire");
			info.TimeOut = reader.GetDateTime("TimeOut");
			
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(InformationInfo obj)
		{
		    InformationInfo info = obj as InformationInfo;
			Hashtable hash = new Hashtable();

            hash.Add("Id", info.Id);
 			hash.Add("Title", info.Title);
 			hash.Add("Content", info.Content);
 			hash.Add("Attachment_GUID", info.Attachment_GUID);
            hash.Add("Category", info.Category.ToString());
 			hash.Add("SubType", info.SubType);
            hash.Add("EditorId", info.EditorId);
            hash.Add("LastUpdateTime", info.LastUpdateTime);
 			hash.Add("IsChecked", info.IsChecked);
 			hash.Add("CheckUser", info.CheckUser);
 			hash.Add("CheckTime", info.CheckTime);
 			hash.Add("ForceExpire", info.ForceExpire);
 			hash.Add("TimeOut", info.TimeOut);
 				
			return hash;
		}

        private InformationCategory ConvertCategory(string strCategory)
        {
            InformationCategory categoryType = InformationCategory.其他;
            try
            {
                categoryType = (InformationCategory)Enum.Parse(typeof(InformationCategory), strCategory);
            }
            catch
            {
            }
            return categoryType;
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
            dict.Add("Title", "标题");
            dict.Add("Content", "内容");
            dict.Add("Attachment_GUID", "附件GUID");
            dict.Add("Category", "大类名称");
            dict.Add("SubType", "子类名称");
            dict.Add("Editor", "编辑者");
            dict.Add("EditTime", "编辑时间");
            dict.Add("IsChecked", "是否审批通过");
            dict.Add("CheckUser", "审批者");
            dict.Add("CheckTime", "审批时间");
            dict.Add("ForceExpire", "是否强制过期");
            dict.Add("TimeOut", "过期截止时间");
            #endregion

            return dict;
        }

        /// <summary>
        /// 获取我的通知信息
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <param name="infoType">信息类型</param>
        /// <returns></returns>
        public DataTable GetMyInformation(int userId, InformationCategory infoType)
        {
            string readIdString = string.Format(@"select INFORMATION_ID from TB_InformationStatus 
            where USER_ID='{0}' and CATEGORY='{1}' and STATUS=1 ", userId, infoType.ToString());//已读ID

            string infoSql = string.Format(@" SELECT ID, TITLE, ATTACHMENT_GUID, EDITOR, EDITTIME FROM TB_INFORMATION 
            WHERE Category='{2}' 
            AND (ID NOT IN ({0})  OR (ForceExpire=1 AND TIME_OUT>'{1}') ) 
            ORDER BY EDITTIME DESC", readIdString, DateTime.Now.ToShortDateString(), infoType.ToString());

            return SqlTable(infoSql);
        }
    }
}