using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;

namespace JCodes.Framework.Common.Files
{
    /// <summary>
    /// 监视文件变化的类，包括创建、修改、删除等操作的辅助类
    /// <code>
    ///             MyFileSystemWatcher fsw = new MyFileSystemWatcher(@"C:\Test", "Test.txt");
    ///             fsw.Created += new System.IO.FileSystemEventHandler(fsw_Created);
    ///             fsw.Changed += new System.IO.FileSystemEventHandler(fsw_Changed);
    ///             fsw.Deleted += new System.IO.FileSystemEventHandler(fsw_Deleted);
    ///             fsw.Renamed += new System.IO.RenamedEventHandler(fsw_Renamed);
    ///             fsw.EnableRaisingEvents = true;
    /// </code>
    /// </summary>
    public class MyFileSystemWatcher : FileSystemWatcher, IDisposable
    {
        #region Private Members
        // This Dictionary keeps the track of when an event occured last for a particular file
        private Dictionary<string, DateTime> _lastFileEvent;
        // Interval in Millisecond
        private int _interval;
        //Timespan created when interval is set
        private TimeSpan _recentTimeSpan;
        #endregion

        #region Constructors
        /// <summary>
        /// 构造函数，调用基类构造函数并初始化成员
        /// </summary>
        public MyFileSystemWatcher() : base()
        {
            InitializeMembers();
        }

        /// <summary>
        /// 指定路径进行构造
        /// </summary>
        /// <param name="Path">文件路径</param>
        public MyFileSystemWatcher(string Path) : base(Path)
        {
            InitializeMembers();            
        }

        /// <summary>
        /// 指定路径和过滤器进行构造
        /// </summary>
        /// <param name="Path">文件或文件夹路径</param>
        /// <param name="Filter">过滤器</param>
        public MyFileSystemWatcher(string Path, string Filter) : base(Path, Filter)
        {
            InitializeMembers();
        }
        #endregion

        #region Events
        // These events hide the events from the base class. 
        // We want to raise these events appropriately and we do not want the 
        // users of this class subscribing to these events of the base class accidentally
        
        /// <summary>
        /// 文件系统发生变化
        /// </summary>
        public new event FileSystemEventHandler Changed;
        /// <summary>
        /// 文件创建事件
        /// </summary>
        public new event FileSystemEventHandler Created;
        /// <summary>
        /// 文件删除事件
        /// </summary>
        public new event FileSystemEventHandler Deleted;
        /// <summary>
        /// 文件重命名事件
        /// </summary>
        public new event RenamedEventHandler Renamed;
        #endregion

        #region Protected Methods
        /// <summary>
        /// 文件发生变化
        /// </summary>
        protected new virtual void OnChanged(FileSystemEventArgs e)
        {
            if (Changed != null) Changed(this, e); 
        }
        /// <summary>
        /// 文件创建后
        /// </summary>
        protected new virtual void OnCreated(FileSystemEventArgs e)
        {
            if (Created != null) Created(this, e);
        }
        /// <summary>
        /// 文件删除后
        /// </summary>
        protected new virtual void OnDeleted(FileSystemEventArgs e)
        {
            if (Deleted != null) Deleted(this, e);
        }

        /// <summary>
        /// 文件重命名后
        /// </summary>
        protected new virtual void OnRenamed(RenamedEventArgs e)
        {
            if (Renamed != null) Renamed(this, e);
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// This Method Initializes the private members.
        /// Interval is set to its default value of 100 millisecond
        /// FilterRecentEvents is set to true, _lastFileEvent dictionary is initialized
        /// We subscribe to the base class events.
        /// </summary>
        private void InitializeMembers()
        {
            Interval = 100;
            FilterRecentEvents = true;
            _lastFileEvent = new Dictionary<string, DateTime>();

            base.Created += new FileSystemEventHandler(OnCreated);
            base.Changed += new FileSystemEventHandler(OnChanged);
            base.Deleted += new FileSystemEventHandler(OnDeleted);
            base.Renamed += new RenamedEventHandler(OnRenamed);
        }

        /// <summary>
        /// This method searches the dictionary to find out when the last event occured 
        /// for a particular file. If that event occured within the specified timespan
        /// it returns true, else false
        /// </summary>
        /// <param name="FileName">The filename to be checked</param>
        /// <returns>True if an event has occured within the specified interval, False otherwise</returns>
        private bool HasAnotherFileEventOccuredRecently(string FileName)
        {
            bool retVal = false;

            // Check dictionary only if user wants to filter recent events otherwise return Value stays False
            if (FilterRecentEvents)
            {
                if (_lastFileEvent.ContainsKey(FileName))
                {
                    // If dictionary contains the filename, check how much time has elapsed
                    // since the last event occured. If the timespan is less that the 
                    // specified interval, set return value to true 
                    // and store current datetime in dictionary for this file
                    DateTime lastEventTime = _lastFileEvent[FileName];
                    DateTime currentTime = DateTime.Now;
                    TimeSpan timeSinceLastEvent = currentTime - lastEventTime;
                    retVal = timeSinceLastEvent < _recentTimeSpan;
                    _lastFileEvent[FileName] = currentTime;
                }
                else
                {
                    // If dictionary does not contain the filename, 
                    // no event has occured in past for this file, so set return value to false
                    // and annd filename alongwith current datetime to the dictionary
                    _lastFileEvent.Add(FileName, DateTime.Now);
                    retVal = false;
                }
            }

            return retVal;
        }

        #region FileSystemWatcher EventHandlers
        // Base class Event Handlers. Check if an event has occured recently and call method
        // to raise appropriate event only if no recent event is detected
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (!HasAnotherFileEventOccuredRecently(e.FullPath))
                this.OnChanged(e);
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            if (!HasAnotherFileEventOccuredRecently(e.FullPath))
                this.OnCreated(e);
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            if (!HasAnotherFileEventOccuredRecently(e.FullPath))
                this.OnDeleted(e);
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            if (!HasAnotherFileEventOccuredRecently(e.OldFullPath))
                this.OnRenamed(e);
        }
        #endregion
        #endregion

        #region Public Properties
        
        /// <summary>
        /// 时间间隔，以毫秒为单位，在其中的事件被认为是“最近”
        /// </summary>
        public int Interval
        {
            get
            {
                return _interval;
            }
            set
            {
                _interval = value;
                // Set timespan based on the value passed
                _recentTimeSpan = new TimeSpan(0, 0, 0, 0, value);
            }
        }

        /// <summary>
        /// 允许用户设置是否过滤最近发生的事件。如果此设为false，该类的行为就像System.IO.FileSystemWatcher类
        /// </summary>
        public bool FilterRecentEvents { get; set; }
        #endregion

        #region IDisposable Members

        /// <summary>
        /// 释放资源
        /// </summary>
        public new void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
