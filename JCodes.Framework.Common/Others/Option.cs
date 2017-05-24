using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace JCodes.Framework.Common.Office
{
    /// <summary>
    /// 包含单个的程序启动参数可选项
    /// </summary>
    public class Option
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Text">参数可选项文本</param>
        /// <param name="OptionStarter">参数可选项的开始字符 ("/", "-", etc.)</param>
        public Option(string Text, string OptionStarter)
        {
            if (string.IsNullOrEmpty(Text))
                throw new ArgumentNullException("Text");
            if (string.IsNullOrEmpty(OptionStarter))
                throw new ArgumentNullException("OptionStarter");
            Regex CommandParser = new Regex(string.Format(@"{0}(?<Command>[^\s]*)\s(?<Parameters>.*)", OptionStarter));
            Regex ParameterParser = new Regex("(?<Parameter>\"[^\"]*\")[\\s]?|(?<Parameter>[^\\s]*)[\\s]?");
            Parameters = new List<string>();
            Match CommandMatch = CommandParser.Match(Text);
            Command = CommandMatch.Groups["Command"].Value;
            Text = CommandMatch.Groups["Parameters"].Value;

            foreach(Match match in ParameterParser.Matches(Text))
            {
                if(!string.IsNullOrEmpty(match.Value))
                {
                    Parameters.Add(match.Groups["Parameter"].Value);
                }
            }
        }

        /// <summary>
        /// ToString描述
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("Command: ")
                   .Append(Command)
                   .Append("\n")
                   .Append("Parameters: ");
            Parameters.ForEach(x => Builder.Append(x).Append(" "));
            return Builder.Append("\n")
                          .ToString();
        }

        /// <summary>
        /// 命令字符串
        /// </summary>
        public virtual string Command { get; set; }

        /// <summary>
        /// 找到的参数列表
        /// </summary>
        public virtual List<string> Parameters { get; set; }

    }
}
