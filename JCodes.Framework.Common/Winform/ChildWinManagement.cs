using System;
using System.Windows.Forms;

namespace JCodes.Framework.Common
{
	/// <summary>
	/// 窗口管理辅助类
	/// </summary>
	public sealed class ChildWinManagement
	{
		private ChildWinManagement()
		{
		}

		/// <summary> 
		/// 获取MDI父窗口是否有窗口标题为指定字符串的子窗口（如果已经存在把此子窗口推向前台） 
		/// </summary> 
		/// <param name="MDIwin">MDI父窗口</param> 
		/// <param name="caption">窗口标题</param> 
		/// <returns></returns> 
		public static bool ExistWin(Form MDIwin, string caption)
		{
			bool R = false;
			foreach (Form f in MDIwin.MdiChildren)
			{
				if (f.Text == caption)
				{
					R = true;
					f.Show();
					f.Activate();
					break;
				}
			}
			return R;
		}

        /// <summary>
        /// 唯一加载某个类型的窗体，如果存在则显示，否则创建。
        /// </summary>
        /// <param name="mainDialog">主窗体对象</param>
        /// <param name="formType">待显示的窗体类型</param>
        /// <returns></returns>
		public static Form LoadMdiForm(Form mainDialog, Type formType)
		{
			bool bFound = false;
			Form tableForm = null;
			foreach (Form form in mainDialog.MdiChildren)
			{
				if (form.GetType() == formType)
				{
					bFound = true;
					tableForm = form;
					break;
				}
			}
			if (!bFound)
			{
				tableForm = (Form) Activator.CreateInstance(formType);
                tableForm.MdiParent = mainDialog;
				tableForm.Show();
			}

			//tableForm.Dock = DockStyle.Fill;
			//tableForm.WindowState = FormWindowState.Maximized;
			tableForm.BringToFront();
			tableForm.Activate();

			return tableForm;
		}

        /// <summary>
        /// 把控件附加到窗体上弹出
        /// </summary>
        /// <param name="control">待显示的控件</param>
        /// <param name="caption">窗体显示的标题</param>
        public static void PopControlForm(Type control, string caption)
        {
            object ctr = ReflectionUtil.CreateInstance(control);
            if ((typeof(Control)).IsAssignableFrom(ctr.GetType()))
            {
                Form tmp = new Form();
                tmp.WindowState = FormWindowState.Maximized;
                tmp.ShowIcon = false;
                tmp.Text = caption;
                tmp.ShowInTaskbar = false;
                tmp.StartPosition = FormStartPosition.CenterScreen;
                Control ctrtmp = ctr as Control;
                ctrtmp.Dock = DockStyle.Fill;
                tmp.Controls.Add(ctrtmp);
                tmp.ShowDialog();
            }
        }

        /// <summary>
        /// 弹出窗体
        /// </summary>
        /// <param name="type">待显示的窗体类型</param>
        public static void PopDialogForm(Type type)
        {
            object form = ReflectionUtil.CreateInstance(type);
            if ((typeof(Form)).IsAssignableFrom(form.GetType()))
            {
                Form tmp = form as Form;
                tmp.ShowInTaskbar = false;
                tmp.StartPosition = FormStartPosition.CenterScreen;
                tmp.ShowDialog();
            }
        }
	}
}