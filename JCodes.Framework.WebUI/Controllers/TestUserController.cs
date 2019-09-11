using System;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using System.Collections.Generic;
using JCodes.Framework.BLL;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Extension;

namespace JCodes.Framework.WebUI.Controllers
{
    public class TestUserController : BusinessController<TestUser, TestUserInfo>
    {
        public TestUserController() : base()
        {
        }

        #region 导入Excel数据操作

        //导入或导出的字段列表   
        string columnString = "姓名,手机,邮箱,主页,兴趣爱好,性别,年龄,出生日期,身高,备注";

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
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(TestUserController));
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

            List<TestUserInfo> list = new List<TestUserInfo>();

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
                    TestUserInfo info = new TestUserInfo();

                    info.Name = dr["姓名"].ToString();
                    info.MobilePhone = dr["手机"].ToString();
                    info.Email = dr["邮箱"].ToString();
                    info.Homepage = dr["主页"].ToString();
                    info.Hobby = dr["兴趣爱好"].ToString();
                    info.Gender = dr["性别"].ToString().ToInt32();
                    info.Age = dr["年龄"].ToString().ToInt32();
                    converted = DateTime.TryParse(dr["出生日期"].ToString(), out dt);
                    if (converted && dt > dtDefault)
                    {
                        info.Birthday = dt;
                    }
                    info.Height = dr["身高"].ToString().ToDecimal();
                    info.Remark = dr["备注"].ToString();

                    //info.Creator = CurrentUser.Id;
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
        public ActionResult SaveExcelData(List<TestUserInfo> list)
        {
            CommonResult result = new CommonResult();
            if (list != null && list.Count > 0)
            {
                #region 采用事务进行数据提交

                DbTransaction trans = BLLFactory<TestUser>.Instance.CreateTransaction();
                if (trans != null)
                {
                    try
                    {
                        //int seq = 1;
                        foreach (TestUserInfo detail in list)
                        {
                            //detail.Seq = seq++;//增加1
                            detail.CreatorTime = DateTime.Now;
                            detail.CreatorId = CurrentUser.Id;
                            detail.EditorId = CurrentUser.Id;
                            detail.LastUpdateTime = DateTime.Now;

                            BLLFactory<TestUser>.Instance.Insert(detail, trans);
                        }
                        trans.Commit();
                        result.Success = true;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(TestUserController));
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
            List<TestUserInfo> list = new List<TestUserInfo>();

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
                dr["姓名"] = list[i].Name;
                dr["手机"] = list[i].MobilePhone;
                dr["邮箱"] = list[i].Email;
                dr["主页"] = list[i].Homepage;
                dr["兴趣爱好"] = list[i].Hobby;
                dr["性别"] = list[i].Gender;
                dr["年龄"] = list[i].Age;
                dr["出生日期"] = list[i].Birthday;
                dr["身高"] = list[i].Height;
                dr["备注"] = list[i].Remark;
                //如果为外键，可以在这里进行转义，如下例子
                //dr["客户名称"] = BLLFactory<Customer>.Instance.GetCustomerName(list[i].Customer_ID);//转义为客户名称

                datatable.Rows.Add(dr);
            }
            #endregion

            #region 把DataTable转换为Excel并输出

            //根据用户创建目录，确保生成的文件不会产生冲突
            string filePath = string.Format("/GenerateFiles/{0}/TestUser.xls", CurrentUser.Name);
            GenerateExcel(datatable, filePath);

            #endregion

            //返回生成后的文件路径，让客户端根据地址下载
            return Content(filePath);
        }

        #endregion

        #region 写入数据前修改部分属性
        protected override void OnBeforeInsert(TestUserInfo info)
        {
            //info.ID = string.IsNullOrEmpty(info.Id) ? Guid.NewGuid().ToString() : info.ID;

            //子类对参数对象进行修改
            info.CreatorTime = DateTime.Now;
            info.CreatorId = CurrentUser.Id;
            //info.Company_ID = CurrentUser.Company_ID;
            //info.Dept_ID = CurrentUser.Dept_ID;
        }

        protected override void OnBeforeUpdate(TestUserInfo info)
        {
            //子类对参数对象进行修改
            info.EditorId = CurrentUser.Id;
            info.LastUpdateTime = DateTime.Now;
        }
        #endregion

        public override ActionResult FindWithPager()
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(AuthorizeKey.ListKey);

            string where = GetPagerCondition();
            PagerInfo pagerInfo = GetPagerInfo();
            List<TestUserInfo> list = baseBLL.FindWithPager(where, pagerInfo);

            //如果需要修改字段显示，则参考下面代码处理
            //foreach(TestUserInfo info in list)
            //{
            //    info.PID = BLLFactory<TestUser>.Instance.GetFieldValue(info.PID, "Name");
            //    if (!string.IsNullOrEmpty(info.Creator))
            //    {
            //        info.Creator = BLLFactory<User>.Instance.GetFullNameByID(info.Creator.ToInt32());
            //    }
            //}

            //Json格式的要求{total:22,rows:{}}
            //构造成Json的格式传递
            var result = new { total = pagerInfo.RecordCount, rows = list };
            return ToJsonContentDate(result);
        }

        /// <summary>
        /// 上传用户头像图片
        /// </summary>
        /// <param name="id">用户的ID</param>
        /// <returns></returns>
        public ActionResult EditPortrait(string id)
        {
            CommonResult result = new CommonResult();

            try
            {
                var files = Request.Files;
                if (files != null && files.Count > 0)
                {
                    TestUserInfo info = BLLFactory<TestUser>.Instance.FindByID(id);
                    if (info != null)
                    {
                        var fileData = ReadFileBytes(files[0]);
                        result.Success = BLLFactory<TestUser>.Instance.UpdatePersonImageBytes(id, fileData);
                    }
                }
            }
            catch(Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            return ToJsonContent(result);
        }
        public ActionResult GetPortrait(string id)
        {
            ActionResult result = Content("");

            var fileData = BLLFactory<TestUser>.Instance.GetPersonImageBytes(id);
            if (fileData != null)
            {
                result = File(fileData, @"image/png");
            }
            else
            {
                var file = Server.MapPath("/Content/Images/user_male.png");
                fileData = FileUtil.FileToBytes(file);
                result = File(fileData, @"image/png");
            }
            return result;
        }

    }
}
