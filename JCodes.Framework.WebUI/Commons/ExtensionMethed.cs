using JCodes.Framework.Common.Databases;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JCodes.Framework.WebUI.Common
{
    public static class ExtensionMethed
    {
        public static SearchCondition AddNumberCondition(this SearchCondition searchCondition, string fieldName, string fieldValue)
        {
            if (!string.IsNullOrEmpty(fieldValue))
            {
                bool isRangeValue = fieldValue.Contains('~');//判断是否为区间的值，否则使用Equal操作符
                string[] itemArray = fieldValue.Split('~');
                if (itemArray != null)
                {
                    decimal value = 0M;
                    bool result = false;

                    if (itemArray.Length > 0)
                    {
                        result = decimal.TryParse(itemArray[0].Trim(), out value);
                        if (result)
                        {
                            if (isRangeValue)
                            {
                                searchCondition.AddCondition(fieldName, value, SqlOperator.MoreThanOrEqual);
                            }
                            else
                            {
                                searchCondition.AddCondition(fieldName, value, SqlOperator.Equal);
                            }
                        }
                    }
                    if (itemArray.Length > 1)
                    {
                        result = decimal.TryParse(itemArray[1].Trim(), out value);
                        if (result)
                        {
                            searchCondition.AddCondition(fieldName, value, SqlOperator.LessThanOrEqual);
                        }
                    }
                }
            }
            return searchCondition;
        }

        public static SearchCondition AddDateCondition(this SearchCondition searchCondition, string fieldName, string fieldValue)
        {
            if (!string.IsNullOrEmpty(fieldValue))
            {
                string[] itemArray = fieldValue.Split('~');
                if (itemArray != null)
                {
                    DateTime value;
                    bool result = false;
                    if (itemArray.Length > 0)
                    {
                        result = DateTime.TryParse(itemArray[0].Trim(), out value);
                        if (result)
                        {
                            searchCondition.AddCondition(fieldName, value, SqlOperator.MoreThanOrEqual);
                        }
                    }
                    if (itemArray.Length > 1)
                    {
                        result = DateTime.TryParse(itemArray[1].Trim(), out value);
                        if (result)
                        {
                            searchCondition.AddCondition(fieldName, value.AddDays(1), SqlOperator.LessThan);
                        }
                    }
                }
            }
            return searchCondition;
        }

    }
}