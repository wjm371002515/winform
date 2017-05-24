using JCodes.Framework.Common;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestCommons
{
    /// <summary>
    /// 单件测试类
    /// </summary>
    public class TestSingletonClass
    {
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private TestSingletonClass()
        {
        }

        public void ShowMessage()
        {
            MessageDxUtil.ShowTips("单件实例测试函数");
        }
    }

    /// <summary>
    /// 单件测试类
    /// </summary>
    public class TestSingletonClass2
    {
        private static TestSingletonClass2 m_Instance;

        /// <summary>
        /// 单件实例
        /// </summary>
        public static TestSingletonClass2 Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new TestSingletonClass2();
                }
                return m_Instance;
            }
        }

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private TestSingletonClass2()
        {
        }

        public void ShowMessage()
        {
            MessageDxUtil.ShowTips("单件实例测试函数");
        }
    }
}
