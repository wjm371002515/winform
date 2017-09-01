using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Collections;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;

namespace JCodes.Framework.CommonControl.Pager
{
    public partial class FrmSelectColumnDisplay : DevExpress.XtraEditors.XtraForm
    {
        private CheckedListBox mCheckedListBox;
        private GridView mDataGridView;

        /// <summary>
        /// 显示数据的DataGridView对象
        /// </summary>
        public GridView DataGridView
        {
            get { return mDataGridView; }
            set { mDataGridView = value; }
        }

        /// <summary>
        /// 显示的列名称（按顺序排列）
        /// </summary>
        public string DisplayColumNames
        {
            get;
            set;
        }

        /// <summary>
        /// 别名对照字典
        /// </summary>
        public Dictionary<string, string> ColumnNameAlias
        {
            get;
            set;
        }

        private int MaxHeight = 300;
        private int DisplayWidth = 200;
        private bool isSelectAll = false;

        public FrmSelectColumnDisplay()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取真正的列名称
        /// </summary>
        /// <param name="columnName">可能大小写不一样的列名</param>
        /// <returns></returns>
        private string GetRightColumnName(string columnName)
        {
            foreach (GridColumn c in mDataGridView.Columns)
            {
                if (c.FieldName.Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return c.FieldName;
            }
            return "";
        }

        private string GetColumnNameAlias(string name)
        {
            if (ColumnNameAlias.ContainsKey(name.ToUpper()))
            {
                return ColumnNameAlias[name.ToUpper()];
            }
            else
            {
                return name;
            }
        }

        private void Init()
        {
            mCheckedListBox = new CheckedListBox();
            mCheckedListBox.CheckOnClick = true;
            mCheckedListBox.ItemCheck += new ItemCheckEventHandler(mCheckedListBox_ItemCheck);

            mCheckedListBox.Items.Clear();
            foreach (string columnName in DisplayColumNames.Split(new char[] { '|', ',' }))
            {
                string newName = GetRightColumnName(columnName);
                if (!string.IsNullOrEmpty(newName))
                {
                    mCheckedListBox.Items.Add(new CListItem(newName, GetColumnNameAlias(newName)), true);
                }
            }

            int PreferredHeight = (mCheckedListBox.Items.Count * 16) + 7;
            mCheckedListBox.Height = (PreferredHeight < MaxHeight) ? PreferredHeight : MaxHeight;
            mCheckedListBox.Width = this.DisplayWidth;
            mCheckedListBox.Dock = DockStyle.Fill;

            this.panel1.Controls.Clear();
            this.panel1.Controls.Add(mCheckedListBox);
        }

        void mCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CListItem item = this.mCheckedListBox.Items[e.Index] as CListItem;
            if (item != null)
            {
                mDataGridView.Columns[item.Value].Visible = (e.NewValue == CheckState.Checked);
            }
            //mDataGridView.Columns[e.Index].Visible = (e.NewValue == CheckState.Checked);
        }

        private void FrmSelectColumnDisplay_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            isSelectAll = chkSelectAll.Checked;
            for (int i = 0; i < this.mCheckedListBox.Items.Count; i++)
            {
                this.mCheckedListBox.SetItemChecked(i, isSelectAll);
            }
        }

        private void chkInverse_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.mCheckedListBox.Items.Count; i++)
            {
                this.mCheckedListBox.SetItemChecked(i, !this.mCheckedListBox.GetItemChecked(i));
            }
        }
    }
}
