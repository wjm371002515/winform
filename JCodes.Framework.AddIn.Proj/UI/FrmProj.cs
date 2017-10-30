using System;
using JCodes.Framework.Entity;
using JCodes.Framework.CommonControl.BaseUI;
using System.Xml;
using JCodes.Framework.Common.Files;
using JCodes.Framework.CommonControl.Other;
using DevExpress.XtraEditors;
using System.Collections.Generic;
using JCodes.Framework.CommonControl.Controls;
using System.Windows.Forms;
using JCodes.Framework.Common.Format;

namespace JCodes.Framework.AddIn.Proj
{
    public partial class FrmProj : BaseDock
    {
        /// <summary>
        /// 读取项目配置文件
        /// </summary>
        private XmlHelper xmlprojectthelper = new XmlHelper(@"XML\project.xml");

        private ProjectInfo projectInfo = null;

        public FrmProj()
        {
            InitializeComponent();
        }

        private void FrmProj_Load(object sender, EventArgs e)
        {
            BindData();

            InitView();
        }

        private void BindData()
        {
            #region 读取项目数据
            XmlNodeList xmlprejectNodeLst = xmlprojectthelper.Read("datatype");

            if (xmlprejectNodeLst.Count == 0)
                return;

            XmlNode xn1project = xmlprejectNodeLst[0];
            projectInfo = new ProjectInfo();

            // 将节点转换为元素，便于得到节点的属性值
            XmlElement xeproject = (XmlElement)xn1project;
            // 得到Type和ISBN两个属性的属性值
            projectInfo.GUID = xeproject.GetAttribute("guid").ToString();
            // 得到DataTypeInfo节点的所有子节点
            XmlNodeList xnl0project = xeproject.ChildNodes;
            projectInfo.Name = xnl0project.Item(0).InnerText;
            projectInfo.Version = xnl0project.Item(1).InnerText;
            projectInfo.Contract = xnl0project.Item(2).InnerText;
            projectInfo.Remark = xnl0project.Item(3).InnerText;
            projectInfo.DbType = xnl0project.Item(4).InnerText;
            projectInfo.DicttypeTable = xnl0project.Item(5).InnerText;
            projectInfo.DictdataTable = xnl0project.Item(6).InnerText;
            projectInfo.ErrTable = xnl0project.Item(7).InnerText;
            projectInfo.LastTime = xnl0project.Item(8).InnerText;
            projectInfo.OutputPath = xnl0project.Item(9).InnerText;

            #endregion

            #region 绑定数据库类型
            List<CListItem> dbtypeList = new List<CListItem>();
            dbtypeList.Add(new CListItem("Oracle", "Oracle数据库"));
            dbtypeList.Add(new CListItem("Mysql", "Mysql数据库"));
            dbtypeList.Add(new CListItem("DB2", "DB2数据库"));
            dbtypeList.Add(new CListItem("SqlServer", "SqlServer数据库"));
            dbtypeList.Add(new CListItem("Sqlite", "Sqlite数据库"));
            dbtypeList.Add(new CListItem("Access", "Access数据库"));
            cbbdbtype.BindDictItems(dbtypeList);
            #endregion
        }

        private void InitView()
        {
            txtName.Text = projectInfo.Name;
            txtVersion.Text = projectInfo.Version;
            txtContract.Text = projectInfo.Contract;
            txtRemark.Text = projectInfo.Remark;
            cbbdbtype.SetComboBoxItem(projectInfo.DbType);
            txtdicttype.Text = projectInfo.DicttypeTable;
            txtdictdata.Text = projectInfo.DictdataTable;
            txterr.Text = projectInfo.ErrTable;
            txtlasttime.Text = projectInfo.LastTime;
            txtoutputpath.Text = projectInfo.OutputPath;
        }

        /// <summary>
        /// 主版本号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVersion_Click(object sender, EventArgs e)
        {
            // 主版本号 . 子版本号 [. 修正版本号 build- [编译版本号 ]]
            // modrecoreInfo.ModVersion = new Version(string.Format("{0}.{1}.{2}.{3}", lastVersion.Major, lastVersion.Minor, lastVersion.Build, lastVersion.Revision + 1)).ToString();
            try
            {
                Version v = new Version(txtVersion.Text);

                var btn = sender as SimpleButton;
                if (string.Equals(btn.Name, "btnMajor"))
                {
                    txtVersion.Text = new Version(string.Format("{0}.{1}.{2}.{3}", v.Major + 1, v.Minor, v.Build, v.Revision)).ToString();
                }
                else if (string.Equals(btn.Name, "btnMinor"))
                {
                    txtVersion.Text = new Version(string.Format("{0}.{1}.{2}.{3}", v.Major, v.Minor + 1, v.Build, v.Revision)).ToString();
                }
                else if (string.Equals(btn.Name, "btnBuild"))
                {
                    txtVersion.Text = new Version(string.Format("{0}.{1}.{2}.{3}", v.Major, v.Minor, v.Build + 1, v.Revision)).ToString();
                }
                else if (string.Equals(btn.Name, "btnRevision"))
                {
                    txtVersion.Text = new Version(string.Format("{0}.{1}.{2}.{3}", v.Major, v.Minor, v.Build, v.Revision + 1)).ToString();
                }

                string curdatetime = DateTimeHelper.GetServerDateTime();
                xmlprojectthelper.Replace("datatype/item[@guid=\"" + projectInfo.GUID + "\"]/lasttime", curdatetime);
                xmlprojectthelper.Save(false);
                txtlasttime.Text = curdatetime;
            }
            catch (Exception ex)
            {
                MessageDxUtil.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 路径选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnoutputpath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog = new FolderBrowserDialog();

            if (folderbrowserdialog.ShowDialog() == DialogResult.OK)
            {
                txtoutputpath.Text = folderbrowserdialog.SelectedPath;
                string curdatetime = DateTimeHelper.GetServerDateTime();
                xmlprojectthelper.Replace("datatype/item[@guid=\"" + projectInfo.GUID + "\"]/lasttime", curdatetime);
                xmlprojectthelper.Save(false);
                txtlasttime.Text = curdatetime;
            }
        }

        /// <summary>
        /// 验证正确修改其对应的xml值 并更新其更新时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtValue_Validated(object sender, EventArgs e)
        {
            Control c = sender as Control;
            string result = string.Empty;
            string value = string.Empty;

            // 判断版本手工修改是否符合格式
            if (string.Equals(c.Name, "txtVersion"))
            {
                try
                {
                    new Version(c.Text);
                }
                catch (Exception ex)
                {
                    MessageDxUtil.ShowError(ex.Message);
                    return;
                }
            }

            switch (c.Name)
            {
                case "txtName":
                    result = "datatype/item[@guid=\"" + projectInfo.GUID + "\"]/name";
                    break;
                case "txtVersion":
                    result = "datatype/item[@guid=\"" + projectInfo.GUID + "\"]/version";
                    break;
                case "txtContract":
                    result = "datatype/item[@guid=\"" + projectInfo.GUID + "\"]/contract";
                    break;
                case "txtRemark":
                    result = "datatype/item[@guid=\"" + projectInfo.GUID + "\"]/remark";
                    break;
                case "cbbdbtype":
                    result = "datatype/item[@guid=\"" + projectInfo.GUID + "\"]/dbtype";
                    break;
                case "txtdicttype":
                    result = "datatype/item[@guid=\"" + projectInfo.GUID + "\"]/dicttype_table";
                    break;
                case "txtdictdata":
                    result = "datatype/item[@guid=\"" + projectInfo.GUID + "\"]/dictdata_table";
                    break;
                case "txterr":
                    result = "datatype/item[@guid=\"" + projectInfo.GUID + "\"]/err_table";
                    break;
                case "txtoutputpath":
                    result = "datatype/item[@guid=\"" + projectInfo.GUID + "\"]/outputpath";
                    break;
            }

            // 如果不在这些已知的控件中则停止
            if (string.IsNullOrEmpty(result)) {
                MessageDxUtil.ShowError("修改失败，其修改的控件不在已知范围内");
                return;
            } 

            string curdatetime = DateTimeHelper.GetServerDateTime();
            xmlprojectthelper.Replace(result, c.Name == "cbbdbtype" ? c.Text.Split('-')[0] : c.Text);
            xmlprojectthelper.Replace("datatype/item[@guid=\"" + projectInfo.GUID + "\"]/lasttime", curdatetime);
            xmlprojectthelper.Save(false);
            txtlasttime.Text = curdatetime;
        }
    }
}
