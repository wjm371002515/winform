using System;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace JCodes.Framework.WebUI.Common
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
            catch (SecurityException)
            {
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
            catch (ThreadStateException)
            {
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
            catch (Exception exception2)
            {
                e = exception2;
                for (Exception exception3 = exception2; exception3 != null; exception3 = exception3.InnerException)
                {
                    if (exception3 is ThreadAbortException)
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
