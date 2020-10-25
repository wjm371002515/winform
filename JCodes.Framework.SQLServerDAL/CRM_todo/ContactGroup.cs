using System.Collections;
using System.Data;
using System.Collections.Generic;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Databases;

namespace JCodes.Framework.SQLServerDAL
{
    /// <summary>
    /// 联系人组别
    /// </summary>
    public class ContactGroup : BaseDALSQLServer<ContactGroupInfo>, IContactGroup
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
            this.sortField = "UserCode";
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

            info.Id = reader.GetInt32("Id");
            info.Pid = reader.GetInt32("Pid");
			info.UserCode = reader.GetString("UserCode");
			info.Name = reader.GetString("Name");
            info.Remark = reader.GetString("Remark");
            info.CreatorId = reader.GetInt32("CreatorId");
            info.CreatorTime = reader.GetDateTime("CreatorTime");
            info.EditorId = reader.GetInt32("EditorId");
            info.LastUpdateTime = reader.GetDateTime("LastUpdateTime");
            info.DeptId = reader.GetInt32("DeptId");
            info.CompanyId = reader.GetInt32("CompanyId");			
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

            hash.Add("Id", info.Id);
            hash.Add("Pid", info.Pid);
 			hash.Add("UserCode", info.UserCode);
 			hash.Add("Name", info.Name);
            hash.Add("Remark", info.Remark);
            hash.Add("CreatorId", info.CreatorId);
            hash.Add("CreatorTime", info.CreatorTime);
            hash.Add("EditorId", info.EditorId);
            hash.Add("LastUpdateTime", info.LastUpdateTime);
            hash.Add("DeptId", info.DeptId);
            hash.Add("CompanyId", info.CompanyId);				
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
            dict.Add("Pid", "上级ID");
            dict.Add("UserCode", "编号");
            dict.Add("Name", "分组名称");
            dict.Add("Remark", "备注");
            dict.Add("CreatorId", "创建人");
            dict.Add("CreatorTime", "创建时间");
            dict.Add("EditorId", "编辑人");
            dict.Add("LastUpdateTime", "编辑时间");
            dict.Add("DeptId", "所属部门");
            dict.Add("CompanyId", "所属公司");
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
            string sql = string.Format("Select * From {0} Where 1=1 {1} Order By PID, UserCode ", tableName, condition);

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
            ContactGroupInfo info = this.FindById(id);
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