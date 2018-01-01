using JCodes.Framework.BLL;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace JCodes.Framework.WebUI.Controllers
{
    public class ContactGroupController : BusinessController<ContactGroup, ContactGroupInfo>
    {
        public ContactGroupController() : base()
        {
        }

        #region 写入数据前修改部分属性
        protected override void OnBeforeInsert(ContactGroupInfo info)
        {
            //留给子类对参数对象进行修改
            info.CreateTime = DateTime.Now;
            info.Creator = CurrentUser.ID.ToString();
            info.Company_ID = CurrentUser.Company_ID;
            info.Dept_ID = CurrentUser.Dept_ID;
        }

        protected override void OnBeforeUpdate(ContactGroupInfo info)
        {
            //留给子类对参数对象进行修改
            info.Editor = CurrentUser.ID.ToString();
            info.EditTime = DateTime.Now;
        }
        #endregion

        #region Bootstrap的树列表

        /// <summary>
        /// 获取分组的列表，用作下拉列表
        /// </summary>
        /// <param name="creator">当前用户的ID</param>
        /// <returns></returns>
        public ActionResult GetDictJson(string creator)
        {
            List<CListItem> treeList = new List<CListItem>();
            CListItem topNode = new CListItem( "-1", "无");
            treeList.Add(topNode);

            List<ContactGroupNodeInfo> groupList = BLLFactory<ContactGroup>.Instance.GetTree(creator);
            AddGroupDict(groupList, treeList);

            return ToJsonContent(treeList);
        }
        private void AddGroupDict(List<ContactGroupNodeInfo> nodeList, List<CListItem> treeList)
        {
            foreach (ContactGroupNodeInfo nodeInfo in nodeList)
            {
                CListItem subNode = new CListItem(nodeInfo.ID, nodeInfo.Name);
                treeList.Add(subNode);

                AddGroupDict(nodeInfo.Children, treeList);
            }
        }

        /// <summary>
        /// 获取联系人分组树Json字符串
        /// </summary>
        /// <returns></returns>
        public ActionResult GetGroupJsTreeJson(string userId)
        {
            //添加一个未分类和全部客户的组别
            List<JsTreeData> treeList = new List<JsTreeData>();
            JsTreeData pNode = new JsTreeData("", "所有联系人", "fa fa-users icon-state-warning icon-lg");
            treeList.Insert(0, pNode);
            treeList.Add(new JsTreeData("", "未分组联系人", "fa fa-users icon-state-warning icon-lg"));

            List<ContactGroupNodeInfo> groupList = BLLFactory<ContactGroup>.Instance.GetTree(userId);
            AddContactGroupJsTree(groupList, pNode);

            return ToJsonContent(treeList);
        }

        /// <summary>
        /// 初始化并绑定客户个人分组信息
        /// </summary>
        public ActionResult GetMyContactGroupJsTree(string contactId, string userId)
        {
            List<ContactGroupInfo> myGroupList = BLLFactory<ContactGroup>.Instance.GetByContact(contactId);
            List<string> groupIdList = new List<string>();
            foreach (ContactGroupInfo info in myGroupList)
            {
                groupIdList.Add(info.ID);
            }

            List<ContactGroupNodeInfo> groupList = BLLFactory<ContactGroup>.Instance.GetTree(userId);

            List<JsTreeData> treeList = new List<JsTreeData>();
            foreach (ContactGroupNodeInfo nodeInfo in groupList)
            {
                bool check = groupIdList.Contains(nodeInfo.ID);
                JsTreeData treeData = new JsTreeData(nodeInfo.ID, nodeInfo.Name);
                treeData.state  = new JsTreeState(true, check);

                treeList.Add(treeData);
            }

            return ToJsonContent(treeList);
        }

        /// <summary>
        /// 获取客户分组并绑定
        /// </summary>
        private void AddContactGroupJsTree(List<ContactGroupNodeInfo> nodeList, JsTreeData treeNode)
        {
            foreach (ContactGroupNodeInfo nodeInfo in nodeList)
            {
                JsTreeData subNode = new JsTreeData(nodeInfo.ID, nodeInfo.Name, "fa fa-user icon-state-warning icon-lg");
                treeNode.children.Add(subNode);

                AddContactGroupJsTree(nodeInfo.Children, subNode);
            }
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
