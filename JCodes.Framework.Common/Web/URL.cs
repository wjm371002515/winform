using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCodes.Framework.Common.Web
{
    /// <summary>
    /// URL 操作类
    /// </summary>
    public class URL
    {
        /// <summary>
        /// URL 字符转成转义符
        /// </summary>
        /// <param name="character">转义的字符</param>
        /// +       url中+表示空格    　　　　%2B
        /// /       分割目录和子目录　　　　  %20
        /// ?       分割实际的url和参数       %3F
        /// %       指定特殊字符              %25
        /// #       表示书签                  %23
        /// &       url中指定的参数的分隔符   %26
        /// =       url指定参数的值           %3D
        /// <returns>转义后的字符(如果没有找到则范围空)</returns>
        public static string EnConvert(char character)
        {
            string result = null;
            switch (character)
            {
                case '+': result = "%2B"; break;
                case '/': result = "%20"; break;
                case '?': result = "%3F"; break;
                case '%': result = "%25"; break;
                case '#': result = "%23"; break;
                case '&': result = "%26"; break;
                case '=': result = "%3D"; break;
            }
            return result;
        }

        /// <summary>
        /// URL 转义符转成字符
        /// </summary>
        /// <param name="escapeChar">转义字符串</param>
        /// %2B      url中+表示空格    　　　+　
        /// %20      分割目录和子目录　　　　 /  
        /// %3F      分割实际的url和参数      ? 
        /// %25       指定特殊字符            %  
        /// %23       表示书签                # 
        /// %26       url中指定的参数的分隔符 &
        /// %3D       url指定参数的值         =
        /// <returns>字符(如果没有则返回\0字符)</returns>
        public static char DeConvert(string escapeChar)
        {
            char result = '\0';
            switch (escapeChar)
            {
                case "%2B": result = '+'; break;
                case "%20": result = '/'; break;
                case "%3F": result = '?'; break;
                case "%25": result = '%'; break;
                case "%23": result = '#'; break;
                case "%26": result = '&'; break;
                case "%3D": result = '='; break;
            }
            return result;
        }

        /// <summary>
        /// URL地址转换为含有转义符的URL
        /// </summary>
        /// <param name="url">不含有转义符的URL</param>
        /// <returns>含有转义符的URL</returns>
        public static string URLDeConvert(string url)
        {
            return url.Replace("%", "%25").Replace("+", "%2B").Replace("/", "%20").Replace("?", "%3F").Replace("#", "%23").Replace("&", "%26").Replace("=", "%3D");
        }

        /// <summary>
        /// URL含有转义符的转换为URL地址
        /// </summary>
        /// <param name="url"><含有转义符的/param>
        /// <returns>不含有转义符的</returns>
        public static string URLEnConvert(string url)
        {
            return url.Replace("%2B", "+").Replace("%20", "/").Replace("%3F", "?").Replace("%23", "#").Replace("%26", "&").Replace("%3D", "=").Replace("%25", "%");
        }
    }
}
