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
            info.EditorId = CurrentUser.Id;
            info.LastUpdated = DateTime.Now;
        }

        protected override void OnBeforeUpdate(DictTypeInfo info)
        {
            //留给子类对参数对象进行修改
            info.EditorId = CurrentUser.Id;
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

            List<CDicKeyValue> itemList = new List<CDicKeyValue>();
            foreach (DictTypeInfo info in list)
            {
                // 已调整
                itemList.Add(new CDicKeyValue(info.Id, info.Name));
            }
            itemList.Add(new CDicKeyValue(-1, "无"));

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
                TreeData node = new TreeData(info.Id, info.Pid, info.Name);
                GetTreeJson(info.Id.ToString(), node);

                treeList.Add(node);
            }
            return ToJsonContent(treeList);
        }

        /// <summary>
        /// 递归获取树形信息
        /// </summary>
        /// <returns></returns>
        private void GetTreeJson(string Pid, TreeData treeNode)
        {
            string condition = string.Format("PID='{0}' ", Pid);
            List<DictTypeInfo> nodeList = BLLFactory<DictType>.Instance.Find(condition);
            StringBuilder content = new StringBuilder();

            foreach (DictTypeInfo model in nodeList)
            {
                TreeData node = new TreeData(model.Id, model.Pid, model.Name);
                treeNode.children.Add(node);
                GetTreeJson(model.Id.ToString(), node);
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
                JsTreeData node = new JsTreeData(info.Id, info.Name, "fa fa-file icon-state-warning icon-lg");
                GetJsTreeJson(info.Id.ToString(), node);

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
                JsTreeData node = new JsTreeData(model.Id, model.Name, "fa fa-file icon-state-warning icon-lg");
                treeNode.children.Add(node);

                GetJsTreeJson(model.Id.ToString(), node);
            }
        }
        #endregion

    }
}
