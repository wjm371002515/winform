using System;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using System.Collections.Generic;
using JCodes.Framework.WebUI.Controllers;
using JCodes.Framework.BLL;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.WebUI.Controllers
{
    public class ProvinceController : BusinessController<Province, ProvinceInfo>
    {
        public ProvinceController() : base()
        {
        }

        #region 导入Excel数据操作
 		 
	    //导入或导出的字段列表   
        string columnString = "省份名称";

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
                    result.ErrorCode = DataTableHelper.ContainAllColumns(dt, columnString)?0:1;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(ProvinceController));
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

            List<ProvinceInfo> list = new List<ProvinceInfo>();

            DataTable table = ConvertExcelFileToTable(guid);
            if (table != null)
            {
                #region 数据转换
                foreach (DataRow dr in table.Rows)
                {
                    DateTime dtDefault = Convert.ToDateTime("1900-01-01");                
                    ProvinceInfo info = new ProvinceInfo();
                    
                     info.ProvinceName = dr["省份名称"].ToString();
  
                    //info.Creator = CurrentUser.ID.ToString();
                    //info.CreateTime = DateTime.Now;
                    //info.Editor = CurrentUser.ID.ToString();
                    //info.EditTime = DateTime.Now;

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
        public ActionResult SaveExcelData(List<ProvinceInfo> list)
        {
            ReturnResult result = new ReturnResult();
            if (list != null && list.Count > 0)
            {
                #region 采用事务进行数据提交

                DbTransaction trans = BLLFactory<Province>.Instance.CreateTransaction();
                if (trans != null)
                {
                    try
                    {
                        //int seq = 1;
                        foreach (ProvinceInfo detail in list)
                        {
                            //detail.Seq = seq++;//增加1
                            //detail.CreateTime = DateTime.Now;
                            //detail.Creator = CurrentUser.ID.ToString();
                            //detail.Editor = CurrentUser.ID.ToString();
                            //detail.EditTime = DateTime.Now;

                            BLLFactory<Province>.Instance.Insert(detail, trans);
                        }
                        trans.Commit();
                        result.ErrorCode = 0;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(ProvinceController));
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
            List<ProvinceInfo> list = new List<ProvinceInfo>();

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
                dr["省份名称"] = list[i].ProvinceName;
                //如果为外键，可以在这里进行转义，如下例子
                //dr["客户名称"] = BLLFactory<Customer>.Instance.GetCustomerName(list[i].Customer_ID);//转义为客户名称

                datatable.Rows.Add(dr);
            }
            #endregion

            #region 把DataTable转换为Excel并输出

            //根据用户创建目录，确保生成的文件不会产生冲突
            string filePath = string.Format("/GenerateFiles/{0}/Province.xls", CurrentUser.Name);
            GenerateExcel(datatable, filePath);

            #endregion

            //返回生成后的文件路径，让客户端根据地址下载
            return Content(filePath);
        }
        
        #endregion
		
        #region 写入数据前修改部分属性
        protected override void OnBeforeInsert(ProvinceInfo info)
        {
            //info.ID = string.IsNullOrEmpty(info.ID) ? Guid.NewGuid().ToString() : info.ID;
            
            //子类对参数对象进行修改
            //info.CreateTime = DateTime.Now;
            //info.Creator = CurrentUser.ID.ToString();
            //info.Company_ID = CurrentUser.Company_ID;
            //info.Dept_ID = CurrentUser.Dept_ID;
        }

        protected override void OnBeforeUpdate(ProvinceInfo info)
        {
            //子类对参数对象进行修改
            //info.Editor = CurrentUser.ID.ToString();
            //info.EditTime = DateTime.Now;
        } 
        #endregion

        public override ActionResult FindWithPager()
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.ListKey);

            string where = GetPagerCondition();
            PagerInfo pagerInfo = GetPagerInfo();
            List<ProvinceInfo> list = baseBLL.FindWithPager(where, pagerInfo);

			//如果需要修改字段显示，则参考下面代码处理
            //foreach(ProvinceInfo info in list)
            //{
            //    info.PID = BLLFactory<Province>.Instance.GetFieldValue(info.PID, "Name");
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

        /// <summary>
        /// 获取所有的省份
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllProvinceJsTree()
        {
            List<JsTreeData> treeList = new List<JsTreeData>();
            JsTreeData pNode = new JsTreeData("", "选择记录", "fa fa-home icon-state-warning icon-lg");
            treeList.Add(pNode);

            List<ProvinceInfo> provinceList = BLLFactory<Province>.Instance.GetAll();
            foreach (ProvinceInfo info in provinceList)
            {
                JsTreeData item = new JsTreeData(info.Id, info.ProvinceName, "fa fa-file icon-state-warning icon-lg");
                pNode.children.Add(item);
            }
            return ToJsonContent(treeList);
        }

        /// <summary>
        /// 获取所有的省份
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllProvinceDictJson()
        {
            List<CDicKeyValue> treeList = new List<CDicKeyValue>();

            List<ProvinceInfo> provinceList = BLLFactory<Province>.Instance.GetAll();
            foreach (ProvinceInfo info in provinceList)
            {
                CDicKeyValue item = new CDicKeyValue(info.Id, info.ProvinceName);
                treeList.Add(item);
            }
            return ToJsonContent(treeList);
        }
        /// <summary>
        /// 获取所有的省份
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllProvinceNameDictJson()
        {
            List<CDicKeyValue> treeList = new List<CDicKeyValue>();

            List<ProvinceInfo> provinceList = BLLFactory<Province>.Instance.GetAll();
            foreach (ProvinceInfo info in provinceList)
            {
                CDicKeyValue item = new CDicKeyValue(info.Id, info.ProvinceName);
                treeList.Add(item);
            }
            return ToJsonContent(treeList);
        } 

        /// <summary>
        /// 获取省份名称
        /// </summary>
        /// <param name="id">记录ID</param>
        /// <returns></returns>
        public ActionResult GetName(string id)
        {
            string ProvinceName = baseBLL.GetFieldValue(id, "ProvinceName");
            return ToJsonContent(ProvinceName);
        }
    }
}
