using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;

namespace JCodes.Framework.Common.Winform
{
    /// <summary>
    /// 可拖拉的ListView封装控件
    /// </summary>
    public class DragAndDropListView : ListView
    {
        /// <summary>
        /// 处理拖放列表项的委托定义。
        /// dragItemData代表移动的列表项目;hoveItem代表插入项在它的前面，如果放到最后，则为hoveItem为NULL
        /// </summary>
        public delegate bool ProcessDragItemEventHandler(DragItemData dragItemData, ListViewItem hoveItem);

        #region Private Members

        private ListViewItem m_previousItem;
        private bool m_allowReorder;
        private Color m_lineColor;

        #endregion

        #region Public Properties

        [Category("Behavior"), Description("允许重新排序")]
        public bool AllowReorder
        {
            get { return m_allowReorder; }
            set { m_allowReorder = value; }
        }

        [Category("Appearance"), Description("拖拉的线条颜色显示")]
        public Color LineColor
        {
            get { return m_lineColor; }
            set { m_lineColor = value; }
        }

        /// <summary>
        /// 移动事件处理:dragItemData代表移动的列表项目;hoveItem代表插入项在它的前面，如果放到最后，则为hoveItem为NULL
        /// </summary>
        public event ProcessDragItemEventHandler ProcessDragItem;

        #endregion

        #region Protected and Public Methods

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DragAndDropListView() : base()
        {
            m_allowReorder = true;
            m_lineColor = Color.Red;
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            if (!m_allowReorder)
            {
                base.OnDragDrop(drgevent);
                return;
            }

            // get the currently hovered row that the items will be dragged to
            Point clientPoint = base.PointToClient(new Point(drgevent.X, drgevent.Y));
            ListViewItem hoverItem = base.GetItemAt(clientPoint.X, clientPoint.Y);

            if (!drgevent.Data.GetDataPresent(typeof(DragItemData).ToString()) || ((DragItemData)drgevent.Data.GetData(typeof(DragItemData).ToString())).ListView == null || ((DragItemData)drgevent.Data.GetData(typeof(DragItemData).ToString())).DragItems.Count == 0)
                return;

            // retrieve the drag item data
            DragItemData data = (DragItemData)drgevent.Data.GetData(typeof(DragItemData).ToString());

            //拖拉触发事件
            if (ProcessDragItem != null && !ProcessDragItem(data, hoverItem))
            {
                return;//如果没有处理通过，则不继续
            }

            if (hoverItem == null)
            {
                // the user does not wish to re-order the items, just append to the end
                for (int i = 0; i < data.DragItems.Count; i++)
                {
                    ListViewItem newItem = (ListViewItem)data.DragItems[i];
                    base.Items.Add(newItem);
                }
            }
            else
            {
                // the user wishes to re-order the items
                // get the index of the hover item
                int hoverIndex = hoverItem.Index;

                // determine if the items to be dropped are from
                // this list view. If they are, perform a hack
                // to increment the hover index so that the items
                // get moved properly.
                if (this == data.ListView)
                {
					if(hoverIndex > base.SelectedItems[0].Index)
						hoverIndex++;
				}

                // insert the new items into the list view
                // by inserting the items reversely from the array list
                for (int i = data.DragItems.Count - 1; i >= 0; i--)
                {
                    ListViewItem newItem = (ListViewItem)data.DragItems[i];
                    base.Items.Insert(hoverIndex, newItem);
                    int posIndex = hoverIndex + 1 > (base.Items.Count - 1) ? (base.Items.Count - 1) : hoverIndex + 1;
                    base.Items[hoverIndex].Position = base.Items[posIndex].Position;
                }
            }

            // remove all the selected items from the previous list view
            // if the list view was found
            if (data.ListView != null)
            {
                foreach (ListViewItem itemToRemove in data.ListView.SelectedItems)
                {
                    data.ListView.Items.Remove(itemToRemove);
                }
            }

            this.Refresh();
            // set the back color of the previous item, then nullify it
            if (m_previousItem != null)
            {
                m_previousItem = null;
            }

            this.Invalidate();

            // call the base on drag drop to raise the event
            base.OnDragDrop(drgevent);
        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            if (!m_allowReorder)
            {
                base.OnDragOver(drgevent);
                return;
            }

            if (!drgevent.Data.GetDataPresent(typeof(DragItemData).ToString()))
            {
                // the item(s) being dragged do not have any data associated
                drgevent.Effect = DragDropEffects.None;
                return;
            }

            if (base.Items.Count > 0)
            {
                // get the currently hovered row that the items will be dragged to
                Point clientPoint = base.PointToClient(new Point(drgevent.X, drgevent.Y));
                ListViewItem hoverItem = base.GetItemAt(clientPoint.X, clientPoint.Y);

                Graphics g = this.CreateGraphics();

				if(hoverItem == null)
				{
					//MessageBox.Show(base.GetChildAtPoint(new Point(clientPoint.X, clientPoint.Y)).GetType().ToString());

                    // no item was found, so no drop should take place
                    drgevent.Effect = DragDropEffects.Move;

                    if (m_previousItem != null)
                    {
                        m_previousItem = null;
                        Invalidate();
                    }

                    hoverItem = base.Items[base.Items.Count - 1];

                    if (this.View == View.Details || this.View == View.List)
                    {
                        g.DrawLine(new Pen(m_lineColor, 2), new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y + hoverItem.Bounds.Height), new Point(hoverItem.Bounds.X + this.Bounds.Width, hoverItem.Bounds.Y + hoverItem.Bounds.Height));
						g.FillPolygon(new SolidBrush(m_lineColor), new Point[] {new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y + hoverItem.Bounds.Height - 5), new Point(hoverItem.Bounds.X + 5, hoverItem.Bounds.Y + hoverItem.Bounds.Height), new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y + hoverItem.Bounds.Height + 5)});
						g.FillPolygon(new SolidBrush(m_lineColor), new Point[] {new Point(this.Bounds.Width - 4, hoverItem.Bounds.Y + hoverItem.Bounds.Height - 5), new Point(this.Bounds.Width - 9, hoverItem.Bounds.Y + hoverItem.Bounds.Height), new Point(this.Bounds.Width - 4, hoverItem.Bounds.Y + hoverItem.Bounds.Height + 5)});
                    }
                    else
                    {
                        g.DrawLine(new Pen(m_lineColor, 2), new Point(hoverItem.Bounds.X + hoverItem.Bounds.Width, hoverItem.Bounds.Y), new Point(hoverItem.Bounds.X + hoverItem.Bounds.Width, hoverItem.Bounds.Y + hoverItem.Bounds.Height));
						g.FillPolygon(new SolidBrush(m_lineColor), new Point[] {new Point(hoverItem.Bounds.X + hoverItem.Bounds.Width - 5, hoverItem.Bounds.Y), new Point(hoverItem.Bounds.X + hoverItem.Bounds.Width + 5, hoverItem.Bounds.Y), new Point(hoverItem.Bounds.X + hoverItem.Bounds.Width, hoverItem.Bounds.Y + 5)});
						g.FillPolygon(new SolidBrush(m_lineColor), new Point[] {new Point(hoverItem.Bounds.X + hoverItem.Bounds.Width - 5, hoverItem.Bounds.Y + hoverItem.Bounds.Height), new Point(hoverItem.Bounds.X + hoverItem.Bounds.Width + 5, hoverItem.Bounds.Y + hoverItem.Bounds.Height), new Point(hoverItem.Bounds.X + hoverItem.Bounds.Width, hoverItem.Bounds.Y + hoverItem.Bounds.Height - 5)});
                    }

                    // call the base OnDragOver event
                    base.OnDragOver(drgevent);

                    return;
                }

                // determine if the user is currently hovering over a new
                // item. If so, set the previous item's back color back
                // to the default color.
                if ((m_previousItem != null && m_previousItem != hoverItem) || m_previousItem == null)
                {
                    this.Invalidate();
                }

                // set the background color of the item being hovered
                // and assign the previous item to the item being hovered
                //hoverItem.BackColor = Color.Beige;
                m_previousItem = hoverItem;

                if (this.View == View.Details || this.View == View.List)
                {
                    g.DrawLine(new Pen(m_lineColor, 2), new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y/* + hoverItem.Bounds.Height*/), new Point(hoverItem.Bounds.X + this.Bounds.Width, hoverItem.Bounds.Y /*+ 2 *hoverItem.Bounds.Height*/));
					g.FillPolygon(new SolidBrush(m_lineColor), new Point[] {new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y - 5), new Point(hoverItem.Bounds.X + 5, hoverItem.Bounds.Y), new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y + 5)});
					g.FillPolygon(new SolidBrush(m_lineColor), new Point[] {new Point(this.Bounds.Width - 4, hoverItem.Bounds.Y - 5), new Point(this.Bounds.Width - 9, hoverItem.Bounds.Y), new Point(this.Bounds.Width - 4, hoverItem.Bounds.Y + 5)});
                }
                else
                {
                    g.DrawLine(new Pen(m_lineColor, 2), new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y), new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y + hoverItem.Bounds.Height));
					g.FillPolygon(new SolidBrush(m_lineColor), new Point[] {new Point(hoverItem.Bounds.X - 5, hoverItem.Bounds.Y), new Point(hoverItem.Bounds.X + 5, hoverItem.Bounds.Y), new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y + 5)});
					g.FillPolygon(new SolidBrush(m_lineColor), new Point[] {new Point(hoverItem.Bounds.X - 5, hoverItem.Bounds.Y + hoverItem.Bounds.Height), new Point(hoverItem.Bounds.X + 5, hoverItem.Bounds.Y + hoverItem.Bounds.Height), new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y + hoverItem.Bounds.Height - 5)});
                }

                // go through each of the selected items, and if any of the
                // selected items have the same index as the item being
                // hovered, disable dropping.
                foreach (ListViewItem itemToMove in base.SelectedItems)
                {
                    if (itemToMove.Index == hoverItem.Index)
                    {
                        drgevent.Effect = DragDropEffects.None;
                        hoverItem.EnsureVisible();
                        return;
                    }
                }

                // ensure that the hover item is visible
                hoverItem.EnsureVisible();
            }

            // everything is fine, allow the user to move the items
            drgevent.Effect = DragDropEffects.Move;

            // call the base OnDragOver event
            base.OnDragOver(drgevent);
        }

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            if (!m_allowReorder)
            {
                base.OnDragEnter(drgevent);
                return;
            }

            if (!drgevent.Data.GetDataPresent(typeof(DragItemData).ToString()))
            {
                // the item(s) being dragged do not have any data associated
                drgevent.Effect = DragDropEffects.None;
                return;
            }

            // everything is fine, allow the user to move the items
            drgevent.Effect = DragDropEffects.Move;

            // call the base OnDragEnter event
            base.OnDragEnter(drgevent);
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            if (!m_allowReorder)
            {
                base.OnItemDrag(e);
                return;
            }

            // call the DoDragDrop method
            base.DoDragDrop(GetDataForDragDrop(), DragDropEffects.Move);

            // call the base OnItemDrag event
            base.OnItemDrag(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            // reset the selected items background and remove the previous item
            ResetOutOfRange();

            Invalidate();

            // call the OnLostFocus event
            base.OnLostFocus(e);
        }

        protected override void OnDragLeave(EventArgs e)
        {
            // reset the selected items background and remove the previous item
            ResetOutOfRange();

            Invalidate();

            // call the base OnDragLeave event
            base.OnDragLeave(e);
        }

        #endregion

        #region Private Methods

        private DragItemData GetDataForDragDrop()
        {
            // create a drag item data object that will be used to pass along with the drag and drop
            DragItemData data = new DragItemData(this);

            // go through each of the selected items and 
            // add them to the drag items collection
            // by creating a clone of the list item
            foreach (ListViewItem item in this.SelectedItems)
            {
                data.DragItems.Add(item.Clone());
            }

            return data;
        }

        private void ResetOutOfRange()
        {
            // determine if the previous item exists,
            // if it does, reset the background and release 
            // the previous item
            if (m_previousItem != null)
            {
                m_previousItem = null;
            }

        }

        #endregion

        #region DragItemData Class

        public class DragItemData
        {
            #region Private Members

            private DragAndDropListView m_listView;
            private ArrayList m_dragItems;

            #endregion

            #region Public Properties

            public DragAndDropListView ListView
            {
                get { return m_listView; }
            }

            public ArrayList DragItems
            {
                get { return m_dragItems; }
            }

            #endregion

            #region Public Methods and Implementation

            public DragItemData(DragAndDropListView listView)
            {
                m_listView = listView;
                m_dragItems = new ArrayList();
            }

            #endregion
        }

        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }
    }

}
