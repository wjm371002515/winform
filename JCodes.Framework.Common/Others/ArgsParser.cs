using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace JCodes.Framework.Common.Office
{
    /// <summary>
    /// 分析应用程序的args变量，即分析参数。
    /// 作为CommandLine辅助类相同功能的补充。
    /// </summary>
    public class ArgsParser
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ArgsParser() : this("/")
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="OptionStarter">确定其中一个参数可选项开始的文本</param>
        public ArgsParser(string OptionStarter)
        {
            if (string.IsNullOrEmpty(OptionStarter))
                throw new ArgumentNullException("OptionStarter");

            this.OptionStarter = OptionStarter;
            OptionRegex = new Regex(string.Format(@"(?<Command>{0}[^\s]+)[\s|\S|$](?<Parameter>""[^""]*""|[^""{0}]*)", OptionStarter));
        }

        /// <summary>
        /// 解析程序参数为单独的参数可选项列表
        /// </summary>
        /// <param name="Args">待转换的参数</param>
        /// <returns>选项列表</returns>
        public virtual List<Option> Parse(string[] Args)
        {
            if (Args == null)
                return new List<Option>();
            List<Option> Result = new List<Option>();
            string Text = "";
            string Splitter = "";
            foreach (string Arg in Args)
            {
                Text += Splitter + Arg;
                Splitter = " ";
            }

            MatchCollection Matches = OptionRegex.Matches(Text);
            string Option = "";
            foreach (Match OptionMatch in Matches)
            {
                if (OptionMatch.Value.StartsWith(OptionStarter) && !string.IsNullOrEmpty(Option))
                {
                    Result.Add(new Option(Option, OptionStarter));
                    Option = "";
                }
                Option += OptionMatch.Value + " ";
            }
            Result.Add(new Option(Option, OptionStarter));
            return Result;
        }

        /// <summary>
        /// 启动参数可选项的正则表达式
        /// </summary>
        protected virtual Regex OptionRegex { get; set; }

        /// <summary>
        /// 启动参数可选项的字符串
        /// </summary>
        protected virtual string OptionStarter { get; set; }

    }
}