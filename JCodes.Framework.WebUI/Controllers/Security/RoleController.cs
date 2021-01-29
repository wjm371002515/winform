//using JCodes.Framework.BLL;
//using JCodes.Framework.Common.Format;
//using JCodes.Framework.Common.Framework;
//using JCodes.Framework.Entity;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Common;
//using System.Text;
//using System.Web.Mvc;
//using JCodes.Framework.Common.Extension;
//using JCodes.Framework.Common.Databases;
//using JCodes.Framework.Common;
//using JCodes.Framework.jCodesenum.BaseEnum;
//using JCodes.Framework.jCodesenum;
//using JCodes.Framework.WebUI.Common;

//namespace JCodes.Framework.WebUI.Controllers
//{
//    /// <summary>
//    /// 角色业务操作控制器
//    /// </summary>
//    public class RoleController : BusinessController<Role, RoleInfo>
//    {       
//        public RoleController() : base()
//        {
//        }

//        #region 导入Excel数据操作

//        //导入或导出的字段列表   
//        string columnString = "父ID,角色编码,角色名称,备注,排序,角色分类";

//        /// <summary>
//        /// 检查Excel文件的字段是否包含了必须的字段
//        /// </summary>
//        /// <param name="guid">附件的GUID</param>
//        /// <returns></returns>
//        public ActionResult CheckExcelColumns(string guid)
//        {
//            ReturnResult result = new ReturnResult();

//            try
//            {
//                DataTable dt = ConvertExcelFileToTable(guid);
//                if (dt != null)
//                {
//                    //检查列表是否包含必须的字段
//                    result.ErrorCode = DataTableHelper.ContainAllColumns(dt, columnString)?0:1;
//                }
//            }
//            catch (Exception ex)
//            {
//                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(RoleController));
//                result.ErrorMessage = ex.Message;
//            }

//            return ToJsonContent(result);
//        }

//        /// <summary>
//        /// 获取服务器上的Excel文件，并把它转换为实体列表返回给客户端
//        /// </summary>
//        /// <param name="guid">附件的GUID</param>
//        /// <returns></returns>
//        public ActionResult GetExcelData(string guid)
//        {
//            if (string.IsNullOrEmpty(guid))
//            {
//                return null;
//            }

//            List<RoleInfo> list = new List<RoleInfo>();

//            DataTable table = ConvertExcelFileToTable(guid);
//            if (table != null)
//            {
//                #region 数据转换
//                foreach (DataRow dr in table.Rows)
//                {
//                    DateTime dtDefault = Convert.ToDateTime("1900-01-01");
//                    RoleInfo info = new RoleInfo();

//                    //info.Pid = dr["父ID"].ToString().ToInt32();
//                    info.RoleCode = dr["角色编码"].ToString();
//                    info.Name = dr["角色名称"].ToString();
//                    info.Remark = dr["备注"].ToString();
//                    info.Seq = dr["排序"].ToString();
//                    //info.RoleType = Convert.ToInt16( dr["角色分类"]);

//                    info.CompanyId = CurrentUser.CompanyId;
//                    //info.CompanyName = CurrentUser.com;
//                    //info.Creator = CurrentUser.LoginName.ToString();
//                    info.CreatorId = CurrentUser.Id;
//                    info.CreatorTime = DateTime.Now;
//                    //info.Editor = CurrentUser.LoginName.ToString();
//                    info.EditorId = CurrentUser.Id;
//                    info.LastUpdateTime = DateTime.Now;

//                    list.Add(info);
//                }
//                #endregion
//            }

//            var result = new { total = list.Count, rows = list };
//            return ToJsonContentDate(result);
//        }

//        /// <summary>
//        /// 保存客户端上传的相关数据列表
//        /// </summary>
//        /// <param name="list">数据列表</param>
//        /// <returns></returns>
//        public ActionResult SaveExcelData(List<RoleInfo> list)
//        {
//            ReturnResult result = new ReturnResult();
//            if (list != null && list.Count > 0)
//            {
//                #region 采用事务进行数据提交

//                DbTransaction trans = BLLFactory<Role>.Instance.CreateTransaction();
//                if (trans != null)
//                {
//                    try
//                    {
//                        //int seq = 1;
//                        foreach (RoleInfo info in list)
//                        {
//                            //detail.Seq = seq++;//增加1
//                            info.CompanyId = CurrentUser.CompanyId;
//                            //info.CompanyName = CurrentUser.CompanyName;
//                            //info.Creator = CurrentUser.LoginName.ToString();
//                            info.CreatorId = CurrentUser.Id;
//                            info.CreatorTime = DateTime.Now;
//                            //info.Editor = CurrentUser.LoginName.ToString();
//                            info.EditorId = CurrentUser.Id;
//                            info.LastUpdateTime = DateTime.Now;

//                            BLLFactory<Role>.Instance.Insert(info, trans);
//                        }
//                        trans.Commit();
//                        result.ErrorCode = 0;
//                    }
//                    catch (Exception ex)
//                    {
//                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(RoleController));
//                        result.ErrorMessage = ex.Message;
//                        trans.Rollback();
//                    }
//                }
//                #endregion
//            }
//            else
//            {
//                result.ErrorMessage = "导入信息不能为空";
//            }

//            return ToJsonContent(result);
//        }

//        /// <summary>
//        /// 根据查询条件导出列表数据
//        /// </summary>
//        /// <returns></returns>
//        public ActionResult Export()
//        {
//            #region 根据参数获取List列表
//            string where = GetPagerCondition();
//            string CustomedCondition = Request["CustomedCondition"] ?? "";
//            List<RoleInfo> list = new List<RoleInfo>();

//            if (!string.IsNullOrWhiteSpace(CustomedCondition))
//            {
//                //如果为自定义的json参数列表，那么可以使用字典反序列化获取参数，然后处理
//                //Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(CustomedCondition);

//                //如果是条件的自定义，可以使用Find查找
//                list = baseBLL.Find(CustomedCondition);
//            }
//            else
//            {
//                list = baseBLL.Find(where);
//            }

//            #endregion

//            #region 把列表转换为DataTable
//            DataTable datatable = DataTableHelper.CreateTable("序号|int," + columnString);
//            DataRow dr;
//            int j = 1;
//            for (int i = 0; i < list.Count; i++)
//            {
//                dr = datatable.NewRow();
//                dr["序号"] = j++;
//                //dr["父ID"] = list[i].Pid;
//                dr["角色编码"] = list[i].RoleCode;
//                dr["角色名称"] = list[i].Name;
//                dr["备注"] = list[i].Remark;
//                dr["排序"] = list[i].Seq;
//                //dr["角色分类"] = list[i].RoleType;

//                //如果为外键，可以在这里进行转义，如下例子
//                //dr["客户名称"] = BLLFactory<Customer>.Instance.GetCustomerName(list[i].Customer_ID);//转义为客户名称

//                datatable.Rows.Add(dr);
//            }
//            #endregion

//            #region 把DataTable转换为Excel并输出

//            //根据用户创建目录，确保生成的文件不会产生冲突
//            string filePath = string.Format("/GenerateFiles/{0}/Role.xls", CurrentUser.Name);
//            GenerateExcel(datatable, filePath);

//            #endregion

//            //返回生成后的文件路径，让客户端根据地址下载
//            return Content(filePath);
//        }

//        #endregion

//        #region 写入数据前修改部分属性
//        protected override void OnBeforeInsert(RoleInfo info)
//        {
//            //info.ID = string.IsNullOrEmpty(info.ID) ? Guid.NewGuid().ToString() : info.ID;

//            //子类对参数对象进行修改
//            //info.Company_ID = CurrentUser.Company_ID;
//            //info.CompanyName = CurrentUser.CompanyName;
//            //info.Creator = CurrentUser.LoginName.ToString();
//            info.CreatorId = CurrentUser.Id;
//            info.CreatorTime = DateTime.Now;
//            //info.Editor = CurrentUser.LoginName.ToString();
//            info.EditorId = CurrentUser.Id;
//            info.LastUpdateTime = DateTime.Now;
//        }

//        protected override void OnBeforeUpdate(RoleInfo info)
//        {
//            //子类对参数对象进行修改
//            //info.Company_ID = CurrentUser.Company_ID;
//            //info.CompanyName = CurrentUser.CompanyName;
//            //info.Creator = CurrentUser.LoginName.ToString();
//            //info.Creator_ID = CurrentUser.ID.ToString();
//            //info.CreateTime = DateTime.Now;
//            //info.Editor = CurrentUser.LoginName.ToString();
//            info.EditorId = CurrentUser.Id;
//            info.LastUpdateTime = DateTime.Now;
//        }
//        #endregion

//        public override ActionResult FindWithPager()
//        {
//            //检查用户是否有权限，否则抛出MyDenyAccessException异常
//            base.CheckAuthorized(authorizeKeyInfo.ListKey);

//            string where = GetPagerCondition();
//            PagerInfo pagerInfo = GetPagerInfo();
//            List<RoleInfo> list = baseBLL.FindWithPager(where, pagerInfo);

//            //如果需要修改字段显示，则参考下面代码处理
//            //foreach(RoleInfo info in list)
//            //{
//            //    info.PID = BLLFactory<Role>.Instance.GetFieldValue(info.PID, "Name");
//            //    if (!string.IsNullOrEmpty(info.Creator))
//            //    {
//            //        info.Creator = BLLFactory<User>.Instance.GetNameById(info.Creator.ToInt32());
//            //    }
//            //}

//            //Json格式的要求{total:22,rows:{}}
//            //构造成Json的格式传递
//            var result = new { total = pagerInfo.RecordCount, rows = list };
//            return ToJsonContentDate(result);
//        }


//        /// <summary>
//        /// 获取角色分类：系统角色、业务角色、应用角色...
//        /// </summary>
//        /// <returns></returns>
//        public ActionResult GetRoleCategorys()
//        {
//            List<CListItem> listItem = new List<CListItem>();
//            string[] enumNames = EnumHelper.GetMemberNames<RoleType>();

//            foreach (string item in enumNames)
//            {
//                listItem.Add(new CListItem(item, item));
//            }
//            return Json(listItem, JsonRequestBehavior.AllowGet);
//        }

//        public ActionResult EditUsers(string roleId, string newList)
//        {
//            ReturnResult result = new ReturnResult();
//            if (!string.IsNullOrEmpty(roleId) && ValidateUtil.IsValidInt(roleId))
//            {
//                if (!string.IsNullOrWhiteSpace(newList))
//                {
//                    List<int> list = new List<int>();
//                    foreach (string id in newList.Split(','))
//                    {
//                        list.Add(id.ToInt32());
//                    }

//                    result.ErrorCode = BLLFactory<Role>.Instance.EditRoleUsers(roleId.ToInt32(), list)?0:1;
//                }
//            }
//            else
//            {
//                result.ErrorMessage = "参数roleId不正确";
//            }
//            return ToJsonContent(result);
//        }

//        public ActionResult EditOUs(string roleId, string newList)
//        {
//            ReturnResult result = new ReturnResult();
//            if (!string.IsNullOrEmpty(roleId) && ValidateUtil.IsValidInt(roleId))
//            {
//                if (!string.IsNullOrWhiteSpace(newList))
//                {
//                    List<int> list = new List<int>();
//                    foreach (string id in newList.Split(','))
//                    {
//                        list.Add(id.ToInt32());
//                    }

//                    result.ErrorCode = BLLFactory<Role>.Instance.EditRoleOUs(roleId.ToInt32(), list)?0:1;
//                }
//            }
//            else
//            {
//                result.ErrorMessage = "参数roleId不正确";
//            }
//            return ToJsonContent(result);
//        }
//        public ActionResult EditFunctions(string roleId, string newList)
//        {
//            ReturnResult result = new ReturnResult();
//            if (!string.IsNullOrEmpty(roleId) && ValidateUtil.IsValidInt(roleId))
//            {
//                if (!string.IsNullOrWhiteSpace(newList))
//                {
//                    List<string> list = new List<string>();
//                    foreach (string id in newList.Split(','))
//                    {
//                        list.Add(id);
//                    }

//                    result.ErrorCode = BLLFactory<Role>.Instance.EditRoleFunctions(roleId.ToInt32(), list)?0:1;
//                }
//            }
//            else
//            {
//                result.ErrorMessage = "参数roleId不正确";
//            }
//            return ToJsonContent(result);
//        }

//        public ActionResult EditUserRelation(string roleId, string addList, string removeList)
//        {
//            ReturnResult result = new ReturnResult();
//            if (!string.IsNullOrEmpty(roleId) && ValidateUtil.IsValidInt(roleId))
//            {
//                if (!string.IsNullOrWhiteSpace(removeList))
//                {
//                    foreach (string id in removeList.Split(','))
//                    {
//                        if (!string.IsNullOrEmpty(id) && ValidateUtil.IsValidInt(id))
//                        {
//                            BLLFactory<Role>.Instance.RemoveUser(Convert.ToInt32(id), Convert.ToInt32(roleId));
//                        }
//                    }
//                }
//                if (!string.IsNullOrWhiteSpace(addList))
//                {
//                    foreach (string id in addList.Split(','))
//                    {
//                        if (!string.IsNullOrEmpty(id) && ValidateUtil.IsValidInt(id))
//                        {
//                            BLLFactory<Role>.Instance.AddUser(Convert.ToInt32(id), Convert.ToInt32(roleId));
//                        }
//                    }
//                }
//                result.ErrorCode = 0;
//            }
//            else
//            {
//                result.ErrorMessage = "参数roleId不正确";
//            }
//            return ToJsonContent(result);
//        }

//        public ActionResult EditOURelation(string roleId, string addList, string removeList)
//        {
//            ReturnResult result = new ReturnResult();
//            if (!string.IsNullOrEmpty(roleId) && ValidateUtil.IsValidInt(roleId))
//            {
//                if (!string.IsNullOrWhiteSpace(removeList))
//                {
//                    foreach (string id in removeList.Split(','))
//                    {
//                        if (!string.IsNullOrEmpty(id) && ValidateUtil.IsValidInt(id))
//                        {
//                            BLLFactory<Role>.Instance.RemoveOU(Convert.ToInt32(id), Convert.ToInt32(roleId));
//                        }
//                    }
//                }
//                if (!string.IsNullOrWhiteSpace(addList))
//                {
//                    foreach (string id in addList.Split(','))
//                    {
//                        if (!string.IsNullOrEmpty(id) && ValidateUtil.IsValidInt(id))
//                        {
//                            BLLFactory<Role>.Instance.AddOU(Convert.ToInt32(id), Convert.ToInt32(roleId));
//                        }
//                    }
//                }
//                result.ErrorCode = 0;
//            }
//            else
//            {
//                result.ErrorMessage = "参数roleId不正确";
//            }
//            return ToJsonContent(result);
//        }

//        public ActionResult EditFunctionRelation(string roleId, string addList, string removeList)
//        {
//            ReturnResult result = new ReturnResult();
//            if (!string.IsNullOrEmpty(roleId) && ValidateUtil.IsValidInt(roleId))
//            {
//                if (!string.IsNullOrWhiteSpace(removeList))
//                {
//                    foreach (string id in removeList.Split(','))
//                    {
//                        if (!string.IsNullOrEmpty(id))
//                        {
//                            BLLFactory<Role>.Instance.RemoveFunction(id, Convert.ToInt32(roleId));
//                        }
//                    }
//                }

//                if (!string.IsNullOrWhiteSpace(addList))
//                {
//                    foreach (string id in addList.Split(','))
//                    {
//                        if (!string.IsNullOrEmpty(id))
//                        {
//                            BLLFactory<Role>.Instance.AddFunction(id, Convert.ToInt32(roleId));
//                        }
//                    }
//                } 
//                result.ErrorCode = 0;
//            }
//            else
//            {
//                result.ErrorMessage = "参数roleId不正确";
//            }
//            return ToJsonContent(result);
//        }

//        public ActionResult GetRolesByUser(string userid)
//        {
//            if (!string.IsNullOrEmpty(userid) && ValidateUtil.IsValidInt(userid))
//            {
//                List<RoleInfo> roleList = BLLFactory<Role>.Instance.GetRolesByUser(Convert.ToInt32(userid));
//                return Json(roleList, JsonRequestBehavior.AllowGet);
//            }

//            return Content("");
//        }

//        public ActionResult GetRolesByFunction(string functionId)
//        {
//            if (!string.IsNullOrEmpty(functionId))
//            {
//                List<RoleInfo> roleList = BLLFactory<Role>.Instance.GetRolesByFunction(functionId);
//                return Json(roleList, JsonRequestBehavior.AllowGet);
//            }
//            return Content("");
//        }

//        public ActionResult GetRolesByOU(string ouid)
//        {
//            if (!string.IsNullOrEmpty(ouid) && ValidateUtil.IsValidInt(ouid))
//            {
//                List<RoleInfo> roleList = BLLFactory<Role>.Instance.GetRolesByOU(Convert.ToInt32(ouid));
//                return Json(roleList, JsonRequestBehavior.AllowGet);
//            }
//            return Content("");
//        }

//        /// <summary>
//        /// 新增和编辑同时需要修改的内容
//        /// </summary>
//        /// <param name="info"></param>
//        private void SetCommonInfo(RoleInfo info)
//        {
//            //info.Editor = CurrentUser.LoginName;
//            info.EditorId = CurrentUser.Id;
//            info.LastUpdateTime = DateTime.Now;

//            OUInfo companyInfo = BLLFactory<OU>.Instance.FindById(info.CompanyId);
//            if (companyInfo != null)
//            {
//                info.CompanyName = companyInfo.Name;
//            }
//        }
//        public override ActionResult Insert(RoleInfo info)
//        {
//            //检查用户是否有权限，否则抛出MyDenyAccessException异常
//            base.CheckAuthorized(authorizeKeyInfo.InsertKey);

//            ReturnResult result = new ReturnResult();
//            if (info != null)
//            {
//                string filter = string.Format("Name='{0}'  and Company_ID={1}", info.Name, info.CompanyId);
//                bool isExist = BLLFactory<Role>.Instance.IsExistRecord(filter);
//                if (isExist)
//                {
//                    result.ErrorMessage = "指定角色名称重复，请重新输入！";
//                }
//                else
//                {
//                    try
//                    {
//                        info.CreatorTime = DateTime.Now;
//                        //info.Creator = CurrentUser.LoginName;
//                        info.CreatorId = CurrentUser.Id;
//                        SetCommonInfo(info);

//                        result.ErrorCode = baseBLL.Insert(info)?0:1;
//                    }
//                    catch (Exception ex)
//                    {
//                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(RoleController));
//                        result.ErrorMessage = ex.Message;
//                    }
//                }
//            }

//            return ToJsonContent(result);
//        }

//        public override ActionResult Insert2(RoleInfo info)
//        {
//            //检查用户是否有权限，否则抛出MyDenyAccessException异常
//            base.CheckAuthorized(authorizeKeyInfo.InsertKey);

//            int result = -1;
//            if (info != null)
//            {
//                string filter = string.Format("Name='{0}' and Company_ID={1}", info.Name, info.CompanyId);
//                bool isExist = BLLFactory<Role>.Instance.IsExistRecord(filter);
//                if (isExist)
//                {
//                    throw new ArgumentException("指定角色名称重复，请重新输入！");
//                }

//                info.CreatorTime = DateTime.Now;
//                ///info.Creator = CurrentUser.LoginName;
//                info.CreatorId = CurrentUser.Id;
//                SetCommonInfo(info);
//                result = baseBLL.Insert2(info);
//            }
//            return Content(result);
//        }

//        /// <summary>
//        /// 重写方便写入公司、部门、编辑时间的名称等信息
//        /// </summary>
//        /// <param name="id">对象ID</param>
//        /// <param name="info">对象信息</param>
//        /// <returns></returns>
//        protected override bool Update(string id, RoleInfo info)
//        {
//            string filter = string.Format("Name='{0}' and ID <>'{1}' and CompanyId={2}", info.Name, info.Id, info.CompanyId);
//            bool isExist = BLLFactory<Role>.Instance.IsExistRecord(filter);
//            if (isExist)
//            {
//                throw new ArgumentException("指定角色名称重复，请重新输入！");
//            }

//            SetCommonInfo(info);

//            return base.Update(id, info);
//        }

//        /// <summary>
//        /// 获取用户的部门角色树结构(分级需要）（bootstrap的JSTree)
//        /// </summary>
//        /// <param name="userId">用户ID</param>
//        /// <returns></returns>
//        public ActionResult GetMyRoleJsTreeJson(int userId)
//        {
//            StringBuilder content = new StringBuilder();
//            UserInfo userInfo = BLLFactory<User>.Instance.FindById(userId);
//            if (userInfo != null)
//            {
//                List<OUInfo> list = null;
//                if (Portal.hh.IsSuperAdmin)
//                   list = BLLFactory<OU>.Instance.GetSuperAdminTopGroup(CurrentUser.Id);
//                else
//                   list = BLLFactory<OU>.Instance.GetMyTopGroup(CurrentUser.Id);
//                foreach (OUInfo groupInfo in list)
//                {
//                    if (groupInfo != null && groupInfo.IsDelete == 0)
//                    {
//                        //JsTreeData topnode = new JsTreeData("dept" + groupInfo.Id, groupInfo.Name, GetBootstrapIcon(groupInfo.OuType));
//                        JsTreeData topnode = new JsTreeData("dept" + groupInfo.Id, groupInfo.Name);
//                        AddJsRole(groupInfo, topnode);

//                        if (groupInfo.OuType == 0)
//                        {
//                            List<OUInfo> sublist = BLLFactory<OU>.Instance.GetAllCompany(groupInfo.Id);
//                            foreach (OUInfo info in sublist)
//                            {
//                                if (info.IsDelete == 0)
//                                {
//                                    //JsTreeData companyNode = new JsTreeData("dept" + info.Id, info.Name, GetBootstrapIcon(info.OuType));
//                                    JsTreeData companyNode = new JsTreeData("dept" + info.Id, info.Name);
//                                    topnode.children.Add(companyNode);

//                                    AddJsRole(info, companyNode);
//                                }
//                            }
//                        }

//                        content.Append(base.ToJson(topnode));
//                    }
//                }
//            }

//            string json = string.Format("[{0}]", content.ToString().Trim(','));
//            return Content(json);
//        }

//        private void AddJsRole(OUInfo ouInfo, JsTreeData treeNode)
//        {
//            List<RoleInfo> roleList = BLLFactory<Role>.Instance.GetRolesByCompanyId(ouInfo.Id);
//            foreach (RoleInfo roleInfo in roleList)
//            {
//                JsTreeData roleNode = new JsTreeData("role" + roleInfo.Id, roleInfo.Name, "fa fa-user icon-state-info icon-lg");
//                treeNode.children.Add(roleNode);
//            }
//        }
//    }
//}
