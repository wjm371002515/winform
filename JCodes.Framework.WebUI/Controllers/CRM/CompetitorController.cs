using System;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using System.Collections.Generic;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.BLL;

namespace JCodes.Framework.WebUI.Controllers
{
    public class CompetitorController : BusinessController<Competitor, CompetitorInfo>
    {
        public CompetitorController() : base()
        {
        }

        #region 导入Excel数据操作
 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		 		   		 		 
	    //导入或导出的字段列表   
        string columnString = "编号,对手名称,对手简称,所在省份,城市,所在行政区,公司地址,公司邮编,办公电话,传真号码,联系人,联系人电话,联系人手机,电子邮件,QQ号码,单位网站,备注信息";

        /// <summary>
        /// 检查Excel文件的字段是否包含了必须的字段
        /// </summary>
        /// <param name="guid">附件的GUID</param>
        /// <returns></returns>
        public ActionResult CheckExcelColumns(string guid)
        {
            CommonResult result = new CommonResult();

            try
            {
                DataTable dt = ConvertExcelFileToTable(guid);
                if (dt != null)
                {
                    //检查列表是否包含必须的字段
                    result.Success = DataTableHelper.ContainAllColumns(dt, columnString);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(CompetitorController));
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

            List<CompetitorInfo> list = new List<CompetitorInfo>();

            DataTable table = ConvertExcelFileToTable(guid);
            if (table != null)
            {
                #region 数据转换
                int i = 1;
                foreach (DataRow dr in table.Rows)
                {
                    bool converted = false;
                    DateTime dtDefault = Convert.ToDateTime("1900-01-01");
                    DateTime dt;
                    CompetitorInfo info = new CompetitorInfo();

                    info.HandNo = dr["编号"].ToString();
                    info.Name = dr["对手名称"].ToString();
                    info.SimpleName = dr["对手简称"].ToString();
                    info.Province = dr["所在省份"].ToString();
                    info.City = dr["城市"].ToString();
                    info.District = dr["所在行政区"].ToString();
                    info.Address = dr["公司地址"].ToString();
                    info.ZipCode = dr["公司邮编"].ToString();
                    info.Telephone = dr["办公电话"].ToString();
                    info.Fax = dr["传真号码"].ToString();
                    info.Contact = dr["联系人"].ToString();
                    info.ContactPhone = dr["联系人电话"].ToString();
                    info.ContactMobile = dr["联系人手机"].ToString();
                    info.Email = dr["电子邮件"].ToString();
                    info.Qq = dr["QQ号码"].ToString();
                    info.WebSite = dr["单位网站"].ToString();
                    //info.AttachGUID = dr["附件组别ID"].ToString();
                    info.Note = dr["备注信息"].ToString();

                    info.Creator = CurrentUser.ID.ToString();
                    info.CreateTime = DateTime.Now;
                    info.Editor = CurrentUser.ID.ToString();
                    info.EditTime = DateTime.Now;

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
        public ActionResult SaveExcelData(List<CompetitorInfo> list)
        {
            CommonResult result = new CommonResult();
            if (list != null && list.Count > 0)
            {
                #region 采用事务进行数据提交

                DbTransaction trans = BLLFactory<Competitor>.Instance.CreateTransaction();
                if (trans != null)
                {
                    try
                    {
                        //int seq = 1;
                        foreach (CompetitorInfo detail in list)
                        {
                            //detail.Seq = seq++;//增加1
                            detail.CreateTime = DateTime.Now;
                            detail.Creator = CurrentUser.ID.ToString();
                            detail.Editor = CurrentUser.ID.ToString();
                            detail.EditTime = DateTime.Now; 
                            
                            detail.Company_ID = CurrentUser.Company_ID;
                            detail.Dept_ID = CurrentUser.Dept_ID;

                            BLLFactory<Competitor>.Instance.Insert(detail, trans);
                        }
                        trans.Commit();
                        result.Success = true;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(CompetitorController));
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
            List<CompetitorInfo> list = new List<CompetitorInfo>();

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
                dr["序号"] = j++;                
                 dr["编号"] = list[i].HandNo;
                 dr["对手名称"] = list[i].Name;
                 dr["对手简称"] = list[i].SimpleName;
                 dr["所在省份"] = list[i].Province;
                 dr["城市"] = list[i].City;
                 dr["所在行政区"] = list[i].District;
                 dr["公司地址"] = list[i].Address;
                 dr["公司邮编"] = list[i].ZipCode;
                 dr["办公电话"] = list[i].Telephone;
                 dr["传真号码"] = list[i].Fax;
                 dr["联系人"] = list[i].Contact;
                 dr["联系人电话"] = list[i].ContactPhone;
                 dr["联系人手机"] = list[i].ContactMobile;
                 dr["电子邮件"] = list[i].Email;
                 dr["QQ号码"] = list[i].Qq;
                 dr["单位网站"] = list[i].WebSite;
                 dr["备注信息"] = list[i].Note;
                 //如果为外键，可以在这里进行转义，如下例子
                //dr["客户名称"] = BLLFactory<Customer>.Instance.GetCustomerName(list[i].Customer_ID);//转义为客户名称

                datatable.Rows.Add(dr);
            } 
            #endregion

            #region 把DataTable转换为Excel并输出

            //根据用户创建目录，确保生成的文件不会产生冲突
            string filePath = string.Format("/GenerateFiles/{0}/Competitor.xls", CurrentUser.Name);
            GenerateExcel(datatable, filePath);

            #endregion

            //返回生成后的文件路径，让客户端根据地址下载
            return Content(filePath);
        }
        
        #endregion
		
        #region 写入数据前修改部分属性
        protected override void OnBeforeInsert(CompetitorInfo info)
        {
            info.ID = string.IsNullOrEmpty(info.ID) ? Guid.NewGuid().ToString() : info.ID;
            
            //子类对参数对象进行修改
            info.CreateTime = DateTime.Now;
            info.Creator = CurrentUser.ID.ToString();
            info.Company_ID = CurrentUser.Company_ID;
            info.Dept_ID = CurrentUser.Dept_ID;
        }

        protected override void OnBeforeUpdate(CompetitorInfo info)
        {
            //子类对参数对象进行修改
            info.Editor = CurrentUser.ID.ToString();
            info.EditTime = DateTime.Now;
        } 
        #endregion

        public override ActionResult FindWithPager()
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(AuthorizeKey.ListKey);

            string where = GetPagerCondition();
            PagerInfo pagerInfo = GetPagerInfo();
            List<CompetitorInfo> list = baseBLL.FindWithPager(where, pagerInfo);

			//如果需要修改字段显示，则参考下面代码处理
            foreach (CompetitorInfo info in list)
            {
                //可以使用这种在服务器中解析，并返回
                //也可以在页面端进行调用解析
                info.Data1 = BLLFactory<Province>.Instance.GetFieldValue(info.Province, "ProvinceName");
                info.Data2 = BLLFactory<City>.Instance.GetFieldValue(info.City, "CityName");
                info.Data3 = BLLFactory<District>.Instance.GetFieldValue(info.District, "DistrictName");
            }

            //Json格式的要求{total:22,rows:{}}
            //构造成Json的格式传递
            var result = new { total = pagerInfo.RecordCount, rows = list };
            return ToJsonContentDate(result);
        }

    }
}
