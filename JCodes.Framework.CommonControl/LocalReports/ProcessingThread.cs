using JCodes.Framework.Common;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace JCodes.Framework.CommonControl.LocalReports
{
    internal sealed class ProcessingThread
    {
        private Thread m_backgroundThread;
        private object m_operation;
        private Action _beginAsyncExecutionDelegate;
        private Action<Exception> _endEndAsyncExecutionDelegate;

        public void BeginBackgroundOperation(object operation)
        {
            if (this.m_backgroundThread != null)
            {
                this.m_backgroundThread.Join();
            }
            this.m_operation = operation;
            this.m_backgroundThread = new Thread(new ParameterizedThreadStart(this.ProcessThreadMain));

            Type t = operation.GetType();
            _beginAsyncExecutionDelegate = (Action)Delegate.CreateDelegate(typeof(Action), operation, "BeginAsyncExecution");
            _endEndAsyncExecutionDelegate = (Action<Exception>)Delegate.CreateDelegate(typeof(Action<Exception>), operation, "EndAsyncExecution");

            try
            {
                this.PropagateThreadCulture();
            }
            catch (SecurityException ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(ProcessingThread));
                MessageDxUtil.ShowError(ex.Message);
            }
            this.m_backgroundThread.Name = "Rendering";
            this.m_backgroundThread.IsBackground = true;
            this.m_backgroundThread.Start(operation);
        }

        public bool Cancel(int millisecondsTimeout)
        {
            if (!this.IsRendering)
            {
                return true;
            }
            try
            {
                object operation = this.m_operation;
                if (operation != null)
                {
                    this.m_backgroundThread.Abort();
                }
            }
            catch (ThreadStateException ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(ProcessingThread));
                MessageDxUtil.ShowError(ex.Message);
                if (this.IsRendering)
                {
                    throw;
                }
            }
            return ((millisecondsTimeout != 0) && this.m_backgroundThread.Join(millisecondsTimeout));
        }

        private void ProcessThreadMain(object arg)
        {
            Exception e = null;
            try
            {
                _beginAsyncExecutionDelegate();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(ProcessingThread));
                MessageDxUtil.ShowError(ex.Message);
                e = ex;
                for (Exception ex1 = ex; ex1 != null; ex1 = ex.InnerException)
                {
                    if (ex1 is ThreadAbortException)
                    {
                        e = new OperationCanceledException();
                        return;
                    }
                }
            }
            finally
            {
                _endEndAsyncExecutionDelegate(e);
                this.m_operation = null;
            }
        }

        [SecurityCritical, SecurityTreatAsSafe, SecurityPermission(SecurityAction.Assert, ControlThread = true)]
        private void PropagateThreadCulture()
        {
            this.m_backgroundThread.CurrentCulture = Thread.CurrentThread.CurrentCulture;
            this.m_backgroundThread.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
        }

        // Properties
        private bool IsRendering
        {
            get
            {
                return ((this.m_backgroundThread != null) && this.m_backgroundThread.IsAlive);
            }
        }
    }
}
