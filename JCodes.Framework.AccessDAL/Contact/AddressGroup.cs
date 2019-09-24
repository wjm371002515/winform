using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.AccessDAL
{
    /// <summary>
    /// 通讯录分组
    /// </summary>
    public class AddressGroup : BaseDALAccess<AddressGroupInfo>, IAddressGroup
	{
		#region 对象实例及构造函数

		public static AddressGroup Instance
		{
			get
			{
				return new AddressGroup();
			}
		}
		public AddressGroup() : base(AccessPortal.gc._contactTablePre+"AddressGroup","ID")
		{
            this.sortField = "SEQ";
            this.isDescending = false;
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override AddressGroupInfo DataReaderToEntity(IDataReader dataReader)
		{
			AddressGroupInfo info = new AddressGroupInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);

            info.Id = reader.GetInt32("Id");
            info.Pid = reader.GetInt32("Pid");
            info.AddressType = Convert(reader.GetString("AddressType"));
            info.Name = reader.GetString("Name");
            info.Remark = reader.GetString("Remark");
            info.Seq = reader.GetString("Seq");
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
        protected override Hashtable GetHashByEntity(AddressGroupInfo obj)
		{
		    AddressGroupInfo info = obj as AddressGroupInfo;
			Hashtable hash = new Hashtable();
            hash.Add("Id", info.Id);
            hash.Add("Pid", info.Pid);
            hash.Add("AddressType", info.AddressType.ToString());
            hash.Add("Name", info.Name);
            hash.Add("Remark", info.Remark);
            hash.Add("Seq", info.Seq);
            hash.Add("CreatorId", info.CreatorId);
            hash.Add("CreatorTime", info.CreatorTime);
            hash.Add("EditorId", info.EditorId);
            hash.Add("LastUpdateTime", info.LastUpdateTime);
            hash.Add("DeptId", info.DeptId);
            hash.Add("CompanyId", info.CompanyId); 				
			return hash;
		}

        private AddressType Convert(string strAddressType)
        {
            AddressType addressType = AddressType.个人;
            try
            {
                addressType = (AddressType)Enum.Parse(typeof(AddressType), strAddressType);
            }
            catch
            {
            }
            return addressType;
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
            dict.Add("Pid", "父ID");
            dict.Add("AddressType", "通讯录类型[个人,公司]");
            dict.Add("Name", "分组名称");
            dict.Add("Remark", "备注");
            dict.Add("Seq", "排序序号");
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
        public List<AddressGroupNodeInfo> GetTree(string addressType, Int32 ?creatorId = null)
        {
            string condition = creatorId>0 ? string.Format("AND Creator='{0}'", creatorId) : "";
            List<AddressGroupNodeInfo> nodeList = new List<AddressGroupNodeInfo>();
            string sql = string.Format("Select * From {0} Where AddressType ='{1}' {2} Order By PID, SEQ ", tableName, addressType, condition);

            DataTable dt = base.SqlTable(sql);
            DataRow[] dataRows = dt.Select(string.Format(" PID = '{0}' ", -1));
            for (int i = 0; i < dataRows.Length; i++)
            {
                string id = dataRows[i]["ID"].ToString();
                AddressGroupNodeInfo nodeInfo = GetNode(id, dt);
                nodeList.Add(nodeInfo);
            }

            return nodeList;
        }

        private AddressGroupNodeInfo GetNode(string id, DataTable dt)
        {
            AddressGroupInfo info = this.FindByID(id);
            AddressGroupNodeInfo nodeInfo = new AddressGroupNodeInfo(info);

            DataRow[] dChildRows = dt.Select(string.Format(" PID='{0}'", id));
            for (int i = 0; i < dChildRows.Length; i++)
            {
                string childId = dChildRows[i]["ID"].ToString();
                AddressGroupNodeInfo childNodeInfo = GetNode(childId, dt);
                nodeInfo.Children.Add(childNodeInfo);
            }
            return nodeInfo;
        }

        /// <summary>
        /// 根据联系人ID，获取客户对应的分组集合
        /// </summary>
        /// <param name="contactId">联系人ID</param>
        /// <returns></returns>
        public List<AddressGroupInfo> GetByContact(Int32 contactId)
        {
            string sql = string.Format(@"Select t.* From {1} t inner join {0}AddressGroup_Address m 
            ON t.ID = m.Group_ID Where m.Address_ID = '{2}' ", AccessPortal.gc._contactTablePre, tableName, contactId);

            return base.GetList(sql);
        }
    }
}