using DevExpress.XtraBars.Alerter;
using DevExpress.XtraEditors;
using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Others;
using JCodes.Framework.Common.Threading;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.Cells;
using JCodes.Framework.Common.Format;

namespace JCodes.Framework.TestWinForm.Haotian
{
    public partial class FrmXlsDataDeal : XtraForm
    {
        AppConfig config = new AppConfig();

        Int32 normalRow = 0;

        Dictionary<string, string> cacheProducts = new Dictionary<string, string>();

        public FrmXlsDataDeal()
        {
            InitializeComponent();
        }

        private delegate void InvokeCallback(LogLevel loglevel, String logstr);

        private void AddLog(LogLevel loglevel, String logstr) {

            LogHelper.WriteLog(loglevel, logstr, typeof(FrmXlsDataDeal));

            if (memoEdit1.InvokeRequired)//当前线程不是创建线程
                memoEdit1.Invoke(new InvokeCallback(AddLog), new object[] { loglevel, logstr });//回调
            else//当前线程是创建线程（界面线程）
            {
                memoEdit1.Text = string.Format("日志级别:{0}----日志内容{1}\r\n", loglevel, logstr) + memoEdit1.Text;//直接更新
            }
        }

        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeal_Click(object sender, EventArgs e)
        {
            // 校验文件是否已经选择了
            if (string.IsNullOrEmpty(txtReferenceFile.Text.Trim()))
            {
                MessageDxUtil.ShowError("参考XLS文件未配置");
                txtReferenceFile.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtToDealFile.Text.Trim()))
            {
                MessageDxUtil.ShowError("需要处理的XLS文件未配置");
                txtToDealFile.Focus();
                return;
            }

            Task task1 = new Task(() => {
                Workbook workbookSrc = null;

                string file = txtReferenceFile.Text.Trim();
                // 特殊处理 如果遇到打不开的文件则此文件被破坏需要跳过
                try
                {
                    // 读取xls数据 判断是否是合法的xls表格
                    workbookSrc = new Workbook(file);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, string.Format("{0}文件被破坏打不开", file), typeof(FrmXlsDataDeal));
                }

                #region 加载第一页
                Worksheet sheetSrc = workbookSrc.Worksheets[0];
                Cells cells = sheetSrc.Cells;

                Int32 productNameIndex = string.IsNullOrEmpty(config.AppConfigGet("SrcProductNameIndex")) ? 0 : ConvertHelper.ToInt32(config.AppConfigGet("SrcProductNameIndex"), 0);
                string productName = config.AppConfigGet("SrcProductName");
                Int32 tmpproductNameIndex = 0;
                for (Int32 i = 0; i < 10; i++)
                {
                    if (string.Equals(cells[tmpproductNameIndex + i, productNameIndex].DisplayStringValue, productName))
                    {
                        tmpproductNameIndex = tmpproductNameIndex + i;
                        break;
                    }
                }

                Int32 productUnitIndex = string.IsNullOrEmpty(config.AppConfigGet("SrcProductUnitIndex")) ? 0 : ConvertHelper.ToInt32(config.AppConfigGet("SrcProductUnitIndex"), 0);
                string productUnit = config.AppConfigGet("SrcProductUnit");
                Int32 tmpproductUnitIndex = 0;
                for (Int32 i = 0; i < 10; i++)
                {
                    if (string.Equals(cells[tmpproductUnitIndex + i, productUnitIndex].DisplayStringValue, productUnit))
                    {
                        tmpproductUnitIndex = tmpproductUnitIndex + i;
                        break;
                    }
                }

                if (tmpproductUnitIndex == tmpproductNameIndex)
                {
                    normalRow = tmpproductNameIndex;
                }
                else
                {
                    MessageDxUtil.ShowError("参考XLS文件 格式有错误请检查");
                    return;
                }

                cacheProducts.Clear();
                normalRow++;
                // 两个都不为空则为有效数据，如果一个为空或者2个都为空则到结尾了
                while (!string.IsNullOrEmpty(cells[normalRow, productNameIndex].DisplayStringValue) && !string.IsNullOrEmpty(cells[normalRow, productUnitIndex].DisplayStringValue))
                {
                    cacheProducts.Add(cells[normalRow, productNameIndex].DisplayStringValue, cells[normalRow, productUnitIndex].DisplayStringValue);

                    AddLog(LogLevel.LOG_LEVEL_INFO, string.Format("缓存加载 参考项识别内容: {0}配置为{1} {2}配置为{3}", productName, cells[normalRow, productNameIndex].DisplayStringValue, productUnit, cells[normalRow, productUnitIndex].DisplayStringValue));

                    normalRow++;
                }
                #endregion


                #region 加载第二页
                sheetSrc = workbookSrc.Worksheets[1];
                cells = sheetSrc.Cells;

                productNameIndex = string.IsNullOrEmpty(config.AppConfigGet("SrcProductNameIndex")) ? 0 : ConvertHelper.ToInt32(config.AppConfigGet("SrcProductNameIndex"), 0);
                productName = config.AppConfigGet("SrcProductName");
                tmpproductNameIndex = 0;
                for (Int32 i = 0; i < 10; i++)
                {
                    if (string.Equals(cells[tmpproductNameIndex + i, productNameIndex].DisplayStringValue, productName))
                    {
                        tmpproductNameIndex = tmpproductNameIndex + i;
                        break;
                    }
                }

                productUnitIndex = string.IsNullOrEmpty(config.AppConfigGet("SrcProductUnitIndex")) ? 0 : ConvertHelper.ToInt32(config.AppConfigGet("SrcProductUnitIndex"), 0);
                productUnit = config.AppConfigGet("SrcProductUnit");
                tmpproductUnitIndex = 0;
                for (Int32 i = 0; i < 10; i++)
                {
                    if (string.Equals(cells[tmpproductUnitIndex + i, productUnitIndex].DisplayStringValue, productUnit))
                    {
                        tmpproductUnitIndex = tmpproductUnitIndex + i;
                        break;
                    }
                }

                if (tmpproductUnitIndex == tmpproductNameIndex)
                {
                    normalRow = tmpproductNameIndex;
                }
                else
                {
                    MessageDxUtil.ShowError("参考XLS文件 格式有错误请检查");
                    return;
                }

                normalRow++;
                // 两个都不为空则为有效数据，如果一个为空或者2个都为空则到结尾了
                while (!string.IsNullOrEmpty(cells[normalRow, productNameIndex].DisplayStringValue) && !string.IsNullOrEmpty(cells[normalRow, productUnitIndex].DisplayStringValue))
                {
                    cacheProducts.Add(cells[normalRow, productNameIndex].DisplayStringValue, cells[normalRow, productUnitIndex].DisplayStringValue);

                    AddLog(LogLevel.LOG_LEVEL_INFO, string.Format("缓存加载 参考项识别内容: {0}配置为{1} {2}配置为{3}", productName, cells[normalRow, productNameIndex].DisplayStringValue, productUnit, cells[normalRow, productUnitIndex].DisplayStringValue));

                    normalRow++;
                }
                #endregion

                Workbook workbookDesc = null;

                string fileDesc = txtToDealFile.Text.Trim();
                // 特殊处理 如果遇到打不开的文件则此文件被破坏需要跳过
                try
                {
                    // 读取xls数据 判断是否是合法的xls表格
                    workbookDesc = new Workbook(fileDesc);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, string.Format("{0}文件被破坏打不开", file), typeof(FrmXlsDataDeal));
                }

                Worksheet sheetDesc = workbookDesc.Worksheets[0];
                Cells cellsDesc = sheetDesc.Cells;

                Int32 descproductNameIndex = string.IsNullOrEmpty(config.AppConfigGet("DescProductNameIndex")) ? 0 : ConvertHelper.ToInt32(config.AppConfigGet("DescProductNameIndex"), 0);
                string descproductName = config.AppConfigGet("DescProductName");
                Int32 desctmpproductNameIndex = 0;
                for (Int32 i = 0; i < 10; i++)
                {
                    if (string.Equals(cellsDesc[desctmpproductNameIndex + i, descproductNameIndex].DisplayStringValue, descproductName))
                    {
                        desctmpproductNameIndex = desctmpproductNameIndex + i;
                        break;
                    }
                }

                Int32 descproductUnitIndex = string.IsNullOrEmpty(config.AppConfigGet("DescProductUnitIndex")) ? 0 : ConvertHelper.ToInt32(config.AppConfigGet("DescProductUnitIndex"), 0);
                string descproductUnit = config.AppConfigGet("DescProductUnit");
                Int32 desctmpproductUnitIndex = 0;
                for (Int32 i = 0; i < 10; i++)
                {
                    if (string.Equals(cellsDesc[desctmpproductUnitIndex + i, descproductUnitIndex].DisplayStringValue, descproductUnit))
                    {
                        desctmpproductUnitIndex = desctmpproductUnitIndex + i;
                        break;
                    }
                }

                if (desctmpproductUnitIndex == desctmpproductNameIndex)
                {
                    normalRow = desctmpproductNameIndex;
                }
                else
                {
                    MessageDxUtil.ShowError("参考XLS文件 格式有错误请检查");
                    return;
                }

                normalRow++;
                // 两个都不为空则为有效数据，如果一个为空或者2个都为空则到结尾了

                while (!string.IsNullOrEmpty(cellsDesc[normalRow, descproductNameIndex].DisplayStringValue) && !string.IsNullOrEmpty(cellsDesc[normalRow, descproductUnitIndex].DisplayStringValue))
                {
                    Boolean isFind = false;
                    foreach (var v in cacheProducts)
                    {
                        if (v.Key.Contains(cellsDesc[normalRow, descproductNameIndex].DisplayStringValue))
                        {
                            cellsDesc[normalRow, descproductUnitIndex].PutValue(v.Value);

                            AddLog(LogLevel.LOG_LEVEL_INFO, string.Format("{0}配置为{1} {2}配置为{3} 匹配到记录 {2}更新为{4}", descproductName, cellsDesc[normalRow, descproductNameIndex].DisplayStringValue, descproductUnit, cellsDesc[normalRow, descproductUnitIndex].DisplayStringValue, v.Value));

                            isFind = true;
                            break;
                        }
                    }

                    if (!isFind)
                    {
                        AddLog(LogLevel.LOG_LEVEL_INFO, string.Format("{0}配置为{1} {2}配置为{3} 未匹配到记录 不做更新", descproductName, cellsDesc[normalRow, descproductNameIndex].DisplayStringValue, descproductUnit, cellsDesc[normalRow, descproductUnitIndex].DisplayStringValue));
                    }
                    normalRow++;
                }

                FileInfo f = new FileInfo(txtToDealFile.Text.Trim());
                workbookDesc.Save(f.Name, SaveFormat.Xlsx);
                System.Diagnostics.Process.Start(f.Name);

                MessageBox.Show("执行完成");
            });

            task1.Start();
        }

        //默认打开路径
        private string InitialDirectory = "D:\\";

        //统一对话框
        private bool InitialDialog(FileDialog fileDialog, string title)
        {
            fileDialog.InitialDirectory = InitialDirectory;//初始化路径
            fileDialog.Filter = "xls files (*.xls,*.xlsx,*.*)|*.xls;*.xlsx;*.*";//过滤选项设置，文本文件，所有文件。
            fileDialog.FilterIndex = 1;//当前使用第二个过滤字符串
            fileDialog.RestoreDirectory = true;//对话框关闭时恢复原目录
            fileDialog.Title = title;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                for (int i = 1; i <= fileDialog.FileName.Length; i++)
                {
                    if (fileDialog.FileName.Substring(fileDialog.FileName.Length - i, 1).Equals(@"\"))
                    {
                        //更改默认路径为最近打开路径
                        InitialDirectory = fileDialog.FileName.Substring(0, fileDialog.FileName.Length - i + 1);
                        return true;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private void txt_Enter(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();//打开文件对话框              
            if (InitialDialog(openFileDialog, "Open"))
            {
                using (Stream stream = openFileDialog.OpenFile())
                {
                    string fileName = ((System.IO.FileStream)stream).Name;
                    // 执行相关文件操作
                    (sender as TextEdit).Text = fileName;
                }
            } 
        }        
    }
}
