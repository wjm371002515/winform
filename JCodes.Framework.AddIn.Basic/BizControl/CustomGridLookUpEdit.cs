using System;
using System.Data;
using System.Text;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.Data.Filtering;
using DevExpress.XtraGrid.Columns;
using JCodes.Framework.Common;

namespace JCodes.Framework.AddIn.Basic.BizControl
{
    [UserRepositoryItem("RegisterCustomGridLookUpEdit")]
    public class RepositoryItemCustomGridLookUpEdit : RepositoryItemGridLookUpEdit
    {
        static RepositoryItemCustomGridLookUpEdit() { RegisterCustomGridLookUpEdit(); }

        public RepositoryItemCustomGridLookUpEdit()
        {
            TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            AutoComplete = false;
        }
        [Browsable(false)]
        public override DevExpress.XtraEditors.Controls.TextEditStyles TextEditStyle
        {
            get
            {
                return base.TextEditStyle;
            }
            set { base.TextEditStyle = value; }
        }

        public override string EditorTypeName { get { return Const.CustomGridLookUpEditName; } }

        public static void RegisterCustomGridLookUpEdit()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(Const.CustomGridLookUpEditName,
              typeof(CustomGridLookUpEdit), typeof(RepositoryItemCustomGridLookUpEdit),
              typeof(GridLookUpEditBaseViewInfo), new ButtonEditPainter(), true));
        }

        protected override GridView CreateViewInstance() { return new CustomGridView(); }
        protected override GridControl CreateGrid() { return new CustomGridControl(); }
    }
    
    /// <summary>
    /// 自定义的GridLookUpEdit控件
    /// </summary>
    public class CustomGridLookUpEdit : GridLookUpEdit
    {
        /// <summary>
        /// 是否禁止新增内容
        /// </summary>
        [Browsable(true), Description("是否禁止新增内容")]
        public bool DisableAddNew { get; set; }

        static CustomGridLookUpEdit()
        {
            RepositoryItemCustomGridLookUpEdit.RegisterCustomGridLookUpEdit();
        }

        public CustomGridLookUpEdit() : base() 
        {
            //初始化一些状态
            this.Properties.PopupFilterMode = PopupFilterMode.Contains;//包含即可
            this.Properties.ImmediatePopup = true;//是否马上弹出窗体
            this.Properties.ValidateOnEnterKey = true;//回车确认
            this.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;//文本框可输入
            this.Properties.NullText = "";
            this.Properties.NullValuePrompt = "";

            this.ProcessNewValue += new DevExpress.XtraEditors.Controls.ProcessNewValueEventHandler(CustomGridLookUpEdit_ProcessNewValue);            
        }
        
        /// <summary>
        /// 实现在列表没有记录的时候，可以录入一个不存在的记录，类似ComoboEidt功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CustomGridLookUpEdit_ProcessNewValue(object sender, DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs e)
        {
            if (!DisableAddNew && !this.DesignMode)
            {
                string displayName = this.Properties.DisplayMember;
                string valueName = this.Properties.ValueMember;
                string display = e.DisplayValue.ToString();

                DataTable dtTemp = this.Properties.DataSource as DataTable;
                if (dtTemp != null)
                {
                    DataRow[] selectedRows = dtTemp.Select(string.Format("{0}='{1}'", displayName, display.Replace("'", "‘")));
                    if (selectedRows == null || selectedRows.Length == 0)
                    {
                        DataRow row = dtTemp.NewRow();
                        row[displayName] = display;
                        row[valueName] = display;
                        dtTemp.Rows.Add(row);
                        dtTemp.AcceptChanges();
                    }
                }

                e.Handled = true;
            }
        }

        public override string EditorTypeName
        {
            get
            {
                return Const.CustomGridLookUpEditName;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemCustomGridLookUpEdit Properties
        {
            get
            {
                return base.Properties as RepositoryItemCustomGridLookUpEdit;
            }
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if (!DisableAddNew && !this.DesignMode)
                {
                    //如果列表里面没有，添加一个，否则无法显示
                    string displayName = this.Properties.DisplayMember;
                    string valueName = this.Properties.ValueMember;
                    string display = value;

                    DataTable dtTemp = this.Properties.DataSource as DataTable;
                    if (dtTemp != null)
                    {
                        DataRow[] selectedRows = dtTemp.Select(string.Format("{0}='{1}'", displayName, display.Replace("'", "‘")));
                        if (selectedRows == null || selectedRows.Length == 0)
                        {
                            DataRow row = dtTemp.NewRow();
                            row[displayName] = display;
                            row[valueName] = display;
                            dtTemp.Rows.Add(row);
                            dtTemp.AcceptChanges();
                        }
                    }
                    base.Text = value;
                }
                else
                {
                    base.Text = value;
                }
            }
        }

    }
}