using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Databases;

namespace JCodes.Framework.SQLServerDAL
{
    /// <summary>
    /// 通讯录联系人
    /// </summary>
    public class Address : BaseDALSQLServer<AddressInfo>, IAddress
    {
        #region 对象实例及构造函数

        public static Address Instance
        {
            get
            {
                return new Address();
            }
        }
        public Address()
            : base(SQLServerPortal.gc._contactTablePre + "Address", "ID")
        {
        }

        #endregion

        /// <summary>
        /// 将DataReader的属性值转化为实体类的属性值，返回实体类
        /// </summary>
        /// <param name="dr">有效的DataReader对象</param>
        /// <returns>实体类对象</returns>
        protected override AddressInfo DataReaderToEntity(IDataReader dataReader)
        {
            AddressInfo info = new AddressInfo();
            SmartDataReader reader = new SmartDataReader(dataReader);

            info.ID = reader.GetString("ID");
            info.AddressType = Convert(reader.GetString("AddressType"));
            info.Name = reader.GetString("Name");
            info.Sex = reader.GetString("Sex");
            info.Birthdate = reader.GetDateTime("Birthdate");
            info.Mobile = reader.GetString("Mobile");
            info.Email = reader.GetString("Email");
            info.QQ = reader.GetString("QQ");
            info.HomeTelephone = reader.GetString("HomeTelephone");
            info.OfficeTelephone = reader.GetString("OfficeTelephone");
            info.HomeAddress = reader.GetString("HomeAddress");
            info.OfficeAddress = reader.GetString("OfficeAddress");
            info.Fax = reader.GetString("Fax");
            info.Company = reader.GetString("Company");
            info.Dept = reader.GetString("Dept");
			info.Position = reader.GetString("Position");
            info.Other = reader.GetString("Other");
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
        protected override Hashtable GetHashByEntity(AddressInfo obj)
        {
            AddressInfo info = obj as AddressInfo;
            Hashtable hash = new Hashtable();

            hash.Add("ID", info.ID);
            hash.Add("AddressType", info.AddressType.ToString());
            hash.Add("Name", info.Name);
            hash.Add("Sex", info.Sex);
            hash.Add("Birthdate", info.Birthdate);
            hash.Add("Mobile", info.Mobile);
            hash.Add("Email", info.Email);
            hash.Add("QQ", info.QQ);
            hash.Add("HomeTelephone", info.HomeTelephone);
            hash.Add("OfficeTelephone", info.OfficeTelephone);
            hash.Add("HomeAddress", info.HomeAddress);
            hash.Add("OfficeAddress", info.OfficeAddress);
            hash.Add("Fax", info.Fax);
            hash.Add("Company", info.Company);
            hash.Add("Dept", info.Dept);
 			hash.Add("Position", info.Position);
            hash.Add("Other", info.Other);
            hash.Add("Note", info.Note);
            hash.Add("Creator", info.Creator);
            hash.Add("CreateTime", info.CreateTime);
            hash.Add("Editor", info.Editor);
            hash.Add("EditTime", info.EditTime);
            hash.Add("Dept_ID", info.Dept_ID);
            hash.Add("Company_ID", info.Company_ID);
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
            dict.Add("ID", "编号");
            dict.Add("AddressType", "通讯录类型[个人,公司]");
            dict.Add("Name", "姓名");
            dict.Add("Sex", "性别");
            dict.Add("Birthdate", "出生日期");
            dict.Add("Mobile", "手机");
            dict.Add("Email", "电子邮箱");
            dict.Add("QQ", "QQ");
            dict.Add("HomeTelephone", "家庭电话");
            dict.Add("OfficeTelephone", "办公电话");
            dict.Add("HomeAddress", "家庭住址");
            dict.Add("OfficeAddress", "办公地址");
            dict.Add("Fax", "传真号码");
            dict.Add("Company", "公司单位");
            dict.Add("Dept", "部门");
            dict.Add("Position", "职位");
            dict.Add("Other", "其他");
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
        /// 根据联系人分组的名称，搜索属于该分组的联系人列表
        /// </summary>
        /// <param name="ownerUser">联系人所属用户</param>
        /// <param name="groupName">联系人分组的名称,如果联系人分组为空，那么返回未分组联系人列表</param>
        /// <param name="pagerInfo">分页条件</param>
        /// <returns></returns>
        public List<AddressInfo> FindByGroupName(string ownerUser, string groupName, PagerInfo pagerInfo = null)
        {
            //所属用户条件,非删除状态
            string condition = string.Format(" AND t.Creator='{0}' ", ownerUser);

            string sql = "";
            if (string.IsNullOrEmpty(groupName))
            {
                sql = string.Format("Select t.* from {1} t where ID not In (select Address_ID from {0}AddressGroup_Address) {2}", SQLServerPortal.gc._contactTablePre, tableName, condition);
            }
            else
            {
                string subSql = string.Format("select ID from {0}AddressGroup g where g.Name ='{1}' ", SQLServerPortal.gc._contactTablePre, groupName);
                sql = string.Format(@"select t.* from {1} t inner join {0}AddressGroup_Address m on t.ID = m.Address_ID 
                where m.Group_ID in ({2}) {3} ", SQLServerPortal.gc._contactTablePre, tableName, subSql, condition);
            }

            if (pagerInfo != null && pagerInfo.PageSize > 0)
            {
                return base.GetListWithPager(sql, pagerInfo);
            }
            else
            {
                return base.GetList(sql);
            }
        }

        /// <summary>
        /// 获取公共分组的联系人列表。根据联系人分组的名称，搜索属于该分组的联系人列表。
        /// </summary>
        /// <param name="groupName">联系人分组的名称,如果联系人分组为空，那么返回未分组联系人列表</param>
        /// <param name="pagerInfo">分页条件</param>
        /// <returns></returns>
        public List<AddressInfo> FindByGroupNamePublic(string groupName, PagerInfo pagerInfo = null)
        {
            //所属分组条件为公共通讯录
            string condition = string.Format(" AND t.AddressType='公共' ");

            string sql = "";
            if (string.IsNullOrEmpty(groupName))
            {
                sql = string.Format("Select t.* from {1} t where ID not In (select Address_ID from {0}AddressGroup_Address) {2}", SQLServerPortal.gc._contactTablePre, tableName, condition);
            }
            else
            {
                string subSql = string.Format("select ID from {0}AddressGroup g where g.Name ='{1}' ", SQLServerPortal.gc._contactTablePre, groupName);
                sql = string.Format(@"select t.* from {1} t inner join {0}AddressGroup_Address m on t.ID = m.Address_ID 
                where m.Group_ID in ({2}) {3} ", SQLServerPortal.gc._contactTablePre, tableName, subSql, condition);
            }

            if (pagerInfo != null && pagerInfo.PageSize > 0)
            {
                return base.GetListWithPager(sql, pagerInfo);
            }
            else
            {
                return base.GetList(sql);
            }
        }

        /// <summary>
        /// 获取联系人的名称
        /// </summary>
        /// <param name="id">联系人ID</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public string GetAddressName(string id, DbTransaction trans = null)
        {
            string result = "";
            if (!string.IsNullOrEmpty(id))
            {
                string sql = string.Format("Select Name from {0} where ID ='{1}' ", tableName, id);
                result = SqlValueList(sql, trans);
            }
            return result;
        }

        /// <summary>
        /// 调整联系人的组别
        /// </summary>
        /// <param name="contactId">联系人ID</param>
        /// <param name="groupIdList">客户分组Id集合</param>
        /// <returns></returns>
        public bool ModifyAddressGroup(string contactId, List<string> groupIdList)
        {
            bool result = false;
            DbTransaction trans = base.CreateTransaction();
            if (trans != null)
            {
                string sql = string.Format("Delete from {0}AddressGroup_Address where Address_ID='{1}' ", SQLServerPortal.gc._contactTablePre, contactId);
                base.SqlExecute(sql, trans);

                foreach (string groupId in groupIdList)
                {
                    sql = string.Format("Insert into {0}AddressGroup_Address(Address_ID, Group_ID) values('{1}', '{2}') ", SQLServerPortal.gc._contactTablePre, contactId, groupId);
                    base.SqlExecute(sql, trans);
                }

                try
                {
                    trans.Commit();
                    result = true;
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
            return result;
        }
    }
}