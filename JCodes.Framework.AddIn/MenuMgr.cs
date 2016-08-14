using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using JCodes.Framework.AddIn;
using System.IO;
using System.Text.RegularExpressions;
using JCodes.Framework.Entity;
using JCodes.Framework.Enum;
using JCodes.Framework.WinFormUI;
using System.Reflection;
using DevExpress.XtraBars.Alerter;

namespace JCodes.Framework.AddIn
{
    public partial class MenuMgr : DevExpress.XtraEditors.XtraForm
    {
        public MenuMgr()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 导入操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            // 弹出对话框
            var openFileDialog = new OpenFileDialog();
            // 只支持xml 文件导入 并生成对应的sql 文件
            openFileDialog.Filter = "文本文件(*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 获取选择的文件名
                string FileName = openFileDialog.FileName;
                string str = File.ReadAllText(FileName);

                List<TMenu> lst = new List<TMenu>();

                // 匹配正则表达式 查找对应的数据值
                Regex reg = new Regex(@"<menu id=""(\d+)"" title=""(.+)"" pid=""(\d+)"" sort=""(\d+)"" url=""(.+)"" hide=""(\d+)"" tip=""(.*)"" is_show=""(\d+)"" menu_group=""(.*)"" is_dev=""(\d+)"" status=""(\d+)""></menu>");

                MatchCollection matches = reg.Matches(str);
                foreach (Match match in matches)
                {
                    lst.Add(new TMenu()
                    {
                        id = Convert.ToInt32(match.Groups[1].Value),
                        title = match.Groups[2].Value,
                        pid = Convert.ToInt32(match.Groups[3].Value),
                        sort = Convert.ToInt32(match.Groups[4].Value),
                        url = match.Groups[5].Value,
                        hide = (Dic000000)Convert.ToInt32(match.Groups[6].Value),
                        tip = match.Groups[7].Value,
                        is_show = (Dic000000)Convert.ToInt32(match.Groups[8].Value),
                        menu_group = match.Groups[9].Value,
                        is_dev = (Dic000000)Convert.ToInt32(match.Groups[10].Value),
                        status = (Dic000002)Convert.ToInt32(match.Groups[11].Value)
                    });
                }

                // 设置对应的主键
                tlstMenus.KeyFieldName = "id";
                // 设置对应的父节点字段
                tlstMenus.ParentFieldName = "pid";
                // 设置跟目录节点名字
                tlstMenus.RootValue = 0;
                // 绑定数据源
                tlstMenus.DataSource = lst;
            }
        }

        /// <summary>
        /// 导出sql 判断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnexportsql_Click(object sender, EventArgs e)
        {
            var alter = new AlertControl();
            alter.FormLocation = AlertFormLocation.BottomRight;
            alter.FormShowingEffect = AlertFormShowingEffect.SlideHorizontal;
            alter.Show(this, "待完善功能", "通讯配置功能等待完善");
           
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "导出菜单生成sql 开始", typeof(MenuMgr));
            try
            {
                List<TMenu> lst = tlstMenus.DataSource as List<TMenu>;
                var dd = new GenericList<TMenu>(lst);
                dd.ListToSql("test");

                //foreach (var one in lst)
                //{


                //}
            }
            catch (Exception ex)
            {
                
            }
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "导出菜单生成sql 结束", typeof(MenuMgr));
        }

        private void btnExportxls_Click(object sender, EventArgs e)
        {
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "导出菜单为xls 开始", typeof(MenuMgr));
            tlstMenus.ExportToXls(@"C:\1.xls");
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "导出菜单为xls 结束", typeof(MenuMgr));
        }

        private void ListToSql()
        { }
    }

    public class GenericList<T>
    {
        private List<T> _lst;
        public GenericList(List<T> lst)
        {
            this._lst = lst;
        }

        public void ListToSql(string tablename)
        {
            // 如果List 没有对应的数据，则直接不在支持转换了
            if (_lst.Count <= 0)
                return;

            // 获取字段类型
            Type t = _lst[0].GetType();

            // 保存 insert 对应的字段 和select 对应的字段
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            sb.Append("insert into " + tablename + "(");
            foreach(var one in t.GetProperties())
            {
                sb.Append(one.Name+", ");
                sb2.Append(_lst[0].GetType().GetProperty(one.Name).GetValue(_lst[0], null) + ", ");
            }
            // 去除最后一个，
            sb.Remove(sb.Length - 2, 2);
            sb2.Remove(sb2.Length - 2, 2);
            sb.Append(") values (" + sb2.ToString() + ");");
            sb.AppendLine();

            Console.WriteLine(sb.ToString());
        }
    }
    
}