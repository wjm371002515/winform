using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Reporting.WebForms;

namespace JCodes.Framework.WebUI.Common
{
    /// <summary>
    /// 报表打印辅助类
    /// </summary>
    public static class ReportHelper
    {
        private readonly static string REPORT_DIRECTORY = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Report");

        /// <summary>
        /// 通过报表名称加载报表
        /// </summary>
        /// <param name="report">LocalReport对象</param>
        /// <param name="reportName">报表名称（不带".rdlc"）</param>
        /// <param name="dataSourceDict">数据源集合</param>
        /// <param name="parameters">参数集合</param>
        public static void LoadReport(LocalReport report, string reportName, Dictionary<string, object> dataSourceDict, NameValueCollection parameters)
        {
            string reportPath = Path.Combine(REPORT_DIRECTORY, reportName) + ".rdlc";
            LoadReportWithPath(report, reportPath, dataSourceDict, parameters);
        }

        /// <summary>
        /// 通过报表相对路径，加载报表
        /// </summary>
        /// <param name="report">LocalReport对象</param>
        /// <param name="reportPath">报表文件相对路径（带".rdlc"后缀）</param>
        /// <param name="dataSourceDict">数据源集合</param>
        /// <param name="parameters">参数集合</param>
        public static void LoadReportWithPath(LocalReport report, string reportPath, Dictionary<string, object> dataSourceDict, NameValueCollection parameters)
        {
            report.ReportPath = reportPath;

            report.DataSources.Clear();
            foreach (string sourceName in dataSourceDict.Keys)
            {
                report.DataSources.Add(new ReportDataSource(sourceName, dataSourceDict[sourceName]));
            }
            report.Refresh();

            if (parameters != null && parameters.Count > 0)
            {
                foreach (string key in parameters)
                {
                    // TODO 这里先注释掉
                    //report.SetParameters(new ReportParameter(key, parameters[key]));
                }
            }
        }

        /// <summary>
        /// 打印报表
        /// </summary>
        /// <param name="reportName">报表名称</param>
        /// <param name="dataSourceDict">报表数据集和数据源的映射关系</param>
        /// <param name="parameters">报表参数集合</param>
        public static void Print(string reportName, Dictionary<string, object> dataSourceDict, NameValueCollection parameters = null)
        {
            LocalReport report = new LocalReport();
            LoadReport(report, reportName, dataSourceDict, parameters);
            LocalReportPrinter executor = new LocalReportPrinter(report);
            executor.Print();
        }

        /// <summary>
        /// 根据报表文件路径，打印报表
        /// </summary>
        /// <param name="reportName">报表名称</param>
        /// <param name="dataSourceDict">报表数据集和数据源的映射关系</param>
        /// <param name="parameters">报表参数集合</param>
        public static void PrintWithPath(string reportPath, Dictionary<string, object> dataSourceDict, NameValueCollection parameters = null)
        {
            LocalReport report = new LocalReport();
            LoadReportWithPath(report, reportPath, dataSourceDict, parameters);
            LocalReportPrinter executor = new LocalReportPrinter(report);
            executor.Print();
        }

        /// <summary>
        /// 转换Image对象为Base64字符串
        /// </summary>
        /// <param name="image">Image对象</param>
        /// <param name="format">图片格式</param>
        /// <returns></returns>
        public static string ConvertImageToBase64(System.Drawing.Image image, ImageFormat format)
        {
            byte[] imageArray;

            using (System.IO.MemoryStream imageStream = new System.IO.MemoryStream())
            {
                image.Save(imageStream, format);
                imageArray = new byte[imageStream.Length];
                imageStream.Seek(0, System.IO.SeekOrigin.Begin);
                imageStream.Read(imageArray, 0, (int)imageStream.Length);
            }

            return Convert.ToBase64String(imageArray);
        }



        public static byte[] GenerateReport(string dataSetName, IEnumerable dataSource, string reportFilePath,
                               string reportType, out string mimeType, out string encoding, out string fileNameExtension)
        {

            ReportDataSource reportDataSource = new ReportDataSource(dataSetName, dataSource);

            LocalReport localReport = new LocalReport();
            localReport.ReportPath = reportFilePath;
            localReport.DataSources.Add(reportDataSource);

            string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>" + reportType + "</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return renderedBytes;
        }
    }
}
