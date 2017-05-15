/*-----------当前类的问题----------------------
 * 1.treeView_ItemDrag方法里需要用到被拖动节点的图标，这个具体通过未来封装的BaseTreeNode自身包含的属性来实现。
 * 2.以后这里所有的TreeNode类型都应该替换为自定义扩展的子类类实现，即BaseTreeNode。
 * 3.有可能的话，拖动节点的活动应该记录到日志里。
*/
using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// TreeView的包装类，实现树的DragDrop拖拉的操作的辅助类。
    /// <code>
    ///private void Init()
    ///{
    ///    TreeViewDrager treeViewDrager = new TreeViewDrager(this.treeView1);
    ///    treeViewDrager.TreeImageList = this.imageList1;//不设置这个也可以，只是拖动的时候没图标。
    ///    treeViewDrager.ProcessDragNode += new ProcessDragNodeEventHandler(treeViewDrager_ProcessDragNode);
    ///}
    ///
    ///private bool treeViewDrager_ProcessDragNode(TreeNode from, TreeNode to)
    ///{
    ///    ///这里根据from/to两个节点记录的信息去进行数据库持久化的工作。
    ///    ///根据持久化的结果决定节点是否会最终实现拖动操作。
    ///    return true;
    ///}
    /// </code>
    /// </summary>
    public class TreeViewDrager
    {
        #region Data

        private Timer timer = new Timer();
        private TreeNode dragNode = null;
        private TreeNode tempDropNode = null;
        private TreeView tempTreeView;
        private ImageList imageListDrag = new ImageList();
		private ImageList imageListTreeView;
        private Image nodeImage;//将来用来记录节点的图标。

        /// <summary>
        /// 设置拖动树的时候，显示的图片，显示其中第一个
        /// </summary>
		public ImageList TreeImageList
		{
			get
			{
				return this.imageListTreeView;
			}
			set
			{
				this.imageListTreeView = value;
			}
		}

        #endregion // Data

        #region Constructors

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public TreeViewDrager()
        {
        }

        /// <summary>
        /// 参数构造函数，设置操作的TreeView
        /// </summary>
        /// <param name="treeView"></param>
        public TreeViewDrager(TreeView treeView)
        {
            this.tempTreeView = treeView;

            this.InitializeDrager();
        }

        private void InitializeDrager()
        {
            this.timer.Interval = 200;
            this.timer.Tick += new EventHandler(timer_Tick);

            this.tempTreeView.AllowDrop = true;
            this.tempTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView_DragDrop);
            this.tempTreeView.DragOver += new System.Windows.Forms.DragEventHandler(this.treeView_DragOver);
            this.tempTreeView.DragLeave += new System.EventHandler(this.treeView_DragLeave);
            this.tempTreeView.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.treeView_GiveFeedback);
            this.tempTreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView_DragEnter);
            this.tempTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView_ItemDrag);
        }
        #endregion // Constructors

        #region Interface

        /// <summary>
        /// 处理拖动节点后的事件
        /// </summary>
        public event ProcessDragNodeEventHandler ProcessDragNode;
		
        /// <summary>
        /// 当指定的计时器间隔已过去而且计时器处于启用状态时发生。
        /// </summary>
        private void timer_Tick(object sender, EventArgs e)
        {
            // 取得鼠标位置的节点
            Point pt = tempTreeView.PointToClient(Control.MousePosition);
            TreeNode node = this.tempTreeView.GetNodeAt(pt);

            if (node == null) return;

            // 如果鼠标靠近顶端，设置向上滚动条
            if (pt.Y < 30)
            {
                if (node.PrevVisibleNode != null)
                {
                    node = node.PrevVisibleNode;

                    DragHelper.ImageList_DragShowNolock(false);
                    node.EnsureVisible();
                    this.tempTreeView.Refresh();
                    DragHelper.ImageList_DragShowNolock(true);
                }
            }
            // 如果鼠标靠近底端，设置向下滚动条
            else if (pt.Y > this.tempTreeView.Size.Height - 30)
            {
                if (node.NextVisibleNode != null)
                {
                    node = node.NextVisibleNode;

                    DragHelper.ImageList_DragShowNolock(false);
                    node.EnsureVisible();
                    this.tempTreeView.Refresh();
                    DragHelper.ImageList_DragShowNolock(true);
                }
            }
        }

        /// <summary>
        /// 当用户开始拖动节点时发生。
        /// </summary>
        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // 取得拖动的节点，并选中它。
            this.dragNode = (TreeNode)e.Item;
            this.tempTreeView.SelectedNode = this.dragNode;
            //this.nodeImage =  //知道被拖动的节点，就应该知道节点的图标是什么？

            // 重新设置拖动节点的图标
            this.imageListDrag.Images.Clear();
            this.imageListDrag.ImageSize = new Size(this.dragNode.Bounds.Size.Width + this.tempTreeView.Indent, this.dragNode.Bounds.Height);

            // 创建新图标
            // 这个图标将包含被拖动的树节点的图象
            Bitmap bitmap = new Bitmap(this.dragNode.Bounds.Width + this.tempTreeView.Indent, this.dragNode.Bounds.Height);

            Graphics graphics = Graphics.FromImage(bitmap);

            // 在图标里包含被拖动节点的图象 
			if (this.imageListTreeView != null)
			{
				graphics.DrawImage(this.imageListTreeView.Images[0], 0, 0);
			}

            // 把被拖动节点的标签
            graphics.DrawString(this.dragNode.Text,
                this.tempTreeView.Font,
                new SolidBrush(this.tempTreeView.ForeColor),
                (float)this.tempTreeView.Indent, 1.0f);

            this.imageListDrag.Images.Add(bitmap);

			Point point = this.tempTreeView.PointToClient(Control.MousePosition);
			int dxHotspot = point.X + this.tempTreeView.Indent - this.dragNode.Bounds.Left;
			int dyHotspot = point.Y - this.dragNode.Bounds.Top;

            // 开始拖拽图象
			if (DragHelper.ImageList_BeginDrag(this.imageListDrag.Handle, 0, dxHotspot, dyHotspot))
            {
                // 开始
				this.tempTreeView.DoDragDrop(bitmap, DragDropEffects.Move);
                // 结束
                DragHelper.ImageList_EndDrag();
            }

        }

        /// <summary>
        /// 在将对象拖到控件的边界上发生。
        /// </summary>
        private void treeView_DragOver(object sender, DragEventArgs e)
        {
            // 计算拖动的位置并移动图片
			Point point = this.tempTreeView.PointToClient(new Point(e.X, e.Y));
			DragHelper.ImageList_DragMove(point.X - this.tempTreeView.Left, point.Y - this.tempTreeView.Top);

            // 取得实际拖拽到目标节点
			TreeNode nodeAt = this.tempTreeView.GetNodeAt(this.tempTreeView.PointToClient(new Point(e.X, e.Y)));
			if (nodeAt == null)
            {
                e.Effect = DragDropEffects.None;
			}
			else
			{
				e.Effect = DragDropEffects.Move;
				if (this.tempDropNode != nodeAt)
				{
					DragHelper.ImageList_DragShowNolock(false);
					this.tempTreeView.SelectedNode = nodeAt;
					DragHelper.ImageList_DragShowNolock(true);
					this.tempDropNode = nodeAt;
				}
				TreeNode treeNode = nodeAt;
				while (treeNode.Parent != null)
				{
					if (treeNode.Parent == this.dragNode)
					{
						e.Effect = DragDropEffects.None;
					}
					treeNode = treeNode.Parent;
				}
			}
		}

        /// <summary>
        /// 在完成拖放操作时发生，所有具体移动操作都发生在这里。
        /// </summary>
        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            // Unlock updates
            DragHelper.ImageList_DragLeave(this.tempTreeView.Handle);

            // 取得目标节点
			TreeNode nodeAt = this.tempTreeView.GetNodeAt(this.tempTreeView.PointToClient(new Point(e.X, e.Y)));

            // 如果拖拽的节点不等于目标节点，则增加拖拽的节点为目标节点的子节点。
			if (this.dragNode != nodeAt)
            {
                //委托出去做持久化操作，如果持久化成功，才能移动，否则不移动。
				if (this.ProcessDragNode != null && this.ProcessDragNode(this.dragNode, nodeAt))
                {
                    if (this.dragNode.Parent == null)
                    {
                        this.tempTreeView.Nodes.Remove(this.dragNode);
                    }
                    else
                    {
                        this.dragNode.Parent.Nodes.Remove(this.dragNode);
                    }

                    // 把拖拽的节点增加到目标节点上。
					nodeAt.Nodes.Add(this.dragNode);
					nodeAt.ExpandAll();

                    this.dragNode = null;
                    this.timer.Enabled = false;
                }
                else
				{
					MessageBox.Show("持久化失败，不能移动节点！");
				}
			}
		}

        /// <summary>
        /// 在将对象拖入控件的边界时发生。
        /// </summary>
        private void treeView_DragEnter(object sender, DragEventArgs e)
        {
			DragHelper.ImageList_DragEnter(this.tempTreeView.Handle, e.X - this.tempTreeView.Left, e.Y - this.tempTreeView.Top);
            this.timer.Enabled = true;
        }

        /// <summary>
        /// 在将对象拖出控件的边界时发生。
        /// </summary>
        private void treeView_DragLeave(object sender, EventArgs e)
        {
            DragHelper.ImageList_DragLeave(this.tempTreeView.Handle);
            this.timer.Enabled = false;
        }

        /// <summary>
        /// 在控件接收焦点时发生。
        /// </summary>
        private void treeView_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move)
            {
                // 显示拖动时候的鼠标样式
                e.UseDefaultCursors = false;
                this.tempTreeView.Cursor = Cursors.Default;
            }
			else
			{
				e.UseDefaultCursors = true;
			}
        }

        #endregion //Private Interface
    }

    /// <summary>
    /// 如果其他要用到跟事件有关的地方比较多就统一放到另一个Events类里。
    /// </summary>
    /// <param name="dragNode">拖动的节点</param>
    /// <param name="dropNode">放下的节点</param>
    /// <returns></returns>
    public delegate bool ProcessDragNodeEventHandler(TreeNode dragNode, TreeNode dropNode);

    /// <summary>
    /// TreeView 拖动辅助类
    /// </summary>
    internal class DragHelper
    {
        [DllImport("comctl32.dll")]
        public static extern bool InitCommonControls();

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_BeginDrag(IntPtr himlTrack, int
            iTrack, int dxHotspot, int dyHotspot);

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_DragMove(int x, int y);

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern void ImageList_EndDrag();

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_DragEnter(IntPtr hwndLock, int x, int y);

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_DragLeave(IntPtr hwndLock);

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_DragShowNolock(bool fShow);

        static DragHelper()
        {
            InitCommonControls();
        }
    }
}
