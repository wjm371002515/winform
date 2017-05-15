using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.CommonControl.Settings
{
    /// <summary>
    /// 参数配置界面控件
    /// </summary>
    public partial class FirefoxDialog : DevExpress.XtraEditors.XtraUserControl
	{
		#region Data Members

		private PropertyPage activePage = null;
		private Dictionary<string, PageProp> pages = new Dictionary<string, PageProp>();
		
		#endregion

        /// <summary>
        /// 构造函数
        /// </summary>
		public FirefoxDialog()
		{
			InitializeComponent();
		}

		#region Private
		private void AddPage(MozItem item, PropertyPage page)
		{
			PageProp pageProp = new PageProp();
			pageProp.Page = page;
			pageProp.MozItem = item;

			this.mozPane1.Items.Add(item);

			this.pages.Add(item.Name, pageProp);
		}

		private MozItem GetMozItem(string text)
		{
			return this.GetMozItem(text, this.ImageList == null ? 0 : this.pages.Count);
		}

		private MozItem GetMozItem(string text, int imageIndex)
		{
			MozItem item = new MozItem();
			item.Name = "mozItem" + this.pages.Count + 1;
			item.Text = text;

			if (imageIndex < this.ImageList.Images.Count)
			{
				item.Images.NormalImage = this.ImageList.Images[imageIndex];
			}

			return item;
		}

		#region Activate Page
		private void mozPane1_ItemClick(object sender, MozItemClickEventArgs e)
		{
			this.ActivatePage(e.MozItem);
		}

		private bool ActivatePage(MozItem item)
		{
			if (!this.pages.ContainsKey(item.Name))
			{
				return false;
			}

			PageProp pageProp = this.pages[item.Name];
			PropertyPage page = pageProp.Page;

			if (activePage != null)
			{
				activePage.Visible = false;
			}

			activePage = page;

			if (activePage != null)
			{
				this.mozPane1.SelectByName(item.Name);
				activePage.Visible = true;

				if (!page.IsInit)
				{
					page.OnInit();
					page.IsInit = true;
				}

				activePage.OnSetActive();
			}

			return true;
		}

		#endregion
		
		#endregion

		#region Public Interface

		#region Properties

        /// <summary>
        /// 参数页面对象
        /// </summary>
		public Dictionary<string, PageProp> Pages
		{
			get { return pages; }
		}

        /// <summary>
        /// 图片列表
        /// </summary>
		public ImageList ImageList
		{
			get { return this.mozPane1.ImageList; }
			set { this.mozPane1.ImageList = value; }
		}
		#endregion

		#region Methods

		public void AddPage(string text, PropertyPage page)
		{
			this.AddPage(this.GetMozItem(text), page);
		}

		public void AddPage(string text, int imageIndex, PropertyPage page)
		{
			this.AddPage(this.GetMozItem(text, imageIndex), page);
		}

		public void Init()
		{
			foreach (PageProp pageProp in pages.Values)
			{
				PropertyPage page = pageProp.Page;

				pagePanel.Controls.Add(page);
				page.Dock = DockStyle.Fill;
				page.Visible = false;
			}

			if (this.pages.Count != 0)
			{
				ActivatePage(this.mozPane1.Items[0]);
			}
		}  
		#endregion

		#endregion

		#region Dialog Buttons
		private void btnOK_Click(object sender, EventArgs e)
		{
			bool result = this.Apply();
            if (result)
            {
                this.Close();
            }
            else
            {
                if (this.ParentForm != null)
                {
                    this.ParentForm.DialogResult = DialogResult.None;
                }
            }
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
            bool result = this.Apply();
            if (result)
            {
                this.Close();
            }
            else
            {
                if (this.ParentForm != null)
                {
                    this.ParentForm.DialogResult = DialogResult.None;
                }
            }

		}

		private bool Apply()
		{
			foreach (PageProp pageProp in pages.Values)
			{
				if (pageProp.Page.IsInit)
				{
                    bool result = pageProp.Page.OnApply();
                    if (!result)
                    {
                        return result;
                    }
				}
			}
            return true;//最后全部返回
		}

		private void Close()
		{
			if (this.ParentForm != null)
			{
				this.ParentForm.Close();
			}
		}
		#endregion
	}
}
