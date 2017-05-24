using JCodes.Framework.Common;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn.UI.SmallTools
{
    public partial class xlsDataDeal : BaseForm
    {
        // 定义gc数据变量 
        private List<MFileInfo> _fileInfolst = new List<MFileInfo>();
        // 定义客户基本信息 主键ID
        private List<MClientInfo> _clientInfolst = new List<MClientInfo>();
        // 定义开户金额信息 外键ID
        private List<MAmountInfo> _amountInfolst = new List<MAmountInfo>();
        // 定义银行信息     主键ID
        private List<MBankInfo> _bankInfolst = new List<MBankInfo>();
        // 从第几行开始写数据
        private Int32 _rowNum = 2;


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
                rtbLog.AppendText(DateTime.Now.ToString() + " 选择文件夹路径为: " + path + "\r\n");
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
            rtbLog.AppendText(DateTime.Now.ToString() + " 检查文件名: " + filename + "\r\n");

            // 过滤临时文件
            if (filename.StartsWith("~$"))
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 过滤掉含有~$垃圾文件: " + filename, typeof(xlsDataDeal));
                return false;
            }

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
            if (!fileDetail[2].Contains("网下利率询价及认购申请表"))
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "第三个串内容不是 网下利率询价及认购申请表 不符合 " + filename, typeof(xlsDataDeal));
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
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, "检查错误 请选择xls路径", typeof(xlsDataDeal));
                MessageBox.Show("请选择xls路径");
                return;
            }

            if (_fileInfolst.Count <= 0)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, "检查错误 此路径下不存在有效的xls文件", typeof(xlsDataDeal));
                MessageBox.Show("此路径下不存在有效的xls文件");
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
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
           
            List<MFileInfo> tmp = (List<MFileInfo>)gcDataView.DataSource;
            _rowNum = 2;

            try
            {
                Workbook workbook = new Workbook();
                workbook.LoadFromFile(Application.StartupPath + "\\Template\\网下利率询价及申购申请表汇总表.xlsx");
                Worksheet sheet = workbook.Worksheets[0];

                foreach (var one in tmp)
                {
                    one.DealStatus = 1;
                    gcDataView.RefreshData();

                    _clientInfolst.Clear();
                    _amountInfolst.Clear();
                    _bankInfolst.Clear();

                    if (DoDeal(one.FileName, sheet))
                    {
                        one.DealStatus = 2;
                        gcDataView.RefreshData();
                    }
                    else
                    {
                        one.DealStatus = 3;
                        gcDataView.RefreshData();
                    }
                }

                workbook.SaveToFile("网下利率询价及申购申请表汇总表.xlsx");
                workbook.Dispose();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(xlsDataDeal));
                MessageBox.Show(ex.Message);
                return;
            }

            rtbLog.AppendText(DateTime.Now.ToString() + " 【处理完成】导出路径 " + Application.StartupPath + "\\网下利率询价及申购申请表汇总表.xlsx\r\n");
        }

        private bool DoDeal(string filename, Worksheet sheet)
        {
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 开始处理文件: " + filename, typeof(xlsDataDeal));
            rtbLog.AppendText(DateTime.Now.ToString() + " 开始处理文件: " + filename + "\r\n");
            string path = txtPath.Text.Trim();
            if (!File.Exists(path + "\\"+ filename))
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, "检查错误 路径[" + path + "\\" + filename + "] 对应的文件不存在", typeof(xlsDataDeal));
                MessageBox.Show("路径["+path+"\\"+filename+"] 对应的文件不存在");
                return false;
            }

            if (!File.Exists(Application.StartupPath + "\\Template\\网下利率询价及申购申请表汇总表.xlsx"))
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, "检查错误 Template 目录下模板文件不存在 请联系管理员", typeof(xlsDataDeal));
                MessageBox.Show("Template 目录下模板文件不存在 请联系管理员");
                return false;
            }

            if (!ReadXlsData(path + "\\" + filename))
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, "检查错误 读取 [" + filename + "] 内部数据失败，请检查格式正确性", typeof(xlsDataDeal));
                MessageBox.Show("读取 ["+filename+"] 内部数据失败，请检查格式正确性");
                return false;
            }

            if (!WriteXlsData(path + "\\" + filename, sheet))
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, "检查错误 写入 [" + filename + "] 数据失败，请检查格式正确性", typeof(xlsDataDeal));
                MessageBox.Show("写入 [" + filename + "] 数据失败，请检查格式正确性");
                return false;
            }
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 结束处理文件: " + filename, typeof(xlsDataDeal));
            return true;
        }

        private bool ReadXlsData(string filename)
        {
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 开始读取文件: " + filename, typeof(xlsDataDeal));
            rtbLog.AppendText(DateTime.Now.ToString() + " 开始读取文件: " + filename + "\r\n");
            // 列
            Int32 column = 0;
            // 行
            Int32 row = 0;
            try
            {
                Workbook workbook = new Workbook();
                workbook.LoadFromFile(filename);
                Worksheet sheet = workbook.Worksheets[0];

                #region 机构名称
                CellRange[] ranges = sheet.FindAllString("机构名称", false, false);
               
                foreach (CellRange range in ranges)
                {
                    column = range.Rows[0].Column;
                    row = range.Rows[0].Row;
                }
                // 机构名称
                string organizeName = sheet[row, column + 2].Value;

                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, String.Format(" 插入调试信息: 查找机构名称 行[row={0}],列[column={1}],机构名称[organizeName={2}]", row, column, organizeName), typeof(xlsDataDeal));

                #endregion

                if (string.IsNullOrEmpty(organizeName))
                {
                    #region 个人姓名
                    ranges = sheet.FindAllString("个人姓名", false, false);
                    foreach (CellRange range in ranges)
                    {
                        column = range.Rows[0].Column;
                        row = range.Rows[0].Row;
                    }
                    organizeName = sheet[row, column + 2].Value;

                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, String.Format(" 插入调试信息: 机构名称为空 再次查找个人信息 行[row={0}],列[column={1}],个人信息[organizeName={2}]", row, column, organizeName), typeof(xlsDataDeal));
                    #endregion
                }

                // 认购主体信息及申购信息
                #region 认购主体信息及申购信息
                ranges = sheet.FindAllString("认购主体信息及申购信息", false, false);
                foreach (CellRange range in ranges)
                {
                    column = range.Rows[0].Column;
                    row = range.Rows[0].Row;
                }

                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, String.Format(" 插入调试信息: 认购主体信息及申购信息查找  行[row={0}],列[column={1}]", row, column), typeof(xlsDataDeal));
                Int32 cnt = 2;
                while (true)
                {
                    string strhangshu = sheet[row + cnt, column].Value;
                    try
                    {
                        Int32 numhangshu = Int32.Parse(strhangshu);
                        string accountName = sheet[row + cnt, column + 1].Value;
                        if (string.IsNullOrEmpty(accountName)) break;
                        string accountCode = sheet[row + cnt, column + 2].Value;
                        string seat = sheet[row + cnt, column + 3].Value;
                        string cardId = sheet[row + cnt, column + 4].Value;

                        _clientInfolst.Add(new MClientInfo() { Id = numhangshu, OrganizeName = organizeName, AccountName = accountName, AccountCode = accountCode, Seat = seat, CardId = cardId });

                        #region 开户金额信息
                        // 第一个
                        string strrate = sheet[row + cnt, column + 5].Value;
                        if (string.IsNullOrEmpty(strrate)) {
                            cnt++;
                            continue;
                        };

                        double dourate = 0;
                        try {
                            dourate = Convert.ToDouble(strrate);
                        }
                        catch (Exception ex)
                        {
                            dourate = 0.0;
                        }
                        string strbalance = sheet[row + cnt, column + 6].Value;
                        double doubalance = 0;
                        try
                        {
                            doubalance = Convert.ToDouble(strbalance);
                        }
                        catch (Exception ex)
                        {
                            doubalance = 0.0;
                        }
                        _amountInfolst.Add(new MAmountInfo() { Id = numhangshu, Rate = dourate, Balance = doubalance });

                        // 第二个
                        strrate = sheet[row + cnt, column + 7].Value;
                        if (string.IsNullOrEmpty(strrate))
                        {
                            cnt++;
                            continue;
                        };

                        dourate = 0;
                        try
                        {
                            dourate = Convert.ToDouble(strrate);
                        }
                        catch (Exception ex)
                        {
                            dourate = 0.0;
                        }
                        strbalance = sheet[row + cnt, column + 8].Value;
                        doubalance = 0;
                        try
                        {
                            doubalance = Convert.ToDouble(strbalance);
                        }
                        catch (Exception ex)
                        {
                            doubalance = 0.0;
                        }
                        _amountInfolst.Add(new MAmountInfo() { Id = numhangshu, Rate = dourate, Balance = doubalance });

                        // 第三个
                        strrate = sheet[row + cnt, column + 9].Value;
                        if (string.IsNullOrEmpty(strrate))
                        {
                            cnt++;
                            continue;
                        };

                        dourate = 0;
                        try
                        {
                            dourate = Convert.ToDouble(strrate);
                        }
                        catch (Exception ex)
                        {
                            dourate = 0.0;
                        }
                        strbalance = sheet[row + cnt, column + 10].Value;
                        doubalance = 0;
                        try
                        {
                            doubalance = Convert.ToDouble(strbalance);
                        }
                        catch (Exception ex)
                        {
                            doubalance = 0.0;
                        }
                        _amountInfolst.Add(new MAmountInfo() { Id = numhangshu, Rate = dourate, Balance = doubalance });

                        #endregion
                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                    
                    cnt++;
                }

                #endregion

                // 退款信息
                ranges = sheet.FindAllString("退款信息", false, false);
                foreach (CellRange range in ranges)
                {
                    column = range.Rows[0].Column;
                    row = range.Rows[0].Row;
                }

                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, String.Format(" 插入调试信息: 退款信息查找  行[row={0}],列[column={1}]", row, column), typeof(xlsDataDeal));
                cnt = 2;
                while (true)
                {
                    string strhangshu = sheet[row + cnt, column].Value;
                    try
                    {
                        Int32 numhangshu = Int32.Parse(strhangshu);
                        string bankName = sheet[row + cnt, column + 1].Value;
                        if (string.IsNullOrEmpty(bankName)) break;
                        string clientName = sheet[row + cnt, column + 3].Value;
                        string bankAccount = sheet[row + cnt, column + 5].Value;
                        string systemId = sheet[row + cnt, column + 7].Value;
                        string bankProvince = sheet[row + cnt, column + 9].Value;
                        string bankCity = sheet[row + cnt, column + 10].Value;

                        _bankInfolst.Add(new MBankInfo() { Id = numhangshu, BankName = bankName, ClientName = clientName, BankAccount = bankAccount, SystemId = systemId, BankProvince = bankProvince, BankCity = bankCity });
                    }
                    catch (Exception ex)
                    {
                        break;
                    }

                    cnt++;
                }

                workbook.Dispose();
                //workbook.SaveToFile("替换.xlsx");
                //System.Diagnostics.Process.Start("替换.xlsx");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(xlsDataDeal));
                MessageBox.Show(ex.Message);
                return false;
            }

            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 结束读取文件: " + filename, typeof(xlsDataDeal));
            return true;
        }

        private bool WriteXlsData(string filename, Worksheet sheet)
        {
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 开始把上一次读的数据写入到文件: " + filename, typeof(xlsDataDeal));
            rtbLog.AppendText(DateTime.Now.ToString() + " 开始把上一次读的数据写入到文件: " + filename + "\r\n");
            try
            {
                _amountInfolst.ForEach(a =>
                {
                    MClientInfo clientInfo = _clientInfolst.Find(
                        delegate(MClientInfo oneinfo){
                            return oneinfo.Id == a.Id;
                        });

                    MBankInfo bankInfo = _bankInfolst.Find(
                        delegate(MBankInfo oneinfo)
                        {
                            return oneinfo.Id == a.Id;
                        });

                    sheet[_rowNum, 1].Text = clientInfo.OrganizeName;
                    sheet[_rowNum, 2].Text = clientInfo.AccountName;
                    sheet[_rowNum, 3].Text = clientInfo.AccountCode;
                    sheet[_rowNum, 4].Text = clientInfo.Seat;
                    sheet[_rowNum, 5].Text = clientInfo.CardId;
                    sheet[_rowNum, 6].Text = a.Rate.ToString();
                    sheet[_rowNum, 7].Text = a.Balance.ToString();
                    sheet[_rowNum, 8].Text = bankInfo.BankName;
                    sheet[_rowNum, 9].Text = bankInfo.ClientName;
                    sheet[_rowNum, 10].Text = bankInfo.BankAccount;
                    sheet[_rowNum, 11].Text = bankInfo.SystemId;
                    sheet[_rowNum, 12].Text = bankInfo.BankProvince;
                    sheet[_rowNum, 13].Text = bankInfo.BankCity;

                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, String.Format(" 插入调试信息: 文件名[filename={0}],行号[Id={1}],机构名或姓名[OrganizeName={2}]," +
                    "证券账户户名[AccountName={3}],证券账户代码[AccountCode={4}],托管席位号[Seat={5}],身份证明号码[CardId={6}],票面利率[Rate={7}],申购金额[Balance={8}],退款汇入行全称[BankName={9}],退款收款人全称[ClientName={10}],退款收款人账号[BankAccount={11}],大额支付系统号[SystemId={12}],退款汇入行省份[BankProvince={13}],退款汇入行地市[BankCity={14}]", filename, a.Id, clientInfo.OrganizeName, clientInfo.AccountName, clientInfo.AccountCode, clientInfo.Seat, clientInfo.CardId, a.Rate.ToString(), a.Balance.ToString(), bankInfo.BankName, bankInfo.ClientName, bankInfo.BankAccount, bankInfo.SystemId, bankInfo.BankProvince, bankInfo.BankCity), typeof(xlsDataDeal));

                    _rowNum++;
                });
            }
            catch (Exception ex){
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(xlsDataDeal));
                MessageBox.Show(ex.Message);
                return false;
            }
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, " 结束把上一次读的数据写入到文件: " + filename, typeof(xlsDataDeal));
            return true;
        }
    }
}
