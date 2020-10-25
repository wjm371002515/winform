using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Databases;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.SQLServerDAL
{
    /// <summary>
    /// 客户基本资料
    /// </summary>
    public class Customer : BaseDALSQLServer<CustomerInfo>, ICustomer
    {
        #region 对象实例及构造函数

        public static Customer Instance
        {
            get
            {
                return new Customer();
            }
        }
        public Customer()
            : base("T_CRM_Customer", "Id")
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
        protected override CustomerInfo DataReaderToEntity(IDataReader dataReader)
        {
            CustomerInfo info = new CustomerInfo();
            SmartDataReader reader = new SmartDataReader(dataReader);

            info.Id = reader.GetInt32("Id");
            info.UserCode = reader.GetString("UserCode");
            info.Name = reader.GetString("Name");
            info.LoginName = reader.GetString("LoginName");
            info.ProvinceName = reader.GetString("ProvinceName");
            info.CityName = reader.GetString("CityName");
            info.DistrictName = reader.GetString("DistrictName");
            info.MarketArea = reader.GetString("MarketArea");
            info.WorkAddress = reader.GetString("WorkAddress");
            info.ZipCode = reader.GetString("ZipCode");
            info.MobilePhone = reader.GetString("MobilePhone");
            info.Fax = reader.GetString("Fax");
            info.Contacts = reader.GetString("Contacts");
            info.ContactPhone = reader.GetString("ContactPhone");
            info.Email = reader.GetString("Email");
            info.QQ = reader.GetInt32("QQ");
            info.Industry = reader.GetString("Industry");
            info.BusinessScope = reader.GetString("BusinessScope");
            info.Brand = reader.GetString("Brand");
            info.PrimaryClient = reader.GetString("PrimaryClient");
            info.PrimaryBusiness = reader.GetString("PrimaryBusiness");
            info.RegisterCapital = reader.GetDouble("RegisterCapital");
            info.TurnOver = reader.GetDouble("TurnOver");
            info.LicenseNo = reader.GetString("LicenseNo");
            info.OpenBank = reader.GetString("OpenBank");
            info.BankAccount = reader.GetString("BankAccount");
            info.LocalTaxNo = reader.GetString("LocalTaxNo");
            info.NationalTaxNo = reader.GetString("NationalTaxNo");
            info.LegalMan = reader.GetString("LegalMan");
            info.LegalPhone = reader.GetString("LegalPhone");
            info.CustomerSource = reader.GetString("CustomerSource");
            info.WebSiteUrl = reader.GetString("WebSiteUrl");
            //info.CompanyPictureGUID = reader.GetString("CompanyPictureGUID");
            //info.AttachGUID = reader.GetString("AttachGUID");
            info.CustomerType = reader.GetInt16("CustomerType");
            //info. = reader.GetString("Grade");
            info.CreditStatus = reader.GetInt16("CreditStatus");
            info.ImportanceLevel = reader.GetInt16("ImportanceLevel");
            info.IsPublic = reader.GetInt16("IsPublic");
            info.SatisfactionLevel = reader.GetInt16("SatisfactionLevel");
            info.Remark = reader.GetString("Remark");
            info.TransactionCount = reader.GetInt32("TransactionCount");
            info.TransactionTotal = reader.GetDouble("TransactionTotal");
            info.TransactionFirstDate = reader.GetDateTime("TransactionFirstDate");
            info.TransactionLastDate = reader.GetDateTime("TransactionLastDate");
            info.LastContactDate = reader.GetDateTime("LastContactDate");
            info.CustomerStage = reader.GetString("CustomerStage");
            info.AuditStatus = reader.GetInt16("AuditStatus");
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
        protected override Hashtable GetHashByEntity(CustomerInfo obj)
        {
            CustomerInfo info = obj as CustomerInfo;
            Hashtable hash = new Hashtable();

            /*hash.Add("Id", info.Id);
            hash.Add("UserCode", info.UserCode);
            hash.Add("Name", info.Name);
            hash.Add("FullName", info.FullName);
            hash.Add("ProvinceName", info.ProvinceName);
            hash.Add("CityName", info.CityName);
            hash.Add("DistrictName", info.DistrictName);
            hash.Add("Area", info.Area);
            hash.Add("Address", info.Address);
            hash.Add("ZipCode", info.ZipCode);
            hash.Add("MobilePhone", info.MobilePhone);
            hash.Add("Fax", info.Fax);
            hash.Add("Contact", info.Contact);
            hash.Add("ContactPhone", info.ContactPhone);
            hash.Add("ContactMobile", info.ContactMobile);
            hash.Add("Email", info.Email);
            hash.Add("QQ", info.QQ);
            hash.Add("Industry", info.Industry);
            hash.Add("BusinessScope", info.BusinessScope);
            hash.Add("Brand", info.Brand);
            hash.Add("PrimaryClient", info.PrimaryClient);
            hash.Add("PrimaryBusiness", info.PrimaryBusiness);
            hash.Add("RegisterCapital", info.RegisterCapital);
            hash.Add("TurnOver", info.TurnOver);
            hash.Add("LicenseNo", info.LicenseNo);
            hash.Add("Bank", info.Bank);
            hash.Add("BankAccount", info.BankAccount);
            hash.Add("LocalTaxNo", info.LocalTaxNo);
            hash.Add("NationalTaxNo", info.NationalTaxNo);
            hash.Add("LegalMan", info.LegalMan);
            hash.Add("LegalTelephone", info.LegalTelephone);
            hash.Add("LegalMobile", info.LegalMobile);
            hash.Add("Source", info.Source);
            hash.Add("WebSite", info.WebSite);
            hash.Add("CompanyPictureGUID", info.CompanyPictureGUID);
            hash.Add("AttachGUID", info.AttachGUID);
            hash.Add("CustomerType", info.CustomerType);
            hash.Add("Grade", info.Grade);
            hash.Add("CreditStatus", info.CreditStatus);
            hash.Add("Importance", info.Importance);
            hash.Add("IsPublic", info.IsPublic ? 1 : 0);
            hash.Add("Satisfaction", info.Satisfaction);
            hash.Add("Remark", info.Remark);
            hash.Add("TransactionCount", info.TransactionCount);
            hash.Add("TransactionTotal", info.TransactionTotal);
            hash.Add("TransactionFirstDay", info.TransactionFirstDay);
            hash.Add("TransactionLastDay", info.TransactionLastDay);
            hash.Add("LastContactDate", info.LastContactDate);
            hash.Add("Stage", info.Stage);
            hash.Add("AuditStatus", info.AuditStatus);
            hash.Add("CreatorId", info.CreatorId);
            hash.Add("CreatorTime", info.CreatorTime);
            hash.Add("EditorId", info.EditorId);
            hash.Add("LastUpdateTime", info.LastUpdateTime);
            hash.Add("IsDelete", info.IsDelete);
            hash.Add("DeptId", info.DeptId);
            hash.Add("CompanyId", info.CompanyId);*/
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
            dict.Add("UserCode", "客户编号");
            dict.Add("Name", "客户名称");
            dict.Add("LoginName", "客户简称");
            dict.Add("Province", "所在省份");
            dict.Add("City", "城市");
            dict.Add("District", "所在行政区");
            dict.Add("Area", "市场分区");
            dict.Add("Address", "公司地址");
            dict.Add("ZipCode", "公司邮编");
            dict.Add("Telephone", "办公电话");
            dict.Add("Fax", "传真号码");
            dict.Add("Contact", "主联系人");
            dict.Add("ContactPhone", "联系人电话");
            dict.Add("ContactMobile", "联系人手机");
            dict.Add("Email", "电子邮件");
            dict.Add("Qq", "QQ号码");
            dict.Add("Industry", "所属行业");
            dict.Add("BusinessScope", "经营范围");
            dict.Add("Brand", "经营品牌");
            dict.Add("PrimaryClient", "主要客户群");
            dict.Add("PrimaryBusiness", "主营业务");
            dict.Add("RegisterCapital", "注册资金");
            dict.Add("TurnOver", "营业额");
            dict.Add("LicenseNo", "营业执照");
            dict.Add("Bank", "开户银行");
            dict.Add("BankAccount", "银行账号");
            dict.Add("LocalTaxNo", "地税登记号");
            dict.Add("NationalTaxNo", "国税登记号");
            dict.Add("LegalMan", "法人名称");
            dict.Add("LegalTelephone", "法人电话");
            dict.Add("LegalMobile", "法人手机");
            dict.Add("Source", "客户来源");
            dict.Add("WebSite", "单位网站");
            dict.Add("CompanyPictureGUID", "公司图片信息");
            dict.Add("AttachGUID", "附件组别ID");
            dict.Add("CustomerType", "客户类别");
            dict.Add("Grade", "客户级别");
            dict.Add("CreditStatus", "信用等级");
            dict.Add("Importance", "重要级别");
            dict.Add("IsPublic", "公开与否");
            dict.Add("Satisfaction", "客户满意度");
            dict.Add("Note", "备注信息");
            dict.Add("TransactionCount", "交易次数");
            dict.Add("TransactionTotal", "交易金额");
            dict.Add("TransactionFirstDay", "首次交易时间");
            dict.Add("TransactionLastDay", "最近交易时间");
            dict.Add("LastContactDate", "最近联系日期");
            dict.Add("Stage", "客户阶段");
            dict.Add("Status", "客户状态");
            dict.Add("Creator", "创建人/所属人员");
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
        /// 修改客户的所属人员/创建人员
        /// </summary>
        /// <param name="id">客户ID</param>
        /// <param name="userId">所属人员ID</param>
        /// <returns></returns>
        public bool ChangeOwner(string id, string userId)
        {
            string sql = string.Format("Update {0} Set CreatorId='{1}' Where Id = '{2}' ", tableName, userId, id);
            return SqlExecute(sql) > 0;
        }

        /// <summary>
        /// 设置删除标志
        /// </summary>
        /// <param name="id">客户ID</param>
        /// <param name="deleted">是否删除</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public bool SetDeletedFlag(Int32 id, IsDelete isDelete = IsDelete.是, DbTransaction trans = null)
        {
            string sql = string.Format("Update {0} Set IsDelete={1} Where Id = '{2}' ", tableName, (short)isDelete, id);
            return SqlExecute(sql, trans) > 0;
        }

        /// <summary>
        /// 根据客户分组的名称，搜索属于该分组的客户列表
        /// </summary>
        /// <param name="ownerUser">客户所属用户</param>
        /// <param name="groupName">客户分组的名称,如果客户分组为空，那么返回未分组客户列表</param>
        /// <param name="pagerInfo">分页条件</param>
        /// <returns></returns>
        public List<CustomerInfo> FindByGroupName(string ownerUser, string groupName, PagerInfo pagerInfo = null)
        {
            //所属用户条件,非删除状态
            string condition = string.Format(" AND t.CreatorId='{0}' AND IsDelete=0", ownerUser);

            string sql = "";
            if (string.IsNullOrEmpty(groupName))
            {
                sql = string.Format("Select t.* from {0} t where ID not In (select Customer_ID from T_CRM_CustomerGroup_Customer) {1}", tableName, condition);
            }
            else
            {
                string subSql = string.Format("select ID from T_CRM_CustomerGroup g where g.Name ='{0}' ", groupName);
                sql = string.Format(@"select t.* from {0} t inner join T_CRM_CustomerGroup_Customer m on t.ID = m.Customer_ID 
                where m.CustomerGroup_ID in ({1}) {2} ", tableName, subSql, condition);
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
        /// 调整客户的组别
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <param name="groupIdList">客户分组Id集合</param>
        /// <returns></returns>
        public bool ModifyCustomerGroup(string customerId, List<string> groupIdList)
        {
            bool result = false;
            DbTransaction trans = base.CreateTransaction();
            if (trans != null)
            {
                string sql = string.Format("Delete from T_CRM_CustomerGroup_Customer where Customer_ID='{0}' ", customerId);
                base.SqlExecute(sql, trans);

                foreach (string groupId in groupIdList)
                {
                    sql = string.Format("Insert into T_CRM_CustomerGroup_Customer(Customer_ID,CustomerGroup_ID) values('{0}', '{1}') ", customerId, groupId);
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
        /// 更新客户的状态信息
        /// </summary>
        /// <param name="id">客户Id</param>
        /// <param name="orderDate">订单日期</param>
        /// <param name="orderCount">交易次数</param>
        /// <param name="orderMoney">交易金额</param>
        /// <returns></returns>
        public bool UpdateTransactionStatus(string id, DateTime orderDate, int orderCount, decimal orderMoney, DbTransaction trans = null)
        {
            bool result = false;

            Database db = CreateDatabase();

            string sql = string.Format("update {0} set TransactionCount = {2} where ID='{1}' ", tableName, id, orderCount);
            DbCommand command = db.GetSqlStringCommand(sql);
            if (trans != null)
            {
                db.ExecuteNonQuery(command, trans);
            }
            else
            {
                db.ExecuteNonQuery(command);
            }

            sql = string.Format("update {0} set TransactionTotal = {2} where ID='{1}' ", tableName, id, orderMoney);
            command = db.GetSqlStringCommand(sql);
            if (trans != null)
            {
                db.ExecuteNonQuery(command, trans);
            }
            else
            {
                db.ExecuteNonQuery(command);
            }

            sql = string.Format("update {0} set TransactionFirstDay = {2}TransactionFirstDay where ID='{1}' AND TransactionFirstDay is null ", tableName, id, parameterPrefix);
            command = db.GetSqlStringCommand(sql);
            db.AddInParameter(command, "TransactionFirstDay", DbType.DateTime, orderDate);
            if (trans != null)
            {
                db.ExecuteNonQuery(command, trans);
            }
            else
            {
                db.ExecuteNonQuery(command);
            }

            //仅当日期为最新才更新
            sql = string.Format("update {0} set TransactionLastDay = {2}TransactionLastDay where ID='{1}' and (TransactionLastDay < {2}TransactionLastDay or TransactionLastDay is null) ", tableName, id, parameterPrefix);
            command = db.GetSqlStringCommand(sql);
            db.AddInParameter(command, "TransactionLastDay", DbType.DateTime, orderDate);
            if (trans != null)
            {
                db.ExecuteNonQuery(command, trans);
            }
            else
            {
                db.ExecuteNonQuery(command);
            }

            result = true;//最后设置为True

            return result;
        }

        /// <summary>
        /// 更新客户的最后联系日期
        /// </summary>
        /// <param name="id">客户ID</param>
        /// <param name="lastContactDate">最后联系日期</param>
        /// <returns></returns>
        public bool UpdateContactDate(string id, DateTime lastContactDate, DbTransaction trans = null)
        {
            bool result = false;

            Database db = CreateDatabase();

            //仅当日期为最新才更新
            string sql = string.Format("update {0} set LastContactDate = {2}LastContactDate where ID='{1}' and (LastContactDate < {2}LastContactDate or LastContactDate is null) ", tableName, id, parameterPrefix);
            DbCommand command = db.GetSqlStringCommand(sql);
            db.AddInParameter(command, "LastContactDate", DbType.DateTime, lastContactDate);
            if (trans != null)
            {
                result = db.ExecuteNonQuery(command, trans) > 0;
            }
            else
            {
                result = db.ExecuteNonQuery(command) > 0;
            }
            
            return result;
        }

        /// <summary>
        /// 根据供应商ID，分页获取客户列表（关联客户列表）
        /// </summary>
        /// <param name="supplierID">供应商ID</param>
        /// <param name="pagerInfo">分页条件</param>
        /// <returns></returns>
        public List<CustomerInfo> FindBySupplier(string supplierID, string condition, PagerInfo pagerInfo = null)
        {
            string sql = "";

            sql = string.Format(@"select t.* from {0} t inner join T_CRM_Customer_Supplier m on t.ID = m.Customer_ID 
            where m.Supplier_ID ='{1}' ", tableName, supplierID);
            if (!string.IsNullOrEmpty(condition))
            {
                sql += string.Format("AND {0}", condition);
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
        /// 如果没有建立关系，则创建供应商和客户关系
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="supplierID">供应商ID</param>
        /// <returns></returns>
        public bool AddSupplier(string customerID, string supplierID)
        {
            string sql = string.Format(@"
            if NOT EXISTS(
                  SELECT *
                  FROM T_CRM_Customer_Supplier
                  WHERE Customer_ID='{0}' and Supplier_ID='{1}'
            )
            INSERT INTO T_CRM_Customer_Supplier(Customer_ID,Supplier_ID) Values('{0}', '{1}') ", customerID, supplierID);

            return SqlExecute(sql) > 0;
        }

        /// <summary>
        /// 如果建立关系，则移除供应商和客户关系
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="supplierID">供应商ID</param>
        /// <returns></returns>
        public bool RemoveSupplier(string customerID, string supplierID)
        {
            string sql = string.Format(@"Delete From T_CRM_Customer_Supplier WHERE Customer_ID='{0}' and Supplier_ID='{1}' ", customerID, supplierID);
            return SqlExecute(sql) > 0;
        }

        /// <summary>
        /// 如果建立关系，则移除供应商和客户的所有关系
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        public bool RemoveSupplier(string customerID)
        {
            string sql = string.Format(@"Delete From T_CRM_Customer_Supplier WHERE Customer_ID='{0}' ", customerID);
            return SqlExecute(sql) > 0;
        }

        /// <summary>
        /// 如果没有建立关系，则创建联系人和客户关系
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="contactID">联系人ID</param>
        /// <returns></returns>
        public bool AddContact(string customerID, string contactID)
        {
            string sql = string.Format(@"
            if NOT EXISTS(
                  SELECT *
                  FROM T_CRM_Customer_Contact
                  WHERE Customer_ID='{0}' and Contact_ID='{1}'
            )
            INSERT INTO T_CRM_Customer_Contact(Customer_ID,Contact_ID) Values('{0}', '{1}') ", customerID, contactID);

            return SqlExecute(sql) > 0;
        }

        /// <summary>
        /// 如果建立关系，则移除联系人和客户关系
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="contactID">联系人ID</param>
        /// <returns></returns>
        public bool RemoveContact(string customerID, string contactID)
        {
            string sql = string.Format(@"Delete From T_CRM_Customer_Contact WHERE Customer_ID='{0}' and Contact_ID='{1}' ", customerID, contactID);
            return SqlExecute(sql) > 0;
        }

        /// <summary>
        /// 如果建立关系，则移除联系人和客户的所有关系
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        public bool RemoveContact(string customerID)
        {
            string sql = string.Format(@"Delete From T_CRM_Customer_Contact WHERE Customer_ID='{0}' ", customerID);
            return SqlExecute(sql) > 0;
        }
    }
}