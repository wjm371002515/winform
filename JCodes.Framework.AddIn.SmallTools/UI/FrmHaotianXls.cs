using JCodes.Framework.Common;
using JCodes.Framework.Common.Format;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn.SmallTools
{
    public partial class FrmHaotianXls : BaseDock
    {
        // 定义gc数据变量 
        private List<MFileInfo> _fileInfolst = new List<MFileInfo>();

        // 从第几行开始写数据
        private Int32 _rowNum = 2;

        Dictionary<string, List<MCaiwuInfo>> lstTotal = new Dictionary<string, List<MCaiwuInfo>>();

        public FrmHaotianXls()
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
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 选择xls导入路径为: " + path, typeof(FrmHaotianXls));
                rtbLog.AppendText(DateTimeHelper.GetServerDateTime() + " 选择xls导入路径为: " + path + "\r\n");
                // 赋值文件路径
                txtPath.Text = path;
                _fileInfolst.Clear();
                // 查询xls 和xlsx
                foreach (FileInfo oneFile in new DirectoryInfo(path).GetFiles("*.xls"))
                {
                    // 过滤掉正在打开的文件
                    if (oneFile.Name.StartsWith("~$")) continue;

                    _fileInfolst.Add(new MFileInfo() { FileName = oneFile.Name, DealStatus = 0 });
                }

                gcViewData.DataSource = _fileInfolst;
                gcDataView.RefreshData();
            }
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
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_WARN, "检查错误 请选择xls路径", typeof(FrmHaotianXls));
                MessageBox.Show("请选择xls路径");
                return;
            }

            if (_fileInfolst.Count <= 0)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_WARN, "检查错误 此路径下不存在有效的xls文件", typeof(FrmHaotianXls));
                MessageBox.Show("此路径下不存在有效的xls文件");
                return;
            }

            List<MFileInfo> tmp = (List<MFileInfo>)gcDataView.DataSource;
            _rowNum = 2;
            lstTotal.Clear();

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

                if (!GenerateXls())
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "处理xls文件报错", typeof(FrmHaotianXls));
                    MessageBox.Show("处理xls文件报错");
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmHaotianXls));
                MessageBox.Show(ex.Message);
                return;
            }

            rtbLog.AppendText(DateTimeHelper.GetServerDateTime() + " 【处理完成】应用程序目录下 【生成分销售文件】文件夹\r\n");
            MessageBox.Show("处理完成");
        }

        /// <summary>
        /// 处理文件
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        private bool DoDeal(string filename)
        {
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 开始处理文件: " + filename, typeof(FrmHaotianXls));

            rtbLog.AppendText(DateTimeHelper.GetServerDateTime() + " 开始处理文件: " + filename + "\r\n");
            string path = txtPath.Text.Trim();
            if (!File.Exists(path + "\\"+ filename))
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "检查错误 路径[" + path + "\\" + filename + "] 对应的文件不存在", typeof(FrmHaotianXls));
                MessageBox.Show("路径[" + path + "\\" + filename + "] 对应的文件不存在");
                return false;
            }

            if (!ReadXlsData(path + "\\" + filename))
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "检查错误 读取 [" + filename + "] 内部数据失败，请检查格式正确性", typeof(FrmHaotianXls));
                MessageBox.Show("读取 [" + filename + "] 内部数据失败，请检查格式正确性");
                return false;
            }
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 结束处理文件: " + filename, typeof(FrmHaotianXls));
            return true;
        }

        private bool ReadXlsData(string filename)
        {
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 开始读取文件: " + filename, typeof(FrmHaotianXls));
            rtbLog.AppendText(DateTimeHelper.GetServerDateTime() + " 开始读取文件: " + filename + "\r\n");

            try
            {
                Workbook workbook = new Workbook();
                workbook.LoadFromFile(filename);
                Worksheet sheet = workbook.Worksheets[0];

                // 匹配模板
                if (string.Equals("年", sheet[1, 2].Value) &&
                    string.Equals("月", sheet[1, 3].Value) &&
                    string.Equals("生效日期", sheet[1, 4].Value) &&
                    string.Equals("出库机构", sheet[1, 5].Value) && // xls 中配置的是出库机构 实际保存为客户机构代码
                    string.Equals("出库机构", sheet[1, 6].Value) &&
                    string.Equals("客户名称", sheet[1, 7].Value) && // xls 中配置的是客户名称 实际保存为客户名称代码
                    string.Equals("客户名称", sheet[1, 8].Value) &&
                    string.Equals("供应商", sheet[1, 9].Value) &&
                    string.Equals("单据编号", sheet[1, 10].Value) &&
                    string.Equals("单据类型", sheet[1, 11].Value) &&
                    string.Equals("商品编码", sheet[1, 12].Value) &&
                    string.Equals("商品名称", sheet[1, 13].Value) &&
                    string.Equals("规格", sheet[1, 14].Value) &&
                    string.Equals("单位", sheet[1, 15].Value) &&
                    string.Equals("产地", sheet[1, 16].Value) &&
                    string.Equals("数量", sheet[1, 17].Value) &&
                    string.Equals("进价金额", sheet[1, 18].Value) &&
                    string.Equals("备注", sheet[1, 19].Value) &&
                    string.Equals("配送金额", sheet[1, 20].Value) &&
                    string.Equals("底价金额", sheet[1, 21].Value) &&
                    string.Equals("零售金额", sheet[1, 22].Value))
                {
                    while (!string.IsNullOrEmpty(sheet[_rowNum, 2].Value.Trim()))
                    {
                        MCaiwuInfo ipoInfo = new MCaiwuInfo();

                        ipoInfo.年 = sheet[_rowNum, 2].Value.Trim();
                        ipoInfo.月 = sheet[_rowNum, 3].Value.Trim();
                        ipoInfo.生效日期 = sheet[_rowNum, 4].Value.Trim();
                        ipoInfo.出库机构代码 = sheet[_rowNum, 5].Value.Trim();
                        ipoInfo.出库机构 = sheet[_rowNum, 6].Value.Trim();
                        ipoInfo.客户名称代码 = sheet[_rowNum, 7].Value.Trim();
                        ipoInfo.客户名称 = sheet[_rowNum, 8].Value.Trim();
                        ipoInfo.供应商 = sheet[_rowNum, 9].Value.Trim();
                        ipoInfo.单据编号 = sheet[_rowNum, 10].Value.Trim();
                        ipoInfo.单据类型 = sheet[_rowNum, 11].Value.Trim();
                        ipoInfo.商品编码 = sheet[_rowNum, 12].Value;
                        ipoInfo.商品名称 = sheet[_rowNum, 13].Value;
                        ipoInfo.规格 = sheet[_rowNum, 14].Value;
                        ipoInfo.单位 = sheet[_rowNum, 15].Value;
                        ipoInfo.产地 = sheet[_rowNum, 16].Value;
                        ipoInfo.数量 = sheet[_rowNum, 17].Value;
                        ipoInfo.进价金额 = sheet[_rowNum, 18].Value;
                        ipoInfo.备注 = sheet[_rowNum, 19].Value;
                        ipoInfo.配送金额 = sheet[_rowNum, 20].Value;
                        ipoInfo.底价金额 = sheet[_rowNum, 21].Value;
                        ipoInfo.零售金额 = sheet[_rowNum, 22].Value;

                        if (lstTotal.ContainsKey(sheet[_rowNum, 8].Value.Trim()))
                        {
                            lstTotal[sheet[_rowNum, 8].Value.Trim()].Add(ipoInfo);
                        }
                        else
                        { 
                            List<MCaiwuInfo> d = new List <MCaiwuInfo>();
                            d.Add(ipoInfo);
                            lstTotal.Add(sheet[_rowNum, 8].Value.Trim(), d);
                        }

                        _rowNum++;
                    }

                    sheet.Dispose();
                    workbook.Dispose();
                }
                else if (string.Equals("年", sheet[1, 2].Value) &&
                    string.Equals("会计月", sheet[1, 3].Value) &&
                    string.Equals("销售日期", sheet[1, 4].Value) &&
                    string.Equals("业务机构", sheet[1, 5].Value) &&
                    string.Equals("客户", sheet[1, 6].Value) &&
                    string.Equals("客户名称", sheet[1, 7].Value) &&
                    string.Equals("供应商", sheet[1, 8].Value) &&
                    string.Equals("商品编码", sheet[1, 9].Value) &&
                    string.Equals("商品名称", sheet[1, 10].Value) &&
                    string.Equals("商品规格", sheet[1, 11].Value) &&
                    string.Equals("单位", sheet[1, 12].Value) &&
                    string.Equals("生产企业", sheet[1, 13].Value) &&
                    string.Equals("单据类型", sheet[1, 14].Value) &&
                    string.Equals("单据编号", sheet[1, 15].Value) &&
                    string.Equals("批号", sheet[1, 16].Value) &&
                    string.Equals("有效期至", sheet[1, 17].Value) &&
                    string.Equals("数量", sheet[1, 18].Value) &&
                    string.Equals("品种数", sheet[1, 19].Value) &&
                    string.Equals("进价", sheet[1, 20].Value) &&
                    string.Equals("进价金额", sheet[1, 21].Value) &&
                    string.Equals("批发价", sheet[1, 22].Value) &&
                    string.Equals("批发金额", sheet[1, 23].Value) &&
                    string.Equals("毛利", sheet[1, 24].Value) &&
                    string.Equals("毛利率", sheet[1, 25].Value) &&
                    string.Equals("备注", sheet[1, 26].Value))
                {

                    while (!string.IsNullOrEmpty(sheet[_rowNum, 2].Value.Trim()))
                    {
                        MCaiwuInfo ipoInfo = new MCaiwuInfo();

                        ipoInfo.年 = sheet[_rowNum, 2].Value.Trim();         // 年
                        ipoInfo.月 = sheet[_rowNum, 3].Value.Trim();         // 会计月
                        ipoInfo.生效日期 = sheet[_rowNum, 4].Value.Trim();   // 销售日期
                        ipoInfo.出库机构代码 = string.Empty;  // sheet[_rowNum, 5].Value.Trim();
                        ipoInfo.出库机构 = sheet[_rowNum, 5].Value.Trim();  // 业务机构
                        ipoInfo.客户名称代码 = sheet[_rowNum, 6].Value.Trim();// 客户
                        ipoInfo.客户名称 = sheet[_rowNum, 7].Value.Trim();      // 客户名称
                        ipoInfo.供应商 = sheet[_rowNum, 8].Value.Trim();       // 供应商
                        ipoInfo.单据编号 = sheet[_rowNum, 15].Value.Trim();     // 单据编号
                        ipoInfo.单据类型 = sheet[_rowNum, 14].Value.Trim();     // 单据类型
                        ipoInfo.商品编码 = sheet[_rowNum, 9].Value;            // 商品编码
                        ipoInfo.商品名称 = sheet[_rowNum, 10].Value;            // 商品名称
                        ipoInfo.规格 = sheet[_rowNum, 11].Value;                  // 商品规格
                        ipoInfo.单位 = sheet[_rowNum, 12].Value;                  //单位
                        ipoInfo.产地 = sheet[_rowNum, 13].Value;                  // 生产企业
                        ipoInfo.数量 = sheet[_rowNum, 18].Value;                  // 数量
                        ipoInfo.进价金额 = sheet[_rowNum, 21].Value;                // 进价金额
                        ipoInfo.备注 = sheet[_rowNum, 26].Value;                  // 备注
                        ipoInfo.配送金额 = string.Empty;    //sheet[_rowNum, 20].Value;
                        ipoInfo.底价金额 = string.Empty;    // sheet[_rowNum, 21].Value;
                        ipoInfo.零售金额 = string.Empty;    // sheet[_rowNum, 22].Value;

                        if (lstTotal.ContainsKey(sheet[_rowNum, 8].Value.Trim()))
                        {
                            lstTotal[sheet[_rowNum, 8].Value.Trim()].Add(ipoInfo);
                        }
                        else
                        {
                            List<MCaiwuInfo> d = new List<MCaiwuInfo>();
                            d.Add(ipoInfo);
                            lstTotal.Add(sheet[_rowNum, 8].Value.Trim(), d);
                        }

                        _rowNum++;
                    }

                    sheet.Dispose();
                    workbook.Dispose();
                }
                else if (string.Equals("年", sheet[1, 2].Value) &&
                    string.Equals("会计月", sheet[1, 3].Value) &&
                    string.Equals("销售日期", sheet[1, 4].Value) &&
                    string.Equals("业务机构", sheet[1, 5].Value) &&
                    string.Equals("客户", sheet[1, 6].Value) &&
                    string.Equals("客户名称", sheet[1, 7].Value) &&
                    string.Equals("供应商", sheet[1, 8].Value) &&
                    string.Equals("商品编码", sheet[1, 9].Value) &&
                    string.Equals("商品名称", sheet[1, 10].Value) &&
                    string.Equals("商品规格", sheet[1, 11].Value) &&
                    string.Equals("单位", sheet[1, 12].Value) &&
                    string.Equals("生产企业", sheet[1, 13].Value) &&
                    string.Equals("单据类型", sheet[1, 14].Value) &&
                    string.Equals("单据编号", sheet[1, 15].Value) &&
                    string.Equals("批号", sheet[1, 16].Value) &&
                    string.Equals("有效期至", sheet[1, 17].Value) &&
                    string.Equals("数量", sheet[1, 18].Value) &&
                    string.Equals("品种数", sheet[1, 19].Value) &&
                    string.Equals("进价", sheet[1, 20].Value) &&
                    string.Equals("进价金额", sheet[1, 21].Value) &&
                    string.Equals("批发价", sheet[1, 22].Value) &&
                    string.Equals("批发金额", sheet[1, 23].Value) &&
                    string.Equals("毛利", sheet[1, 24].Value) &&
                    string.Equals("毛利率", sheet[1, 25].Value) &&
                    string.Equals("底价", sheet[1, 26].Value) &&
                    string.Equals("底价毛利", sheet[1, 27].Value) &&
                    string.Equals("底价毛利率", sheet[1, 28].Value) &&
                    string.Equals("备注", sheet[1, 29].Value))
                {
                    while (!string.IsNullOrEmpty(sheet[_rowNum, 2].Value.Trim()))
                    {
                        MCaiwuInfo ipoInfo = new MCaiwuInfo();

                        ipoInfo.年 = sheet[_rowNum, 2].Value.Trim();         // 年
                        ipoInfo.月 = sheet[_rowNum, 3].Value.Trim();         // 会计月
                        ipoInfo.生效日期 = sheet[_rowNum, 4].Value.Trim();   // 销售日期
                        ipoInfo.出库机构代码 = string.Empty;//sheet[_rowNum, 5].Value.Trim();
                        ipoInfo.出库机构 = sheet[_rowNum, 5].Value.Trim();  // 业务机构
                        ipoInfo.客户名称代码 = sheet[_rowNum, 6].Value.Trim();// 客户
                        ipoInfo.客户名称 = sheet[_rowNum, 7].Value.Trim();      // 客户名称
                        ipoInfo.供应商 = sheet[_rowNum, 8].Value.Trim();       // 供应商
                        ipoInfo.单据编号 = sheet[_rowNum, 15].Value.Trim();     // 单据编号
                        ipoInfo.单据类型 = sheet[_rowNum, 14].Value.Trim();     // 单据类型
                        ipoInfo.商品编码 = sheet[_rowNum, 9].Value;            // 商品编码
                        ipoInfo.商品名称 = sheet[_rowNum, 10].Value;            // 商品名称
                        ipoInfo.规格 = sheet[_rowNum, 11].Value;                  // 商品规格
                        ipoInfo.单位 = sheet[_rowNum, 12].Value;                  //单位
                        ipoInfo.产地 = sheet[_rowNum, 13].Value;                  // 生产企业
                        ipoInfo.数量 = sheet[_rowNum, 18].Value;                  // 数量
                        ipoInfo.进价金额 = sheet[_rowNum, 21].Value;                // 进价金额
                        ipoInfo.备注 = sheet[_rowNum, 29].Value;                  // 备注
                        ipoInfo.配送金额 = string.Empty;    //sheet[_rowNum, 20].Value;
                        ipoInfo.底价金额 = string.Empty;    // sheet[_rowNum, 21].Value;
                        ipoInfo.零售金额 = string.Empty;    // sheet[_rowNum, 22].Value;

                        if (lstTotal.ContainsKey(sheet[_rowNum, 8].Value.Trim()))
                        {
                            lstTotal[sheet[_rowNum, 8].Value.Trim()].Add(ipoInfo);
                        }
                        else
                        {
                            List<MCaiwuInfo> d = new List<MCaiwuInfo>();
                            d.Add(ipoInfo);
                            lstTotal.Add(sheet[_rowNum, 8].Value.Trim(), d);
                        }

                        _rowNum++;
                    }

                    sheet.Dispose();
                    workbook.Dispose();
                }
                else
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 文件模板检查失败 文件名为" + filename, typeof(FrmHaotianXls));
                    rtbLog.AppendText(DateTimeHelper.GetServerDateTime() + " 文件模板检查失败 文件名为" + filename + "\r\n");
                    sheet.Dispose();
                    workbook.Dispose();
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, ex, typeof(FrmHaotianXls));
                MessageBox.Show(ex.Message);
                return false;
            }

            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 结束读取文件: " + filename, typeof(FrmHaotianXls));
            return true;
        }

        /// <summary>
        /// 生产XLS文件
        /// </summary>
        /// <returns></returns>
        private bool GenerateXls()
        {
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 开始生成文件 请耐心等待", typeof(FrmHaotianXls));
            rtbLog.AppendText(DateTimeHelper.GetServerDateTime() + " 开始生成文件 请耐心等待\r\n");

            // 汇总文件
            Workbook workbooktotal = new Workbook();
            workbooktotal.LoadFromFile("Template//税率销售.xlsx");
            Worksheet sheettotal = workbooktotal.Worksheets[0];
            Int32 FileNumTotal = 1;
            Int32 __rowNum = 2;
            Int32 RownumTotal = 1;

            if (Directory.Exists("生成分销售文件"))
                Directory.Delete("生成分销售文件", true);

            Thread.Sleep(500);

            Directory.CreateDirectory("生成分销售文件");

            foreach (var one in lstTotal.Keys)
            {
                //File.Copy("Template//税率销售.xlsx", string.Format("生成分销售文件//{0}_税率销售.xls", one), true);

                _rowNum = 2;

                Workbook workbook = new Workbook();
                workbook.LoadFromFile("Template//税率销售.xlsx");
                Worksheet sheet = workbook.Worksheets[0];

                // 超过 65536 重新新建一个文件夹
                Int32 FileNum = 1;// 文件数量
                Int32 Rownum = 1;

                try
                {
                    lstTotal[one].ForEach(a =>
                    {
                        if (a.客户名称 == one)
                        {
                            sheet[_rowNum, 1].Text = (Rownum).ToString();
                            sheet[_rowNum, 2].Text = a.年;
                            sheet[_rowNum, 3].Text = a.月;
                            sheet[_rowNum, 4].Text = a.生效日期;
                            sheet[_rowNum, 5].Text = a.出库机构代码;
                            sheet[_rowNum, 6].Text = a.出库机构;
                            sheet[_rowNum, 7].Text = a.客户名称代码;
                            sheet[_rowNum, 8].Text = a.客户名称;
                            sheet[_rowNum, 9].Text = a.供应商;
                            sheet[_rowNum, 10].Text = a.单据编号;
                            sheet[_rowNum, 11].Text = a.单据类型;
                            sheet[_rowNum, 12].Text = a.商品编码;
                            sheet[_rowNum, 13].Text = a.商品名称;
                            sheet[_rowNum, 14].Text = a.规格;
                            sheet[_rowNum, 15].Text = a.单位;
                            sheet[_rowNum, 16].Text = a.产地;
                            sheet[_rowNum, 17].Text = a.数量;
                            sheet[_rowNum, 18].Value = a.进价金额;
                            sheet[_rowNum, 19].Text = a.备注;
                            sheet[_rowNum, 20].Value = a.配送金额;
                            sheet[_rowNum, 21].Value = a.底价金额;
                            sheet[_rowNum, 22].Value = a.零售金额;
                        }

                        // 汇总数据不区分客户名称
                        sheettotal[__rowNum, 1].Text = (RownumTotal).ToString();
                        sheettotal[__rowNum, 2].Text = a.年;
                        sheettotal[__rowNum, 3].Text = a.月;
                        sheettotal[__rowNum, 4].Text = a.生效日期;
                        sheettotal[__rowNum, 5].Text = a.出库机构代码;
                        sheettotal[__rowNum, 6].Text = a.出库机构;
                        sheettotal[__rowNum, 7].Text = a.客户名称代码;
                        sheettotal[__rowNum, 8].Text = a.客户名称;
                        sheettotal[__rowNum, 9].Text = a.供应商;
                        sheettotal[__rowNum, 10].Text = a.单据编号;
                        sheettotal[__rowNum, 11].Text = a.单据类型;
                        sheettotal[__rowNum, 12].Text = a.商品编码;
                        sheettotal[__rowNum, 13].Text = a.商品名称;
                        sheettotal[__rowNum, 14].Text = a.规格;
                        sheettotal[__rowNum, 15].Text = a.单位;
                        sheettotal[__rowNum, 16].Text = a.产地;
                        sheettotal[__rowNum, 17].Text = a.数量;
                        sheettotal[__rowNum, 18].Value = a.进价金额;
                        sheettotal[__rowNum, 19].Text = a.备注;
                        sheettotal[__rowNum, 20].Value = a.配送金额;
                        sheettotal[__rowNum, 21].Value = a.底价金额;
                        sheettotal[__rowNum, 22].Value = a.零售金额;

                        _rowNum++;
                        __rowNum++;

                        Rownum++;
                        RownumTotal++;

                        if (Rownum % 1000 == 0)
                        {
                            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, string.Format(" 已成功导入1000条记录到{0}_税率销售{1}.xls 文件中 请耐心等待", one, FileNum), typeof(FrmHaotianXls));
                            rtbLog.AppendText(DateTimeHelper.GetServerDateTime() + string.Format(" 已成功导入1000条记录到{0}_税率销售{1}.xls 文件中 请耐心等待", one, FileNum));
                        }

                        /// 超过了xls最大支持的行号则换一个新文件
                        if (_rowNum >= 65531)
                        {
                            workbook.SaveToFile(string.Format("生成分销售文件//{0}_税率销售{1}.xls", one, FileNum));
                            sheet.Dispose();
                            workbook.Dispose();

                            workbook = new Workbook();
                            workbook.LoadFromFile("Template//税率销售.xlsx");
                            sheet = workbook.Worksheets[0];
                            FileNum++;
                            _rowNum = 2;
                        }

                        if (__rowNum >= 65531)
                        {
                            workbooktotal.SaveToFile(string.Format("生成分销售文件//汇总表_税率销售{0}.xls", FileNumTotal));
                            sheettotal.Dispose();
                            workbooktotal.Dispose();

                            workbooktotal = new Workbook();
                            workbooktotal.LoadFromFile("Template//税率销售.xlsx");
                            sheettotal = workbooktotal.Worksheets[0];
                            FileNumTotal++;
                            __rowNum = 2;
                        }
                    });
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmHaotianXls));
                    MessageBox.Show(ex.Message);
                    return false;
                }
                finally
                {
                    workbook.SaveToFile(string.Format("生成分销售文件//{0}_税率销售{1}.xls", one, FileNum));
                    sheet.Dispose();
                    workbook.Dispose();
                }
            }

            workbooktotal.SaveToFile(string.Format("生成分销售文件//汇总表_税率销售{0}.xls", FileNumTotal));
            sheettotal.Dispose();
            workbooktotal.Dispose();

            return true;
        }
    }
}
