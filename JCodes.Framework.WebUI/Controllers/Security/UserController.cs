using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Collections;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Format;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Office;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web.Mvc;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.Common.Network;
using JCodes.Framework.Common.Device;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.WebUI.Controllers
{
    public class UserController : BusinessController<User, UserInfo>
    {
        public UserController() : base()
        {
        }

        #region 导入Excel数据操作

        //导入或导出的字段列表   
        string columnString = "用户编码,用户名/登录名,真实姓名,职务头衔,移动电话,办公电话,邮件地址,性别,QQ号码,备注";

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
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(UserController));
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

            List<UserInfo> list = new List<UserInfo>();

            DataTable table = ConvertExcelFileToTable(guid);
            if (table != null)
            {
                #region 数据转换
                foreach (DataRow dr in table.Rows)
                {
                    DateTime dtDefault = Convert.ToDateTime("1900-01-01");
                    UserInfo info = new UserInfo();

                    //用户编码,用户名/登录名,真实姓名,职务头衔,移动电话,办公电话,邮件地址,性别,QQ号码,备注
                    info.UserCode = dr["用户编码"].ToString();
                    info.Name = dr["用户名/登录名"].ToString();
                    info.FullName = dr["真实姓名"].ToString();
                    info.MobilePhone = dr["移动电话"].ToString();
                    info.OfficePhone = dr["办公电话"].ToString();
                    info.Email = dr["邮件地址"].ToString();
                    // TODO
                    //info.Gender = dr["性别"].ToString();
                    //info.QQ = dr["QQ号码"].ToString();
                    info.Remark = dr["备注"].ToString();

                    //converted = DateTime.TryParse(dr["出生日期"].ToString(), out dt);
                    //if (converted && dt > dtDefault)
                    //{
                    //    info.BirthDate = dt;
                    //}

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
        /// <param name="companyid">所属公司</param>
        /// <param name="deptid">所属部门</param>
        /// <returns></returns>
        public ActionResult SaveExcelData(List<UserInfo> list, Int32 CompanyId, Int32 DeptId)
        {
            ReturnResult result = new ReturnResult();
            if (list != null && list.Count > 0)
            {
                #region 采用事务进行数据提交

                DbTransaction trans = BLLFactory<User>.Instance.CreateTransaction();
                if (trans != null)
                {
                    try
                    {
                        //int seq = 1;
                        bool isAllImported = true;//是否全部导入
                        foreach (UserInfo detail in list)
                        {
                            string filter = string.Format("Name='{0}' ", detail.Name);
                            bool isExist = BLLFactory<User>.Instance.IsExistRecord(filter);
                            if (!isExist)
                            {
                                detail.CompanyId = CompanyId;
                                //detail.CompanyName = BLLFactory<OU>.Instance.GetName(companyid.ToInt32());
                                detail.DeptId = DeptId;
                                //detail.DeptName = BLLFactory<OU>.Instance.GetName(deptid.ToInt32());

                                //detail.Seq = seq++;//增加1
                                detail.CreatorId = CurrentUser.Id;
                                detail.CreatorTime = DateTime.Now;
                                detail.EditorId = CurrentUser.Id;
                                detail.LastUpdateTime = DateTime.Now;

                                BLLFactory<User>.Instance.Insert(detail, trans);
                            }
                            else
                            {
                                isAllImported = false;//有任意一个错误则非全部
                                result.ErrorMessage = "记录有重名冲突，无法全部导入！";
                            }
                        }
                        trans.Commit();
                        result.ErrorCode = isAllImported?0:1;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(UserController));
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
            List<UserInfo> list = new List<UserInfo>();

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
                //用户编码,用户名/登录名,真实姓名,职务头衔,移动电话,办公电话,邮件地址,性别,QQ号码,备注
                dr = datatable.NewRow();
                dr["序号"] = j++;
                dr["用户编码"] = list[i].UserCode;
                dr["用户名/登录名"] = list[i].Name;
                dr["真实姓名"] = list[i].FullName;
                dr["移动电话"] = list[i].MobilePhone;
                dr["办公电话"] = list[i].OfficePhone;
                dr["邮件地址"] = list[i].Email;
                dr["性别"] = list[i].Gender;
                dr["QQ号码"] = list[i].QQ;
                dr["备注"] = list[i].Remark;

                //如果为外键，可以在这里进行转义，如下例子
                //dr["客户名称"] = BLLFactory<Customer>.Instance.GetCustomerName(list[i].Customer_ID);//转义为客户名称

                datatable.Rows.Add(dr);
            }
            #endregion

            #region 把DataTable转换为Excel并输出

            //根据用户创建目录，确保生成的文件不会产生冲突
            string filePath = string.Format("/GenerateFiles/{0}/User.xls", CurrentUser.Name);
            GenerateExcel(datatable, filePath);

            #endregion

            //返回生成后的文件路径，让客户端根据地址下载
            return Content(filePath);
        }

        #endregion

        #region 写入数据前修改部分属性
        protected override void OnBeforeInsert(UserInfo info)
        {
            //留给子类对参数对象进行修改
            info.CreatorTime = DateTime.Now;
            info.CreatorId = CurrentUser.Id;
            info.CompanyId = CurrentUser.CompanyId;
            info.DeptId = CurrentUser.DeptId;
        }

        protected override void OnBeforeUpdate(UserInfo info)
        {
            //留给子类对参数对象进行修改
            info.EditorId = CurrentUser.Id;
            info.LastUpdateTime = DateTime.Now;
        }
        #endregion

        public virtual ActionResult DeletedList()
        {
            return View("DeletedList");
        }

        public virtual ActionResult UserDetail()
        {
            return View("UserDetail");
        }

        /// <summary>
        /// 删除多个ID的记录(彻底删除)
        /// </summary>
        /// <param name="ids">多个id组合，逗号分开（1,2,3,4,5）</param>
        /// <returns></returns>
        public virtual ActionResult ConfirmDeleteByIds(string ids)
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.DeleteKey);

            ReturnResult result = new ReturnResult();
            try
            {
                if (!string.IsNullOrEmpty(ids))
                {
                    string condition = string.Format("ID in ({0}) ", ids);
                    result.ErrorCode = baseBLL.DeleteByCondition(condition)?0:1;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(UserController));
                result.ErrorMessage = ex.Message;
            }
            return ToJsonContent(result);
        } 

        /// <summary>
        /// 重置用户密码为12345678
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        public ActionResult ResetPassword(string id)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    result.ErrorMessage = "用户id不能为空";
                }
                else
                {
                    UserInfo info = BLLFactory<User>.Instance.FindByID(id);
                    if (info != null)
                    {
                        string defaultPassword = Const.defaultPwd;
                        string ip = NetworkUtil.GetLocalIP();
                        string macAddr = HardwareInfoHelper.GetMacAddress();
                        bool tempBool = BLLFactory<User>.Instance.ModifyPassword(info.Name, defaultPassword, Const.SystemTypeID, ip, macAddr);
                        if (tempBool)
                        {
                            result.ErrorCode = 0;
                        }
                        else
                        {
                            result.ErrorMessage = "口令初始化失败";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(UserController));
                result.ErrorMessage = ex.Message;
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="name">用户名称</param>
        /// <param name="oldpass">旧密码</param>
        /// <param name="newpass">修改密码</param>
        /// <returns></returns>
        public ActionResult ModifyPass(string name, string oldpass, string newpass)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                // TODO 这里要修改为Request IP
                string ip = NetworkUtil.GetLocalIP();
                string macAddr = HardwareInfoHelper.GetMacAddress();
                string identity = BLLFactory<User>.Instance.VerifyUser(name, oldpass, Const.SystemTypeID, ip, macAddr);
                if (string.IsNullOrEmpty(identity))
                {
                    result.ErrorMessage = "原口令错误";
                }
                else
                {
                    bool tempBool = BLLFactory<User>.Instance.ModifyPassword(name, newpass, Const.SystemTypeID, ip, macAddr);
                    if (tempBool)
                    {
                        result.ErrorCode = 0;
                    }
                    else
                    {
                        result.ErrorMessage = "口令修改失败";
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(UserController));
                result.ErrorMessage = ex.Message;
            }
            return ToJsonContent(result);
        }

        #region 基于bootstrap的JsTree的树形列表数据
        /// <summary>
        /// 获取用户的部门树结构(分级需要）（bootstrap的JSTree)
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public ActionResult GetMyDeptJsTreeJson(int userId)
        {
            StringBuilder content = new StringBuilder();
            UserInfo userInfo = BLLFactory<User>.Instance.FindByID(userId);
            if (userInfo != null)
            {
                List<OUInfo> list = BLLFactory<OU>.Instance.GetMyTopGroup(userId);
                foreach (OUInfo groupInfo in list)
                {
                    if (groupInfo != null && groupInfo.IsDelete == 0)
                    {
                        List<OUNodeInfo> sublist = BLLFactory<OU>.Instance.GetTreeByID(groupInfo.Id);

                        //JsTreeData treeData = new JsTreeData(groupInfo.Id, groupInfo.Name, GetBootstrapIcon(groupInfo.OuType));
                        JsTreeData treeData = new JsTreeData(groupInfo.Id, groupInfo.Name);
                        GetJsTreeDataWithOUNode(sublist, treeData);

                        content.Append(base.ToJson(treeData));
                    }
                }
            }
            string json = string.Format("[{0}]", content.ToString().Trim(','));
            return Content(json);
        }
        private void GetJsTreeDataWithOUNode(List<OUNodeInfo> list, JsTreeData parent)
        {
            List<JsTreeData> result = new List<JsTreeData>();
            foreach (OUNodeInfo ouInfo in list)
            {
                // JsTreeData treeData = new JsTreeData(ouInfo.ID, ouInfo.Name, GetBootstrapIcon(ouInfo.Category));
                JsTreeData treeData = new JsTreeData(ouInfo.Id, ouInfo.Name);
                GetJsTreeDataWithOUNode(ouInfo.Children, treeData);

                result.Add(treeData);
            }
            parent.children.AddRange(result);
        }

        /// <summary>
        /// 获取用户的部门树结构(分级需要）（bootstrap的下拉列表)
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public ActionResult GetMyDeptDictJson(int userId)
        {
            List<CDicKeyValue> itemList = new List<CDicKeyValue>();

            StringBuilder content = new StringBuilder();
            UserInfo userInfo = BLLFactory<User>.Instance.FindByID(userId);
            if (userInfo != null)
            {
                List<OUInfo> list = BLLFactory<OU>.Instance.GetMyTopGroup(userId);
                foreach (OUInfo groupInfo in list)
                {
                    if (groupInfo != null && groupInfo.IsDelete == 0)
                    {
                        List<OUInfo> allList = BLLFactory<OU>.Instance.GetAllOUsByParent(groupInfo.Id); 
                        allList.Insert(0, groupInfo);
                        allList = CollectionHelper<OUInfo>.Fill(groupInfo.Id, 0, allList, "Pid", "Id", "Name");

                        itemList.Add(new CDicKeyValue(groupInfo.Id, groupInfo.Name));
                        foreach (OUInfo info in allList)
                        {
                            itemList.Add(new CDicKeyValue(info.Id, info.Name));
                        }
                    }
                }
            }
            return ToJsonContent(itemList);
        }

        private List<OUInfo> GetOuList(List<OUNodeInfo> nodeList)
        {
            List<OUInfo> list = new List<OUInfo>();
            foreach(OUNodeInfo info in nodeList)
            {
                list.Add(info as OUInfo);
                list.AddRange(GetOuList(info.Children));
            }
            return list;
        }

        /// <summary>
        /// 获取用户的公司结构(分级需要）（bootstrap的JSTree)
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public ActionResult GetMyCompanyJsTreeJson(int userId)
        {
            List<JsTreeData> treeList = new List<JsTreeData>();

            UserInfo userInfo = BLLFactory<User>.Instance.FindByID(userId);
            if (userInfo != null)
            {
                List<OUNodeInfo> list = new List<OUNodeInfo>();
                /*if (BLLFactory<User>.Instance.UserInRole(userInfo.Name, RoleInfo.SuperAdminName))
                {
                    list = BLLFactory<OU>.Instance.GetGroupCompanyTree();
                }
                else
                {
                    OUInfo myCompanyInfo = BLLFactory<OU>.Instance.FindByID(userInfo.CompanyId);
                    if (myCompanyInfo != null)
                    {
                        list.Add(new OUNodeInfo(myCompanyInfo));
                    }
                }*/

                if (list.Count > 0)
                {
                    OUNodeInfo info = list[0];//无论是集团还是公司，节点只有一个
                    //JsTreeData node = new JsTreeData(info.ID, info.Name, GetBootstrapIcon(info.Category));
                    JsTreeData node = new JsTreeData(info.Id, info.Name);
                    GetJsTreeDataWithOUNode(info.Children, node);
                    treeList.Add(node);
                }
            }

            return ToJsonContent(treeList);
        }

        /// <summary>
        /// 获取用户的公司结构(分级需要）给Select2控件显示的下拉列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public ActionResult GetMyCompanyDictJson(int userId)
        {
            List<CDicKeyValue> itemList = new List<CDicKeyValue>();

            UserInfo userInfo = BLLFactory<User>.Instance.FindByID(userId);
            if (userInfo != null)
            {
                List<OUNodeInfo> list = new List<OUNodeInfo>();
                /*if (BLLFactory<User>.Instance.UserInRole(userInfo.Name, RoleInfo.SuperAdminName))
                {
                    list = BLLFactory<OU>.Instance.GetGroupCompanyTree();
                }
                else
                {
                    OUInfo myCompanyInfo = BLLFactory<OU>.Instance.FindByID(userInfo.CompanyId);
                    if (myCompanyInfo != null)
                    {
                        list.Add(new OUNodeInfo(myCompanyInfo));
                    }
                }*/

                foreach (OUNodeInfo info in list)
                {
                    itemList.Add(new CDicKeyValue(info.Id, info.Name));
                    foreach(OUNodeInfo subInfo in info.Children)
                    {
                        itemList.Add(new CDicKeyValue(subInfo.Id, "    " + subInfo.Name));
                    }
                }
            }

            return ToJsonContent(itemList);
        } 

        /// <summary>
        /// 根据公司ID获取对应部门的树Json（bootstrap的JSTree)
        /// </summary>
        /// <param name="parentId">父部门ID</param>
        /// <returns></returns>
        public ActionResult GetDeptJsTreeJson(string parentId)
        {
            List<JsTreeData> treeList = new List<JsTreeData>();
            treeList.Insert(0, new JsTreeData(-1, "无"));

            if (!string.IsNullOrEmpty(parentId) && parentId != "null")
            {
                OUInfo groupInfo = BLLFactory<OU>.Instance.FindByID(parentId);
                if (groupInfo != null)
                {
                    List<OUNodeInfo> list = BLLFactory<OU>.Instance.GetTreeByID(groupInfo.Id);

                    JsTreeData treeData = new JsTreeData(groupInfo.Id, groupInfo.Name, "fa fa-users icon-state-warning icon-lg");
                    GetJsTreeDataWithOUNode(list, treeData);

                    treeList.Add(treeData);
                }
            }

            return ToJsonContent(treeList);
        }

        /// <summary>
        /// 根据公司ID获取对应部门, 给Select2控件显示的下拉列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public ActionResult GetDeptDictJson(string parentId)
        {
            List<CDicKeyValue> itemList = new List<CDicKeyValue>();

            if (!string.IsNullOrEmpty(parentId) && parentId != "null")
            {
                OUInfo groupInfo = BLLFactory<OU>.Instance.FindByID(parentId);
                if (groupInfo != null)
                {
                    itemList.Add(new CDicKeyValue(groupInfo.Id, groupInfo.Name));

                    List<OUInfo> list = BLLFactory<OU>.Instance.GetAllOUsByParent(groupInfo.Id);
                    list = CollectionHelper<OUInfo>.Fill(groupInfo.Id, 0, list, "PID", "ID", "Name");
                    foreach (OUInfo info in list)
                    {
                        itemList.Add(new CDicKeyValue(info.Id, info.Name));
                    }
                }
            }

            return ToJsonContent(itemList);
        }
                
        /// <summary>
        /// 根据用户获取对应人员层次的树Json（bootstrap的JSTree)
        /// </summary>
        /// <param name="deptId">用户所在部门</param>
        /// <returns></returns>
        public ActionResult GetUserJsTreeJson(int deptId)
        {
            List<JsTreeData> treeList = new List<JsTreeData>();
            treeList.Insert(0, new JsTreeData(-1, "无"));

            List<UserInfo> list = BLLFactory<User>.Instance.FindByDept(deptId);
            foreach (UserInfo info in list)
            {
                treeList.Add(new JsTreeData(info.Id, info.FullName, "fa fa-user icon-state-warning icon-lg"));
            }

            return ToJsonContent(treeList);
        }

        /// <summary>
        /// 根据用户获取对应人员层次（给Select2控件显示的下拉列表)
        /// </summary>
        /// <param name="deptId">用户所在部门</param>
        /// <returns></returns>
        public ActionResult GetUserDictJson(int deptId)
        {
            List<CDicKeyValue> itemList = new List<CDicKeyValue>();
            itemList.Insert(0, new CDicKeyValue(-1, "无"));

            List<UserInfo> list = BLLFactory<User>.Instance.FindByDept(deptId);
            foreach (UserInfo info in list)
            {
                itemList.Add(new CDicKeyValue(info.Id, info.FullName));
            }

            return ToJsonContent(itemList);
        }

        #endregion
                
        /// <summary>
        /// 根据角色获取对应的用户
        /// </summary>
        /// <param name="roleid">角色ID</param>
        /// <returns></returns>
        public ActionResult GetUsersByRole(string roleid)
        {
            ActionResult result = Content("");
            if (!string.IsNullOrEmpty(roleid) && ValidateUtil.IsValidInt(roleid))
            {
                List<UserInfo> roleList = BLLFactory<User>.Instance.GetUsersByRole(Convert.ToInt32(roleid));
                result = ToJsonContentDate(roleList);
            }
            return result;
        }

        /// <summary>
        /// 根据机构获取对应的用户
        /// </summary>
        /// <param name="ouid">机构ID</param>
        /// <returns></returns>
        public ActionResult GetUsersByOU(string ouid)
        {
            ActionResult result = Content("");
            if (!string.IsNullOrEmpty(ouid) && ValidateUtil.IsValidInt(ouid))
            {
                List<UserInfo> roleList = BLLFactory<User>.Instance.GetUsersByOU(Convert.ToInt32(ouid));
                result = ToJsonContentDate(roleList);
            }
            return result;
        }

        /// <summary>
        /// 获取分页操作的查询条件
        /// </summary>
        /// <returns></returns>
        protected override string GetPagerCondition()
        {
            string condition = "";
            //增加对角色、部门、公司的判断
            string deptId = Request["DeptId"] ?? "";    

            if (!string.IsNullOrEmpty(deptId))
            {
                condition = string.Format("DeptId = {0} or CompanyId ={0}", deptId);
            }
            else
            {
                condition = base.GetPagerCondition();
            }

            return condition;
        }

        /// <summary>
        /// 重写分页操作，对特殊条件进行处理
        /// </summary>
        /// <returns></returns>
        public override ActionResult FindWithPager()
        {
            string roleId = Request["RoldId"] ?? "";
            if (!string.IsNullOrEmpty(roleId))
            {
                //检查用户是否有权限，否则抛出MyDenyAccessException异常
                base.CheckAuthorized(authorizeKeyInfo.ListKey);
                List<UserInfo> list = BLLFactory<User>.Instance.GetUsersByRole(roleId.ToInt32());

                //Json格式的要求{total:22,rows:{}}
                //构造成Json的格式传递
                var result = new { total = list.Count, rows = list };
                return ToJsonContentDate(result);
            }
            else
            {
                return base.FindWithPager();
            }
        }

        public override ActionResult Insert(UserInfo info)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                //检查用户是否有权限，否则抛出MyDenyAccessException异常
                base.CheckAuthorized(authorizeKeyInfo.InsertKey);

                string filter = string.Format("Name='{0}' ", info.Name);
                bool isExist = BLLFactory<User>.Instance.IsExistRecord(filter);
                if (isExist)
                {
                    result.ErrorMessage = "指定用户名重复，请重新输入！";
                }
                else
                {
                    OnBeforeInsert(info);
                    result.ErrorCode = baseBLL.Insert(info)?0:1;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(UserController));
                result.ErrorMessage = ex.Message;
            }
            return ToJsonContent(result);            
        }

        public override ActionResult Insert2(UserInfo info)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                //检查用户是否有权限，否则抛出MyDenyAccessException异常
                base.CheckAuthorized(authorizeKeyInfo.InsertKey);

                string filter = string.Format("Name='{0}' ", info.Name);
                bool isExist = BLLFactory<User>.Instance.IsExistRecord(filter);
                if (isExist)
                {
                    throw new ArgumentException("指定用户名重复，请重新输入！");
                }

                return base.Insert2(info);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(UserController));
                result.ErrorMessage = ex.Message;
            }
            return ToJsonContent(result);   
        }

        /// <summary>
        /// 重写方便写入公司、部门、编辑时间的名称等信息
        /// </summary>
        /// <param name="id">对象主键ID</param>
        /// <param name="info">对象信息</param>
        /// <returns></returns>
        protected override bool Update(string id, UserInfo info)
        {
            string filter = string.Format("Name='{0}' and ID <>'{1}'", info.Name, info.Id);
            bool isExist = BLLFactory<User>.Instance.IsExistRecord(filter);
            if (isExist)
            {
                throw new ArgumentException("指定用户名重复，请重新输入！");
            }

            return base.Update(id, info);
        }

        public ActionResult ChartIndex()
        {
            return View("ChartIndex");
        }

        /// <summary>
        /// 统计各个分子公司的人数，返回Json字符串，供图表统计
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCompanyUserCountJson()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();

            List<OUInfo> ouList = BLLFactory<OU>.Instance.GetTopGroup();
            foreach (OUInfo info in ouList)
            {
                List<OUInfo> companyList = BLLFactory<OU>.Instance.GetAllCompany(info.Id);
                foreach (OUInfo companyInfo in companyList)
                {
                    string condition = string.Format("CompanyId={0} AND IsDelete=0", companyInfo.Id);
                    int count = BLLFactory<User>.Instance.GetRecordCount(condition);
                    if (!dict.ContainsKey(companyInfo.Name))
                    {
                        dict.Add(companyInfo.Name, count);
                    }
                }
            }

            return ToJsonContent(dict);
        }

        /// <summary>
        /// 根据用户的ID，获取用户的全名，并放到缓存里面
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public ActionResult GetFullNameByID(string userId)
        {
            string result = "";
            if (!string.IsNullOrEmpty(userId))
            {
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                //string key = string.Format("{0}-{1}-{2}", method.DeclaringType.FullName, method.Name, userId);
                string key = string.Format("GetFullNameByID-{0}", userId);

                result = MemoryCacheHelper.GetCacheItem<string>(key,
                    delegate() { return BLLFactory<User>.Instance.GetFullNameByID(userId.ToInt32()); },
                    new TimeSpan(0, 30, 0));//30分钟过期
            }
            return ToJsonContent(result); ;
        }

        public ActionResult RDLCReport()
        {
            return View("RDLCReport");
        }

        /// <summary>
        /// 基于RDLC的报表数据操作
        /// </summary>
        /// <param name="format">图片格式</param>
        /// <returns></returns>
        public ActionResult UserRdlcReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Report/JCodes.Framework.WebUI.UserReport.rdlc");
            var dt = baseBLL.GetAll();

            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", dt);
            localReport.DataSources.Add(reportDataSource);

            if(string.IsNullOrEmpty(format))
            {
                format = "Image";
            }

            string reportType = format;
            string deviceType = (format.ToLower() == "image") ? "jpeg" : format;
            string mimeType;
            string encoding;
            string fileNameExtension;

            //The DeviceInfo settings should be changed based on the reportType
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx
            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>" + deviceType + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            //"  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>";

            if(format.ToLower() == "image")
            {
                //deviceInfo += string.Format("<StartPage>{0}</StartPage>", 0);
                //deviceInfo += string.Format("<EndPage>{0}</EndPage>", int.MaxValue);
                double inchValue = (dt.Count / 37.0) * 11; 
                deviceInfo += string.Format("  <PageHeight>{0}in</PageHeight>", inchValue);
            }
            else
            {
                deviceInfo += "  <PageHeight>11in</PageHeight>";
            }

            deviceInfo += "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, (format.ToLower() == "image") ? "image/jpeg" : mimeType);
            //return new ReportsResult(renderedBytes, mimeType);

            //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension);

            //return File(renderedBytes, "pdf");
            //return File(renderedBytes, "image/jpeg");
        }


        /// <summary>
        /// 上传用户头像图片
        /// </summary>
        /// <param name="id">用户的ID</param>
        /// <returns></returns>
        public ActionResult EditPortrait(int id)
        {
            ReturnResult result = new ReturnResult();

            try
            {
                var files = Request.Files;
                if (files != null && files.Count > 0)
                {
                    UserInfo info = BLLFactory<User>.Instance.FindByID(id);
                    if (info != null)
                    {
                        var fileData = ReadFileBytes(files[0]);
                        result.ErrorCode = BLLFactory<User>.Instance.UpdatePersonImageBytes(UserImageType.个人肖像, id, fileData)?0:1;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            return ToJsonContent(result);
        }
        public ActionResult GetPortrait(int id)
        {
            ActionResult result = Content("");

            var fileData = BLLFactory<User>.Instance.GetPersonImageBytes(UserImageType.个人肖像, id);
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
