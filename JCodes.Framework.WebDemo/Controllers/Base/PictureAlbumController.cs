using System;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using System.Collections.Generic;
using JCodes.Framework.BLL;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Collections;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Extension;

namespace JCodes.Framework.WebDemo.Controllers
{
    public class PictureAlbumController : BusinessController<PictureAlbum, PictureAlbumInfo>
    {
        public PictureAlbumController() : base()
        {
        }

        #region 导入Excel数据操作
 		 		 		   		 		 
	    //导入或导出的字段列表   
        string columnString = "父ID,名称,备注,创建人,创建时间";

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
                    result.ErrorCode = DataTableHelper.ContainAllColumns(dt, columnString) ? 0: 1;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(PictureAlbumController));
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

            List<PictureAlbumInfo> list = new List<PictureAlbumInfo>();

            DataTable table = ConvertExcelFileToTable(guid);
            if (table != null)
            {
                #region 数据转换
                foreach (DataRow dr in table.Rows)
                {
                    DateTime dtDefault = Convert.ToDateTime("1900-01-01");
                    PictureAlbumInfo info = new PictureAlbumInfo();

                    info.Pid = dr["父ID"].ToString().ToInt32() ;
                    info.Name = dr["名称"].ToString();
                    info.Remark = dr["备注"].ToString();

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
        public ActionResult SaveExcelData(List<PictureAlbumInfo> list)
        {
            ReturnResult result = new ReturnResult();
            if (list != null && list.Count > 0)
            {
                #region 采用事务进行数据提交

                DbTransaction trans = BLLFactory<PictureAlbum>.Instance.CreateTransaction();
                if (trans != null)
                {
                    try
                    {
                        //int seq = 1;
                        foreach (PictureAlbumInfo detail in list)
                        {
                            //detail.Seq = seq++;//增加1
                            detail.CreatorTime = DateTime.Now;
                            detail.CreatorId = CurrentUser.Id;
                            detail.EditorId = CurrentUser.Id;
                            detail.LastUpdateTime = DateTime.Now;

                            BLLFactory<PictureAlbum>.Instance.Insert(detail, trans);
                        }
                        trans.Commit();
                        result.ErrorCode = 0;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(PictureAlbumController));
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
            List<PictureAlbumInfo> list = new List<PictureAlbumInfo>();

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
                 dr["父ID"] = list[i].Pid;
                 dr["名称"] = list[i].Name;
                 dr["备注"] = list[i].Remark;
                 dr["创建人"] = list[i].CreatorId;
                 dr["创建时间"] = list[i].CreatorTime;
                 //如果为外键，可以在这里进行转义，如下例子
                //dr["客户名称"] = BLLFactory<Customer>.Instance.GetCustomerName(list[i].Customer_ID);//转义为客户名称

                datatable.Rows.Add(dr);
            } 
            #endregion

            #region 把DataTable转换为Excel并输出

            //根据用户创建目录，确保生成的文件不会产生冲突
            string filePath = string.Format("/GenerateFiles/{0}/PictureAlbum.xls", CurrentUser.Name);
            GenerateExcel(datatable, filePath);

            #endregion

            //返回生成后的文件路径，让客户端根据地址下载
            return Content(filePath);
        }
        
        #endregion
		
        #region 写入数据前修改部分属性
        protected override void OnBeforeInsert(PictureAlbumInfo info)
        {
            //info.ID = string.IsNullOrEmpty(info.ID) ? Guid.NewGuid().ToString() : info.ID;
            
            //子类对参数对象进行修改
            info.CreatorId = CurrentUser.Id;
            info.CreatorTime = DateTime.Now;
            info.EditorId = CurrentUser.Id;
            info.LastUpdateTime = DateTime.Now;

            //info.Company_ID = CurrentUser.Company_ID;
            //info.Dept_ID = CurrentUser.Dept_ID;
        }

        protected override void OnBeforeUpdate(PictureAlbumInfo info)
        {
            //子类对参数对象进行修改
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
            List<PictureAlbumInfo> list = baseBLL.FindWithPager(where, pagerInfo);

			//如果需要修改字段显示，则参考下面代码处理
            //foreach(PictureAlbumInfo info in list)
            //{
            //    info.PID = BLLFactory<PictureAlbum>.Instance.GetFieldValue(info.PID, "Name");
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
        /// 获取JSTree的列表集合
        /// </summary>
        /// <param name="userid">所属用户ID</param>
        /// <returns></returns>
        public ActionResult GetJsTreeJson(string userid)
        {
            ActionResult result = Content("");

            if (!string.IsNullOrEmpty(userid))
            {
                List<JsTreeTable> jsTable = new List<JsTreeTable>();
                string condition = string.Format("Creator='{0}'", userid);
                List<PictureAlbumInfo> list = BLLFactory<PictureAlbum>.Instance.Find(condition);
                foreach(PictureAlbumInfo info in list)
                {
                    bool isParent = (info.Pid == -1);
                    string icon =isParent ? "fa fa-home icon-state-warning icon-lg" : "fa fa-folder icon-state-success icon-lg";
                    string parent = isParent ? "#" : info.Pid.ToString();
                    JsTreeTable tree = new JsTreeTable(info.Id.ToString(), info.Name, icon, parent);
                    jsTable.Add(tree);
                }
                result = ToJsonContent(jsTable);
            }

            return result;
        }

        /// <summary>
        /// 用作下拉列表的菜单Json数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDictJson()
        {
            List<PictureAlbumInfo> list = baseBLL.GetAll();
            list = CollectionHelper<PictureAlbumInfo>.Fill("-1", 0, list, "PID", "ID", "Name");

            List<CDicKeyValue> itemList = new List<CDicKeyValue>();
            foreach (PictureAlbumInfo info in list)
            {
                itemList.Add(new CDicKeyValue(info.Id, info.Name));
            }
            itemList.Insert(0, new CDicKeyValue(-1, "无"));
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }

    }
}
