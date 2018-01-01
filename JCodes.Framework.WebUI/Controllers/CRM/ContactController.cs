using System;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Aspose.Cells;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Extension;

namespace JCodes.Framework.WebUI.Controllers
{
    public class ContactController : BusinessController<Contact, ContactInfo>
    {
        public ContactController() : base()
        {
        }


        #region 导入Excel数据操作
        
        //导入或导出的字段列表    
        string columnString = "客户名称,编号,姓名,身份证号码,出生日期,性别,办公电话,家庭电话,传真,联系人手机,联系人地址,邮政编码,电子邮件,QQ号码,备注,排序序号,所在省份,城市,所在行政区,籍贯,家庭住址,民族,教育程度,毕业学校,政治面貌,职业类型,职称,职务,所在部门,爱好,属相,星座,婚姻状态,健康状况,重要级别,认可程度,关系,负责需求,关心重点,利益诉求,体型,吸烟,喝酒,身高,体重,视力,个人简述";

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
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(ContactController));
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

            List<ContactInfo> list = new List<ContactInfo>();

            DataTable table = ConvertExcelFileToTable(guid);
            if (table != null)
            {
                #region 数据转换
                int i = 1;
                foreach (DataRow dr in table.Rows)
                {
                    string customerName = dr["客户名称"].ToString();
                    if (string.IsNullOrEmpty(customerName))
                    {
                        continue;//客户名称为空，记录跳过
                    }

                    CustomerInfo customerInfo = BLLFactory<Customer>.Instance.FindByName(customerName);
                    if (customerInfo == null)
                    {
                        continue;//客户名称不存在，记录跳过
                    }

                    bool converted = false;
                    DateTime dtDefault = Convert.ToDateTime("1900-01-01");
                    DateTime dt;
                    ContactInfo info = new ContactInfo();
                    info.ID = customerInfo.ID;//客户ID
                    info.HandNo = dr["编号"].ToString();
                    info.Name = dr["姓名"].ToString();
                    info.IDCarNo = dr["身份证号码"].ToString();
                    converted = DateTime.TryParse(dr["出生日期"].ToString(), out dt);
                    if (converted && dt > dtDefault)
                    {
                        info.Birthday = dt;
                    }
                    info.Sex = dr["性别"].ToString();
                    info.OfficePhone = dr["办公电话"].ToString();
                    info.HomePhone = dr["家庭电话"].ToString();
                    info.Fax = dr["传真"].ToString();
                    info.Mobile = dr["联系人手机"].ToString();
                    info.Address = dr["联系人地址"].ToString();
                    info.ZipCode = dr["邮政编码"].ToString();
                    info.Email = dr["电子邮件"].ToString();
                    info.QQ = dr["QQ号码"].ToString();
                    info.Note = dr["备注"].ToString();
                    info.Seq = dr["排序序号"].ToString();
                    info.Province = dr["所在省份"].ToString();
                    info.City = dr["城市"].ToString();
                    info.District = dr["所在行政区"].ToString();
                    info.Hometown = dr["籍贯"].ToString();
                    info.HomeAddress = dr["家庭住址"].ToString();
                    info.Nationality = dr["民族"].ToString();
                    info.Eduction = dr["教育程度"].ToString();
                    info.GraduateSchool = dr["毕业学校"].ToString();
                    info.Political = dr["政治面貌"].ToString();
                    info.JobType = dr["职业类型"].ToString();
                    info.Titles = dr["职称"].ToString();
                    info.Rank = dr["职务"].ToString();
                    info.Department = dr["所在部门"].ToString();
                    info.Hobby = dr["爱好"].ToString();
                    info.Animal = dr["属相"].ToString();
                    info.Constellation = dr["星座"].ToString();
                    info.MarriageStatus = dr["婚姻状态"].ToString();
                    info.HealthCondition = dr["健康状况"].ToString();
                    info.Importance = dr["重要级别"].ToString();
                    info.Recognition = dr["认可程度"].ToString();
                    info.RelationShip = dr["关系"].ToString();
                    info.ResponseDemand = dr["负责需求"].ToString();
                    info.CareFocus = dr["关心重点"].ToString();
                    info.InterestDemand = dr["利益诉求"].ToString();
                    info.BodyType = dr["体型"].ToString();
                    info.Smoking = dr["吸烟"].ToString();
                    info.Drink = dr["喝酒"].ToString();
                    info.Height = dr["身高"].ToString();
                    info.Weight = dr["体重"].ToString();
                    info.Vision = dr["视力"].ToString();
                    info.Introduce = dr["个人简述"].ToString();

                    info.Creator = CurrentUser.ID.ToString();
                    info.CreateTime = DateTime.Now;
                    info.Editor = CurrentUser.ID.ToString();
                    info.EditTime = DateTime.Now;

                    //增加一个特殊字段的转义
                    info.Data1 = BLLFactory<Customer>.Instance.GetCustomerName(info.ID);

                    list.Add(info);
                }
                #endregion
            }

            #region 其他转义操作
            ////增加一个客户名称字段，然后进行解析，构建一个DataTable返回
            //DataTable dtReturn = DataTableHelper.ListToDataTable<ContactInfo>(list);
            //dtReturn.Columns.Add("CustomerName");

            //foreach (DataRow row in dtReturn.Rows)
            //{
            //    row["CustomerName"] = BLLFactory<Customer>.Instance.GetCustomerName(row["Customer_ID"].ToString());
            //}
            //var result = new { total = dtReturn.Rows.Count, rows = dtReturn }; 
            #endregion

            var result = new { total = list.Count, rows = list };
            return ToJsonContentDate(result);
        }

        /// <summary>
        /// 保存客户端上传的相关数据列表
        /// </summary>
        /// <param name="list">数据列表</param>
        /// <returns></returns>
        public ActionResult SaveExcelData(List<ContactInfo> list)
        {
            CommonResult result = new CommonResult();
            if (list != null && list.Count > 0)
            {
                #region 采用事务进行数据提交

                DbTransaction trans = BLLFactory<Contact>.Instance.CreateTransaction();
                if (trans != null)
                {
                    try
                    {
                        //int seq = 1;
                        foreach (ContactInfo detail in list)
                        {
                            //detail.Seq = seq++;//增加1
                            detail.CreateTime = DateTime.Now;
                            detail.Creator = CurrentUser.ID.ToString();
                            detail.Editor = CurrentUser.ID.ToString();
                            detail.EditTime = DateTime.Now;

                            detail.Company_ID = CurrentUser.Company_ID;
                            detail.Dept_ID = CurrentUser.Dept_ID;

                            BLLFactory<Contact>.Instance.Insert(detail, trans);
                        }
                        trans.Commit();
                        result.Success = true;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(ContactController));
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
            List<ContactInfo> list = new List<ContactInfo>();

            if (!string.IsNullOrWhiteSpace(CustomedCondition))
            {
                Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(CustomedCondition);
                if (dict != null)
                {
                    string id = dict["id"];
                    string groupname = dict["groupname"];
                    string userid = dict["userid"];

                    if (string.IsNullOrEmpty(id))
                    {
                        if (groupname == "所有联系人")
                        {
                            where = "";//直接使用空条件
                            list = baseBLL.Find(where);
                        }
                        else if (groupname == "未分组联系人")
                        {
                            list = BLLFactory<Contact>.Instance.FindByGroupName(userid, null);
                        }
                    }
                    else
                    {
                        list = BLLFactory<Contact>.Instance.FindByGroupName(userid, groupname);
                    }
                }
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
                dr["客户名称"] = BLLFactory<Customer>.Instance.GetCustomerName(list[i].ID);//转义为客户名称
                dr["编号"] = list[i].HandNo;
                dr["姓名"] = list[i].Name;
                dr["身份证号码"] = list[i].IDCarNo;
                dr["出生日期"] = list[i].Birthday;
                dr["性别"] = list[i].Sex;
                dr["办公电话"] = list[i].OfficePhone;
                dr["家庭电话"] = list[i].HomePhone;
                dr["传真"] = list[i].Fax;
                dr["联系人手机"] = list[i].Mobile;
                dr["联系人地址"] = list[i].Address;
                dr["邮政编码"] = list[i].ZipCode;
                dr["电子邮件"] = list[i].Email;
                dr["QQ号码"] = list[i].QQ;
                dr["备注"] = list[i].Note;
                dr["排序序号"] = list[i].Seq;
                dr["所在省份"] = list[i].Province;
                dr["城市"] = list[i].City;
                dr["所在行政区"] = list[i].District;
                dr["籍贯"] = list[i].Hometown;
                dr["家庭住址"] = list[i].HomeAddress;
                dr["民族"] = list[i].Nationality;
                dr["教育程度"] = list[i].Eduction;
                dr["毕业学校"] = list[i].GraduateSchool;
                dr["政治面貌"] = list[i].Political;
                dr["职业类型"] = list[i].JobType;
                dr["职称"] = list[i].Titles;
                dr["职务"] = list[i].Rank;
                dr["所在部门"] = list[i].Department;
                dr["爱好"] = list[i].Hobby;
                dr["属相"] = list[i].Animal;
                dr["星座"] = list[i].Constellation;
                dr["婚姻状态"] = list[i].MarriageStatus;
                dr["健康状况"] = list[i].HealthCondition;
                dr["重要级别"] = list[i].Importance;
                dr["认可程度"] = list[i].Recognition;
                dr["关系"] = list[i].RelationShip;
                dr["负责需求"] = list[i].ResponseDemand;
                dr["关心重点"] = list[i].CareFocus;
                dr["利益诉求"] = list[i].InterestDemand;
                dr["体型"] = list[i].BodyType;
                dr["吸烟"] = list[i].Smoking;
                dr["喝酒"] = list[i].Drink;
                dr["身高"] = list[i].Height;
                dr["体重"] = list[i].Weight;
                dr["视力"] = list[i].Vision;
                dr["个人简述"] = list[i].Introduce;

                datatable.Rows.Add(dr);
            } 
            #endregion

            #region 把DataTable转换为Excel并输出

            //根据用户创建目录，确保生成的文件不会产生冲突
            string filePath = string.Format("/GenerateFiles/{0}/Contact.xls", CurrentUser.Name);
            GenerateExcel(datatable, filePath);

            #endregion

            //返回生成后的文件路径，让客户端根据地址下载
            return Content(filePath);
        }

        #endregion


        #region 写入数据前修改部分属性
        protected override void OnBeforeInsert(ContactInfo info)
        {
            //留给子类对参数对象进行修改
            info.CreateTime = DateTime.Now;
            info.Creator = CurrentUser.ID.ToString();
            info.Company_ID = CurrentUser.Company_ID;
            info.Dept_ID = CurrentUser.Dept_ID;
        }

        protected override void OnBeforeUpdate(ContactInfo info)
        {
            //留给子类对参数对象进行修改
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
            List<ContactInfo> list = new List<ContactInfo>();

            //如果自定义查询条件非空，那么采用自定义查询方法
            string CustomedCondition = Request["CustomedCondition"] ?? "";
            if (!string.IsNullOrWhiteSpace(CustomedCondition))
            {
                Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(CustomedCondition);
                if (dict != null)
                {
                    string id = dict["id"];
                    string groupname = dict["groupname"];
                    string userid = dict["userid"];

                    if (string.IsNullOrEmpty(id) || id.StartsWith("j")) //对于JSTree如果id为空，会自动分配一个j*的自动ID
                    {
                        if (groupname == "所有联系人")
                        {
                            where = "";//直接使用空条件
                            list = baseBLL.FindWithPager(where, pagerInfo);
                        }
                        else if (groupname == "未分组联系人")
                        {
                            list = BLLFactory<Contact>.Instance.FindByGroupName(userid, null, pagerInfo);
                        }
                    }
                    else
                    {
                        list = BLLFactory<Contact>.Instance.FindByGroupName(userid, groupname, pagerInfo);
                    }
                }
            }
            else
            {
                list = baseBLL.FindWithPager(where, pagerInfo);
            }

            foreach (ContactInfo info in list)
            {
                //增加一个特殊字段的转义
                info.Data1 = BLLFactory<Customer>.Instance.GetCustomerName(info.ID);
            }

            //Json格式的要求{total:22,rows:{}}
            //构造成Json的格式传递
            var result = new { total = pagerInfo.RecordCount, rows = list };
            return ToJsonContentDate(result);
        }

        /// <summary>
        /// 调整联系人的组别
        /// </summary>
        /// <param name="contactId">联系人ID</param>
        /// <param name="groupIdList">分组Id集合</param>
        /// <returns></returns>
        public ActionResult ModifyContactGroup(string contactId, string groupIdList)
        {
            List<string> idList = new List<string>();
            if (!string.IsNullOrEmpty(groupIdList))
            {
                idList = groupIdList.ToDelimitedList<string>(",");
            }

            CommonResult result = new CommonResult();
            try
            {
                result.Success = BLLFactory<Contact>.Instance.ModifyContactGroup(contactId, idList);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(ContactController));
                result.ErrorMessage = ex.Message;
            }
            return ToJsonContent(result);
        }
    }
}
