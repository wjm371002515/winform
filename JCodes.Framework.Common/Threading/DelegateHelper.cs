using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Threading;

namespace JCodes.Framework.Common.Threading
{
    /// <summary>
    /// 委托处理辅助类
    /// </summary>
    public static  class DelegateHelper
    {
        private static WaitCallback dynamicInvoker = new WaitCallback(DynamicInvoke);

        /// <summary>
        /// 执行委托操作
        /// </summary>
        /// <param name="target">目标委托对象</param>
        /// <param name="args">参数</param>
        public static WorkItem InvokeDelegate(Delegate target, params object[] args)
        {
            return AbortableThreadPool.QueueUserWorkItem(dynamicInvoker, new TargetInfo(target, args));
        }

        /// <summary>
        /// 执行委托操作
        /// </summary>
        /// <param name="target">目标委托对象</param>
        public static WorkItem InvokeDelegate(Delegate target)
        {
            return AbortableThreadPool.QueueUserWorkItem(dynamicInvoker, new TargetInfo(target, null));
        }

        /// <summary>
        /// 中止指定的队列中委托
        /// </summary>
        /// <param name="target">目标委托对象</param>
        /// <returns>项目队列中止操作的状态</returns>
        public static WorkItemStatus AbortDelegate(WorkItem target)
        {
            return AbortableThreadPool.Cancel(target, true);
        }

        /// <summary>
        /// 动态调用的委托
        /// </summary>
        /// <param name="obj">委托对象</param>
        private static void DynamicInvoke(object obj)
        {
            TargetInfo ti = (TargetInfo)obj;
            ti.Target.DynamicInvoke((object[])ti.Arguments);
        }     
    }

    /// <summary>
    /// 内部用来在线程池调用委托的对象
    /// </summary>
    internal class TargetInfo
    {
        /// <summary>
        /// 委托对象
        /// </summary>
        public readonly Delegate Target;
        /// <summary>
        /// 方法参数
        /// </summary>
        public readonly object[] Arguments;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="target">目标委托</param>
        /// <param name="args">参数</param>
        public TargetInfo(Delegate target, params object[] args)
        {
            Target = target;
            Arguments = args;
        }
    }
}
