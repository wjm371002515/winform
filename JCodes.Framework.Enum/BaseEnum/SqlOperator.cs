using System.ComponentModel;

namespace JCodes.Framework.jCodesenum.BaseEnum
{
    /// <summary>
    /// Sql的查询符号
    /// </summary>
    public enum SqlOperator
    {
        /// <summary>
        /// Like 模糊查询
        /// </summary>
        [Description("Like 模糊查询")]
        Like,

        /// <summary>
        /// Not LiKE 模糊查询
        /// </summary>
        [Description("Not LiKE 模糊查询")]
        NotLike,

        /// <summary>
        /// Like 开始匹配模糊查询，如Like 'ABC%'
        /// </summary>
        [Description("Like 开始匹配模糊查询，如Like 'ABC%'")]
        LikeStartAt,

        /// <summary>
        /// ＝ 等于号 
        /// </summary>
        [Description("＝ 等于号")]
        Equal,

        /// <summary>
        /// ＜＞ (≠) 不等于号
        /// </summary>
        [Description("<> (≠) 不等于号")]
        NotEqual,

        /// <summary>
        /// ＞ 大于号
        /// </summary>
        [Description("＞ 大于号")]
        MoreThan,

        /// <summary>
        /// ＜ 小于号 
        /// </summary>
        [Description("＜小于号")]
        LessThan,

        /// <summary>
        /// ≥大于或等于号 
        /// </summary>
        [Description("≥大于或等于号 ")]
        MoreThanOrEqual,

        /// <summary>
        /// ≤ 小于或等于号
        /// </summary>
        [Description("≤ 小于或等于号")]
        LessThanOrEqual,

        /*       
        /// <summary>
        /// 在某个值的中间，拆成两个符号 >= 和 <=
        /// </summary>
        Between,
        */

        /// <summary>
        /// 在某个字符串值中
        /// </summary>
        [Description("在某个字符串值中")]
        In
    }
}
