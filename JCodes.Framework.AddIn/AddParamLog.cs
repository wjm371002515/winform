using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Text.RegularExpressions;

namespace JCodes.Framework.AddIn
{
    public partial class AddParamLog : DevExpress.XtraEditors.XtraForm
    {
        public AddParamLog()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            dealAddLogFile(@"C:\Users\wujm09397\Desktop\func\function.php", FileType.PHP);
        }

        private void dealAddLogFile(string pathfilename, FileType fileType)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(pathfilename);
            string filename = file.Name;

            string text = File.ReadAllText(pathfilename);

            // 查找全部的匹配php 函数的正则表达式
            MatchCollection result = Regex.Matches(text, @"function\s\w+\(([(\s)*\$(\D+)]?(\s)*[,(\s)*\$(\D+)]+?)??(\))+?");

            for (int i = 0; i < result.Count; i++)
            {
                StringBuilder sb1 = new StringBuilder();
                sb1.Append("\r\n\twriteLog('");
                // 获取匹配的第一内容
                Match match = result[i];
                string[] str = match.Groups[1].Value.Replace("','", "null").Split(',');
                string str2 = null;
                if (!(str.Length == 1 && string.IsNullOrWhiteSpace(str[0])))
                {
                    for (int j = 0; j < str.Length; j++)
                    {

                        if (str[j].Contains("&$") || str[j].Contains("array $"))
                            continue;

                        // 如果有默认值则删除
                        if (str[j].Contains("="))
                        {
                            int endstr = str[j].IndexOf("=");
                            str2 = str2 + str[j].Substring(0, endstr) + "='." + str[j].Substring(0, endstr) + ".'";
                        }
                        // 如果没有默认值则直接添加
                        else
                        {
                            str2 = str2 + str[j] + "='." + str[j] + ".'";
                        }
                    }
                }
                // writeLog('ApiInterface __construct start', 7, MODULE_NAME.'/'.CONTROLLER_NAME.'/'.ACTION_NAME);
                sb1.Append(match.ToString().Replace("'",@"""") + "[" + str2 + "]',  7, MODULE_NAME.'/'.CONTROLLER_NAME.'/'.ACTION_NAME);");
                
                // 找到此函数的字符串
                Int32 matchInt = text.IndexOf(match.ToString());
                // 查找大括号的位置
                Int32 matchInt2 = text.IndexOf('{', matchInt);

                text = text.Insert(matchInt2+1, sb1.ToString());
            }



            File.WriteAllText(@"C:\Users\wujm09397\Desktop\func\2.php", text);
        }
        
    }
}