using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.IO;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// 命令行参数解析工具类
    /// </summary>
    public static class ParamParser
    {
        /// <summary>
        /// 静态初始化函数
        /// </summary>
        static ParamParser()
        {
            string[] cmdLineArgs = Environment.GetCommandLineArgs();

            _appFullName = cmdLineArgs[0];
            if (Path.IsPathRooted(_appFullName) == false)
            {
                _appFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _appFullName);
            }
            _appName = Path.GetFileNameWithoutExtension(_appFullName);

            string[] args = new string[cmdLineArgs.Length - 1];
            Array.Copy(cmdLineArgs, 1, args, 0, args.Length);

            _switches = new StringDictionary();
            _parameters = new StringCollection();

            Parse(args);
        }

        /// <summary>
        /// 内部转换参数
        /// </summary>
        /// <param name="args"></param>
        private static void Parse(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                string currParam = args[i].ToLower();
                if (currParam.StartsWith("/") || currParam.StartsWith("-"))
                {
                    if (currParam.Length >= 2)
                    {
                        string currSwitch = currParam.Substring(1);
                        int switchValueStartIndex = currSwitch.IndexOf(":");

                        string switchKey = switchValueStartIndex > 0 ?
                            currSwitch.Substring(0, switchValueStartIndex) : currSwitch;

                        string switchValue = switchValueStartIndex > 0 ?
                            currSwitch.Substring(switchValueStartIndex + 1) : null;

                        if (switchValue != null)
                            switchValue = switchValue.Trim('\"');
                        else
                            switchValue = string.Empty;

                        _switches.Add(switchKey.ToLower(), switchValue.ToLower());
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
                else
                {
                    _parameters.Add(currParam.ToLower());
                }
            }
        }

        private static string _appFullName;
        /// <summary>
        /// 获取启动程序的全路径名称
        /// </summary>
        public static string AppFullName
        {
            get { return _appFullName; }
        }

        private static string _appName;
        /// <summary>
        /// 获取启动程序的短名称
        /// </summary>
        public static string AppName
        {
            get { return _appName; }
        }


        private static StringDictionary _switches;
        /// <summary>
        /// 测试启动命令是否包含给定的开关键
        /// </summary>
        /// <param name="switchKey">需要测试的开关键</param>
        /// <returns>如果启动命令包含给定的开关键则返回true，否则返回false</returns>
        public static bool ContainSwitch(string switchKey)
        {
            return _switches.ContainsKey(switchKey.ToLower());
        }

        /// <summary>
        /// 获取启动命令中给定开关键对应的值
        /// </summary>
        /// <param name="switchKey">需要返回值的开关键</param>
        /// <returns>如果启动命令包含给定的开关键则返回该键对应的值，否则返回null</returns>
        public static string GetSwitchValue(string switchKey)
        {
            switchKey = switchKey.ToLower();
            if(_switches.ContainsKey(switchKey))
                return _switches[switchKey];
            return null;
        }

        private static StringCollection _parameters;
        /// <summary>
        /// 获取给定索引处的参数
        /// </summary>
        /// <param name="index">参数的索引值</param>
        /// <returns>如果索引超出范围则返回string.Empty，否则返回给定索引处的参数值</returns>
        public static string GetParameter(int index)
        {
            if (index >= _parameters.Count) return string.Empty;

            return _parameters[index];
        }
    }

    /// <summary>
    /// 格式化输出支持的命令集
    /// </summary>
    public class HelpPrinter
    {
        private string _description;
        private string _pattern;
        private int _patternPadding;
        private NameValueCollection _comment;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="description">对该启动程序的总的说明</param>
        /// <param name="pattern">总的命令行启动模式</param>
        public HelpPrinter(string description, string pattern)
        {
            _description = description;
            _pattern = pattern;
            _patternPadding = 16;
            _comment = new NameValueCollection();
        }

        /// <summary>
        /// 模式与模式说明间的空白补白最大值
        /// </summary>
        public int PatternPadding
        {
            get { return _patternPadding; }
            set { _patternPadding = value; }
        }

        /// <summary>
        /// 添加一项参数的说明
        /// </summary>
        /// <param name="param">参数名称</param>
        /// <param name="descrption">该参数的详细说明</param>
        public void AddComment(string param, string descrption)
        {
            _comment.Add(param, descrption);
        }

        /// <summary>
        /// 向Console打印出格式化的命令集说明
        /// </summary>
        /// <returns></returns>
        public string Print()
        {
            StringBuilder sb = new StringBuilder();

            using (StringWriter writer = new StringWriter(sb))
            {
                writer.WriteLine(_description);
                writer.WriteLine();
                writer.WriteLine(_pattern);
                writer.WriteLine();

                string patternFormat = "  {0,-" + _patternPadding + "}";
                for (int i = 0; i < _comment.Count; i++)
                {
                    string key = _comment.GetKey(i);
                    writer.Write(patternFormat, key);

                    string[] comments = _comment.GetValues(i);
                    for (int j = 0; j < comments.Length; j++)
                    {
                        if ((j > 0) && (j % 2 == 0))
                        {
                            writer.WriteLine();
                            writer.Write(new string(' ', 18));
                        }
                        writer.Write("{0,-24}", comments[j]);
                    }
                    writer.WriteLine();
                }
                writer.WriteLine();
                writer.Flush();
            }
            return sb.ToString();
        }
    }
}
