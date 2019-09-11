using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Databases;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace JCodes.Framework.SQLiteDAL
{
    /// <summary>
    /// 客户联系人
    /// </summary>
    public class Contact : BaseDALSQLite<ContactInfo>, IContact
    {
        #region 对象实例及构造函数

        public static Contact Instance
        {
            get
            {
                return new Contact();
            }
        }
        public Contact()
            : base("T_CRM_Contact", "ID")
        {
            this.sortField = "CreateTime";
            this.isDescending = true;
        }

        #endregion

        /// <summary>
        /// 将DataReader的属性值转化为实体类的属性值，返回实体类
        /// </summary>
        /// <param name="dr">有效的DataReader对象</param>
        /// <returns>实体类对象</returns>
        protected override ContactInfo DataReaderToEntity(IDataReader dataReader)
        {
            ContactInfo info = new ContactInfo();
            SmartDataReader reader = new SmartDataReader(dataReader);

            info.ID = reader.GetString("ID");
            info.UserCode = reader.GetString("UserCode");
            info.Name = reader.GetString("Name");
            info.IDCarNo = reader.GetString("IDCarNo");
            info.Birthday = reader.GetDateTime("Birthday");
            info.Sex = reader.GetString("Sex");
            info.OfficePhone = reader.GetString("OfficePhone");
            info.HomePhone = reader.GetString("HomePhone");
            info.Fax = reader.GetString("Fax");
            info.Mobile = reader.GetString("Mobile");
            info.Address = reader.GetString("Address");
            info.ZipCode = reader.GetString("ZipCode");
            info.Email = reader.GetString("Email");
            info.QQ = reader.GetString("QQ");
            info.Note = reader.GetString("Note");
            info.Seq = reader.GetString("Seq");
            info.AttachGUID = reader.GetString("AttachGUID");
            info.Province = reader.GetString("Province");
            info.City = reader.GetString("City");
            info.District = reader.GetString("District");
            info.Hometown = reader.GetString("Hometown");
            info.HomeAddress = reader.GetString("HomeAddress");
            info.Nationality = reader.GetString("Nationality");
            info.Eduction = reader.GetString("Eduction");
            info.GraduateSchool = reader.GetString("GraduateSchool");
            info.Political = reader.GetString("Political");
            info.JobType = reader.GetString("JobType");
            info.Titles = reader.GetString("Titles");
            info.Rank = reader.GetString("Rank");
            info.Department = reader.GetString("Department");
            info.Hobby = reader.GetString("Hobby");
            info.Animal = reader.GetString("Animal");
            info.Constellation = reader.GetString("Constellation");
            info.MarriageStatus = reader.GetString("MarriageStatus");
            info.HealthCondition = reader.GetString("HealthCondition");
            info.Importance = reader.GetString("Importance");
            info.Recognition = reader.GetString("Recognition");
            info.RelationShip = reader.GetString("RelationShip");
            info.ResponseDemand = reader.GetString("ResponseDemand");
            info.CareFocus = reader.GetString("CareFocus");
            info.InterestDemand = reader.GetString("InterestDemand");
            info.BodyType = reader.GetString("BodyType");
            info.Smoking = reader.GetString("Smoking");
            info.Drink = reader.GetString("Drink");
            info.Height = reader.GetString("Height");
            info.Weight = reader.GetString("Weight");
            info.Vision = reader.GetString("Vision");
            info.Introduce = reader.GetString("Introduce");
            info.Creator = reader.GetString("Creator");
            info.CreateTime = reader.GetDateTime("CreateTime");
            info.Editor = reader.GetString("Editor");
            info.EditTime = reader.GetDateTime("EditTime");
            info.Deleted = reader.GetInt32("Deleted") > 0;
            info.Dept_ID = reader.GetString("Dept_ID");
            info.Company_ID = reader.GetString("Company_ID");
            return info;
        }

        /// <summary>
        /// 将实体对象的属性值转化为Hashtable对应的键值
        /// </summary>
        /// <param name="obj">有效的实体对象</param>
        /// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(ContactInfo obj)
        {
            ContactInfo info = obj as ContactInfo;
            Hashtable hash = new Hashtable();

            hash.Add("ID", info.ID);
            hash.Add("UserCode", info.UserCode);
            hash.Add("Name", info.Name);
            hash.Add("IDCarNo", info.IDCarNo);
            hash.Add("Birthday", info.Birthday);
            hash.Add("Sex", info.Sex);
            hash.Add("OfficePhone", info.OfficePhone);
            hash.Add("HomePhone", info.HomePhone);
            hash.Add("Fax", info.Fax);
            hash.Add("Mobile", info.Mobile);
            hash.Add("Address", info.Address);
            hash.Add("ZipCode", info.ZipCode);
            hash.Add("Email", info.Email);
            hash.Add("QQ", info.QQ);
            hash.Add("Note", info.Note);
            hash.Add("Seq", info.Seq);
            hash.Add("AttachGUID", info.AttachGUID);
            hash.Add("Province", info.Province);
            hash.Add("City", info.City);
            hash.Add("District", info.District);
            hash.Add("Hometown", info.Hometown);
            hash.Add("HomeAddress", info.HomeAddress);
            hash.Add("Nationality", info.Nationality);
            hash.Add("Eduction", info.Eduction);
            hash.Add("GraduateSchool", info.GraduateSchool);
            hash.Add("Political", info.Political);
            hash.Add("JobType", info.JobType);
            hash.Add("Titles", info.Titles);
            hash.Add("Rank", info.Rank);
            hash.Add("Department", info.Department);
            hash.Add("Hobby", info.Hobby);
            hash.Add("Animal", info.Animal);
            hash.Add("Constellation", info.Constellation);
            hash.Add("MarriageStatus", info.MarriageStatus);
            hash.Add("HealthCondition", info.HealthCondition);
            hash.Add("Importance", info.Importance);
            hash.Add("Recognition", info.Recognition);
            hash.Add("RelationShip", info.RelationShip);
            hash.Add("ResponseDemand", info.ResponseDemand);
            hash.Add("CareFocus", info.CareFocus);
            hash.Add("InterestDemand", info.InterestDemand);
            hash.Add("BodyType", info.BodyType);
            hash.Add("Smoking", info.Smoking);
            hash.Add("Drink", info.Drink);
            hash.Add("Height", info.Height);
            hash.Add("Weight", info.Weight);
            hash.Add("Vision", info.Vision);
            hash.Add("Introduce", info.Introduce);
            hash.Add("Creator", info.Creator);
            hash.Add("CreateTime", info.CreateTime);
            hash.Add("Editor", info.Editor);
            hash.Add("EditTime", info.EditTime);
            hash.Add("Deleted", info.Deleted ? 1 : 0);
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
            dict.Add("UserCode", "编号");
            dict.Add("Name", "姓名");
            dict.Add("IDCarNo", "身份证号码");
            dict.Add("Birthday", "出生日期");
            dict.Add("Sex", "性别");
            dict.Add("OfficePhone", "办公电话");
            dict.Add("HomePhone", "家庭电话");
            dict.Add("Fax", "传真");
            dict.Add("Mobile", "联系人手机");
            dict.Add("Address", "联系人地址");
            dict.Add("ZipCode", "邮政编码");
            dict.Add("Email", "电子邮件");
            dict.Add("QQ", "QQ号码");
            dict.Add("Note", "备注");
            dict.Add("Seq", "排序序号");
            dict.Add("AttachGUID", "附件组别ID");
            dict.Add("Province", "所在省份");
            dict.Add("City", "城市");
            dict.Add("District", "所在行政区");
            dict.Add("Hometown", "籍贯");
            dict.Add("HomeAddress", "家庭住址");
            dict.Add("Nationality", "民族");
            dict.Add("Eduction", "教育程度");
            dict.Add("GraduateSchool", "毕业学校");
            dict.Add("Political", "政治面貌");
            dict.Add("JobType", "职业类型");
            dict.Add("Titles", "职称");
            dict.Add("Rank", "职务");
            dict.Add("Department", "所在部门");
            dict.Add("Hobby", "爱好");
            dict.Add("Animal", "属相");
            dict.Add("Constellation", "星座");
            dict.Add("MarriageStatus", "婚姻状态");
            dict.Add("HealthCondition", "健康状况");
            dict.Add("Importance", "重要级别");
            dict.Add("Recognition", "认可程度");
            dict.Add("RelationShip", "关系");
            dict.Add("ResponseDemand", "负责需求");
            dict.Add("CareFocus", "关心重点");
            dict.Add("InterestDemand", "利益诉求");
            dict.Add("BodyType", "体型");
            dict.Add("Smoking", "吸烟");
            dict.Add("Drink", "喝酒");
            dict.Add("Height", "身高");
            dict.Add("Weight", "体重");
            dict.Add("Vision", "视力");
            dict.Add("Introduce", "个人简述");
            dict.Add("Creator", "创建人");
            dict.Add("CreateTime", "创建时间");
            dict.Add("Editor", "编辑人");
            dict.Add("EditTime", "编辑时间");
            dict.Add("Deleted", "是否已删除");
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
        public List<ContactInfo> FindByGroupName(string ownerUser, string groupName, PagerInfo pagerInfo = null)
        { 
            //所属用户条件,非删除状态
            string condition = string.Format(" AND t.Creator='{0}' AND Deleted=0", ownerUser);

            string sql = "";
            if (string.IsNullOrEmpty(groupName))
            {
                sql = string.Format("Select t.* from {0} t where ID not In (select Contact_ID from T_CRM_ContactGroup_Contact) {1}", tableName, condition);
            }
            else
            {
                string subSql = string.Format("select ID from T_CRM_ContactGroup g where g.Name ='{0}' ", groupName);
                sql = string.Format(@"select t.* from {0} t inner join T_CRM_ContactGroup_Contact m on t.ID = m.Contact_ID 
                where m.ContactGroup_ID in ({1}) {2} ", tableName, subSql, condition);
            }

            if (pagerInfo != null)
            {
                return base.GetListWithPager(sql, pagerInfo);
            }
            else
            {
                return base.GetList(sql);
            }
        }

        /// <summary>
        /// 设置删除标志
        /// </summary>
        /// <param name="id">联系人ID</param>
        /// <param name="deleted">是否删除</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public bool SetDeletedFlag(string id, bool deleted = true, DbTransaction trans = null)
        {
            int intDeleted = deleted ? 1 : 0;
            string sql = string.Format("Update {0} Set Deleted={1} Where ID = '{2}' ", tableName, intDeleted, id);
            return SqlExecute(sql, trans) > 0;
        }
                        
        /// <summary>
        /// 调整联系人的组别
        /// </summary>
        /// <param name="contactId">联系人ID</param>
        /// <param name="groupIdList">分组Id集合</param>
        /// <returns></returns>
        public bool ModifyContactGroup(string contactId, List<string> groupIdList)
        {
            bool result = false;
            DbTransaction trans = base.CreateTransaction();
            if (trans != null)
            {
                string sql = string.Format("Delete from T_CRM_ContactGroup_Contact where Contact_ID='{0}' ", contactId);
                base.SqlExecute(sql, trans);

                foreach (string groupId in groupIdList)
                {
                    sql = string.Format("Insert into T_CRM_ContactGroup_Contact(Contact_ID,ContactGroup_ID) values('{0}', '{1}') ", contactId, groupId);
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

        /// <summary>
        /// 根据客户ID获取对应的联系人列表
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <returns></returns>
        public List<ContactInfo> FindByCustomer(string customerID)
        {
            string sql = string.Format(@"select t.* from {0} t inner join T_CRM_Customer_Contact m on t.ID = m.Contact_ID 
            where m.Customer_ID ='{1}' ", tableName, customerID);

            return GetList(sql);
        }

        /// <summary>
        /// 根据客户ID和名称获取实体信息
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public ContactInfo FindByCustomerAndName(string customerID, string name)
        {
            string sql = string.Format(@"select t.* from {0} t inner join T_CRM_Customer_Contact m on t.ID = m.Contact_ID 
            where m.Customer_ID ='{1}' and t.Name ='{2}' ", tableName, customerID, name);

            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);

            return GetEntity(db, command);
        }

        /// <summary>
        /// 根据供应商ID获取对应的联系人列表
        /// </summary>
        /// <param name="supplierID">供应商ID</param>
        /// <returns></returns>
        public List<ContactInfo> FindBySupplier(string supplierID)
        {
            string sql = string.Format(@"select t.* from {0} t inner join T_CRM_Supplier_Contact m on t.ID = m.Contact_ID 
            where m.Supplier_ID ='{1}' ", tableName, supplierID);

            return GetList(sql);
        }

        /// <summary>
        /// 根据供应商ID和名称获取实体信息
        /// </summary>
        /// <param name="supplierID">供应商ID</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public ContactInfo FindBySupplierAndName(string supplierID, string name)
        {
            string sql = string.Format(@"select t.* from {0} t inner join T_CRM_Supplier_Contact m on t.ID = m.Contact_ID 
            where m.Supplier_ID ='{1}' and t.Name ='{2}' ", tableName, supplierID, name);

            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);

            return GetEntity(db, command);
        }
    }
}