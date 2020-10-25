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
    public partial class ZheshangDGTools : XtraForm
    {
        public ZheshangDGTools()
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
            if (string.IsNullOrEmpty(txtxlsFIle.Text.Trim()))
            {
                MessageDxUtil.ShowError("XLS文件请选择");
                txtxlsFIle.Focus();
                return;
            }

            String filePath = @"G:\缺失底稿\";
            if (!Directory.Exists(filePath + @"生成底稿目录\")) {
                Directory.CreateDirectory(filePath + @"生成底稿目录\");
            }

            Task task1 = new Task(() => {
                Workbook workbookSrc = null;

                string file = txtxlsFIle.Text.Trim();
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

                //cells[0, 7].PutValue("吴建明测试");
                Int32 i = 2;
                while (cells[i, 0].Type == Aspose.Cells.CellValueType.IsNumeric )
                {
                    if (cells[i, 6].Value.ToString() == "不存在" && cells[i, 0].Value.ToString() == "30010321")
                    {
                        if (FileUtil.IsExistFile(filePath + cells[i, 5].Value))
                        {
                            //File.Move(filePath + cells[i, 5].Value, filePath + cells[i, 4].Value + "``" + cells[i, 5].Value);
                            if (!File.Exists(filePath + @"生成底稿目录\" + cells[i, 4].Value + "``" + cells[i, 5].Value))
                            {
                                File.Copy(filePath + cells[i, 5].Value, filePath + @"生成底稿目录\" + cells[i, 4].Value + "``" + cells[i, 5].Value);
                                Thread.Sleep(500);
                            }

                            cells[i, 7].PutValue("已处理");
                        }
                    }

                    i++;
                }

                workbookSrc.Save(@"C:\Users\Jimmy\Desktop\处理xls文件.xlsx", SaveFormat.Xlsx);

                /*while (cells[i, 0])

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
                }*/
                #endregion

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
