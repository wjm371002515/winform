using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using JCodes.Framework.Common;
using JCodes.Framework.BLL;
using JCodes.Framework.Entity;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Extension;

namespace JCodes.Framework.AddIn.Security
{
    public partial class FrmEditTree : BaseDock
    {
        public enum DisplayTreeType  { OU, Role, User, Function }
        public DisplayTreeType DisplayType;
        public string RoleID = string.Empty;//在Role中查看其他相关信息的时候
        public string OUID = string.Empty;//在OU中查看用户列表的时候
        public string UserID = string.Empty;//在User的时候查看功能列表

        private List<string> removeList = new List<string>();
        private List<string> addList = new List<string>();

        //CListItem Text=Name, Value = PID
        private Dictionary<string, CListItem> checkedDict = new Dictionary<string, CListItem>();//记录用户原来选择的内容
        private Dictionary<string, CListItem> treeDict = new Dictionary<string, CListItem>();//记录所有列表的内容

        public FrmEditTree()
        {
            InitializeComponent();
        }

        private void FrmEditTree_Load(object sender, EventArgs e)
        {
            RefreshTreeView();
        }

        private void RefreshTreeView()
        {
            ArrayList list = new ArrayList();
            ArrayList chechedList = new ArrayList();

            #region 根据不同条件获取不同的列表
            switch (DisplayType)
            {
                case DisplayTreeType.OU:
                    #region OU
                    list.AddRange(BLLFactory<OU>.Instance.GetAll());

                    if (!string.IsNullOrEmpty(RoleID))
                    {
                        chechedList.AddRange(BLLFactory<OU>.Instance.GetOUsByRole(RoleID.ToInt32()));
                    }

                    if (chechedList == null)
                    {
                        chechedList = new ArrayList();
                    }

                    foreach (OUInfo info in chechedList)
                    {
                        if (!checkedDict.ContainsKey(info.ID.ToString()))
                        {
                            checkedDict.Add(info.ID.ToString(), new CListItem(info.PID.ToString(), info.Name));
                        }
                    }
                    foreach (OUInfo info in list)
                    {
                        if (!treeDict.ContainsKey(info.ID.ToString()))
                        {
                            treeDict.Add(info.ID.ToString(), new CListItem(info.PID.ToString(), info.Name));
                        }
                    } 
                    #endregion
                    break;

                case DisplayTreeType.Role:
                    #region Role

                    list.AddRange(BLLFactory<Role>.Instance.GetAll());

                    if (!string.IsNullOrEmpty(OUID))
                    {
                        chechedList.AddRange(BLLFactory<Role>.Instance.GetRolesByOU(Convert.ToInt32(OUID)));
                    }
                    if (chechedList == null)
                    {
                        chechedList = new ArrayList();
                    }

                    foreach (RoleInfo info in chechedList)
                    {
                        if (!checkedDict.ContainsKey(info.ID.ToString()))
                        {
                            checkedDict.Add(info.ID.ToString(), new CListItem("-1", info.Name));
                        }
                    }
                    foreach (RoleInfo info in list)
                    {
                        if (!treeDict.ContainsKey(info.ID.ToString()))
                        {
                            treeDict.Add(info.ID.ToString(), new CListItem("-1", info.Name));
                        }
                    } 
                    #endregion
                    break;

                case DisplayTreeType.User:
                    #region User
                    list.AddRange(BLLFactory<User>.Instance.GetAll());

                    if (!string.IsNullOrEmpty(RoleID))
                    {
                        chechedList.AddRange(BLLFactory<User>.Instance.GetUsersByRole(RoleID.ToInt32()));
                    }
                    else if (!string.IsNullOrEmpty(OUID))
                    {
                        chechedList.AddRange(BLLFactory<User>.Instance.GetUsersByOU(Convert.ToInt32(OUID)));
                    }

                    if (chechedList == null)
                    {
                        chechedList = new ArrayList();
                    }

                    foreach (UserInfo info in chechedList)
                    {
                        if (!checkedDict.ContainsKey(info.ID.ToString()))
                        {
                            string name = string.Format("{0}（{1}）", info.Name, info.FullName);
                            checkedDict.Add(info.ID.ToString(), new CListItem(info.PID.ToString(), name));
                        }
                    }
                    foreach (UserInfo info in list)
                    {
                        if (!treeDict.ContainsKey(info.ID.ToString()))
                        {
                            string name = string.Format("{0}（{1}）", info.Name, info.FullName);
                            treeDict.Add(info.ID.ToString(), new CListItem(info.PID.ToString(), name));
                        }
                    } 
                    #endregion
                    break;

                case DisplayTreeType.Function:
                    #region Function
                    list.AddRange(BLLFactory<Functions>.Instance.GetAll());

                    if (!string.IsNullOrEmpty(RoleID))
                    {
                        chechedList.AddRange(BLLFactory<Functions>.Instance.GetFunctionsByRole(RoleID.ToInt32()));
                    }
                    else if (!string.IsNullOrEmpty(UserID))
                    {
                        chechedList.AddRange(BLLFactory<Functions>.Instance.GetFunctionsByUser(Convert.ToInt32(UserID), ""));
                    }

                    if (chechedList == null)
                    {
                        chechedList = new ArrayList();
                    }

                    foreach (FunctionInfo info in chechedList)
                    {
                        if (!checkedDict.ContainsKey(info.ID))
                        {
                            // 20170901 wjm 调整key 和value的顺序
                            checkedDict.Add(info.ID, new CListItem( info.PID.ToString(), info.Name));
                        }
                    }
                    foreach (FunctionInfo info in list)
                    {
                        if (!treeDict.ContainsKey(info.ID))
                        {
                            // 20170901 wjm 调整key 和value的顺序
                            treeDict.Add(info.ID, new CListItem(info.PID.ToString(), info.Name));
                        }
                    } 
                    #endregion
                    break;
            } 
            #endregion

            treeView1.Nodes.Clear();
            treeView1.BeginUpdate();
            Cursor.Current = Cursors.WaitCursor;

            foreach(string key in treeDict.Keys)
            {
                if (treeDict[key].Value != "-1")
                {
                    continue;
                }

                TreeNode item = new TreeNode();
                item.Name = key.ToString();
                item.Text = treeDict[key].Text;
                item.Tag = treeDict[key].Text;
                item.Checked = checkedDict.ContainsKey(key);
                this.treeView1.Nodes.Add(item);

                AddChildNode(item);
            }

            Cursor.Current = Cursors.Default;
            treeView1.EndUpdate();
            this.treeView1.ExpandAll();
        }

        private void AddChildNode(TreeNode fnode)
        {
            foreach (string key in treeDict.Keys)
            {
                if (treeDict[key].Value != fnode.Name)
                {
                    continue;
                }
                TreeNode item = new TreeNode();
                item.Name = key.ToString();
                item.Text = treeDict[key].Text;
                item.Tag = treeDict[key].Text;
                item.Checked = checkedDict.ContainsKey(key);
                fnode.Nodes.Add(item);

                AddChildNode(item);
            }
        }

        private void GetChanges(TreeNode node)
        {
            string id = node.Name;
            if (!node.Checked && checkedDict.ContainsKey(id))
            {
                removeList.Add(id);
            }
            if (node.Checked && !checkedDict.ContainsKey(id))
            {
                addList.Add(id);
            }

            foreach (TreeNode subNode in node.Nodes)
            {
                GetChanges(subNode);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in this.treeView1.Nodes)
            {
                GetChanges(node);
            }

            #region 根据不同条件获取不同的列表
            switch (DisplayType)
            {
                case DisplayTreeType.OU:
                    foreach (string id in removeList)
                    {
                        if (!string.IsNullOrEmpty(RoleID))
                        {
                            BLLFactory<Role>.Instance.RemoveOU(id.ToInt32(), RoleID.ToInt32());
                        }
                    }
                    foreach (string id in addList)
                    {
                        if (!string.IsNullOrEmpty(RoleID))
                        {
                            BLLFactory<Role>.Instance.AddOU(id.ToInt32(), RoleID.ToInt32());
                        }
                    }
                    break;

                case DisplayTreeType.Role:
                    break;

                case DisplayTreeType.User:
                    foreach (string id in removeList)
                    {
                        if (!string.IsNullOrEmpty(RoleID))
                        {
                            BLLFactory<Role>.Instance.RemoveUser(id.ToInt32(), RoleID.ToInt32());
                        }
                        else if (!string.IsNullOrEmpty(OUID))
                        {
                            BLLFactory<OU>.Instance.RemoveUser(id.ToInt32(), Convert.ToInt32(OUID));
                        }
                    }         
                    foreach (string id in addList)
                    {
                        if (!string.IsNullOrEmpty(RoleID))
                        {
                            BLLFactory<Role>.Instance.AddUser(id.ToInt32(), RoleID.ToInt32());
                        }
                        else if (!string.IsNullOrEmpty(OUID))
                        {
                            BLLFactory<OU>.Instance.AddUser(id.ToInt32(), Convert.ToInt32(OUID));
                        }
                    }
                    break;

                case DisplayTreeType.Function:
                    foreach (string id in removeList)
                    {
                        if (!string.IsNullOrEmpty(RoleID))
                        {
                            BLLFactory<Role>.Instance.RemoveFunction(id, RoleID.ToInt32());
                        }
                    }
                    foreach (string id in addList)
                    {
                        if (!string.IsNullOrEmpty(RoleID))
                        {
                            BLLFactory<Role>.Instance.AddFunction(id, RoleID.ToInt32());
                        }
                    }
                    break;
            }
            #endregion
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (TreeNode node in this.treeView1.Nodes)
            {
                CheckSelect(node, this.chkAll.Checked);
            }
        }

        private void CheckSelect(TreeNode node, bool selectAll)
        {
            node.Checked = selectAll;
            foreach (TreeNode subNode in node.Nodes)
            {
                CheckSelect(subNode, selectAll);
            }
        }
    }
}
