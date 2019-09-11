using JCodes.Framework.CommonControl.Controls;
using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Office;
using JCodes.Framework.CommonControl.AdvanceSearch;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.CommonControl.Pager.Others;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JCodes.Framework.Common.Format;
using JCodes.Framework.jCodesenum;
using JCodes.Framework.Common.Extension;

namespace JCodes.Framework.AddIn.Contact
{
    /// <summary>
    /// 客户组别
    /// </summary>	
    public partial class FrmAddressGroup : BaseDock
    {
        /// <summary>
        /// 通讯录类型
        /// </summary>
        public AddressType AddressType = AddressType.个人;

        public FrmAddressGroup()
        {
            InitializeComponent();

            this.winGridViewPager1.OnPageChanged += new EventHandler(winGridViewPager1_OnPageChanged);
            this.winGridViewPager1.OnStartExport += new EventHandler(winGridViewPager1_OnStartExport);
            this.winGridViewPager1.OnEditSelected += new EventHandler(winGridViewPager1_OnEditSelected);
            this.winGridViewPager1.OnAddNew += new EventHandler(winGridViewPager1_OnAddNew);
            this.winGridViewPager1.OnDeleteSelected += new EventHandler(winGridViewPager1_OnDeleteSelected);
            this.winGridViewPager1.OnRefresh += new EventHandler(winGridViewPager1_OnRefresh);
            this.winGridViewPager1.AppendedMenu = this.contextMenuStrip1;
            this.winGridViewPager1.ShowLineNumber = true;
            this.winGridViewPager1.BestFitColumnWith = false;//是否设置为自动调整宽度，false为不设置
            this.winGridViewPager1.gridView1.DataSourceChanged += new EventHandler(gridView1_DataSourceChanged);
            this.winGridViewPager1.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(gridView1_CustomColumnDisplayText);
        }

        private void FrmAddressGroup_Load(object sender, EventArgs e)
        {
            Init_Function();
        }

        void Init_Function()
        {
            if (AddressType.个人 == AddressType)
            {
                tsbNew.Enabled = HasFunction("PersonalAddress/GroupAdd");
                tsbEdit.Enabled = HasFunction("PersonalAddress/GroupEdit");
                tsbDelete.Enabled = HasFunction("PersonalAddress/GroupDel");
                btnImport.Enabled = HasFunction("PersonalAddress/Import");
                btnExport.Enabled = HasFunction("PersonalAddress/Export");

            }
            if (AddressType.公共 == AddressType)
            {
                tsbNew.Enabled = HasFunction("CommonAddress/GroupAdd");
                tsbEdit.Enabled = HasFunction("CommonAddress/GroupEdit");
                tsbDelete.Enabled = HasFunction("CommonAddress/GroupDel");
                btnImport.Enabled = HasFunction("CommonAddress/Import");
                btnExport.Enabled = HasFunction("CommonAddress/Export");
            }
        }

        void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.ColumnType == typeof(DateTime))
            {
                string columnName = e.Column.FieldName;
                if (e.Value != null)
                {
                    if (Convert.ToDateTime(e.Value) <= Convert.ToDateTime("1900-1-1"))
                    {
                        e.DisplayText = "";
                    }
                    else
                    {
                        e.DisplayText = Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd HH:mm");//yyyy-MM-dd
                    }
                }
            }
        }

        /// <summary>
        /// 绑定数据后，分配各列的宽度
        /// </summary>
        private void gridView1_DataSourceChanged(object sender, EventArgs e)
        {
            if (this.winGridViewPager1.gridView1.Columns.Count > 0 && this.winGridViewPager1.gridView1.RowCount > 0)
            {
                //统一设置100宽度
                foreach (DevExpress.XtraGrid.Columns.GridColumn column in this.winGridViewPager1.gridView1.Columns)
                {
                    column.Width = 100;
                }

                //可特殊设置特别的宽度
                winGridViewPager1.gridView1.SetGridColumWidth("Name", 200);
                winGridViewPager1.gridView1.SetGridColumWidth("Note", 200);
            }
        }

        /// <summary>
        /// 编写初始化窗体的实现，可以用于刷新
        /// </summary>
        public override void FormOnLoad()
        {
            BindData();
        }

        /// <summary>
        /// 分页控件刷新操作
        /// </summary>
        private void winGridViewPager1_OnRefresh(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 分页控件删除操作
        /// </summary>
        private void winGridViewPager1_OnDeleteSelected(object sender, EventArgs e)
        {
            if (AddressType.个人 == AddressType && !HasFunction("PersonalAddress/GroupDel"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }
            if (AddressType.公共 == AddressType && !HasFunction("CommonAddress/GroupDel"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            if (MessageDxUtil.ShowYesNoAndTips("您确定删除选定的记录么？") == DialogResult.No)
            {
                return;
            }

            int[] rowSelected = this.winGridViewPager1.GridView1.GetSelectedRows();
            foreach (int iRow in rowSelected)
            {
                string ID = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "ID");
                BLLFactory<AddressGroup>.Instance.DeleteByUser(ID, LoginUserInfo.Id);
            }

            BindData();
        }

        /// <summary>
        /// 分页控件编辑项操作
        /// </summary>
        private void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
        {
            if (AddressType.个人 == AddressType && !HasFunction("PersonalAddress/GroupEdit"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }
            if (AddressType.公共 == AddressType && !HasFunction("CommonAddress/GroupEdit"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            Int32 Id = this.winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("ID").ToInt32();
            List<Int32> IdList = new List<Int32>();
            for (int i = 0; i < this.winGridViewPager1.gridView1.RowCount; i++)
            {
                Int32 intTemp = this.winGridViewPager1.GridView1.GetRowCellDisplayText(i, "ID").ToInt32();
                IdList.Add(intTemp);
            }

            if (Id > 0)
            {
                FrmEditAddressGroup dlg = new FrmEditAddressGroup();
                dlg.addressType = this.AddressType;
                dlg.Id = Id;
                dlg.IdList = IdList;
                dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
                dlg.InitFunction(LoginUserInfo, FunctionDict);//给子窗体赋值用户权限信息

                if (DialogResult.OK == dlg.ShowDialog())
                {
                    BindData();
                }
            }
        }

        void dlg_OnDataSaved(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 分页控件新增操作
        /// </summary>        
        private void winGridViewPager1_OnAddNew(object sender, EventArgs e)
        {
            if (AddressType.个人 == AddressType && !HasFunction("PersonalAddress/GroupAdd"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }
            if (AddressType.公共 == AddressType && !HasFunction("CommonAddress/GroupAdd"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            btnAddNew_Click(null, null);
        }

        /// <summary>
        /// 分页控件全部导出操作前的操作
        /// </summary> 
        private void winGridViewPager1_OnStartExport(object sender, EventArgs e)
        {
            if (AddressType.个人 == AddressType && !HasFunction("PersonalAddress/Export"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }
            if (AddressType.公共 == AddressType && !HasFunction("CommonAddress/Export"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            string where = GetConditionSql();
            this.winGridViewPager1.AllToExport = BLLFactory<AddressGroup>.Instance.FindToDataTable(where);
        }

        /// <summary>
        /// 分页控件翻页的操作
        /// </summary> 
        private void winGridViewPager1_OnPageChanged(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 高级查询条件语句对象
        /// </summary>
        private SearchCondition advanceCondition;

        /// <summary>
        /// 根据查询条件构造查询语句
        /// </summary> 
        private string GetConditionSql()
        {
            //如果存在高级查询对象信息，则使用高级查询条件，否则使用主表条件查询
            SearchCondition condition = advanceCondition;
            if (condition == null)
            {
                condition = new SearchCondition();
                //condition.AddCondition("UserCode", this.txtHandNo.Text.Trim(), SqlOperator.Like);
            }
            
            condition.AddCondition("AddressType", AddressType.ToString(), SqlOperator.Equal);
            if (AddressType == AddressType.个人)
            {                
                condition.AddCondition("Creator", LoginUserInfo.Id, SqlOperator.Equal);
            }

            string where = condition.BuildConditionSql().Replace("Where", "");
            return where;
        }

        /// <summary>
        /// 绑定列表数据
        /// </summary>
        private void BindData()
        {
            //entity
            this.winGridViewPager1.DisplayColumns = "Name,Note,Editor,EditTime,Seq";
            this.winGridViewPager1.ColumnNameAlias = BLLFactory<AddressGroup>.Instance.GetColumnNameAlias();//字段列显示名称转义

            #region 添加别名解析

            //this.winGridViewPager1.AddColumnAlias("Seq", "排序序号");
            //this.winGridViewPager1.AddColumnAlias("Name", "分组名称");
            //this.winGridViewPager1.AddColumnAlias("Note", "备注");
            //this.winGridViewPager1.AddColumnAlias("Editor", "编辑人");
            //this.winGridViewPager1.AddColumnAlias("EditTime", "编辑时间");

            #endregion

            string where = GetConditionSql();
            List<AddressGroupInfo> list = BLLFactory<AddressGroup>.Instance.FindWithPager(where, this.winGridViewPager1.PagerInfo);
            this.winGridViewPager1.DataSource = new SortableBindingList<AddressGroupInfo>(list);
            this.winGridViewPager1.PrintTitle = "客户组别报表";
        }

        /// <summary>
        /// 查询数据操作
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            advanceCondition = null;//必须重置查询条件，否则可能会使用高级查询条件了
            BindData();
        }

        /// <summary>
        /// 新增数据操作
        /// </summary>
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            FrmEditAddressGroup dlg = new FrmEditAddressGroup();
            dlg.addressType = this.AddressType;
            dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
            dlg.InitFunction(LoginUserInfo, FunctionDict);//给子窗体赋值用户权限信息

            if (DialogResult.OK == dlg.ShowDialog())
            {
                BindData();
            }
        }

        private string moduleName = "通讯录组别";
        /// <summary>
        /// 导入Excel的操作
        /// </summary>          
        private void btnImport_Click(object sender, EventArgs e)
        {
            string templateFile = string.Format("{0}-模板.xls", moduleName);
            FrmImportExcelData dlg = new FrmImportExcelData();
            dlg.SetTemplate(templateFile, System.IO.Path.Combine(Application.StartupPath, templateFile));
            dlg.OnDataSave += new FrmImportExcelData.SaveDataHandler(ExcelData_OnDataSave);
            dlg.OnRefreshData += new EventHandler(ExcelData_OnRefreshData);
            dlg.ShowDialog();
        }

        void ExcelData_OnRefreshData(object sender, EventArgs e)
        {
            BindData();
        }

        bool ExcelData_OnDataSave(DataRow dr)
        {
            bool success = false;
            DateTime dtDefault = Convert.ToDateTime("1900-01-01");
            AddressGroupInfo info = new AddressGroupInfo();
            //info.PID = "-1";
            info.Seq = dr["排序序号"].ToString();
            info.Name = dr["分组名称"].ToString();
            info.Remark = dr["备注"].ToString();
            info.CreatorId = LoginUserInfo.Id;
            info.CreatorTime = DateTimeHelper.GetServerDateTime2();
            info.DeptId = LoginUserInfo.DeptId;
            info.CompanyId = LoginUserInfo.CompanyId;

            success = BLLFactory<AddressGroup>.Instance.Insert(info);
            return success;
        }

        /// <summary>
        /// 导出Excel的操作
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            string file = FileDialogHelper.SaveExcel(string.Format("{0}.xls", moduleName));
            if (!string.IsNullOrEmpty(file))
            {
                string where = GetConditionSql();
                List<AddressGroupInfo> list = BLLFactory<AddressGroup>.Instance.Find(where);
                DataTable dtNew = DataTableHelper.CreateTable("序号|int,排序序号,分组名称,备注");
                DataRow dr;
                int j = 1;
                for (int i = 0; i < list.Count; i++)
                {
                    dr = dtNew.NewRow();
                    dr["序号"] = j++;
                    dr["排序序号"] = list[i].Seq;
                    dr["分组名称"] = list[i].Name;
                    dr["备注"] = list[i].Remark;
                    dtNew.Rows.Add(dr);
                }

                try
                {
                    string error = "";
                    AsposeExcelTools.DataTableToExcel2(dtNew, file, out error);
                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageDxUtil.ShowError(string.Format("导出Excel出现错误：{0}", error));
                    }
                    else
                    {
                        if (MessageDxUtil.ShowYesNoAndTips("导出成功，是否打开文件？") == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(file);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmAddressGroup));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            winGridViewPager1_OnAddNew(null, null);
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            winGridViewPager1_OnEditSelected(null, null);
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            winGridViewPager1_OnDeleteSelected(null, null);
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            BindData();
        }
    }
}
