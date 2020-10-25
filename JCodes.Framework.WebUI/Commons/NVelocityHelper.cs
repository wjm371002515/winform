using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using NVelocity;
using NVelocity.App;
using NVelocity.Runtime;
using NVelocity.Exception;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.WebUI.Common
{
    /// <summary>
    /// 基于NVelocity的模板文件生成辅助类
    /// </summary>
    public class NVelocityHelper
    {
        protected const string NVELOCITY_PROPERTY = "nvelocity.properties";
        protected VelocityContext context;
        protected Template template;
        protected string templateFile;

        /// <summary>
        /// 存放键值的字典内容
        /// </summary>
        private Dictionary<string, object> KeyObjDict = new Dictionary<string, object>();

        private string fileExtension = ".htm"; //输出的文件后缀名,如".cs"。
        private string directoryOfOutput = ""; //输出文件的文件夹名称
        private string fileNameOfOutput; //输出的文件名称

        /// <summary>
        /// 输出文件的文件夹名称, 如"Entity","Common"等
        /// </summary>
        public string DirectoryOfOutput
        {
            set { directoryOfOutput = value; }
            get { return directoryOfOutput; }
        }

        /// <summary>
        /// 输出的文件名称. 如果想输出的文件为 "A.cs", 那么文件名为"A".
        /// 默认的文件名称为模板文件的名称,没有后缀名.
        /// </summary>
        public string FileNameOfOutput
        {
            set { fileNameOfOutput = value; }
            get { return fileNameOfOutput; }
        }

        /// <summary>
        /// 输出的文件后缀名,如".cs"。
        /// </summary>
        public string FileExtension
        {
            get { return fileExtension; }
            set { fileExtension = value; }
        }

        /// <summary>
        /// 添加一个键值对象
        /// </summary>
        /// <param name="key">键，不可重复</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public NVelocityHelper AddKeyValue(string key, object value)
        {
            if (!KeyObjDict.ContainsKey(key))
            {
                KeyObjDict.Add(key, value);
            }
            return this;
        }

        /// <summary>
        /// 参数化构造函数，根据模板文件构造相应的对象，并初始化
        /// </summary>
        /// <param name="templateFile">模板文件</param>
        public NVelocityHelper(string templateFile)
        {
            this.templateFile = templateFile;

            InitTemplateEngine(); //初始化模板引擎

            fileNameOfOutput = "_" + GetFileNameFromTemplate(templateFile); // 默认情况下, 输出的文件名称为模板名称           
            directoryOfOutput = ""; // 默认情况下,放到当前目录下面
        }

        /// <summary>
        /// 默认构造函数，用于字符串对象的合并
        /// </summary>
        public NVelocityHelper()
        {
        }

        /// <summary>
        /// 合并字符串的内容
        /// </summary>
        /// <returns></returns>
        public string ExecuteMergeString(string inputString)
        {
            VelocityEngine templateEngine = new VelocityEngine();
            templateEngine.Init();

            InitContext();

            System.IO.StringWriter writer = new System.IO.StringWriter();
            templateEngine.Evaluate(context, writer, "mystring", inputString);

            return writer.GetStringBuilder().ToString();
        }

        /// <summary>
        ///根据模板创建输出的文件,并返回生成的文件路径
        /// </summary>
        public virtual string ExecuteFile()
        {
            string fileName = "";
            if (template != null)
            {
                string filePath = CheckEndBySlash(directoryOfOutput);
                fileName = filePath + fileNameOfOutput + fileExtension;

                if (!string.IsNullOrEmpty(filePath) && !Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                //LogTextHelper.Debug(string.Format("Class file output path:{0}", fileName));
                InitContext();
                using (StreamWriter writer = new StreamWriter(fileName, false))
                {
                    template.Merge(context, writer);
                }
            }
            return fileName;
        }

        /// <summary>
        /// 根据模板输出字符串内容
        /// </summary>
        /// <param name="templateFile"></param>
        /// <returns></returns>
        public string ExecuteString()
        {
            InitContext();    
            System.IO.StringWriter writer = new System.IO.StringWriter();
            template.Merge(context, writer);
            return writer.GetStringBuilder().ToString();
        }

        public void SetTemplateLoadPath(string loadPath)
        {
            //templateEngine.SetProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, loadPath);
        }

        /// <summary>
        /// 初始化模板引擎
        /// </summary>
        protected virtual void InitTemplateEngine()
        {
            try
            {
                //Velocity.Init(NVELOCITY_PROPERTY);
                VelocityEngine templateEngine = new VelocityEngine();
                templateEngine.SetProperty(RuntimeConstants.RESOURCE_LOADER, "file");

                templateEngine.SetProperty(RuntimeConstants.INPUT_ENCODING, "utf-8");
                templateEngine.SetProperty(RuntimeConstants.OUTPUT_ENCODING, "utf-8");

                //如果设置了FILE_RESOURCE_LOADER_PATH属性，那么模板文件的基础路径就是基于这个设置的目录，否则默认当前运行目录
                templateEngine.SetProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, AppDomain.CurrentDomain.BaseDirectory);

                templateEngine.Init();

                template = templateEngine.GetTemplate(templateFile);
            }
            catch (ResourceNotFoundException re)
            {
                string error = string.Format("Cannot find template " + templateFile);
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, re, typeof(NVelocityHelper));
                throw new Exception(error, re);
            }
            catch (ParseErrorException pee)
            {
                string error = string.Format("Syntax error in template " + templateFile + ":" + pee.StackTrace);
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, pee, typeof(NVelocityHelper));
                throw new Exception(error, pee);
            }
        }

        /// <summary>
        /// 初始化上下文的内容
        /// </summary>
        private void InitContext()
        {
            context = new VelocityContext();
            foreach (string key in KeyObjDict.Keys)
            {
                context.Put(key, KeyObjDict[key]);
            }
        }

        #region 辅助方法

        /// <summary>
        /// 从模板文件名称获取输出文件名的方法
        /// </summary>
        /// <param name="templateFileName">带后缀名的模板文件名</param>
        /// <returns>输出的文件名(无后缀名)</returns>
        private string GetFileNameFromTemplate(string templateFileName)
        {
            int sindex1 = templateFileName.LastIndexOf('/');
            int sindex2 = templateFileName.LastIndexOf('\\');

            int sindex = (sindex1 > sindex2) ? sindex1 : sindex2;
            int eindex = templateFileName.IndexOf('.');

            if (sindex < eindex)
            {
                return templateFileName.Substring(sindex + 1, eindex - sindex - 1);
            }
            else
            {
                return templateFileName.Substring(sindex);
            }
        }

        /// <summary>
        /// Gets the sourcefile path according the mainSetting values. End by slash char("/").
        /// </summary>
        /// <param name="outputDir">The output directory.</param>
        /// <param name="rootNameSpace">The root namespace of the project.</param>
        /// <returns>The valid directory path end by slash("/").</returns>
        public static string GetFilePath(string outputDir, string rootNameSpace)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(CheckEndBySlash(outputDir));
            sb.Append(CheckEndBySlash(rootNameSpace.Replace(Const.Dot, Const.Slash)));

            return sb.ToString();
        }

        /// <summary>
        /// 确保路径以"/"结束
        /// </summary>
        /// <param name="pathName">路径名称</param>
        /// <returns>以"/"结束德路径名称</returns>
        public static string CheckEndBySlash(string pathName)
        {
            if (!string.IsNullOrEmpty(pathName) && !pathName.EndsWith(Const.Slash))
            {
                return pathName + Const.Slash;
            }
            return pathName;
        }

        #endregion
    }
}
