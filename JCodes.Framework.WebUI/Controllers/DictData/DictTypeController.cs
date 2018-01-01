using JCodes.Framework.BLL;
using JCodes.Framework.Common.Collections;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace JCodes.Framework.WebUI.Controllers
{
    /// <summary>
    /// 数据字典大类的控制器
    /// </summary>
    public class DictTypeController :  BusinessController<DictType, DictTypeInfo>
    {
        #region 写入数据前修改部分属性
        protected override void OnBeforeInsert(DictTypeInfo info)
        {
            //留给子类对参数对象进行修改
            info.Editor = CurrentUser.ID.ToString();
            info.LastUpdated = DateTime.Now;
        }

        protected override void OnBeforeUpdate(DictTypeInfo info)
        {
            //留给子类对参数对象进行修改
            info.Editor = CurrentUser.ID.ToString();
            info.LastUpdated = DateTime.Now;
        }
        #endregion

        /// <summary>
        /// 用作下拉列表的菜单Json数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDictJson()
        {
            List<DictTypeInfo> list = baseBLL.GetAll();
            list = CollectionHelper<DictTypeInfo>.Fill("-1", 0, list, "PID", "ID", "Name");

            List<CListItem> itemList = new List<CListItem>();
            foreach (DictTypeInfo info in list)
            {
                // 已调整
                itemList.Add(new CListItem(info.ID.ToString(), info.Name));
            }
            itemList.Add(new CListItem("-1", "无"));

            return Json(itemList, JsonRequestBehavior.AllowGet);
        }

        #region EasyUI的树形列表
        /// <summary>
        /// 获取树形展示数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTreeJson()
        {
            List<TreeData> treeList = new List<TreeData>();
            List<DictTypeInfo> typeList = BLLFactory<DictType>.Instance.Find("PID='-1' ");
            foreach (DictTypeInfo info in typeList)
            {
                TreeData node = new TreeData(info.ID, info.PID, info.Name);
                GetTreeJson(info.ID.ToString(), node);

                treeList.Add(node);
            }
            return ToJsonContent(treeList);
        }

        /// <summary>
        /// 递归获取树形信息
        /// </summary>
        /// <returns></returns>
        private void GetTreeJson(string PID, TreeData treeNode)
        {
            string condition = string.Format("PID='{0}' ", PID);
            List<DictTypeInfo> nodeList = BLLFactory<DictType>.Instance.Find(condition);
            StringBuilder content = new StringBuilder();

            foreach (DictTypeInfo model in nodeList)
            {
                TreeData node = new TreeData(model.ID, model.PID, model.Name);
                treeNode.children.Add(node);
                GetTreeJson(model.ID.ToString(), node);
            }
        } 
        #endregion

        #region BootStrap JSTree的树形列表

        /// <summary>
        /// 获取树形展示数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetJsTreeJson()
        {
            List<JsTreeData> treeList = new List<JsTreeData>();
            List<DictTypeInfo> typeList = BLLFactory<DictType>.Instance.Find("PID='-1' ");
            foreach (DictTypeInfo info in typeList)
            {
                JsTreeData node = new JsTreeData(info.ID, info.Name, "fa fa-file icon-state-warning icon-lg");
                GetJsTreeJson(info.ID.ToString(), node);

                treeList.Add(node);
            }
            return ToJsonContent(treeList);
        }

        /// <summary>
        /// 递归获取树形信息
        /// </summary>
        /// <returns></returns>
        private void GetJsTreeJson(string PID, JsTreeData treeNode)
        {
            string condition = string.Format("PID='{0}' ", PID);
            List<DictTypeInfo> nodeList = BLLFactory<DictType>.Instance.Find(condition);
            StringBuilder content = new StringBuilder();

            foreach (DictTypeInfo model in nodeList)
            {
                JsTreeData node = new JsTreeData(model.ID, model.Name, "fa fa-file icon-state-warning icon-lg");
                treeNode.children.Add(node);

                GetJsTreeJson(model.ID.ToString(), node);
            }
        }
        #endregion

    }
}
