using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data.Common;
using System.Data;
using JCodes.Framework.WebUI.Controllers;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Collections;
using JCodes.Framework.Common.Format;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.BLL;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.WebUI.Controllers
{
    public class FunctionController : BusinessController<Function, FunctionInfo>
    {
        public FunctionController() :base()
        {
        }

        #region 导入Excel数据操作

        //导入或导出的字段列表   
        string columnString = "父ID,功能名称,控制标识,系统编号,排序";

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
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FunctionController));
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

            List<FunctionInfo> list = new List<FunctionInfo>();

            DataTable table = ConvertExcelFileToTable(guid);
            if (table != null)
            {
                #region 数据转换
                foreach (DataRow dr in table.Rows)
                {
                    DateTime dtDefault = Convert.ToDateTime("1900-01-01");
                    FunctionInfo info = new FunctionInfo();

                    info.Pgid = dr["父ID"].ToString();
                    info.Name = dr["功能名称"].ToString();
                    info.DllPath = dr["控制标识"].ToString();
                    info.SystemtypeId = dr["系统编号"].ToString();
                    info.Seq = dr["排序"].ToString();

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
        public ActionResult SaveExcelData(List<FunctionInfo> list)
        {
            ReturnResult result = new ReturnResult();
            if (list != null && list.Count > 0)
            {
                #region 采用事务进行数据提交

                DbTransaction trans = BLLFactory<Function>.Instance.CreateTransaction();
                if (trans != null)
                {
                    try
                    {
                        //int seq = 1;
                        foreach (FunctionInfo detail in list)
                        {
                            //detail.Seq = seq++;//增加1

                            BLLFactory<Function>.Instance.Insert(detail, trans);
                        }
                        trans.Commit();
                        result.ErrorCode = 0;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FunctionController));
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
            List<FunctionInfo> list = new List<FunctionInfo>();

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
                dr["父ID"] = list[i].Pgid;
                dr["功能名称"] = list[i].Name;
                dr["控制标识"] = list[i].DllPath;
                dr["系统编号"] = list[i].SystemtypeId;
                dr["排序"] = list[i].Seq;
                //如果为外键，可以在这里进行转义，如下例子
                //dr["客户名称"] = BLLFactory<Customer>.Instance.GetCustomerName(list[i].Customer_ID);//转义为客户名称

                datatable.Rows.Add(dr);
            }
            #endregion

            #region 把DataTable转换为Excel并输出

            //根据用户创建目录，确保生成的文件不会产生冲突
            string filePath = string.Format("/GenerateFiles/{0}/Function.xls", CurrentUser.Name);
            GenerateExcel(datatable, filePath);

            #endregion

            //返回生成后的文件路径，让客户端根据地址下载
            return Content(filePath);
        }

        #endregion

        #region 写入数据前修改部分属性
        protected override void OnBeforeInsert(FunctionInfo info)
        {
            //info.ID = string.IsNullOrEmpty(info.ID) ? Guid.NewGuid().ToString() : info.ID;

            //子类对参数对象进行修改
            //info.CreateTime = DateTime.Now;
            //info.Creator = CurrentUser.ID.ToString();
            //info.Company_ID = CurrentUser.Company_ID;
            //info.Dept_ID = CurrentUser.Dept_ID;
        }

        protected override void OnBeforeUpdate(FunctionInfo info)
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
            List<FunctionInfo> list = baseBLL.FindWithPager(where, pagerInfo);

            //如果需要修改字段显示，则参考下面代码处理
            //foreach(FunctionInfo info in list)
            //{
            //    info.PID = BLLFactory<Function>.Instance.GetFieldValue(info.PID, "Name");
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


        protected override void ConvertAuthorizedInfo()
        {
            //屏蔽基类调用

            //base.ConvertAuthorizedInfo();
        }

        public ActionResult GetTreeList()
        {
            List<FunctionInfo> comboList = BLLFactory<Function>.Instance.GetAll();
            comboList = CollectionHelper<FunctionInfo>.Fill("-1", 0, comboList, "PID", "ID", "Name");
            return Json(comboList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取指定角色的功能集合
        /// </summary>
        /// <param name="roleid">角色ID</param>
        /// <returns></returns>
        public ActionResult GetFunctions(Int32 roleId)
        {
            ActionResult result = Content("");
            if (roleId > 0)
            {
                List<FunctionInfo> roleList = BLLFactory<Function>.Instance.GetFunctionsByRoleId(roleId);
                result = Json(roleList, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        /// <summary>
        /// 新增和编辑同时需要修改的内容
        /// </summary>
        /// <param name="info"></param>
        private void SetCommonInfo(FunctionInfo info)
        {
            //info.Editor = CurrentUser.LoginName;
            //info.Editor_ID = CurrentUser.ID.ToString();
            //info.EditTime = DateTime.Now;
        }
        public override ActionResult Insert(FunctionInfo info)
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.InsertKey);

            ReturnResult result = new ReturnResult();
            if (info != null)
            {
                try
                {
                    string filter = string.Format("ControlID='{0}' and SystemtypeId='{1}' ", info.DllPath, info.SystemtypeId);
                    bool isExist = BLLFactory<Function>.Instance.IsExistRecord(filter);
                    if (isExist)
                    {
                        result.ErrorMessage = "指定功能控制ID重复，请重新输入！";
                    }
                    else
                    {
                        //info.CreateTime = DateTime.Now;
                        //info.Creator = CurrentUser.LoginName;
                        //info.Creator_ID = CurrentUser.ID.ToString();
                        SetCommonInfo(info);

                        result.ErrorCode = baseBLL.Insert(info)?0:1;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FunctionController));
                    result.ErrorMessage = ex.Message;
                }
            }
            return ToJsonContent(result);
        }

        public override ActionResult Insert2(FunctionInfo info)
        {
            string filter = string.Format("ControlID='{0}' and SystemtypeId='{1}' ", info.DllPath, info.SystemtypeId);
            bool isExist = BLLFactory<Function>.Instance.IsExistRecord(filter);
            if (isExist)
            {
                throw new ArgumentException("指定功能控制ID重复，请重新输入！");
            }

            //info.CreateTime = DateTime.Now;
            //info.Creator = CurrentUser.LoginName;
            //info.Creator_ID = CurrentUser.ID.ToString();
            SetCommonInfo(info);

            return base.Insert2(info);
        }

        /// <summary>
        /// 重写方便写入公司、部门、编辑时间的名称等信息
        /// </summary>
        /// <param name="id">对象ID</param>
        /// <param name="info">对象信息</param>
        /// <returns></returns>
        protected override bool Update(string id, FunctionInfo info)
        {
            string filter = string.Format("ControlID='{0}' and SystemtypeId='{1}' and Gid <>'{2}'",
                info.DllPath, info.SystemtypeId, info.Gid);
            bool isExist = BLLFactory<Function>.Instance.IsExistRecord(filter);
            if (isExist)
            {
                throw new ArgumentException("指定功能控制ID重复，请重新输入！");
            }

            SetCommonInfo(info);

            return base.Update(id, info);
        }

        /// <summary>
        /// 批量添加功能操作
        /// </summary>
        /// <param name="mainInfo">主功能信息</param>
        /// <param name="controlString">附加的操作功能列表，以逗号分开多个，如：add,delete,edit,view,export,import</param>
        /// <returns></returns>
        public ActionResult BatchAddFunction(FunctionInfo mainInfo, string controlString)
        {
            List<string> controlList = new List<string>();
            if(!string.IsNullOrWhiteSpace(controlString))
            {
                foreach(string item in controlString.ToLower().Split(','))
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        controlList.Add(item);
                    }
                }
            }

            ReturnResult result = new ReturnResult();
            using (DbTransaction trans = BLLFactory<Function>.Instance.CreateTransaction())
            {
                try
                {
                    if (trans != null)
                    {
                        bool sucess = BLLFactory<Function>.Instance.Insert(mainInfo, trans);
                        if (sucess)
                        {
                            FunctionInfo subInfo = null;
                            int seqIndex = 1;

                            #region 子功能操作
                            if (controlList.Contains("add"))
                            {
                                subInfo = CreateSubFunction(mainInfo);
                                subInfo.Seq = (seqIndex++).ToString("D2");
                                //subInfo.FunctionId = string.Format("{0}/Add", mainInfo.FunctionId);
                                subInfo.Name = string.Format("添加{0}", mainInfo.Name);

                                BLLFactory<Function>.Instance.Insert(subInfo, trans);
                            }
                            if (controlList.Contains("delete"))
                            {
                                subInfo = CreateSubFunction(mainInfo);
                                subInfo.Seq = (seqIndex++).ToString("D2");
                                //subInfo.FunctionId = string.Format("{0}/Delete", mainInfo.FunctionId);
                                subInfo.Name = string.Format("删除{0}", mainInfo.Name);
                                BLLFactory<Function>.Instance.Insert(subInfo, trans);
                            }
                            if (controlList.Contains("edit") || controlList.Contains("modify"))
                            {
                                subInfo = CreateSubFunction(mainInfo);
                                subInfo.Seq = (seqIndex++).ToString("D2");
                                //subInfo.FunctionId = string.Format("{0}/Edit", mainInfo.FunctionId);
                                subInfo.Name = string.Format("修改{0}", mainInfo.Name);
                                BLLFactory<Function>.Instance.Insert(subInfo, trans);
                            }
                            if (controlList.Contains("view"))
                            {
                                subInfo = CreateSubFunction(mainInfo);
                                subInfo.Seq = (seqIndex++).ToString("D2");
                                //subInfo.FunctionId = string.Format("{0}/View", mainInfo.FunctionId);
                                subInfo.Name = string.Format("查看{0}", mainInfo.Name);
                                BLLFactory<Function>.Instance.Insert(subInfo, trans);
                            }
                            if (controlList.Contains("import"))
                            {
                                subInfo = CreateSubFunction(mainInfo);
                                subInfo.Seq = (seqIndex++).ToString("D2");
                                //subInfo.FunctionId = string.Format("{0}/Import", mainInfo.FunctionId);
                                subInfo.Name = string.Format("导入{0}", mainInfo.Name);
                                BLLFactory<Function>.Instance.Insert(subInfo, trans);
                            }
                            if (controlList.Contains("export"))
                            {
                                subInfo = CreateSubFunction(mainInfo);
                                subInfo.Seq = (seqIndex++).ToString("D2");
                                //subInfo.FunctionId = string.Format("{0}/Export", mainInfo.FunctionId);
                                subInfo.Name = string.Format("导出{0}", mainInfo.Name);
                                BLLFactory<Function>.Instance.Insert(subInfo, trans);
                            }
                            #endregion

                            trans.Commit();
                            result.ErrorCode = 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (trans != null)
                    {
                        trans.Rollback();
                    }
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FunctionController));
                    result.ErrorMessage = ex.Message;
                }
            }
            return ToJsonContent(result);
        }

        private FunctionInfo CreateSubFunction(FunctionInfo mainInfo)
        {
            FunctionInfo subInfo = new FunctionInfo();
            subInfo.Pgid = mainInfo.Pgid;
            subInfo.SystemtypeId = mainInfo.SystemtypeId;
            return subInfo;
        }

        #region Bootstrap的树列表数据

        /// <summary>
        /// 获取所有的功能树Json
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllJsTreeJson()
        {
            List<JsTreeData> treeList = new List<JsTreeData>();
            List<SystemTypeInfo> typeList = BLLFactory<SystemType>.Instance.GetAll();
            foreach (SystemTypeInfo typeInfo in typeList)
            {
                JsTreeData pNode = new JsTreeData(typeInfo.Gid, typeInfo.Name);
                treeList.Add(pNode);

                string systemType = typeInfo.Gid;//系统标识ID
                //绑定树控件
                List<FunctionNodeInfo> functionList = BLLFactory<Function>.Instance.GetTree(systemType);
                foreach (FunctionNodeInfo info in functionList)
                {
                    JsTreeData item = new JsTreeData(info.Gid, info.Name, "fa fa-key icon-state-danger icon-lg");
                    pNode.children.Add(item);

                    AddJsTreeChildNode(info.Children, item);
                }
            }

            return ToJsonContent(treeList);
        }
        private void AddJsTreeChildNode(List<FunctionNodeInfo> list, JsTreeData fnode)
        {
            foreach (FunctionNodeInfo info in list)
            {
                JsTreeData item = new JsTreeData(info.Gid, info.Name, "fa fa-key icon-state-danger icon-lg");
                fnode.children.Add(item);

                AddJsTreeChildNode(info.Children, item);
            }
        }

        /// <summary>
        /// 获取所有的功能树Json(用于树控件选择）
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFunctionDictJson()
        {
            List<CListItem> treeList = new List<CListItem>();
            List<SystemTypeInfo> typeList = BLLFactory<SystemType>.Instance.GetAll();
            foreach (SystemTypeInfo typeInfo in typeList)
            {
                CListItem pNode = new CListItem(typeInfo.Gid, typeInfo.Name);
                treeList.Add(pNode);

                string condition = string.Format("SystemType_ID='{0}'", typeInfo.Gid);//系统标识ID
                List<FunctionInfo> functionList = BLLFactory<Function>.Instance.Find(condition);
                functionList = CollectionHelper<FunctionInfo>.Fill("-1", 0, functionList, "PID", "ID", "Name");
                foreach (FunctionInfo info in functionList)
                {
                    treeList.Add(new CListItem(info.Gid, info.Name));
                }
            }

            return ToJsonContent(treeList);
        }
        private void AddDictChildNode(List<FunctionNodeInfo> list, List<CListItem> treeList)
        {
            foreach (FunctionNodeInfo info in list)
            {
                CListItem item = new CListItem(info.Gid, info.Name);
                treeList.Add(item);

                AddDictChildNode(info.Children, treeList);
            }
        }


        /// <summary>
        /// 获取用户的可操作功能
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ActionResult GetFunctionJsTreeJsonByUser(int userId)
        {
            List<JsTreeData> treeList = new List<JsTreeData>();

            List<SystemTypeInfo> typeList = BLLFactory<SystemType>.Instance.GetAll();
            foreach (SystemTypeInfo typeInfo in typeList)
            {
                JsTreeData parentNode = new JsTreeData(typeInfo.Gid, typeInfo.Name, "fa fa-sitemap  icon-state-warning icon-lg");
                List<FunctionNodeInfo> list = BLLFactory<Function>.Instance.GetFunctionNodesByUser(userId, typeInfo.Gid);
                AddJsTreeeFunctionNode(parentNode, list);

                treeList.Add(parentNode);
            }

            if (treeList.Count == 0)
            {
                treeList.Insert(0, new JsTreeData(-1, "无"));
            }

            return ToJsonContent(treeList);
        }
        private void AddJsTreeeFunctionNode(JsTreeData node, List<FunctionNodeInfo> list)
        {
            foreach (FunctionNodeInfo info in list)
            {
                JsTreeData subNode = new JsTreeData(info.Gid, info.Name, info.Children.Count > 0 ? "fa fa-users icon-state-info icon-lg" : "fa fa-key icon-state-danger icon-lg");
                node.children.Add(subNode);

                AddJsTreeeFunctionNode(subNode, info.Children);
            }
        }

        /// <summary>
        /// 根据用户角色获取其对应的所能访问的权限集合
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <returns></returns>
        public ActionResult GetRoleFunctionJsTreeByUser(int userId)
        {
            List<JsTreeData> treeList = new List<JsTreeData>();

            bool isSuperAdmin = false;
            UserInfo userInfo = BLLFactory<User>.Instance.FindById(userId);
            if (userInfo != null)
            {
                //isSuperAdmin = BLLFactory<User>.Instance.UserInRole(userInfo.Name, RoleInfo.SuperAdminName);
            }

            List<SystemTypeInfo> typeList = BLLFactory<SystemType>.Instance.GetAll();
            foreach (SystemTypeInfo typeInfo in typeList)
            {
                JsTreeData parentNode = new JsTreeData(typeInfo.Gid, typeInfo.Name);

                //如果是超级管理员，不根据角色获取，否则根据角色获取对应的分配权限
                //也就是说，公司管理员只能分配自己被授权的功能，而超级管理员不受限制
                List<FunctionNodeInfo> allNode = new List<FunctionNodeInfo>();
                if (isSuperAdmin)
                {
                    allNode = BLLFactory<Function>.Instance.GetTree(typeInfo.Gid);
                }
                else
                {
                    allNode = BLLFactory<Function>.Instance.GetFunctionNodesByUser(userId, typeInfo.Gid);
                }

                AddJsTreeeFunctionNode(parentNode, allNode);
                treeList.Add(parentNode);
            }

            if (treeList.Count == 0)
            {
                treeList.Insert(0, new JsTreeData(-1, "无"));
            }

            return ToJsonContent(treeList);
        } 

        #endregion

    }
}
