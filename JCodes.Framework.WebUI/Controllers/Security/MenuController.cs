using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Collections;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using JCodes.Framework.Common.Extension;

namespace JCodes.Framework.WebUI.Controllers
{
    public class MenuController : BusinessController<Menus, MenuInfo>
    {
        #region 导入Excel数据操作

        //导入或导出的字段列表   
        string columnString = "父ID,显示名称,排序,功能ID,是否可见,Web界面Url地址,Web界面的菜单图标,系统编号";

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
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(MenuController));
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

            List<MenuInfo> list = new List<MenuInfo>();

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
                    MenuInfo info = new MenuInfo();

                    info.PID = dr["父ID"].ToString();
                    info.Name = dr["显示名称"].ToString();
                    info.Seq = dr["排序"].ToString();
                    info.FunctionId = dr["功能ID"].ToString();
                    info.Visible = dr["是否可见"].ToString().ToBoolean();
                    info.Url = dr["Web界面Url地址"].ToString();
                    info.WebIcon = dr["Web界面的菜单图标"].ToString();
                    info.SystemType_ID = dr["系统编号"].ToString();

                    info.Creator_ID = CurrentUser.ID.ToString();
                    info.CreateTime = DateTime.Now;
                    info.Editor_ID = CurrentUser.ID.ToString();
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
        public ActionResult SaveExcelData(List<MenuInfo> list)
        {
            CommonResult result = new CommonResult();
            if (list != null && list.Count > 0)
            {
                #region 采用事务进行数据提交

                DbTransaction trans = BLLFactory<Menus>.Instance.CreateTransaction();
                if (trans != null)
                {
                    try
                    {
                        //int seq = 1;
                        foreach (MenuInfo detail in list)
                        {
                            //detail.Seq = seq++;//增加1
                            detail.CreateTime = DateTime.Now;
                            detail.Creator_ID = CurrentUser.ID.ToString();
                            detail.Editor_ID = CurrentUser.ID.ToString();
                            detail.EditTime = DateTime.Now;

                            BLLFactory<Menus>.Instance.Insert(detail, trans);
                        }
                        trans.Commit();
                        result.Success = true;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(MenuController));
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
            List<MenuInfo> list = new List<MenuInfo>();

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
                dr["父ID"] = list[i].PID;
                dr["显示名称"] = list[i].Name;
                dr["排序"] = list[i].Seq;
                dr["功能ID"] = list[i].FunctionId;
                dr["是否可见"] = list[i].Visible;
                dr["Web界面Url地址"] = list[i].Url;
                dr["Web界面的菜单图标"] = list[i].WebIcon;
                dr["系统编号"] = list[i].SystemType_ID;

                datatable.Rows.Add(dr);
            }
            #endregion

            #region 把DataTable转换为Excel并输出

            //根据用户创建目录，确保生成的文件不会产生冲突
            string filePath = string.Format("/GenerateFiles/{0}/Menu.xls", CurrentUser.Name);
            GenerateExcel(datatable, filePath);

            #endregion

            //返回生成后的文件路径，让客户端根据地址下载
            return Content(filePath);
        }

        #endregion

        #region 写入数据前修改部分属性
        protected override void OnBeforeInsert(MenuInfo info)
        {
            info.ID = string.IsNullOrEmpty(info.ID) ? Guid.NewGuid().ToString() : info.ID;

            //子类对参数对象进行修改
            info.CreateTime = DateTime.Now;
            info.Editor_ID = CurrentUser.ID.ToString();

            //由于界面上对父菜单的顶级选项为具体系统类型的OID，
            //在保存菜单数据到数据库前，需要转换为约定的-1，否则导致不能正常显示
            bool isExistName = BLLFactory<SystemType>.Instance.IsExistKey("OID", info.PID);
            if (isExistName)
            {
                info.PID = "-1";
            }
        }

        protected override void OnBeforeUpdate(MenuInfo info)
        {
            //子类对参数对象进行修改
            info.Editor_ID = CurrentUser.ID.ToString();
            info.EditTime = DateTime.Now;

            //由于界面上对父菜单的顶级选项为具体系统类型的OID，
            //在保存菜单数据到数据库前，需要转换为约定的-1，否则导致不能正常显示
            bool isExistName = BLLFactory<SystemType>.Instance.IsExistKey("OID", info.PID);
            if (isExistName)
            {
                info.PID = "-1";
            }
        }
        #endregion

        public override ActionResult FindWithPager()
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(AuthorizeKey.ListKey);

            string where = GetPagerCondition();
            PagerInfo pagerInfo = GetPagerInfo();
            List<MenuInfo> list = baseBLL.FindWithPager(where, pagerInfo);

            //如果需要修改字段显示，则参考下面代码处理
            //foreach(MenuInfo info in list)
            //{
            //    info.PID = BLLFactory<Menu>.Instance.GetFieldValue(info.PID, "Name");
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
        /// 用作下拉列表的菜单Json数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDictJson()
        {
            List<MenuInfo> list = baseBLL.GetAll();
            list = CollectionHelper<MenuInfo>.Fill("-1", 0, list, "PID", "ID", "Name");

            List<CListItem> itemList = new List<CListItem>();
            foreach (MenuInfo info in list)
            {
                itemList.Add(new CListItem(info.ID, info.Name));
            }
            itemList.Insert(0, new CListItem("-1", "无"));
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取菜单的树形展示数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMenuData()
        {
            #region 返回菜单Json格式
            //        {
            //            "default": [
            //                { "menuid": "1", "icon": "icon-computer", "menuname": "权限管理",
            //				  "menus": [
            //						    { "menuid": "13", "menuname": "用户管理", "icon": "icon-user", "url": "ListUser.aspx" },
            //						    { "menuid": "14", "menuname": "组织机构管理", "icon": "icon-organ", "url": "ListOU.aspx" },
            //						    { "menuid": "15", "menuname": "角色管理", "icon": "icon-group-key", "url": "ListRole.aspx" },
            //						    { "menuid": "16", "menuname": "功能管理", "icon": "icon-key", "url": "ListFunction.aspx" }
            //			   ]},
            //               { "menuid": "2", "icon": "icon-user", "menuname": "其他管理",
            //				 "menus": [{ "menuid": "21", "menuname": "修改密码", "icon": "icon-lock", "url": "ModifyPassword.aspx" }
            //			  ]}
            //            ],
            //            "point": [
            //                { "menuid": "3", "icon": "icon-computer", "menuname": "事务中心",
            //				  "menus": [
            //							{ "menuid": "33", "menuname": "测试菜单1", "icon": "icon-user", "url": "../Commonpage/building.htm" },
            //							{ "menuid": "34", "menuname": "测试菜单2", "icon": "icon-organ", "url": "../Commonpage/building.htm" },
            //							{ "menuid": "35", "menuname": "测试菜单3", "icon": "icon-group-key", "url": "../Commonpage/building.htm" },
            //							{ "menuid": "36", "menuname": "测试菜单4", "icon": "icon-key", "url": "../Commonpage/building.htm" }
            //				]},
            //                { "menuid": "4", "icon": "icon-user", "menuname": "其他菜单",
            //                  "menus": [{ "menuid": "41", "menuname": "测试菜单5", "icon": "icon-lock", "url": "../Commonpage/building.htm"}]
            //				}
            //            ],
            //              "1": [{ "menuid": "5", "icon": "icon-computer", "menuname": "行业动态", "menus": [{ "menuid": "1331", "menuname": "政策法规", "icon": "icon-user", "url": "../Expert/ListPolicyLaw.aspx" }, { "menuid": "1333", "menuname": "通知公告", "icon": "icon-user", "url": "../Expert/ListInformation.aspx" }, { "menuid": "1334", "menuname": "动态信息", "icon": "icon-user", "url": "../Expert/ListIndustryNews.aspx"}]}], "1000": [{ "menuid": "1641", "icon": "icon-computer", "menuname": "基础信息", "menus": [{ "menuid": "1504", "menuname": "道路信息", "icon": "icon-user", "url": "../Road/IndexRoad.aspx" }, { "menuid": "1505", "menuname": "桥梁信息", "icon": "icon-user", "url": "../Bridge/IndexBridge.aspx" }, { "menuid": "1506", "menuname": "隧道信息", "icon": "icon-user", "url": "../Tunnel/IndexTunnel.aspx"}] }, { "menuid": "1622", "icon": "icon-computer", "menuname": "路政巡查管理", "menus": [{ "menuid": "1601", "menuname": "排班计划", "icon": "icon-user", "url": "../Schedule/FormList.aspx" }, { "menuid": "1621", "menuname": "PDA终端设备信息", "icon": "icon-user", "url": "../Check/IndexTerminal.aspx" }, { "menuid": "1644", "menuname": "挖掘占道审批信息", "icon": "icon-user", "url": "../Road/ListConstruction.aspx" }, { "menuid": "1645", "menuname": "考勤信息", "icon": "icon-user", "url": "../Check/TotalOnDuty.aspx" }, { "menuid": "1662", "menuname": "责任单位信息", "icon": "icon-user", "url": "../Road/ListAddressbook.aspx" }, { "menuid": "1721", "menuname": "责任单位通讯录", "icon": "icon-user", "url": "../Road/ListAddresslist.aspx" }, { "menuid": "1741", "menuname": "投诉处理", "icon": "icon-user", "url": "../Check/ListJobComplaint.aspx"}] }, { "menuid": "1663", "icon": "icon-computer", "menuname": "巡查监督管理", "menus": [{ "menuid": "1624", "menuname": "巡查问题", "icon": "icon-user", "url": "../Check/indexProblem.aspx" }, { "menuid": "1626", "menuname": "巡查人员监控", "icon": "icon-user", "url": "../GIS/ShowGis.aspx?gettype=1" }, { "menuid": "1643", "menuname": "报警信息", "icon": "icon-user", "url": "../TaskWarning/ListTaskWarning.aspx" }, { "menuid": "1625", "menuname": "任务小结", "icon": "icon-user", "url": "../Check/ListJobSummary.aspx" }, { "menuid": "1642", "menuname": "短信通知", "icon": "icon-user", "url": "../Commonpage/ListSMS.aspx" }, { "menuid": "1761", "menuname": "短信模板", "icon": "icon-user", "url": "../Commonpage/ListSMSTemplate.aspx" }, { "menuid": "1646", "menuname": "整改通知书", "icon": "icon-user", "url": "../Check/ListRectify.aspx"}] }, { "menuid": "1664", "icon": "icon-computer", "menuname": "巡查统计分析", "menus": [{ "menuid": "1661", "menuname": "巡查问题统计", "icon": "icon-user", "url": "../Check/TotalJobProblemN.aspx" }, { "menuid": "1648", "menuname": "任务完成信息", "icon": "icon-user", "url": "../Check/ListTask.aspx" }, { "menuid": "1665", "menuname": "巡查任务统计", "icon": "icon-user", "url": "../Check/TotalTask.aspx"}]}], "3": [{ "menuid": "31", "icon": "icon-computer", "menuname": "用户管理", "menus": [{ "menuid": "32", "menuname": "用户管理", "icon": "icon-user", "url": "../Security/UserFrame.aspx" }, { "menuid": "33", "menuname": "部门管理", "icon": "icon-user", "url": "../Security/GroupFrame.aspx" }, { "menuid": "34", "menuname": "角色管理", "icon": "icon-user", "url": "../Security/ListRoles.aspx" }, { "menuid": "73", "menuname": "功能管理", "icon": "icon-user", "url": "../Security/FunctionFrame.aspx"}] }, { "menuid": "35", "icon": "icon-computer", "menuname": "系统维护", "menus": [{ "menuid": "36", "menuname": "流程设置", "icon": "icon-user", "url": "../App/ListAppForm.aspx" }, { "menuid": "37", "menuname": "申请单管理", "icon": "icon-user", "url": "../App/ListAppApply.aspx" }, { "menuid": "74", "menuname": "流程环节管理", "icon": "icon-user", "url": "../App/ListAppProc.aspx" }, { "menuid": "1285", "menuname": "流程环节用户设置", "icon": "icon-user", "url": "../App/FlowUserFrame.aspx" }, { "menuid": "76", "menuname": "菜单管理", "icon": "icon-user", "url": "../Security/MenuFrame.aspx" }, { "menuid": "75", "menuname": "系统日志", "icon": "icon-user", "url": "../Commonpage/ListSystemLog.aspx" }, { "menuid": "176", "menuname": "数据字典管理", "icon": "icon-user", "url": "../Commonpage/ListMenu.aspx" }, { "menuid": "1667", "menuname": "短信经办人", "icon": "icon-user", "url": "../Commonpage/ListSmsUser.aspx" }, { "menuid": "1422", "menuname": "临时通行口令", "icon": "icon-user", "url": "../Commonpage/SpecialPermit.aspx" }, { "menuid": "1701", "menuname": "整改通知书编码管理", "icon": "icon-user", "url": "../Security/ModifyRectifySerial.aspx"}]}]
            //        }
            #endregion

            Dictionary<string, List<MenuData>> dict = new Dictionary<string, List<MenuData>>();

            List<MenuInfo> list = BLLFactory<Menus>.Instance.GetTopMenu(Const.SystemTypeID);
            int i = 0;
            foreach (MenuInfo info in list)
            {
                if (!HasFunction(info.FunctionId))
                {
                    continue;
                }
                           
                List<MenuData> treeList = new List<MenuData>();
                List<MenuNodeInfo> nodeList = BLLFactory<Menus>.Instance.GetTreeByID(info.ID);
                foreach (MenuNodeInfo nodeInfo in nodeList)
                {
                    if (!HasFunction(nodeInfo.FunctionId))
                    {
                        continue;
                    }
                                                                                                                                                                                  
                    MenuData menuData = new MenuData(nodeInfo.ID, nodeInfo.Name, string.IsNullOrEmpty(nodeInfo.WebIcon) ? "icon-computer" : nodeInfo.WebIcon);
                    foreach (MenuNodeInfo subNodeInfo in nodeInfo.Children)
                    {
                        if (!HasFunction(subNodeInfo.FunctionId))
                        {
                            continue;
                        }
                        string icon = string.IsNullOrEmpty(subNodeInfo.WebIcon) ? "icon-organ" : subNodeInfo.WebIcon;
                        menuData.menus.Add(new MenuData(subNodeInfo.ID, subNodeInfo.Name, icon, subNodeInfo.Url));
                    }
                    treeList.Add(menuData);
                }

                //添加到字典里面，如果是第一个，默认用default名称
                string dictName = (i++ == 0) ? "default" : info.ID;
                dict.Add(dictName, treeList);
            }

            string content = ToJson(dict);
            content = RemoveJsonNulls(content);//移除为null的json对象属性
            return Content(content.Trim(','));
        }

        /// <summary>
        /// Removes Json null objects from the serialized string and return a new string
        /// </summary>
        /// <param name="str">The String to be checked</param>
        /// <returns>Returns the new processed string or NULL if null or empty string passed</returns>
        private string RemoveJsonNulls(string str)
        {
            string JsonNullRegEx = "[\"][a-zA-Z0-9_]*[\"]:null[ ]*[,]?";
            string JsonNullArrayRegEx = "\\[( *null *,? *)*]";

            if (!string.IsNullOrEmpty(str))
            {
                Regex regex = new Regex(JsonNullRegEx);
                string data = regex.Replace(str, string.Empty);
                regex = new Regex(JsonNullArrayRegEx);
                return regex.Replace(data, "[]");
            }
            return null;
        }

        #region 基于BootStrap的JSTree展示

        /// <summary>
        /// 获取菜单树Json字符串(Bootstrap的JSTree)
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMenuJsTreeJson()
        {
            List<JsTreeData> treeList = new List<JsTreeData>();
            List<SystemTypeInfo> typeList = BLLFactory<SystemType>.Instance.GetAll();
            foreach (SystemTypeInfo typeInfo in typeList)
            {
                JsTreeData pNode = new JsTreeData(typeInfo.OID, typeInfo.Name, "fa fa-home icon-state-warning icon-lg");
                treeList.Add(pNode);

                string systemType = typeInfo.OID;//系统标识ID

                //一般情况下，对Ribbon样式而言，一级菜单表示RibbonPage；二级菜单表示PageGroup;三级菜单才是BarButtonItem最终的菜单项。
                List<MenuNodeInfo> menuList = BLLFactory<Menus>.Instance.GetTree(systemType);
                foreach (MenuNodeInfo info in menuList)
                {
                    JsTreeData item = new JsTreeData(info.ID, info.Name);
                    pNode.children.Add(item);
                    SetFileIcon(info, item);

                    AddJsTreeChildNode(info.Children, item);
                }
            }

            return ToJsonContent(treeList);
        }

        private void AddJsTreeChildNode(List<MenuNodeInfo> list, JsTreeData fnode)
        {
            foreach (MenuNodeInfo info in list)
            {
                JsTreeData item = new JsTreeData(info.ID, info.Name);
                fnode.children.Add(item);
                SetFileIcon(info, item);

                AddJsTreeChildNode(info.Children, item);
            }
        }
        private void SetFileIcon(MenuNodeInfo info, JsTreeData item)
        {
            if (info.Children.Count == 0)
            {
                item.icon = "fa fa-file icon-state-warning icon-lg";
            }
        } 
        #endregion
    }
}
