using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;

namespace JCodes.Framework.WebUI.Controllers
{
    public class CustomerController : BusinessController<Customer, CustomerInfo>
    {
        public CustomerController() : base()
        {
        }

        #region 导入Excel数据操作

        string importColumnString = "客户编号,客户名称,客户简称,所在省份,城市,所在行政区,市场分区,公司地址,公司邮编,办公电话,传真号码,主联系人,联系人电话,联系人手机,电子邮件,QQ号码,,备注信息";
        //导入或导出的字段列表   
        string columnString = "客户编号,客户名称,客户简称,所在省份,城市,所在行政区,市场分区,公司地址,公司邮编,办公电话,传真号码,主联系人,联系人电话,联系人手机,电子邮件,QQ号码,所属行业,经营范围,经营品牌,主要客户群,主营业务,注册资金,营业额,营业执照,开户银行,银行账号,地税登记号,国税登记号,法人名称,法人电话,法人手机,客户来源,单位网站,客户类别,客户级别,信用等级,重要级别,公开与否,客户满意度,备注信息,客户阶段,客户状态";

        /// <summary>
        /// 检查Excel文件的字段是否包含了必须的字段
        /// </summary>
        /// <param name="guid">附件的GUID</param>
        /// <returns></returns>
        public ActionResult CheckExcelColumns(string guid)
        {
            ReturnResult result = new ReturnResult();

            try
            {
                DataTable dt = ConvertExcelFileToTable(guid);
                if (dt != null)
                {
                    //检查列表是否包含必须的字段
                    result.ErrorCode = DataTableHelper.ContainAllColumns(dt, importColumnString)?0:1;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(CustomerController));
                result.ErrorMessage = ex.Message;
            }

            return ToJsonContent(result);
        }

        /// <summary>
        /// 获取服务器上的Excel文件，并把它转换为实体列表返回给客户端
        /// </summary>
        /// <param name="guid">附件的GUID</param>
        /// <returns></returns>
        public ActionResult GetExcelData(string guid)
        {
            if (string.IsNullOrEmpty(guid))
            {
                return null;
            }

            List<CustomerInfo> list = new List<CustomerInfo>();

            DataTable table = ConvertExcelFileToTable(guid);
            if (table != null)
            {
                #region 数据转换
                foreach (DataRow dr in table.Rows)
                {
                    DateTime dtDefault = Convert.ToDateTime("1900-01-01");
                    CustomerInfo info = new CustomerInfo();

                    /*info.UserCode = dr["客户编号"].ToString();
                    info.Name = dr["客户名称"].ToString();
                    info.LoginName = dr["客户简称"].ToString();
                    info.ProvinceName = dr["所在省份"].ToString();
                    info.CityName = dr["城市"].ToString();
                    info.DistrictName = dr["所在行政区"].ToString();
                    info.Area = dr["市场分区"].ToString();
                    info.Address = dr["公司地址"].ToString();
                    info.ZipCode = dr["公司邮编"].ToString();
                    info.MobilePhone = dr["办公电话"].ToString();
                    info.Fax = dr["传真号码"].ToString();
                    info.Contact = dr["主联系人"].ToString();
                    info.ContactPhone = dr["联系人电话"].ToString();
                    info.ContactMobile = dr["联系人手机"].ToString();
                    info.Email = dr["电子邮件"].ToString();
                    info.QQ = dr["QQ号码"].ToString();*/
                    //info.Industry = dr["所属行业"].ToString();
                    //info.BusinessScope = dr["经营范围"].ToString();
                    //info.Brand = dr["经营品牌"].ToString();
                    //info.PrimaryClient = dr["主要客户群"].ToString();
                    //info.PrimaryBusiness = dr["主营业务"].ToString();
                    //info.RegisterCapital = dr["注册资金"].ToString().ToDecimal();
                    //info.TurnOver = dr["营业额"].ToString().ToDecimal();
                    //info.LicenseNo = dr["营业执照"].ToString();
                    //info.Bank = dr["开户银行"].ToString();
                    //info.BankAccount = dr["银行账号"].ToString();
                    //info.LocalTaxNo = dr["地税登记号"].ToString();
                    //info.NationalTaxNo = dr["国税登记号"].ToString();
                    //info.LegalMan = dr["法人名称"].ToString();
                    //info.LegalTelephone = dr["法人电话"].ToString();
                    //info.LegalMobile = dr["法人手机"].ToString();
                    //info.Source = dr["客户来源"].ToString();
                    //info.WebSite = dr["单位网站"].ToString();

                    //info.CustomerType = dr["客户类别"].ToString();
                    //info.Grade = dr["客户级别"].ToString();
                    //info.CreditStatus = dr["信用等级"].ToString();
                    //info.Importance = dr["重要级别"].ToString();
                    //info.IsPublic = dr["公开与否"].ToString().ToBoolean();
                    //info.Satisfaction = dr["客户满意度"].ToString().ToInt32();
                    info.Remark = dr["备注信息"].ToString();
                    //info.Stage = dr["客户阶段"].ToString();
                    //info.Status = dr["客户状态"].ToString();

                    info.DeptId = CurrentUser.DeptId;
                    info.CompanyId = CurrentUser.CompanyId;
                    info.CreatorId = CurrentUser.Id;
                    info.CreatorTime = DateTime.Now;
                    info.EditorId = CurrentUser.Id;
                    info.LastUpdateTime = DateTime.Now;

                    list.Add(info);
                }
                #endregion
            }

            var result = new { total = list.Count, rows = list };
            return ToJsonContentDate(result);
        }

        /// <summary>
        /// 保存客户端上传的相关数据列表
        /// </summary>
        /// <param name="list">数据列表</param>
        /// <returns></returns>
        public ActionResult SaveExcelData(List<CustomerInfo> list)
        {
            ReturnResult result = new ReturnResult();
            if (list != null && list.Count > 0)
            {
                #region 采用事务进行数据提交

                DbTransaction trans = BLLFactory<Customer>.Instance.CreateTransaction();
                if (trans != null)
                {
                    try
                    {
                        //int seq = 1;
                        foreach (CustomerInfo detail in list)
                        {
                            //detail.Seq = seq++;//增加1
                            detail.CreatorTime = DateTime.Now;
                            detail.CreatorId = CurrentUser.Id;
                            detail.EditorId = CurrentUser.Id;
                            detail.LastUpdateTime = DateTime.Now;

                            detail.DeptId = CurrentUser.DeptId;
                            detail.CompanyId = CurrentUser.CompanyId;

                            BLLFactory<Customer>.Instance.Insert(detail, trans);
                        }
                        trans.Commit();
                        result.ErrorCode = 0;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(CustomerController));
                        result.ErrorMessage = ex.Message;
                        trans.Rollback();
                    }
                }
                #endregion
            }
            else
            {
                result.ErrorMessage = "导入信息不能为空";
            }

            return ToJsonContent(result);
        }

        /// <summary>
        /// 根据查询条件导出列表数据
        /// </summary>
        /// <returns></returns>
        public ActionResult Export()
        {
            #region 根据参数获取List列表
            string where = GetPagerCondition();
            string CustomedCondition = Request["CustomedCondition"] ?? "";
            List<CustomerInfo> list = new List<CustomerInfo>();

            if (!string.IsNullOrWhiteSpace(CustomedCondition))
            {
                //如果为自定义的json参数列表，那么可以使用字典反序列化获取参数，然后处理
                //Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(CustomedCondition);

                //如果是条件的自定义，可以使用Find查找
                list = baseBLL.Find(CustomedCondition);
            }
            else
            {
                list = baseBLL.Find(where);
            }

            #endregion

            #region 把列表转换为DataTable
            DataTable datatable = DataTableHelper.CreateTable("序号|int," + columnString);
            DataRow dr;
            int j = 1;
            for (int i = 0; i < list.Count; i++)
            {
                dr = datatable.NewRow();
                /*dr["序号"] = j++;
                dr["客户编号"] = list[i].UserCode;
                dr["客户名称"] = list[i].Name;
                dr["客户简称"] = list[i].LoginName;
                dr["所在省份"] = list[i].ProvinceName;
                dr["城市"] = list[i].CityName;
                dr["所在行政区"] = list[i].DistrictName;
                dr["市场分区"] = list[i].Area;
                dr["公司地址"] = list[i].Address;
                dr["公司邮编"] = list[i].ZipCode;
                dr["办公电话"] = list[i].MobilePhone;
                dr["传真号码"] = list[i].Fax;
                dr["主联系人"] = list[i].Contact;
                dr["联系人电话"] = list[i].ContactPhone;
                dr["联系人手机"] = list[i].ContactMobile;
                dr["电子邮件"] = list[i].Email;
                dr["QQ号码"] = list[i].QQ;
                dr["所属行业"] = list[i].Industry;
                dr["经营范围"] = list[i].BusinessScope;
                dr["经营品牌"] = list[i].Brand;
                dr["主要客户群"] = list[i].PrimaryClient;
                dr["主营业务"] = list[i].PrimaryBusiness;
                dr["注册资金"] = list[i].RegisterCapital;
                dr["营业额"] = list[i].TurnOver;
                dr["营业执照"] = list[i].LicenseNo;
                dr["开户银行"] = list[i].Bank;
                dr["银行账号"] = list[i].BankAccount;
                dr["地税登记号"] = list[i].LocalTaxNo;
                dr["国税登记号"] = list[i].NationalTaxNo;
                dr["法人名称"] = list[i].LegalMan;
                dr["法人电话"] = list[i].LegalTelephone;
                dr["法人手机"] = list[i].LegalMobile;
                dr["客户来源"] = list[i].Source;
                dr["单位网站"] = list[i].WebSite;

                dr["客户类别"] = list[i].CustomerType;
                dr["客户级别"] = list[i].Grade;
                dr["信用等级"] = list[i].CreditStatus;
                dr["重要级别"] = list[i].Importance;
                dr["公开与否"] = list[i].IsPublic;
                dr["客户满意度"] = list[i].Satisfaction;
                dr["备注信息"] = list[i].Remark;
                dr["客户阶段"] = list[i].Stage;
                dr["客户状态"] = list[i].AuditStatus;*/

                //如果为外键，可以在这里进行转义，如下例子
                //dr["客户名称"] = BLLFactory<Customer>.Instance.GetCustomerName(list[i].Customer_ID);//转义为客户名称

                datatable.Rows.Add(dr);
            }
            #endregion

            #region 把DataTable转换为Excel并输出

            //根据用户创建目录，确保生成的文件不会产生冲突
            string filePath = string.Format("/GenerateFiles/{0}/Customer.xls", CurrentUser.Name);
            GenerateExcel(datatable, filePath);

            #endregion

            //返回生成后的文件路径，让客户端根据地址下载
            return Content(filePath);
        }

        #endregion

        #region 写入数据前修改部分属性
        protected override void OnBeforeInsert(CustomerInfo info)
        {
            //留给子类对参数对象进行修改
            info.CreatorTime = DateTime.Now;
            info.CreatorId = CurrentUser.Id;
            info.CompanyId = CurrentUser.CompanyId;
            info.DeptId = CurrentUser.DeptId;
        }

        protected override void OnBeforeUpdate(CustomerInfo info)
        {
            //留给子类对参数对象进行修改
            info.EditorId = CurrentUser.Id;
            info.LastUpdateTime = DateTime.Now;
        }
        #endregion

        public override ActionResult FindWithPager()
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.ListKey);

            string where = GetPagerCondition();
            PagerInfo pagerInfo = GetPagerInfo();
            List<CustomerInfo> list = baseBLL.FindWithPager(where, pagerInfo);

            //如果需要修改字段显示，则参考下面代码处理
            //foreach(CustomerInfo info in list)
            //{
            //    info.PID = BLLFactory<Customer>.Instance.GetFieldValue(info.PID, "Name");
            //    if (!string.IsNullOrEmpty(info.Creator))
            //    {
            //        info.Creator = BLLFactory<User>.Instance.GetNameById(info.Creator.ToInt32());
            //    }
            //}

            //Json格式的要求{total:22,rows:{}}
            //构造成Json的格式传递
            var result = new { total = pagerInfo.RecordCount, rows = list };
            return ToJsonContentDate(result);
        }

        public ActionResult SelectCustomer()
        {
            return View("SelectCustomer");
        }

        public ActionResult GetCustomerNameById(Int32 id)
        {
            string name = BLLFactory<Customer>.Instance.GetCustomerNameById(id);
            return ToJsonContent(name);
        }
    }
}
