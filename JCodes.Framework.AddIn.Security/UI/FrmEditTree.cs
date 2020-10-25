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
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.AddIn.Security
{
    public partial class FrmEditTree : BaseDock
    {
        public DisplayTreeType DisplayType;
        public Int32 RoleId = 0;//在Role中查看其他相关信息的时候
        public Int32 OuId = 0;//在OU中查看用户列表的时候
        public Int32 UserId = 0;//在User的时候查看功能列表

        private List<string> removeList = new List<string>();
        private List<string> addList = new List<string>();

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

                    if (RoleId > 0)
                    {
                        chechedList.AddRange(BLLFactory<OU>.Instance.GetOUsByRoleId(RoleId));
                    }

                    if (chechedList == null)
                    {
                        chechedList = new ArrayList();
                    }

                    foreach (OUInfo info in chechedList)
                    {
                        if (!checkedDict.ContainsKey(info.Id.ToString()))
                        {
                            checkedDict.Add(info.Id.ToString(), new CListItem(info.Pid.ToString(), info.Name));
                        }
                    }
                    foreach (OUInfo info in list)
                    {
                        if (!treeDict.ContainsKey(info.Id.ToString()))
                        {
                            treeDict.Add(info.Id.ToString(), new CListItem(info.Pid.ToString(), info.Name));
                        }
                    } 
                    #endregion
                    break;

                case DisplayTreeType.Role:
                    #region Role

                    list.AddRange(BLLFactory<Role>.Instance.GetAll());

                    if (OuId > 0)
                    {
                        chechedList.AddRange(BLLFactory<Role>.Instance.GetRolesByOU(OuId));
                    }
                    if (chechedList == null)
                    {
                        chechedList = new ArrayList();
                    }

                    foreach (RoleInfo info in chechedList)
                    {
                        if (!checkedDict.ContainsKey(info.Id.ToString()))
                        {
                            checkedDict.Add(info.Id.ToString(), new CListItem("-1", info.Name));
                        }
                    }
                    foreach (RoleInfo info in list)
                    {
                        if (!treeDict.ContainsKey(info.Id.ToString()))
                        {
                            treeDict.Add(info.Id.ToString(), new CListItem("-1", info.Name));
                        }
                    } 
                    #endregion
                    break;

                case DisplayTreeType.User:
                    #region User
                    list.AddRange(BLLFactory<User>.Instance.GetAll());

                    if (RoleId > 0)
                    {
                        chechedList.AddRange(BLLFactory<User>.Instance.GetUsersByRoleId(RoleId));
                    }
                    else if (OuId > 0)
                    {
                        chechedList.AddRange(BLLFactory<User>.Instance.GetUsersByOUId(OuId));
                    }

                    if (chechedList == null)
                    {
                        chechedList = new ArrayList();
                    }

                    foreach (UserInfo info in chechedList)
                    {
                        if (!checkedDict.ContainsKey(info.Id.ToString()))
                        {
                            string name = string.Format("{0}（{1}）", info.Name, info.LoginName);
                            //checkedDict.Add(info.Id.ToString(), new CListItem(info.Pid.ToString(), name));
                        }
                    }
                    foreach (UserInfo info in list)
                    {
                        if (!treeDict.ContainsKey(info.Id.ToString()))
                        {
                            string name = string.Format("{0}（{1}）", info.Name, info.LoginName);
                            //treeDict.Add(info.Id.ToString(), new CListItem(info.Pid.ToString(), name));
                        }
                    } 
                    #endregion
                    break;

                case DisplayTreeType.Function:
                    #region Function
                    list.AddRange(BLLFactory<Function>.Instance.GetAll());

                    if (RoleId > 0)
                    {
                        chechedList.AddRange(BLLFactory<Function>.Instance.GetFunctionsByRoleId(RoleId));
                    }
                    else if (UserId > 0)
                    {
                        chechedList.AddRange(BLLFactory<Function>.Instance.GetFunctionsByUser(UserId, ""));
                    }

                    if (chechedList == null)
                    {
                        chechedList = new ArrayList();
                    }

                    foreach (FunctionInfo info in chechedList)
                    {
                        if (!checkedDict.ContainsKey(info.Gid))
                        {
                            // 20170901 wjm 调整key 和value的顺序
                            checkedDict.Add(info.Gid, new CListItem(info.Pgid, info.Name));
                        }
                    }
                    foreach (FunctionInfo info in list)
                    {
                        if (!treeDict.ContainsKey(info.Gid))
                        {
                            // 20170901 wjm 调整key 和value的顺序
                            treeDict.Add(info.Gid, new CListItem(info.Pgid, info.Name));
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
            string Id = node.Name;
            if (!node.Checked && checkedDict.ContainsKey(Id))
            {
                removeList.Add(Id);
            }
            if (node.Checked && !checkedDict.ContainsKey(Id))
            {
                addList.Add(Id);
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
                    foreach (string Id in removeList)
                    {
                        if (RoleId > 0)
                        {
                            BLLFactory<Role>.Instance.RemoveOU(Id.ToInt32(), RoleId);
                        }
                    }
                    foreach (string Id in addList)
                    {
                        if (RoleId > 0)
                        {
                            BLLFactory<Role>.Instance.AddOU(Id.ToInt32(), RoleId);
                        }
                    }
                    break;
                case DisplayTreeType.Role:
                    break;

                case DisplayTreeType.User:
                    foreach (string Id in removeList)
                    {
                        if (RoleId > 0)
                        {
                            BLLFactory<Role>.Instance.RemoveUser(Id.ToInt32(), RoleId);
                        }
                        else if (OuId > 0)
                        {
                            BLLFactory<OU>.Instance.RemoveUser(Id.ToInt32(), OuId);
                        }
                    }
                    foreach (string Id in addList)
                    {
                        if (RoleId > 0)
                        {
                            BLLFactory<Role>.Instance.AddUser(Id.ToInt32(), RoleId);
                        }
                        else if (OuId > 0)
                        {
                            BLLFactory<OU>.Instance.AddUser(Id.ToInt32(), OuId);
                        }
                    }
                    break;

                case DisplayTreeType.Function:
                    foreach (string Id in removeList)
                    {
                        if (RoleId > 0)
                        {
                            BLLFactory<Role>.Instance.RemoveFunction(Id, RoleId);
                        }
                    }
                    foreach (string Id in addList)
                    {
                        if (RoleId > 0)
                        {
                            BLLFactory<Role>.Instance.AddFunction(Id, RoleId);
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
