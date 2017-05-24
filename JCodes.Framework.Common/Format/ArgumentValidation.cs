using System;
using System.Collections.Generic;
using System.Text;

namespace JCodes.Framework.Common.Format
{
    /// <summary>
    /// 参数验证的通用校验辅助类
    /// </summary>
    public sealed class ArgumentValidation
    {
        private ArgumentValidation()
        {
        }

        /// <summary>
        /// <para>检查参数<paramref name="variable"/>是否为空字符串。</para>
        /// </summary>
        /// <param name="variable">待检查的值</param>
        /// <param name="variableName">参数的名称</param>
        public static void CheckForEmptyString(string variable, string variableName)
        {
            CheckForNullReference(variable, variableName);
            CheckForNullReference(variableName, "variableName");
            if (variable.Length == 0)
            {
                string message = string.Format(Const.ExceptionEmptyString, variableName);
                throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// <para>检查参数<paramref name="variable"/>是否为空引用(Null)。</para>
        /// </summary>
        /// <param name="variable">待检查的值</param>
        /// <param name="variableName">待检查变量的名称</param>
        public static void CheckForNullReference(object variable, string variableName)
        {
            if (variableName == null)
            {
                throw new ArgumentNullException("variableName");
            }

            if (null == variable)
            {
                throw new ArgumentNullException(variableName);
            }
        }

        /// <summary>
        /// 验证输入的参数messageName非空字符串，也非空引用
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="messageName">参数的值</param>
        public static void CheckForInvalidNullNameReference(string name, string messageName)
        {
            if ((null == name) || (name.Length == 0))
            {
                string message = string.Format(Const.ExceptionInvalidNullNameArgument, messageName);
                throw new InvalidOperationException(message);
            }
        }

        /// <summary>
        /// <para>验证参数<paramref name="bytes"/>非零长度，如果为零长度，则抛出异常<see cref="ArgumentException"/>。</para>
        /// </summary>
        /// <param name="bytes">待检查的字节数组</param>
        /// <param name="variableName">待检查参数的名称</param>
        public static void CheckForZeroBytes(byte[] bytes, string variableName)
        {
            CheckForNullReference(bytes, "bytes");
            CheckForNullReference(variableName, "variableName");
            if (bytes.Length == 0)
            {
                string message = string.Format(Const.ExceptionByteArrayValueMustBeGreaterThanZeroBytes, variableName);
                throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// <para>检查参数<paramref name="variable"/>是否符合指定的类型。</para>
        /// </summary>
        /// <param name="variable">待检查的值</param>
        /// <param name="type">参数variable的类型</param>
        public static void CheckExpectedType(object variable, Type type)
        {
            CheckForNullReference(variable, "variable");
            CheckForNullReference(type, "type");
            if (!type.IsAssignableFrom(variable.GetType()))
            {
                string message = string.Format(Const.ExceptionExpectedType, type.FullName);
                throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// 检查variable是否一个有效的<paramref name="enumType"/>枚举类型
        /// </summary>
        /// <param name="variable">待检查的值</param>
        /// <param name="enumType">参数variable的枚举类型</param>
        /// <param name="variableName">变量variable的名称</param>
        public static void CheckEnumeration(Type enumType, object variable, string variableName)
        {
            CheckForNullReference(variable, "variable");
            CheckForNullReference(enumType, "enumType");
            CheckForNullReference(variableName, "variableName");

            if (!Enum.IsDefined(enumType, variable))
            {
                string message = string.Format(Const.ExceptionEnumerationNotDefined,
                    variable.ToString(), enumType.FullName, variableName);
                throw new ArgumentException(message);
            }
        }
    }
}
