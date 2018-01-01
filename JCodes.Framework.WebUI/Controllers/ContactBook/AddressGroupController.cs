using System;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using System.Collections.Generic;
using JCodes.Framework.BLL;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Common.Format;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Extension;

namespace JCodes.Framework.WebUI.Controllers
{
    public class AddressGroupController : BusinessController<AddressGroup, AddressGroupInfo>
    {
        public AddressGroupController()
            : base()
        {
        }

        #region 导入Excel数据操作

        //导入或导出的字段列表   
        string columnString = "父ID,通讯录类型,分组名称,备注,排序序号,创建人,创建时间";

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
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(AddressGroupController));
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

            List<AddressGroupInfo> list = new List<AddressGroupInfo>();

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
                    AddressGroupInfo info = new AddressGroupInfo();

                    info.PID = dr["父ID"].ToString();
                    info.AddressType = EnumHelper.GetInstance<AddressType>(dr["通讯录类型"].ToString());
                    info.Name = dr["分组名称"].ToString();
                    info.Note = dr["备注"].ToString();
                    info.Seq = dr["排序序号"].ToString();
                    info.Creator = dr["创建人"].ToString();
                    converted = DateTime.TryParse(dr["创建时间"].ToString(), out dt);
                    if (converted && dt > dtDefault)
                    {
                        info.CreateTime = dt;
                    }
                    info.Dept_ID = CurrentUser.Dept_ID;
                    info.Company_ID = CurrentUser.Company_ID;
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
        public ActionResult SaveExcelData(List<AddressGroupInfo> list)
        {
            CommonResult result = new CommonResult();
            if (list != null && list.Count > 0)
            {
                #region 采用事务进行数据提交

                DbTransaction trans = BLLFactory<AddressGroup>.Instance.CreateTransaction();
                if (trans != null)
                {
                    try
                    {
                        //int seq = 1;
                        foreach (AddressGroupInfo detail in list)
                        {
                            //detail.Seq = seq++;//增加1
                            detail.CreateTime = DateTime.Now;
                            detail.Creator = CurrentUser.ID.ToString();
                            detail.Editor = CurrentUser.ID.ToString();
                            detail.EditTime = DateTime.Now;

                            BLLFactory<AddressGroup>.Instance.Insert(detail, trans);
                        }
                        trans.Commit();
                        result.Success = true;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(AddressGroupController));
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
            List<AddressGroupInfo> list = new List<AddressGroupInfo>();

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
                dr["通讯录类型[个人,公司]"] = list[i].AddressType;
                dr["分组名称"] = list[i].Name;
                dr["备注"] = list[i].Note;
                dr["排序序号"] = list[i].Seq;
                dr["创建人"] = list[i].Creator;
                dr["创建时间"] = list[i].CreateTime;
                //如果为外键，可以在这里进行转义，如下例子
                //dr["客户名称"] = BLLFactory<Customer>.Instance.GetCustomerName(list[i].Customer_ID);//转义为客户名称

                datatable.Rows.Add(dr);
            }
            #endregion

            #region 把DataTable转换为Excel并输出

            //根据用户创建目录，确保生成的文件不会产生冲突
            string filePath = string.Format("/GenerateFiles/{0}/AddressGroup.xls", CurrentUser.Name);
            GenerateExcel(datatable, filePath);

            #endregion

            //返回生成后的文件路径，让客户端根据地址下载
            return Content(filePath);
        }

        #endregion

        #region 写入数据前修改部分属性
        protected override void OnBeforeInsert(AddressGroupInfo info)
        {
            //留给子类对参数对象进行修改
            info.CreateTime = DateTime.Now;
            info.Creator = CurrentUser.ID.ToString();
            info.Company_ID = CurrentUser.Company_ID;
            info.Dept_ID = CurrentUser.Dept_ID;
        }

        protected override void OnBeforeUpdate(AddressGroupInfo info)
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
            List<AddressGroupInfo> list = baseBLL.FindWithPager(where, pagerInfo);
            foreach (AddressGroupInfo info in list)
            {
                info.PID = BLLFactory<AddressGroup>.Instance.GetFieldValue(info.PID, "Name");
                info.Data1 = info.AddressType.ToString();
                if (!string.IsNullOrEmpty(info.Creator))
                {
                    info.Creator = BLLFactory<User>.Instance.GetFullNameByID(info.Creator.ToInt32());
                }
            }


            //Json格式的要求{total:22,rows:{}}
            //构造成Json的格式传递
            var result = new { total = pagerInfo.RecordCount, rows = list };
            return ToJsonContentDate(result);
        }

        #region Bootstrap树列表
        /// <summary>
        /// 获取分组的列表，用作下拉列表
        /// </summary>
        /// <param name="addressType"></param>
        /// <returns></returns>
        public ActionResult GetDictJson(string addressType)
        {
            List<CListItem> treeList = new List<CListItem>();
            CListItem topNode = new CListItem("-1", "无");
            treeList.Add(topNode);

            AddressType type = (addressType == "public") ? AddressType.公共 : AddressType.个人;
            List<AddressGroupNodeInfo> groupList = BLLFactory<AddressGroup>.Instance.GetTree(type.ToString());
            AddGroupDict(groupList, treeList);

            return ToJsonContent(treeList);
        }

        private void AddGroupDict(List<AddressGroupNodeInfo> nodeList, List<CListItem> treeList)
        {
            foreach (AddressGroupNodeInfo nodeInfo in nodeList)
            {
                CListItem subNode = new CListItem(nodeInfo.Name, nodeInfo.ID);
                treeList.Add(subNode);

                AddGroupDict(nodeInfo.Children, treeList);
            }
        }

        public ActionResult GetGroupJsTree(string addressType)
        {
            List<JsTreeData> treeList = new List<JsTreeData>();

            JsTreeData topNode = new JsTreeData("all", "所有联系人");
            treeList.Add(topNode);
            treeList.Add(new JsTreeData("ungroup", "未分组联系人"));

            AddressType type = (addressType == "public") ? AddressType.公共 : AddressType.个人;
            List<AddressGroupNodeInfo> groupList = BLLFactory<AddressGroup>.Instance.GetTree(type.ToString());
            AddContactGroupJsTree(groupList, topNode);

            return ToJsonContent(treeList);
        }

        /// <summary>
        /// 获取分组并绑定
        /// </summary>
        private void AddContactGroupJsTree(List<AddressGroupNodeInfo> nodeList, JsTreeData treeNode)
        {
            foreach (AddressGroupNodeInfo nodeInfo in nodeList)
            {
                JsTreeData subNode = new JsTreeData(nodeInfo.Name, nodeInfo.Name, "fa fa-file icon-state-warning icon-lg");
                treeNode.children.Add(subNode);

                AddContactGroupJsTree(nodeInfo.Children, subNode);
            }
        }

        public ActionResult GetAddressGroupJsTree(string userId, string contactId, string addressType)
        {
            List<string> groupIdList = new List<string>();
            if (!string.IsNullOrEmpty(contactId))
            {
                List<AddressGroupInfo> myGroupList = BLLFactory<AddressGroup>.Instance.GetByContact(contactId);
                foreach (AddressGroupInfo info in myGroupList)
                {
                    groupIdList.Add(info.ID);
                }
            }

            AddressType type = (addressType == "public") ? AddressType.公共 : AddressType.个人;

            List<AddressGroupNodeInfo> groupList = new List<AddressGroupNodeInfo>();
            if (type == AddressType.个人)
            {
                groupList = BLLFactory<AddressGroup>.Instance.GetTree(type.ToString(), userId);
            }
            else
            {
                groupList = BLLFactory<AddressGroup>.Instance.GetTree(type.ToString());
            }

            List<JsTreeData> treeList = new List<JsTreeData>();
            foreach (AddressGroupNodeInfo nodeInfo in groupList)
            {
                bool check = groupIdList.Contains(nodeInfo.ID);
                JsTreeData pNode = new JsTreeData(nodeInfo.ID, nodeInfo.Name, "");
                pNode.state = new JsTreeState(true, check);
                treeList.Add(pNode);
            }

            return ToJsonContent(treeList);
        }
        #endregion

        /// <summary>
        /// 根据ID获取分组名称
        /// </summary>
        /// <param name="id">分组ID</param>
        /// <returns></returns>
        public ActionResult GetNameByID(string id)
        {
            string name = baseBLL.GetFieldValue(id, "Name");
            name = string.IsNullOrEmpty(name) ? "无" : name;

            return ToJsonContent(name);
        }
    }
}
