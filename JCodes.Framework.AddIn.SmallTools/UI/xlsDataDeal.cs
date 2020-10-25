using Aspose.Cells;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Format;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.jCodesenum;
//using JCodes.Framework.Entity;
//using JCodes.Framework.Entity.SmallTools;
using JCodes.Framework.jCodesenum.BaseEnum;
// 20190423 对xls有行号限制删除对其引用
//using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn.SmallTools
{
    public partial class xlsDataDeal : BaseDock
    {
        // 定义gc数据变量 
        private List<MFileInfo> _fileInfolst = new List<MFileInfo>();
        // 定义EBList 数组
        private List<MEBInfo> _mEBInfolst = new List<MEBInfo>();
        // 从第几行开始写数据
        private Int32 _rowNum = 2;
        // 文件标识
        private Int32 _fileNum = 0;

        public xlsDataDeal()
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
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 选择文件夹路径为: " + path, typeof(xlsDataDeal));
                rtbLog.AppendText(DateTimeHelper.GetServerDateTime2().ToString() + " 选择文件夹路径为: " + path + "\r\n");
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
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 检查文件名: " + filename, typeof(xlsDataDeal));
            rtbLog.AppendText(DateTimeHelper.GetServerDateTime2().ToString() + " 检查文件名: " + filename + "\r\n");

            // 过滤临时文件
            if (filename.StartsWith("~$"))
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 过滤掉含有~$垃圾文件: " + filename, typeof(xlsDataDeal));
                return false;
            }

            // 20170829 wjm 不在检查文件名
            /*
            string[] fileDetail = filename.Split('-');

            if (fileDetail.Length != 3)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 文件名中没有带有-的规则 " + filename, typeof(xlsDataDeal));
                return false;
            }
                
            // 投资类型判断 银行、 基金、证券、财务、信托、保险、 个人、 其他
            if (fileDetail[1] != "银行" && fileDetail[1] != "基金" && fileDetail[1] != "证券"
                && fileDetail[1] != "财务" && fileDetail[1] != "信托" && fileDetail[1] != "保险"
                && fileDetail[1] != "个人" && fileDetail[1] != "其他")
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "投资者类型包括银行、 基金、证券、财务、信托、保险、 个人、 其他不符合 " + filename, typeof(xlsDataDeal));
                return false;
            }
                
            // 最后一个判断 是否是 网下利率询价及认购申请表
            if (!fileDetail[2].Contains("17浙报EB网下认购申请表"))
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "第三个串内容不是 17浙报EB网下认购申请表 不符合 " + filename, typeof(xlsDataDeal));
                return false;
            }*/
                
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
            // 重新置文件标识
            _fileNum = 0;

            if (txtPath.Text.Trim().Length <= 0)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, "检查错误 请选择xls路径", typeof(xlsDataDeal));

                MessageDxUtil.ShowWarning("请选择xls路径");
                return;
            }

            if (_fileInfolst.Count <= 0)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, "检查错误 此路径下不存在有效的xls文件", typeof(xlsDataDeal));
                MessageDxUtil.ShowWarning("此路径下不存在有效的xls文件");
                return;
            }

            // 复制文件操作
            if (File.Exists(Application.StartupPath + "\\网下利率询价及申购申请表汇总表.xlsx"))
            {
                try
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "删除 " + Application.StartupPath + "\\网下利率询价及申购申请表汇总表.xlsx 文件", typeof(xlsDataDeal));
                    File.Delete(Application.StartupPath + "\\网下利率询价及申购申请表汇总表.xlsx");
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(xlsDataDeal));
                    MessageDxUtil.ShowError(ex.Message);
                    return;
                }
            }
           
            List<MFileInfo> tmp = (List<MFileInfo>)gcDataView.DataSource;
            _rowNum = 1;

            try
            {
                /*Workbook workbook = new Workbook();
                workbook.LoadFromFile(Application.StartupPath + "\\Template\\网下利率询价及申购申请表汇总表.xlsx");
                Worksheet sheet = workbook.Worksheets[0];

                foreach (var one in tmp)
                {
                    one.DealStatus = 1;
                    gcDataView.RefreshData();
                    Application.DoEvents();

                    _mEBInfolst.Clear();

                    if (DoDeal(one.FileName, sheet))
                    {
                        one.DealStatus = 2;
                        gcDataView.RefreshData();
                        Application.DoEvents();
                    }
                    else
                    {
                        one.DealStatus = 3;
                        gcDataView.RefreshData();
                        Application.DoEvents();
                    }
                }

                workbook.SaveToFile("网下利率询价及申购申请表汇总表.xlsx");
                workbook.Dispose();*/

                Workbook workbook = new Workbook(Application.StartupPath + "\\Template\\网下利率询价及申购申请表汇总表.xlsx");
                Worksheet sheet = workbook.Worksheets[0];

                //创建样式
                Aspose.Cells.Style style = workbook.Styles[workbook.Styles.Add()];
                style.Borders[Aspose.Cells.BorderType.LeftBorder].LineStyle = Aspose.Cells.CellBorderType.Thin; //应用边界线 左边界线  
                style.Borders[Aspose.Cells.BorderType.RightBorder].LineStyle = Aspose.Cells.CellBorderType.Thin; //应用边界线 右边界线  
                style.Borders[Aspose.Cells.BorderType.TopBorder].LineStyle = Aspose.Cells.CellBorderType.Thin; //应用边界线 上边界线  
                style.Borders[Aspose.Cells.BorderType.BottomBorder].LineStyle = Aspose.Cells.CellBorderType.Thin; //应用边界线 下边界线   
                style.HorizontalAlignment = TextAlignmentType.Center; //单元格内容的水平对齐方式文字居中
                style.Font.Name = "宋体"; //字体
                style.IsTextWrapped = true; //单元格内容自动换行

                foreach (var one in tmp)
                {
                    one.DealStatus = 1;
                    gcDataView.RefreshData();
                    Application.DoEvents();

                    _mEBInfolst.Clear();

                    if (DoDeal(one.FileName, sheet))
                    {
                        one.DealStatus = 2;
                        gcDataView.RefreshData();
                        Application.DoEvents();
                    }
                    else
                    {
                        one.DealStatus = 3;
                        gcDataView.RefreshData();
                        Application.DoEvents();
                    }
                }
                sheet.AutoFitColumns(); //自适应宽
                workbook.Save("网下利率询价及申购申请表汇总表.xlsx");
                GC.Collect();
                
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(xlsDataDeal));
                MessageDxUtil.ShowError(ex.Message);
                return;
            }

            rtbLog.AppendText(DateTimeHelper.GetServerDateTime2().ToString() + " 【处理完成】导出路径 " + Application.StartupPath + "\\网下利率询价及申购申请表汇总表.xlsx\r\n");
        }

        private bool DoDeal(string filename, Worksheet sheet)
        {
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 开始处理文件: " + filename, typeof(xlsDataDeal));
            rtbLog.AppendText(DateTimeHelper.GetServerDateTime2().ToString() + " 开始处理文件: " + filename + "\r\n");
            string path = txtPath.Text.Trim();

            if (!File.Exists(path + "\\"+ filename))
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, "检查错误 路径[" + path + "\\" + filename + "] 对应的文件不存在", typeof(xlsDataDeal));
                MessageDxUtil.ShowWarning("路径[" + path + "\\" + filename + "] 对应的文件不存在");
                return false;
            }

            if (!File.Exists(Application.StartupPath + "\\Template\\网下利率询价及申购申请表汇总表.xlsx"))
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, "检查错误 Template 目录下模板文件不存在 请联系管理员", typeof(xlsDataDeal));
                MessageDxUtil.ShowWarning("Template 目录下模板文件不存在 请联系管理员");
                return false;
            }

            if (!ReadXlsData(path + "\\" + filename))
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, "检查错误 读取 [" + filename + "] 内部数据失败，请检查格式正确性", typeof(xlsDataDeal));
                MessageDxUtil.ShowWarning("读取 [" + filename + "] 内部数据失败，请检查格式正确性");
                return false;
            }

            if (!WriteXlsData(path + "\\" + filename, sheet))
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, "检查错误 写入 [" + filename + "] 数据失败，请检查格式正确性", typeof(xlsDataDeal));
                MessageDxUtil.ShowWarning("写入 [" + filename + "] 数据失败，请检查格式正确性");
                return false;
            }
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 结束处理文件: " + filename, typeof(xlsDataDeal));
            return true;
        }

        private bool ReadXlsData(string filename)
        {
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 开始读取文件: " + filename, typeof(xlsDataDeal));
            rtbLog.AppendText(DateTimeHelper.GetServerDateTime2() + " 开始读取文件: " + filename + "\r\n");

            // 每次新增文件标识
            _fileNum++;

            // 列
            Int32 column = 0;
            // 行
            Int32 row = 0;
            /*try
            {
                Workbook workbook = new Workbook();
                workbook.LoadFromFile(filename);
                Worksheet sheet = workbook.Worksheets[0];

                #region 机构名或姓名
                CellRange[] ranges = sheet.FindAllString("机构全称（合格机构投资者填写）：", false, false);
               
                foreach (CellRange range in ranges)
                {
                    column = range.Rows[0].Column;
                    row = range.Rows[0].Row;
                }
                // 机构名称
                string organizeName = sheet[row, column + 1].Value;

                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, String.Format(" 插入调试信息: 查找机构名称 行[row={0}],列[column={1}],机构名称[organizeName={2}]", row, column, organizeName), typeof(xlsDataDeal));

                #endregion

                if (string.IsNullOrEmpty(organizeName))
                {
                    #region 个人姓名
                    ranges = sheet.FindAllString("个人全名（合格个人投资者填写）：", false, false);
                    foreach (CellRange range in ranges)
                    {
                        column = range.Rows[0].Column;
                        row = range.Rows[0].Row;
                    }
                    organizeName = sheet[row, column + 1].Value;

                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, String.Format(" 插入调试信息: 机构名称为空 再次查找个人信息 行[row={0}],列[column={1}],个人信息[organizeName={2}]", row, column, organizeName), typeof(xlsDataDeal));
                    #endregion
                }

                #region 证券账户户名（上海）
                ranges = sheet.FindAllString("证券账户户名（上海）", false, false);
                foreach (CellRange range in ranges)
                {
                    column = range.Rows[0].Column;
                    row = range.Rows[0].Row;
                }

                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, String.Format(" 插入调试信息: 证券账户户名（上海）信息查找  行[row={0}],列[column={1}]", row, column), typeof(xlsDataDeal));
                Int32 cnt = 1;
                while (true)
                {
                    try
                    {
                        string accountName = sheet[row + cnt, column].Value;        // 证券账户户名（上海）
                        if (string.IsNullOrEmpty(accountName)) break;               // 没有数据则表示查询结束
                        string accountCode = sheet[row + cnt, column + 1].Value;    // 证券账户代码（上海）
                        string seat = sheet[row + cnt, column + 2].Value;           // 托管券商席位号（上海）
                        string cardId = sheet[row + cnt, column + 3].Value;         // 身份证明号码

                        // 第一笔记录
                        string rate1 = sheet[row + cnt, column + 4].Value;          // 到期赎回价格1（元）
                        string balance1 = sheet[row + cnt, column + 5].Value;       // 认购数量1（万元）

                        // 第二笔记录
                        string rate2 = sheet[row + cnt, column + 6].Value;          // 到期赎回价格2（元）
                        string balance2 = sheet[row + cnt, column + 7].Value;       // 认购数量2（万元）

                        // 第三笔记录
                        string rate3 = sheet[row + cnt, column + 8].Value;          // 到期赎回价格3（元）
                        string balance3 = sheet[row + cnt, column + 9].Value;       // 认购数量3（万元）

                        string bankName = sheet[row + cnt, column + 10].Value;      // 退款汇入行全称
                        string bankAccount = sheet[row + cnt, column + 11].Value;   // 退款收款人账号
                        string clientName = sheet[row + cnt, column + 12].Value;    // 退款收款人全称
                        string bankProvince = sheet[row + cnt, column + 13].Value;  // 退款汇入行省份/地市
                        string systemId = sheet[row + cnt, column + 14].Value;      // 退款汇入行大额支付系统行号

                        if (!string.IsNullOrEmpty(rate1))
                        {
                            _mEBInfolst.Add(new MEBInfo() { OrganizeName = organizeName, AccountName = accountName, AccountCode = accountCode, Seat = seat, CardId = cardId, Rate = rate1, Balance = balance1, BankName = bankName, BankAccount = bankAccount, ClientName = clientName, BankProvince = bankProvince, SystemId = systemId, Ident = _fileNum.ToString()});
                        }

                        if (!string.IsNullOrEmpty(rate2))
                        {
                            _mEBInfolst.Add(new MEBInfo() { OrganizeName = organizeName, AccountName = accountName, AccountCode = accountCode, Seat = seat, CardId = cardId, Rate = rate2, Balance = balance2, BankName = bankName, BankAccount = bankAccount, ClientName = clientName, BankProvince = bankProvince, SystemId = systemId, Ident = _fileNum.ToString() });
                        }

                        if (!string.IsNullOrEmpty(rate3))
                        {
                            _mEBInfolst.Add(new MEBInfo() { OrganizeName = organizeName, AccountName = accountName, AccountCode = accountCode, Seat = seat, CardId = cardId, Rate = rate3, Balance = balance3, BankName = bankName, BankAccount = bankAccount, ClientName = clientName, BankProvince = bankProvince, SystemId = systemId, Ident = _fileNum.ToString() });
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(xlsDataDeal));
                        break;
                    }
                    
                    cnt++;
                }

                #endregion

                GC.Collect();
                //workbook.SaveToFile("替换.xlsx");
                //System.Diagnostics.Process.Start("替换.xlsx");
            }*/
            try
            {
                Workbook workbook = new Workbook(filename);
                Worksheet sheet = workbook.Worksheets[0];
                Cells cell = sheet.Cells;
                #region 机构名或姓名
                Cell findcell = sheet.Cells.Find("机构全称（合格机构投资者填写）：",null, new FindOptions());
                column = findcell.Column;
                row = findcell.Row;

                // 机构名称
                string organizeName = cell[row, column + 1].StringValue;

                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, String.Format(" 插入调试信息: 查找机构名称 行[row={0}],列[column={1}],机构名称[organizeName={2}]", row, column, organizeName), typeof(xlsDataDeal));

                #endregion

                if (string.IsNullOrEmpty(organizeName))
                {
                    #region 个人姓名
                    findcell = sheet.Cells.Find("个人全名（合格个人投资者填写）：",null, new FindOptions());
                    column = findcell.Column;
                    row = findcell.Row;
                   
                    organizeName = cell[row, column + 1].StringValue;

                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, String.Format(" 插入调试信息: 机构名称为空 再次查找个人信息 行[row={0}],列[column={1}],个人信息[organizeName={2}]", row, column, organizeName), typeof(xlsDataDeal));
                    #endregion
                }

                #region 证券账户户名（上海）
                findcell = sheet.Cells.Find("证券账户户名（上海）",null, new FindOptions());
                column = findcell.Column;
                row = findcell.Row;

                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, String.Format(" 插入调试信息: 证券账户户名（上海）信息查找  行[row={0}],列[column={1}]", row, column), typeof(xlsDataDeal));
                Int32 cnt = 1;
                while (true)
                {
                    try
                    {
                        string accountName = cell[row + cnt, column].StringValue;        // 证券账户户名（上海）
                        if (string.IsNullOrEmpty(accountName)) break;               // 没有数据则表示查询结束
                        string accountCode = cell[row + cnt, column + 1].StringValue;    // 证券账户代码（上海）
                        string seat = cell[row + cnt, column + 2].StringValue;           // 托管券商席位号（上海）
                        string cardId = cell[row + cnt, column + 3].StringValue;         // 身份证明号码

                        // 第一笔记录
                        string rate1 = cell[row + cnt, column + 4].StringValue;          // 到期赎回价格1（元）
                        string balance1 = cell[row + cnt, column + 5].StringValue;       // 认购数量1（万元）

                        // 第二笔记录
                        string rate2 = cell[row + cnt, column + 6].StringValue;          // 到期赎回价格2（元）
                        string balance2 = cell[row + cnt, column + 7].StringValue;       // 认购数量2（万元）

                        // 第三笔记录
                        string rate3 = cell[row + cnt, column + 8].StringValue;          // 到期赎回价格3（元）
                        string balance3 = cell[row + cnt, column + 9].StringValue;       // 认购数量3（万元）

                        string bankName = cell[row + cnt, column + 10].StringValue;      // 退款汇入行全称
                        string bankAccount = cell[row + cnt, column + 11].StringValue;   // 退款收款人账号
                        string clientName = cell[row + cnt, column + 12].StringValue;    // 退款收款人全称
                        string bankProvince = cell[row + cnt, column + 13].StringValue;  // 退款汇入行省份
                        // 20190423 wujianming 新增退款汇入行地市
                        string bankCity = cell[row + cnt, column + 14].StringValue;      // 退款汇入行地市
                        string systemId = cell[row + cnt, column + 15].StringValue;      // 退款汇入行大额支付系统行号
                        

                        if (!string.IsNullOrEmpty(rate1))
                        {
                            _mEBInfolst.Add(new MEBInfo() { OrganizeName = organizeName, AccountName = accountName, AccountCode = accountCode, Seat = seat, CardId = cardId, Rate = rate1, Balance = balance1, BankName = bankName, BankAccount = bankAccount, ClientName = clientName, BankProvince = bankProvince, BankCity = bankCity, SystemId = systemId, Ident = _fileNum.ToString()});
                        }

                        if (!string.IsNullOrEmpty(rate2))
                        {
                            _mEBInfolst.Add(new MEBInfo() { OrganizeName = organizeName, AccountName = accountName, AccountCode = accountCode, Seat = seat, CardId = cardId, Rate = rate2, Balance = balance2, BankName = bankName, BankAccount = bankAccount, ClientName = clientName, BankProvince = bankProvince, BankCity = bankCity, SystemId = systemId, Ident = _fileNum.ToString() });
                        }

                        if (!string.IsNullOrEmpty(rate3))
                        {
                            _mEBInfolst.Add(new MEBInfo() { OrganizeName = organizeName, AccountName = accountName, AccountCode = accountCode, Seat = seat, CardId = cardId, Rate = rate3, Balance = balance3, BankName = bankName, BankAccount = bankAccount, ClientName = clientName, BankProvince = bankProvince, BankCity = bankCity, SystemId = systemId, Ident = _fileNum.ToString() });
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(xlsDataDeal));
                        break;
                    }
                    
                    cnt++;
                }
                #endregion

                GC.Collect();
                //workbook.SaveToFile("替换.xlsx");
                //System.Diagnostics.Process.Start("替换.xlsx");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(xlsDataDeal));
                MessageDxUtil.ShowError(ex.Message);
                return false;
            }

            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 结束读取文件: " + filename, typeof(xlsDataDeal));
            return true;
        }

        private bool WriteXlsData(string filename, Worksheet sheet)
        {
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 开始把上一次读的数据写入到文件: " + filename, typeof(xlsDataDeal));
            rtbLog.AppendText(DateTimeHelper.GetServerDateTime2().ToString() + " 开始把上一次读的数据写入到文件: " + filename + "\r\n");
            Cells cells = sheet.Cells; //单元格
            try
            {
                _mEBInfolst.ForEach(a =>
                {
                    // https://www.e-iceblue.com/Tutorials/Spire.XLS/Spire.XLS-Program-Guide/Spire.XLS-Program-Guide-Content.html
                    /*cells[_rowNum, 1].Style.WrapText = true;
                    cells[_rowNum, 1].Text = a.OrganizeName;
                    cells[_rowNum, 2].Style.WrapText = true;
                    cells[_rowNum, 2].Text = a.AccountName;
                    cells[_rowNum, 3].Text = a.AccountCode;
                    cells[_rowNum, 4].Text = a.Seat;
                    cells[_rowNum, 5].Style.WrapText = true;
                    cells[_rowNum, 5].Text = a.CardId;
                    cells[_rowNum, 6].Text = a.Rate;
                    cells[_rowNum, 7].Text = a.Balance;
                    cells[_rowNum, 8].Style.WrapText = true;
                    cells[_rowNum, 8].Text = a.BankName;
                    cells[_rowNum, 9].Style.WrapText = true;
                    cells[_rowNum, 9].Text = a.ClientName;
                    cells[_rowNum, 10].Text = a.BankAccount;
                    cells[_rowNum, 11].Text = a.SystemId;
                    cells[_rowNum, 12].Text = a.BankProvince;
                    cells[_rowNum, 13].Text = a.Ident;*/


                    cells[_rowNum, 1 - 1].PutValue(a.Ident);
                    Style style = cells[_rowNum, 1 - 1].GetStyle();
                    style.Borders[Aspose.Cells.BorderType.LeftBorder].LineStyle = Aspose.Cells.CellBorderType.Thin; //应用边界线 左边界线  
                    style.Borders[Aspose.Cells.BorderType.RightBorder].LineStyle = Aspose.Cells.CellBorderType.Thin; //应用边界线 右边界线  
                    style.Borders[Aspose.Cells.BorderType.TopBorder].LineStyle = Aspose.Cells.CellBorderType.Thin; //应用边界线 上边界线  
                    style.Borders[Aspose.Cells.BorderType.BottomBorder].LineStyle = Aspose.Cells.CellBorderType.Thin; //应用边界线 下边界线   
                    style.IsTextWrapped = true; //单元格内容自动换行
                    cells[_rowNum, 1 - 1].SetStyle(style);
                    cells[_rowNum, 2 - 1].PutValue(a.OrganizeName);
                    cells[_rowNum, 2 - 1].SetStyle(style);
                    cells[_rowNum, 3 - 1].PutValue(a.AccountName);
                    cells[_rowNum, 3 - 1].SetStyle(style);
                    cells[_rowNum, 4 - 1].PutValue(a.AccountCode);
                    cells[_rowNum, 4 - 1].SetStyle(style);
                    cells[_rowNum, 5 - 1].PutValue(a.Seat);
                    cells[_rowNum, 5 - 1].SetStyle(style);
                    cells[_rowNum, 6 - 1].PutValue(a.CardId);
                    cells[_rowNum, 6 - 1].SetStyle(style);
                    cells[_rowNum, 7 - 1].PutValue(a.Rate);
                    cells[_rowNum, 7 - 1].SetStyle(style);
                    cells[_rowNum, 8 - 1].PutValue(a.Balance);
                    cells[_rowNum, 8 - 1].SetStyle(style);
                    cells[_rowNum, 9 - 1].PutValue(a.BankName);
                    cells[_rowNum, 9 - 1].SetStyle(style);
                    cells[_rowNum, 10 - 1].PutValue(a.ClientName);
                    cells[_rowNum, 10 - 1].SetStyle(style);
                    cells[_rowNum, 11 - 1].PutValue(a.BankAccount);
                    cells[_rowNum, 11 - 1].SetStyle(style);
                    cells[_rowNum, 12 - 1].PutValue(a.SystemId);
                    cells[_rowNum, 12 - 1].SetStyle(style);
                    cells[_rowNum, 13 - 1].PutValue(a.BankProvince);
                    cells[_rowNum, 13 - 1].SetStyle(style);
                    cells[_rowNum, 14 - 1].PutValue(a.BankCity);
                    cells[_rowNum, 14 - 1].SetStyle(style);

                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, String.Format(" 插入调试信息: 文件名[filename={0}],机构名或姓名[OrganizeName={1}]," + "证券账户户名（上海）[AccountName={2}],证券账户代码（上海）[AccountCode={3}],托管席位号[Seat={4}],身份证明号码（如营业执照注册号等）[CardId={5}],到期赎回价格[Rate={6}],申购金额[Balance={7}],退款汇入行全称[BankName={8}],退款收款人全称[ClientName={9}],退款收款人账号[BankAccount={10}],大额支付系统号[SystemId={11}],退款汇入行省份[BankProvince={12},退款汇入行市区[BankProvince={13}]", filename, a.OrganizeName, a.AccountName, a.AccountCode, a.Seat, a.CardId, a.Rate.ToString(), a.Balance.ToString(), a.BankName, a.ClientName, a.BankAccount, a.SystemId, a.BankProvince, a.BankCity), typeof(xlsDataDeal));

                    _rowNum++;
                });
            }
            catch (Exception ex){
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(xlsDataDeal));
                MessageDxUtil.ShowError(ex.Message);
                return false;
            }
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 结束把上一次读的数据写入到文件: " + filename, typeof(xlsDataDeal));
            return true;
        }
    }
}
