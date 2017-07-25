using JCodes.Framework.Common;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn.UI.SmallTools
{
    public partial class FrmIPO : BaseDock
    {
        // 定义gc数据变量 
        private List<MFileInfo> _fileInfolst = new List<MFileInfo>();

        // 从第几行开始写数据
        private Int32 _rowNum = 2;

        private const string STRSPLIT = "|";

        public FrmIPO()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 选择路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChoose_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog = new FolderBrowserDialog();

            if (folderbrowserdialog.ShowDialog() == DialogResult.OK)
            {
                string path = folderbrowserdialog.SelectedPath;
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 选择xls导入路径为: " + path, typeof(FrmIPO));
                rtbLog.AppendText(DateTime.Now.ToString() + " 选择xls导入路径为: " + path + "\r\n");
                // 赋值文件路径
                txtPath.Text = path;
                _fileInfolst.Clear();
                // 查询xls 和xlsx
                foreach (FileInfo oneFile in new DirectoryInfo(path).GetFiles("*.xls"))
                {
                    if (CheckFile(oneFile.Name))
                    {
                        _fileInfolst.Add(new MFileInfo() { FileName = oneFile.Name, DealStatus = 0 });
                    }
                }

                gcViewData.DataSource = _fileInfolst;
                gcDataView.RefreshData();
            }
        }

        private bool CheckFile(string filename)
        {
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 检查文件名: " + filename, typeof(FrmIPO));

            rtbLog.AppendText(DateTime.Now.ToString() + " 检查文件名: " + filename + "\r\n");

            // 过滤临时文件
            if (filename.StartsWith("~$"))
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 过滤掉含有~$垃圾文件: " + filename, typeof(FrmIPO));
                return false;
            }

            string[] fileDetail = filename.Split('-');

            if (fileDetail.Length != 2)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 文件名中没有带有-的规则 " + filename, typeof(FrmIPO));
                return false;
            }

            if (fileDetail[0].Length != 6)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 文件名中前面6位为申购的6位代码 " + fileDetail[0], typeof(FrmIPO));
                return false;
            }

            if (!string.Equals(fileDetail[1], "sgjg.xls", StringComparison.CurrentCultureIgnoreCase) && !string.Equals(fileDetail[1], "sgjg.xlsx", StringComparison.CurrentCultureIgnoreCase))
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 文件名中后面4位为不是sgjg " + fileDetail[1], typeof(FrmIPO));
                return false;
            }
                
            return true;
        }

        /// <summary>
        /// 修改值内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcDataView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if(e.Column.FieldName =="DealStatus")
            {  
                switch(e.Value.ToString().Trim()) 
                {    
                    case "0":
                        e.DisplayText = "未处理";
                        break;
                    case "1":
                        e.DisplayText = "正在处理";
                        break;
                    case "2":
                        e.DisplayText = "处理完成";
                        break;
                    case "3":
                        e.DisplayText = "处理错误";
                        break;
                }
                
            }
        }

        /// <summary>
        /// 处理xls 文件内部内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeal_Click(object sender, EventArgs e)
        {
            if (txtPath.Text.Trim().Length <= 0)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_WARN, "检查错误 请选择xls路径", typeof(FrmIPO));
                MessageBox.Show("请选择xls路径");
                return;
            }

            if (_fileInfolst.Count <= 0)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_WARN, "检查错误 此路径下不存在有效的xls文件", typeof(FrmIPO));
                MessageBox.Show("此路径下不存在有效的xls文件");
                return;
            }

            
            // 删除xls导入文件夹下所有的txt文件 支持重复操作
            foreach (FileInfo oneFile in new DirectoryInfo(txtPath.Text).GetFiles("*.txt"))
            {
                try
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_WARN, "删除 " + oneFile.FullName, typeof(FrmIPO));
                    File.Delete(oneFile.FullName);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmIPO));
                    MessageBox.Show(ex.Message);
                    return;
                }

                if (CheckFile(oneFile.Name))
                {
                    _fileInfolst.Add(new MFileInfo() { FileName = oneFile.Name, DealStatus = 0 });
                }
            }
           
            List<MFileInfo> tmp = (List<MFileInfo>)gcDataView.DataSource;
            _rowNum = 2;

            try
            {
                foreach (var one in tmp)
                {
                    one.DealStatus = 1;
                    gcDataView.RefreshData();
                    System.Threading.Thread.Sleep(500);
                    Application.DoEvents();

                    if (DoDeal(one.FileName))
                    {
                        one.DealStatus = 2;
                        gcDataView.RefreshData();
                        System.Threading.Thread.Sleep(500);
                        Application.DoEvents();
                    }
                    else
                    {
                        one.DealStatus = 3;
                        gcDataView.RefreshData();
                        System.Threading.Thread.Sleep(500);
                        Application.DoEvents();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmIPO));
                MessageBox.Show(ex.Message);
                return;
            }

            rtbLog.AppendText(DateTime.Now.ToString() + " 【处理完成】导出路径 " + txtPath.Text + "目录下对应的txt文件\r\n");
        }

        /// <summary>
        /// 处理文件
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        private bool DoDeal(string filename)
        {
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 开始处理文件: " + filename, typeof(FrmIPO));

            rtbLog.AppendText(DateTime.Now.ToString() + " 开始处理文件: " + filename + "\r\n");
            string path = txtPath.Text.Trim();
            if (!File.Exists(path + "\\"+ filename))
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "检查错误 路径[" + path + "\\" + filename + "] 对应的文件不存在", typeof(FrmIPO));
                MessageBox.Show("路径[" + path + "\\" + filename + "] 对应的文件不存在");
                return false;
            }

            if (!ReadXlsData(path + "\\" + filename))
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "检查错误 读取 [" + filename + "] 内部数据失败，请检查格式正确性", typeof(FrmIPO));
                MessageBox.Show("读取 [" + filename + "] 内部数据失败，请检查格式正确性");
                return false;
            }

            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 结束处理文件: " + filename, typeof(FrmIPO));
            return true;
        }

        private bool ReadXlsData(string filename)
        {
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 开始读取文件: " + filename, typeof(FrmIPO));
            rtbLog.AppendText(DateTime.Now.ToString() + " 开始读取文件: " + filename + "\r\n");

            try
            {
                Workbook workbook = new Workbook();
                workbook.LoadFromFile(filename);
                Worksheet sheet = workbook.Worksheets[0];

                List<MIPOInfo> lst = new List<MIPOInfo>();

                // 检查标题是否合法
                if (!string.Equals("申购流水号", sheet[1, 1].Value))
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 申购流水号 检查失败 正确为:申购流水号,文件中为" + sheet[1, 1].Value, typeof(FrmIPO));
                    rtbLog.AppendText(DateTime.Now.ToString() + " 申购流水号 检查失败 正确为:申购流水号,文件中为" + sheet[1, 1].Value + "\r\n");
                    return false;
                }

                if (!string.Equals("发行流水号", sheet[1, 2].Value))
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 发行流水号 检查失败 正确为:发行流水号,文件中为" + sheet[1, 2].Value, typeof(FrmIPO));
                    rtbLog.AppendText(DateTime.Now.ToString() + " 发行流水号 检查失败 正确为:发行流水号,文件中为" + sheet[1, 2].Value + "\r\n");
                    return false;
                }

                if (!string.Equals("投资者名称", sheet[1, 3].Value))
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 投资者名称 检查失败 正确为:投资者名称,文件中为" + sheet[1, 3].Value, typeof(FrmIPO));
                    rtbLog.AppendText(DateTime.Now.ToString() + " 投资者名称 检查失败 正确为:投资者名称,文件中为" + sheet[1, 3].Value + "\r\n");
                    return false;
                }

                if (!string.Equals("配售对象编码", sheet[1, 4].Value))
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 配售对象编码 检查失败 正确为:配售对象编码,文件中为" + sheet[1, 4].Value, typeof(FrmIPO));
                    rtbLog.AppendText(DateTime.Now.ToString() + " 配售对象编码 检查失败 正确为:配售对象编码,文件中为" + sheet[1, 4].Value + "\r\n");
                    return false;
                }

                if (!string.Equals("配售对象名称", sheet[1, 5].Value))
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 配售对象名称 检查失败 正确为:配售对象名称,文件中为" + sheet[1, 5].Value, typeof(FrmIPO));
                    rtbLog.AppendText(DateTime.Now.ToString() + " 配售对象名称 检查失败 正确为:配售对象名称,文件中为" + sheet[1, 5].Value + "\r\n");
                    return false;
                }

                if (!string.Equals("配售对象类型", sheet[1, 6].Value))
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 配售对象类型 检查失败 正确为:配售对象类型,文件中为" + sheet[1, 6].Value, typeof(FrmIPO));
                    rtbLog.AppendText(DateTime.Now.ToString() + " 配售对象类型 检查失败 正确为:配售对象类型,文件中为" + sheet[1, 6].Value + "\r\n");
                    return false;
                }

                if (!string.Equals("证券代码", sheet[1, 7].Value))
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 证券代码 检查失败 正确为:证券代码,文件中为" + sheet[1, 7].Value, typeof(FrmIPO));
                    rtbLog.AppendText(DateTime.Now.ToString() + " 证券代码 检查失败 正确为:证券代码,文件中为" + sheet[1, 7].Value + "\r\n");
                    return false;
                }

                if (!string.Equals("申购数量（万股）", sheet[1, 8].Value))
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 申购数量（万股） 检查失败 正确为:申购数量（万股）,文件中为" + sheet[1, 8].Value, typeof(FrmIPO));
                    rtbLog.AppendText(DateTime.Now.ToString() + " 申购数量（万股） 检查失败 正确为:申购数量（万股）,文件中为" + sheet[1, 8].Value + "\r\n");
                    return false;
                }

                if (!string.Equals("发行价格（元）", sheet[1, 9].Value))
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 发行价格（元） 检查失败 正确为:发行价格（元）,文件中为" + sheet[1, 9].Value, typeof(FrmIPO));
                    rtbLog.AppendText(DateTime.Now.ToString() + " 发行价格（元） 检查失败 正确为:发行价格（元）,文件中为" + sheet[1, 9].Value + "\r\n");
                    return false;
                }

                if (!string.Equals("申购价格（元）", sheet[1, 10].Value))
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 申购价格（元） 检查失败 正确为:申购价格（元）,文件中为" + sheet[1, 10].Value, typeof(FrmIPO));
                    rtbLog.AppendText(DateTime.Now.ToString() + " 申购价格（元） 检查失败 正确为:申购价格（元）,文件中为" + sheet[1, 10].Value + "\r\n");
                    return false;
                }

                if (!string.Equals("配售股数（万股）", sheet[1, 11].Value))
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 配售股数（万股） 检查失败 正确为:配售股数（万股）,文件中为" + sheet[1, 11].Value, typeof(FrmIPO));
                    rtbLog.AppendText(DateTime.Now.ToString() + " 配售股数（万股） 检查失败 正确为:配售股数（万股）,文件中为" + sheet[1, 11].Value + "\r\n");
                    return false;
                }

                if (!string.Equals("配售金额（万元）", sheet[1, 12].Value))
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 配售金额（万元） 检查失败 正确为:配售金额（万元）,文件中为" + sheet[1, 12].Value, typeof(FrmIPO));
                    rtbLog.AppendText(DateTime.Now.ToString() + " 配售金额（万元） 检查失败 正确为:配售金额（万元）,文件中为" + sheet[1, 12].Value + "\r\n");
                    return false;
                }

                while (!string.IsNullOrEmpty(sheet[_rowNum, 1].Text))
                {
                    MIPOInfo ipoInfo = new MIPOInfo();
                    ipoInfo.WTXH = sheet[_rowNum, 1].Value.Trim();//string.IsNullOrEmpty(sheet[_rowNum, 1].Text) ? 0 : Convert.ToInt32(sheet[_rowNum, 1].Text);
                    ipoInfo.FXLSH = sheet[_rowNum, 2].Value.Trim(); //string.IsNullOrEmpty(sheet[_rowNum, 2].Text) ? 0 : Convert.ToInt32(sheet[_rowNum, 2].Text);
                    ipoInfo.XJDXMC = sheet[_rowNum, 3].Value.Trim();
                    ipoInfo.PSDXID = sheet[_rowNum, 4].Value.Trim();
                    ipoInfo.PSDXMC = sheet[_rowNum, 5].Value.Trim();
                    ipoInfo.PSDXLX = sheet[_rowNum, 6].Value.Trim();
                    ipoInfo.ZQDM = sheet[_rowNum, 7].Value.Trim();//string.IsNullOrEmpty(sheet[_rowNum, 7].Text) ? 0 : Convert.ToInt32(sheet[_rowNum, 7].Text);
                    ipoInfo.SGSL = sheet[_rowNum, 8].Value.Trim();//string.IsNullOrEmpty(sheet[_rowNum, 8].Text) ? 0 : float.Parse(sheet[_rowNum, 8].Text);
                    ipoInfo.SGJG = sheet[_rowNum, 9].Value.Trim();//string.IsNullOrEmpty(sheet[_rowNum, 9].Text) ? 0 : float.Parse(sheet[_rowNum, 9].Text);
                    ipoInfo.FXJG = sheet[_rowNum, 10].Value.Trim();//string.IsNullOrEmpty(sheet[_rowNum, 10].Text) ? 0 : float.Parse(sheet[_rowNum, 10].Text);
                    ipoInfo.REMARK1 = sheet[_rowNum, 11].Value;//string.IsNullOrEmpty(sheet[_rowNum, 11].Text) ? 0 : float.Parse(sheet[_rowNum, 11].Text);
                    ipoInfo.REMARK2 = sheet[_rowNum, 12].Value.Contains("*") ? sheet[_rowNum, 12].NumberText : sheet[_rowNum, 12].Value;//string.IsNullOrEmpty(sheet[_rowNum, 12].Text) ? 0 : float.Parse(sheet[_rowNum, 12].Text);
                    lst.Add(ipoInfo);
                    _rowNum++;
                }

                string filepath = filename.Replace("xlsx", "txt").Replace("xls", "txt").Replace("sgjg", "cbpsjg");

                if (!File.Exists(filepath))
                {
                    FileStream fs = File.Create(filepath);
                    fs.Close();
                    fs.Dispose();
                }

                foreach (var ipoInfo in lst)
                {
                    string str = string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}\r\n", STRSPLIT, ipoInfo.WTXH, ipoInfo.FXLSH, ipoInfo.XJDXMC, ipoInfo.PSDXID, ipoInfo.PSDXMC, ipoInfo.PSDXLX, ipoInfo.ZQDM, ipoInfo.SGSL, ipoInfo.SGJG, ipoInfo.FXJG, ipoInfo.REMARK1,ipoInfo.REMARK2);
                    // 往txt文本中写内容
                    File.AppendAllText(filepath, str, Encoding.Default);
                }
               
                sheet.Dispose();
                workbook.Dispose();

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, ex, typeof(FrmIPO));
                MessageBox.Show(ex.Message);
                return false;
            }

            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 结束读取文件: " + filename, typeof(FrmIPO));
            return true;
        }
    }
}
