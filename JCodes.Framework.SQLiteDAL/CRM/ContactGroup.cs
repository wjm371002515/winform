using System.Collections;
using System.Data;
using System.Collections.Generic;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Common.Databases;

namespace JCodes.Framework.SQLiteDAL
{
    /// <summary>
    /// 联系人组别
    /// </summary>
	public class ContactGroup : BaseDALSQLite<ContactGroupInfo>, IContactGroup
	{
		#region 对象实例及构造函数

		public static ContactGroup Instance
		{
			get
			{
				return new ContactGroup();
			}
		}
		public ContactGroup() : base("T_CRM_ContactGroup","ID")
        {
            this.sortField = "HandNo";
            this.isDescending = false;
        }

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override ContactGroupInfo DataReaderToEntity(IDataReader dataReader)
		{
			ContactGroupInfo info = new ContactGroupInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			
			info.ID = reader.GetString("ID");
			info.PID = reader.GetString("PID");
			info.HandNo = reader.GetString("HandNo");
			info.Name = reader.GetString("Name");
			info.Note = reader.GetString("Note");
			info.Creator = reader.GetString("Creator");
			info.CreateTime = reader.GetDateTime("CreateTime");
			info.Editor = reader.GetString("Editor");
			info.EditTime = reader.GetDateTime("EditTime");
            info.Dept_ID = reader.GetString("Dept_ID");
            info.Company_ID = reader.GetString("Company_ID");			
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(ContactGroupInfo obj)
		{
		    ContactGroupInfo info = obj as ContactGroupInfo;
			Hashtable hash = new Hashtable(); 
			
			hash.Add("ID", info.ID);
 			hash.Add("PID", info.PID);
 			hash.Add("HandNo", info.HandNo);
 			hash.Add("Name", info.Name);
 			hash.Add("Note", info.Note);
 			hash.Add("Creator", info.Creator);
 			hash.Add("CreateTime", info.CreateTime);
 			hash.Add("Editor", info.Editor);
 			hash.Add("EditTime", info.EditTime);
             hash.Add("Dept_ID", info.Dept_ID);
            hash.Add("Company_ID", info.Company_ID);				
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
            dict.Add("PID", "上级ID");
            dict.Add("HandNo", "编号");
            dict.Add("Name", "分组名称");
            dict.Add("Note", "备注");
            dict.Add("Creator", "创建人");
            dict.Add("CreateTime", "创建时间");
            dict.Add("Editor", "编辑人");
            dict.Add("EditTime", "编辑时间");
            dict.Add("Dept_ID", "所属部门");
            dict.Add("Company_ID", "所属公司");
            #endregion

            return dict;
        }

        /// <summary>
        /// 根据用户，获取树形结构的分组列表
        /// </summary>
        public List<ContactGroupNodeInfo> GetTree(string creator)
        {
            string condition = !string.IsNullOrEmpty(creator) ? string.Format("AND Creator='{0}'", creator) : "";
            List<ContactGroupNodeInfo> nodeList = new List<ContactGroupNodeInfo>();
            string sql = string.Format("Select * From {0} Where 1=1 {1} Order By PID, HandNo ", tableName, condition);

            DataTable dt = base.SqlTable(sql);
            DataRow[] dataRows = dt.Select(string.Format(" PID = '{0}' ", -1));
            for (int i = 0; i < dataRows.Length; i++)
            {
                string id = dataRows[i]["ID"].ToString();
                ContactGroupNodeInfo nodeInfo = GetNode(id, dt);
                nodeList.Add(nodeInfo);
            }

            return nodeList;
        }

        private ContactGroupNodeInfo GetNode(string id, DataTable dt)
        {
            ContactGroupInfo info = this.FindByID(id);
            ContactGroupNodeInfo nodeInfo = new ContactGroupNodeInfo(info);

            DataRow[] dChildRows = dt.Select(string.Format(" PID='{0}'", id));
            for (int i = 0; i < dChildRows.Length; i++)
            {
                string childId = dChildRows[i]["ID"].ToString();
                ContactGroupNodeInfo childNodeInfo = GetNode(childId, dt);
                nodeInfo.Children.Add(childNodeInfo);
            }
            return nodeInfo;
        }

        /// <summary>
        /// 根据联系人ID，获取客户对应的分组集合
        /// </summary>
        /// <param name="contactId">联系人ID</param>
        /// <returns></returns>
        public List<ContactGroupInfo> GetByContact(string contactId)
        {
            string sql = string.Format(@"Select t.* From {0} t inner join T_CRM_ContactGroup_Contact m 
            ON t.ID = m.ContactGroup_ID Where m.Contact_ID = '{1}' ", tableName, contactId);

            return base.GetList(sql);
        }

    }
}