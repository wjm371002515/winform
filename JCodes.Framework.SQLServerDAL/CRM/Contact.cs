using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Databases;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace JCodes.Framework.SQLServerDAL
{
    /// <summary>
    /// 客户联系人
    /// </summary>
    public class Contact : BaseDALSQLServer<ContactInfo>, IContact
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

            info.Id = reader.GetInt32("Id");
            info.UserCode = reader.GetString("UserCode");
            info.Name = reader.GetString("Name");
            info.IdCard = reader.GetString("IdCard");
            info.Birthday = reader.GetDateTime("Birthday");
            info.Gender = reader.GetInt16("Gender");
            info.OfficePhone = reader.GetString("OfficePhone");
            info.HomePhone = reader.GetString("HomePhone");
            info.Fax = reader.GetString("Fax");
            info.MobilePhone = reader.GetString("MobilePhone");
            info.Address = reader.GetString("Address");
            info.ZipCode = reader.GetString("ZipCode");
            info.Email = reader.GetString("Email");
            info.QQ = reader.GetInt32("QQ");
            info.Remark = reader.GetString("Remark");
            info.Seq = reader.GetString("Seq");
            //info.AttachGUID = reader.GetString("AttachGUID");
            info.ProvinceName = reader.GetString("ProvinceName");
            info.CityName = reader.GetString("CityName");
            info.DistrictName = reader.GetString("DistrictName");
            info.NativePlace = reader.GetString("NativePlace");
            info.HomeAddress = reader.GetString("HomeAddress");
            info.NativePlace = reader.GetString("NativePlace");
            info.Education = reader.GetString("Education");
            info.GraduateSchool = reader.GetString("GraduateSchool");
            info.Political = reader.GetString("Political");
            info.JobType = reader.GetString("JobType");
            info.ProfessionalTitle = reader.GetString("ProfessionalTitle");
            info.WorkPost = reader.GetString("WorkPost");
            info.DeptName = reader.GetString("DeptName");
            info.PersonalHobby = reader.GetString("PersonalHobby");
            info.ChineseZodiac = reader.GetInt16("ChineseZodiac");
            info.Constellation = reader.GetInt16("Constellation");
            info.MarriageStatus = reader.GetInt16("MarriageStatus");
            info.HealthStatus = reader.GetInt16("HealthStatus");
            info.ImportanceLevel = reader.GetInt16("ImportanceLevel");
            info.RecognitionLevel = reader.GetInt16("RecognitionLevel");
            info.RelationShip = reader.GetString("RelationShip");
            info.ResponseDemand = reader.GetString("ResponseDemand");
            info.CareFocus = reader.GetString("CareFocus");
            info.InterestDemand = reader.GetString("InterestDemand");
            info.BodyType = reader.GetInt16("BodyType");
            info.IsSmoking = reader.GetInt16("IsSmoking");
            info.IsDrink = reader.GetInt16("Drink");
            info.Height = reader.GetInt32("Height");
            info.Weight = reader.GetInt32("Weight");
            info.Vision = reader.GetString("Vision");
            info.Introduce = reader.GetString("Introduce");
            info.CreatorId = reader.GetInt32("CreatorId");
            info.CreatorTime = reader.GetDateTime("CreatorTime");
            info.EditorId = reader.GetInt32("EditorId");
            info.LastUpdateTime = reader.GetDateTime("LastUpdateTime");
            info.IsDelete = reader.GetInt16("IsDelete");
            info.DeptId = reader.GetInt32("DeptId");
            info.CompanyId = reader.GetInt32("CompanyId");
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

            hash.Add("Id", info.Id);
            hash.Add("UserCode", info.UserCode);
            hash.Add("Name", info.Name);
            hash.Add("IdCard", info.IdCard);
            hash.Add("Birthday", info.Birthday);
            hash.Add("Gender", info.Gender);
            hash.Add("OfficePhone", info.OfficePhone);
            hash.Add("HomePhone", info.HomePhone);
            hash.Add("Fax", info.Fax);
            hash.Add("MobilePhone", info.MobilePhone);
            hash.Add("Address", info.Address);
            hash.Add("ZipCode", info.ZipCode);
            hash.Add("Email", info.Email);
            hash.Add("QQ", info.QQ);
            hash.Add("Remark", info.Remark);
            hash.Add("Seq", info.Seq);
            //hash.Add("AttachGUID", info.AttachGUID);
            hash.Add("ProvinceName", info.ProvinceName);
            hash.Add("CityName", info.CityName);
            hash.Add("DistrictName", info.DistrictName);
            hash.Add("NativePlace", info.NativePlace);
            hash.Add("HomeAddress", info.HomeAddress);
            hash.Add("NativePlace", info.NativePlace);
            hash.Add("Education", info.Education);
            hash.Add("GraduateSchool", info.GraduateSchool);
            hash.Add("Political", info.Political);
            hash.Add("JobType", info.JobType);
            hash.Add("ProfessionalTitle", info.ProfessionalTitle);
            hash.Add("WorkPost", info.WorkPost);
            hash.Add("DeptName", info.DeptName);
            hash.Add("PersonalHobby", info.PersonalHobby);
            hash.Add("ChineseZodiac", info.ChineseZodiac);
            hash.Add("Constellation", info.Constellation);
            hash.Add("MarriageStatus", info.MarriageStatus);
            hash.Add("HealthStatus", info.HealthStatus);
            hash.Add("ImportanceLevel", info.ImportanceLevel);
            hash.Add("RecognitionLevel", info.RecognitionLevel);
            hash.Add("RelationShip", info.RelationShip);
            hash.Add("ResponseDemand", info.ResponseDemand);
            hash.Add("CareFocus", info.CareFocus);
            hash.Add("InterestDemand", info.InterestDemand);
            hash.Add("BodyType", info.BodyType);
            hash.Add("IsSmoking", info.IsSmoking);
            hash.Add("IsDrink", info.IsDrink);
            hash.Add("Height", info.Height);
            hash.Add("Weight", info.Weight);
            hash.Add("Vision", info.Vision);
            hash.Add("Introduce", info.Introduce);
            hash.Add("Creator", info.CreatorId);
            hash.Add("CreateTime", info.CreatorTime);
            hash.Add("Editor", info.EditorId);
            hash.Add("LastUpdateTime", info.LastUpdateTime);
            hash.Add("IsDelete", info.IsDelete);
            hash.Add("Dept_ID", info.DeptId);
            hash.Add("Company_ID", info.CompanyId);
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