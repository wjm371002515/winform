using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace JCodes.Framework.CommonControl
{
    public class SortableListView : ListView
    {
        int _sortOrder;
        int _sortColumn;
        Bitmap _imageAscending;
        Bitmap _imageDescending;

        public SortableListView()
        {
            this._sortColumn = 0;
            this._sortOrder = 1;
            this._imageAscending = new Bitmap(JCodes.Framework.CommonControl.Properties.Resource.up);
            this._imageAscending.MakeTransparent(System.Drawing.Color.Magenta);
            this._imageDescending = new Bitmap(JCodes.Framework.CommonControl.Properties.Resource.down);
            this._imageDescending.MakeTransparent(System.Drawing.Color.Magenta);
            this.BorderStyle = BorderStyle.None;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FullRowSelect = true;
            this.HideSelection = false;
            this.LabelEdit = true;
            this.LabelWrap = false;
            this.View = System.Windows.Forms.View.Details;
            this.Sorting = SortOrder.None;
            this.AllowColumnReorder = true;
            this.OwnerDraw = true;
            this.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(SortableListView_DrawColumnHeader);
            this.DrawItem += new DrawListViewItemEventHandler(SortableListView_DrawItem);
            this.DrawSubItem += new DrawListViewSubItemEventHandler(SortableListView_DrawSubItem);
            this.ColumnClick += new ColumnClickEventHandler(SortableListView_ColumnClick);
            this.ColumnReordered += new ColumnReorderedEventHandler(SortableListView_ColumnReordered);
        }

        void SortableListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        void SortableListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        void SortableListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            bool fSorted = (this._sortColumn == e.ColumnIndex);

            if (fSorted)
            {
                e.DrawBackground();
                e.DrawText(TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

                if (fSorted && clickColumn)
                {
                    Point ptImage = new Point(e.Bounds.Left + (int)e.Graphics.MeasureString(e.Header.Text + "XY", e.Font).Width, (e.Bounds.Top + e.Bounds.Bottom - _imageAscending.Height) / 2);
                    e.Graphics.DrawImage((this._sortOrder > 0) ? _imageAscending : _imageDescending, ptImage);
                    //this.Refresh();

                    clickColumn = !clickColumn;
                }
                
            }
            else
            {
                e.DrawDefault = true;
            }            
        }

        public void AddColumns(params string[] columns)
        {
            Columns.Clear();

            foreach (string columnName in columns)
            {
                this.Columns.Add(columnName, 120);
            }

            this.ListViewItemSorter = new ListViewItemComparer(this._sortColumn, this._sortOrder);
        }

        void SortableListView_ColumnReordered(object sender, ColumnReorderedEventArgs e)
        {
            if (e.OldDisplayIndex == this._sortColumn)
            {
                this._sortColumn = e.NewDisplayIndex;
                this.ListViewItemSorter = new ListViewItemComparer(this._sortColumn, this._sortOrder);
            }
        }

        bool clickColumn = false;
        void SortableListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this._sortColumn != e.Column)
            {
                this._sortColumn = e.Column;
                this._sortOrder = 1;
            }
            else
            {
                this._sortOrder *= -1;
            }

            clickColumn = true;

            // Sort by the column
            this.ListViewItemSorter = new ListViewItemComparer(this._sortColumn, this._sortOrder);
        }

        class ListViewItemComparer : IComparer
        {
            int col;
            int order;

            public ListViewItemComparer(int column, int directiron)
            {
                this.order = directiron;
                this.col = column;
            }

            public int Compare(object x, object y)
            {
                return Math.Sign(order) * String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }
        }
    }
}
