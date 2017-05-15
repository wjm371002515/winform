using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.CommonControl
{
    /// <summary>
    /// 摄像头封装类
    /// </summary>
    public class Camera
    {
        private Control _ctrl;               //视频窗口宿主的托管控件
        private IntPtr _controlHandle;       //视频窗口宿主的托管控件的句柄
        private IntPtr _captureWindowHandle; //视频窗口的句柄
        private int _windowWidth;            //视频窗口的宽度
        private int _windowHeight;           //视频窗口的高度
        private int _imageX, _imageY, _imageWidth, _imageHeight;        //图像的位置和大小
        private AviCap32Helper.FrameEventHandler _frameEventHandler;    //原生的收到帧的事件处理委托
        //private AviCap32Helper.CAPDRIVERCAPS _capDriverCAPS; //捕获驱动器的能力，如有无视频叠加能力、有无控制视频源、视频格式的对话框等；
        private AviCap32Helper.CAPSTATUS _capStatus;          //该结构用于保存视频设备捕获窗口的当前状态，如图像的宽、高等


        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="ctrl">视频窗口宿主的托管控件</param>
        public Camera(Control ctrl)
        {
            Debug.Assert(ctrl != null);
            _controlHandle = ctrl.Handle;
            _windowWidth = ctrl.Width;
            _windowHeight = ctrl.Height;
            _ctrl = ctrl;
            //_capDriverCAPS = new AviCap32Helper.CAPDRIVERCAPS();
            _capStatus = new AviCap32Helper.CAPSTATUS();
            State = CameraState.Closed;
        }

        /// <summary>
        /// 接受到一帧的事件
        /// </summary>
        public event RecievedFrameEventHandler RecievedFrame;

        /// <summary>
        /// 摄像头连接成功的事件
        /// </summary>
        public event CameraConnectedEventHandler CameraConnected;

        /// <summary>
        /// 摄像头的状态
        /// </summary>
        public CameraState State { get; set; }

        /// <summary>
        /// 启动摄像头
        /// </summary>
        public void Open()
        {
            if (State != CameraState.Closed)
            {
                return;
            }

            //创建视频窗口
            _captureWindowHandle = AviCap32Helper.capCreateCaptureWindowA(
                "",
                Const.WS_VISIBLE + Const.WS_CHILD,
                0,
                0,
                _windowWidth,
                _windowHeight,
                _controlHandle,
                0);
            //连接设备
            if (CapDriverConnect(_captureWindowHandle, 0))
            {
                //获得当前视频 CAPDRIVERCAPS定义了捕获驱动器的能力，如有无视频叠加能力、有无控制视频源、视频格式的对话框等
                //AviCap32Helper.SendMessage(_captureWindowHandle, AviCap32Helper.WM_CAP_DRIVER_GET_CAPS, AviCap32Helper.SizeOf(_capDriverCAPS), ref _capDriverCAPS);
                //获得当前视频流的尺寸 存入CapStatus结构
                AviCap32Helper.SendMessage(_captureWindowHandle, Const.WM_CAP_GET_STATUS, AviCap32Helper.SizeOf(_capStatus), ref _capStatus);
                //if (CameraConnected != null)
                //{
                //    CameraConnected(new CameraConntectedEventArgs()
                //    {
                //        ImageWidth = _capStatus.uiImageWidth,
                //        ImageHeight = _capStatus.uiImageHeight
                //    });
                //}

                _imageX = (_windowWidth - _capStatus.uiImageWidth) / 2;
                _imageY = (_windowHeight - _capStatus.uiImageHeight) / 2;
                _imageWidth = Math.Min(_windowWidth, _capStatus.uiImageWidth);
                _imageHeight = Math.Min(_windowHeight, _capStatus.uiImageHeight);
                //调整视口的位置和大小
                AviCap32Helper.SetWindowPos(_captureWindowHandle, 0, _imageX, _imageY, _capStatus.uiImageWidth, _capStatus.uiImageHeight, 0);

                //设置视频格式
                //AviCap32Helper.BITMAPINFO bitmapInfo = new AviCap32Helper.BITMAPINFO();
                //bitmapInfo.bmiHeader = new AviCap32Helper.BITMAPINFOHEADER();
                //bitmapInfo.bmiHeader.biSize = AviCap32Helper.SizeOf(bitmapInfo.bmiHeader);
                //bitmapInfo.bmiHeader.biWidth = 50;
                //bitmapInfo.bmiHeader.biHeight = 50;
                //bitmapInfo.bmiHeader.biPlanes = 1;
                //bitmapInfo.bmiHeader.biBitCount = 24;
                //CapSetVideoFormat(_captureWindowHandle, ref bitmapInfo, AviCap32Helper.SizeOf(bitmapInfo));

                //设置在PREVIEW模式下设定视频窗口的刷新率 设置每50毫秒显示一帧，即显示帧速为每秒20帧
                CapSetPreviewRate(_captureWindowHandle, 50);
                //打开预览视频的缩放功能
                //AviCap32Helper.SendMessage(_captureWindowHandle, AviCap32Helper.WM_CAP_SET_SCALE, 1, 0);
                CapSetPreview(_captureWindowHandle, true);

                _frameEventHandler = new AviCap32Helper.FrameEventHandler(FrameCallBack);
                CapSetCallbackFrame(_captureWindowHandle, _frameEventHandler);
                State = CameraState.Opend;
            }
            else
            {
                throw new Exception("无法连接摄像头");
            }

        }

        /// <summary>
        /// 关闭摄像头
        /// </summary>
        public void Close()
        {
            if (State == CameraState.Closed)
            {
                return;
            }
            if (State == CameraState.Scoping)
            {
                StopKinescope();
            }
            CapDriverDisconnect(_captureWindowHandle);
            State = CameraState.Closed;
        }

        /// <summary>
        /// 抓图保存到文件
        /// </summary>
        /// <param name="bmpPath">要保存bmp文件的路径</param>
        public void GrabImage(string bmpPath)
        {
            if (State != CameraState.Closed)
            {
                IntPtr hBmp = Marshal.StringToHGlobalAnsi(bmpPath);
                AviCap32Helper.SendMessage(_captureWindowHandle, Const.WM_CAP_FILE_SAVEDIB, 0, hBmp.ToInt32());
            }
        }

        /// <summary>
        /// 抓图
        /// </summary>
        /// <returns>抓到的图像</returns>
        public Bitmap GrabImage()
        {
            if (State != CameraState.Closed)
            {
                string tempFile = Path.GetTempFileName();
                GrabImage(tempFile);
                if (File.Exists(tempFile))
                {
                    Bitmap bmp = Bitmap.FromFile(tempFile) as Bitmap;
                    Bitmap bmpOut = new Bitmap(_imageWidth, _imageHeight);

                    Graphics g = Graphics.FromImage(bmpOut);
                    g.DrawImage(bmp, new Rectangle(Math.Min(0, _imageX), Math.Min(0, _imageY), bmp.Width, bmp.Height),
                        new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                    g.Dispose();
                    return bmpOut;
                }
            }
            return null;
        }

        /// <summary>
        /// 开始录像
        /// </summary>
        /// <param name="aviPath">要保存avi文件的路径</param>
        public void StartKinescope(string aviPath)
        {
            if (State == CameraState.Opend)
            {
                IntPtr hBmp = Marshal.StringToHGlobalAnsi(aviPath);
                AviCap32Helper.SendMessage(_captureWindowHandle, Const.WM_CAP_FILE_SET_CAPTURE_FILE, 0, hBmp.ToInt32());
                AviCap32Helper.SendMessage(_captureWindowHandle, Const.WM_CAP_SEQUENCE, 0, 0);
                State = CameraState.Scoping;
            }
        }

        /// <summary>
        /// 停止录像
        /// </summary>
        public void StopKinescope()
        {
            if (State == CameraState.Scoping)
            {
                AviCap32Helper.SendMessage(_captureWindowHandle, Const.WM_CAP_STOP, 0, 0);
                State = CameraState.Opend;
            }
        }

        #region private methods

        //连接设备
        private bool CapDriverConnect(IntPtr lwnd, short i)
        {
            return AviCap32Helper.SendMessage(lwnd, Const.WM_CAP_DRIVER_CONNECT, i, 0);
        }

        //断开设备连接
        private bool CapDriverDisconnect(IntPtr lwnd)
        {
            return AviCap32Helper.SendMessage(lwnd, Const.WM_CAP_DRIVER_DISCONNECT, 0, 0);
        }

        //设置预览
        private bool CapSetPreview(IntPtr lwnd, bool f)
        { return AviCap32Helper.SendMessage(lwnd, Const.WM_CAP_SET_PREVIEW, f, 0); }

        //设置预览Rate
        private bool CapSetPreviewRate(IntPtr lwnd, short wMS)
        {
            return AviCap32Helper.SendMessage(lwnd, Const.WM_CAP_SET_PREVIEWRATE, wMS, 0);
        }

        //设置获取帧的回调
        private bool CapSetCallbackFrame(IntPtr lwnd, AviCap32Helper.FrameEventHandler lpProc)
        {
            return AviCap32Helper.SendMessage(lwnd, Const.WM_CAP_SET_CALLBACK_FRAME, 0, lpProc);
        }

        //获取视频格式
        //private int CapGetVideoFormat(IntPtr hCapWnd, ref AviCap32Helper.BITMAPINFO BmpFormat, int CapFormatSize)
        //{
        //    return AviCap32Helper.capGetVideoFormat(hCapWnd, IntPtr.Zero, 0);
        //}

        //设置视频格式
        private bool CapSetVideoFormat(IntPtr hCapWnd, ref AviCap32Helper.BITMAPINFO BmpFormat, int CapFormatSize)
        {
            return AviCap32Helper.SendMessage(hCapWnd, Const.WM_CAP_SET_VIDEOFORMAT, CapFormatSize, ref BmpFormat);
        }

        //收到帧时的处理
        private void FrameCallBack(IntPtr lwnd, IntPtr lpVHdr)
        {
            AviCap32Helper.VIDEOHDR videoHeader = new AviCap32Helper.VIDEOHDR();
            byte[] VideoData;
            videoHeader = (AviCap32Helper.VIDEOHDR)AviCap32Helper.GetStructure(lpVHdr, videoHeader);
            VideoData = new byte[videoHeader.dwBytesUsed];
            AviCap32Helper.Copy(videoHeader.lpData, VideoData);
            if (this.RecievedFrame != null)
            {
                this.RecievedFrame(VideoData);
            }
        }

        #endregion
    }

    /// <summary>
    /// 接收到帧的事件处理参数
    /// </summary>
    /// <param name="data"></param>
    public delegate void RecievedFrameEventHandler(byte[] data);

    /// <summary>
    /// 摄像头连接成功的参数
    /// </summary>
    public delegate void CameraConnectedEventHandler(CameraConntectedEventArgs e);

    /// <summary>
    /// 摄像头连接成功的事件
    /// </summary>
    public class CameraConntectedEventArgs
    {
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
    }

    /// <summary>
    /// AviCap的封装类
    /// AviCap包含了执行视频捕获的函数，它给AVI文件I/O和视频、音频设备驱动程序提供一个高级接口
    /// </summary>
    public class AviCap32Helper
    {
        #region 导入函数

        //avicap32的导出函数
        [DllImport("avicap32.dll")]
        public static extern IntPtr capCreateCaptureWindowA(string lpszWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, int nID);
        [DllImport("avicap32.dll")]
        public static extern bool capGetDriverDescriptionA(short wDriver, byte[] lpszName, int cbName, byte[] lpszVer, int cbVer);
        [DllImport("avicap32.dll")]
        public static extern int capGetVideoFormat(IntPtr hWnd, IntPtr psVideoFormat, int wSize);
        //user32的导出函数
        [DllImport("User32.dll")]
        public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, bool wParam, int lParam);
        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, short wParam, int lParam);
        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, short wParam, FrameEventHandler lParam);
        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, ref BITMAPINFO lParam);
        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, ref CAPDRIVERCAPS lParam);
        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, ref CAPTUREPARMS lParam);
        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, ref CAPSTATUS lParam);

        #endregion

        #region 结构体定义

        /*********VideoHdr 结构体*********
        * 
        * 定义了视频数据块的头信息，
        * 在编写回调函数时常用到其数据成员lpData（指向数据缓存的指针）和dwBufferLength（数据缓存的大小）。
        * 视频帧到缓存的捕获则需要应用回调函数和相应的数据块结构 VIDEOHDR
        * 
        *******************************/
        [StructLayout(LayoutKind.Sequential)]
        public struct VIDEOHDR
        {
            public IntPtr lpData;              /* 指向数据缓存的指针 */
            public int dwBufferLength;         /* 数据缓存的大小 */
            public int dwBytesUsed;            /* Bytes actually used */
            public int dwTimeCaptured;         /* Milliseconds from start of stream */
            public int dwUser;                 /* for client's use */
            public int dwFlags;                /* assorted flags (see defines) */
            public int dwReserved;             /* reserved for driver */
        }

        /*********BitmapInfoHeader 结构体*********
        * 
        * BitmapInfoHeader定义了位图的头部信息
        * 
        ****************************************/
        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFOHEADER
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;
        }


        /*********BitmapInfo 结构体*********
        * 
        * BitmapInfo 定义了位图信息
        * 
        ***********************************/
        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFO
        {
            public BITMAPINFOHEADER bmiHeader;
            public int bmiColors;
        }

        /*********CAPTUREPARMS 结构体*********
        * 
        * CAPTUREPARMS 包含控制视频流捕获过程的参数，如捕获帧频、指定键盘或鼠标键以终止捕获、捕获时间限制等
        * 
        ***********************************/
        [StructLayout(LayoutKind.Sequential)]
        public struct CAPTUREPARMS
        {
            public int dwRequestMicroSecPerFrame;         // 期望的桢播放率，以毫秒为单位，默认为66667，相当于15桢每秒。
            public bool fMakeUserHitOKToCapture;          // Show "Hit OK to cap" dlg?开始捕获标志位，如果值为真，则在开始捕获前要产生一个询问对话框，默认为假。
            public uint wPercentDropForError;             //所允许的最大丢桢百分比，可以从0变化到100，默认值为10。
            public bool fYield;                           /*另起线程标志位，如果为真，则程序重新启动一个线程用于视频流的捕获，默认值是假。
                                                            但是如果你是为了真，你必须要在程序中处理一些潜在的操作，因为当视频捕获时，其他操作并没有被屏蔽。*/
            public int dwIndexSize;                       // 在AVI文件中所允许的最大数目的索引项(32K)
            public uint wChunkGranularity;                // AVI文件的逻辑尺寸，以字节为单位。如果值是0，则说明该尺寸渐增 在 Win32程序中无用。(2K)
            public bool fUsingDOSMemory;                  // Use DOS buffers?
            public uint wNumVideoRequested;               // 所被允许分配的最大视频缓存
            public bool fCaptureAudio;                    // 音频标志位，如果音频流正在捕获，则该值为真。
            public uint wNumAudioRequested;               // 最大数量的音频缓存，默认值为10。
            public uint vKeyAbort;                        // 终止流捕获的虚拟键盘码，默认值为VK_ESCAPE
            [MarshalAs(UnmanagedType.Bool)]
            public bool fAbortLeftMouse;                  // 终止鼠标左键标志位，如果该值为真，则在流捕获过程中如果点击鼠标左键则该捕获终止，默认值为真。
            public bool fAbortRightMouse;                 // Abort on right mouse?
            public bool fLimitEnabled;                    // 捕获操作时间限制，如果为真，则时间到了以后捕获操作终止，默认为假
            public uint wTimeLimit;                       // 具体终止时间，只有 fLimitEnabled是真时.该位才有效
            public bool fMCIControl;                      // Use MCI video source?
            public bool fStepMCIDevice;                   // Step MCI device?MCI 设备标志。
            public int dwMCIStartTime;                    // Time to start in MS
            public int dwMCIStopTime;                     // Time to stop in MS
            public bool fStepCaptureAt2x;                 // Perform spatial averaging 2x
            public int wStepCaptureAverageFrames;         // 当基于平均采样来创建桢时，桢的采样时间，典型值是5
            public int dwAudioBufferSize;                 // 音频缓存的尺寸，如果用默认值0，缓存尺寸是最大0.5秒，或10k字节。
            public int fDisableWriteCache;                // Attempt to disable write cache
            public int AVStreamMaster;                    //音视频同步标志。
        }


        /*********CAPDRIVERCAPS 结构体*********
        * 
        * CAPDRIVERCAPS 定义了捕获驱动器的能力，如有无视频叠加能力、有无控制视频源、视频格式的对话框等
        * 
        **************************************/
        [StructLayout(LayoutKind.Sequential)]
        public struct CAPDRIVERCAPS
        {
            [MarshalAs(UnmanagedType.U2)]
            public UInt16 wDeviceIndex;         //捕获驱动器的索引值，该值可以由0到9变化。
            [MarshalAs(UnmanagedType.Bool)]
            public bool fHasOverlay;            // 视频叠加标志，如果设备支持视频叠加这该位是真。
            [MarshalAs(UnmanagedType.Bool)]
            public bool fHasDlgVideoSource;     //视频资源对话框标志位，如果设备支持视频选择、控制对话框，该值为真。
            [MarshalAs(UnmanagedType.Bool)]
            public bool fHasDlgVideoFormat;     //视频格式对话框标志位，如果设备支持对视频格式对话框的选择，该位真。
            [MarshalAs(UnmanagedType.Bool)]
            public bool fHasDlgVideoDisplay;    //视频展示对话框标志位，如果设备支持对视频捕获缓存区的重新播放，该位是真。
            [MarshalAs(UnmanagedType.Bool)]
            public bool fCaptureInitialized;    //捕获安装标志位，如果捕获驱动器已经成功连接，该值为真。
            //[MarshalAs(UnmanagedType.Bool)]
            public bool fDriverSuppliesPalettes; //驱动器调色板标志位，如果驱动器能创建调色板，则该位是真。
            [MarshalAs(UnmanagedType.I4)]
            public int hVideoIn;
            [MarshalAs(UnmanagedType.I4)]
            public int hVideoOut;
            [MarshalAs(UnmanagedType.I4)]
            public int hVideoExtIn;
            [MarshalAs(UnmanagedType.I4)]
            public int hVideoExtOut;
        }


        /*********CAPSTATUS 结构体*********
        * 
        * CAPSTATUS 定义了捕获窗口的当前状态，如图像的宽、高等
        * 
        **************************************/

        [StructLayout(LayoutKind.Sequential)]
        public struct CAPSTATUS
        {
            public int uiImageWidth;                     //图像宽度
            public int uiImageHeight;                    //图像高度
            public bool fLiveWindow;                     //活动窗口标记，如果窗口正以预览的方式展示图像，那么该值为真
            public bool fOverlayWindow;                  //叠加窗口标志位，如果正在使用硬件叠加，则该位是真。
            public bool fScale;                          //输入所放标志位，如果窗口是正在缩放视频到客户区，那么该位是真。当使用硬件叠加时，改位无效。
            public Point ptScroll;                       //被展示在窗口客户区左上角的那个象素的x、y坐标偏移量。
            public bool fUsingDefaultPalette;            //默认调色板标志位，如果捕获窗口正在使用当前默认调色板，该值为真
            public bool fAudioHardware;                  // 音频硬件标志位，如果系统已经安装了音频硬件，该值为真。
            public bool fCapFileExists;                  //捕获文件标志位，如果一个捕获文件已经被创建，该值为真
            public int dwCurrentVideoFrame;              // 当前或最近流捕获过程中，所处理的桢的数目。包括丢弃的桢。
            public int dwCurrentVideoFramesDropped;      //当前流捕获过程中丢弃的桢的数目。
            public int dwCurrentWaveSamples;             // # of wave samples cap'td
            public int dwCurrentTimeElapsedMS;           // 从当前流捕获开始计算，程序所用的时间，以毫秒为单位。
            public IntPtr hPalCurrent;                   // 当前剪切板的句柄。
            public bool fCapturingNow;                   // 捕获标志位，当捕获是正在进行时，改位是真
            public int dwReturn;                         // 错误返回值，当你的应用程序不支持错误回调函数时可以应用改位
            public int wNumVideoAllocated;               // 被分配的视频缓存的数目。
            public int wNumAudioAllocated;               // 被分配的音频缓存的数目。
        }

        #endregion

        #region 基本函数

        public delegate void FrameEventHandler(IntPtr lwnd, IntPtr lpVHdr);

        public static object GetStructure(IntPtr ptr, ValueType structure)
        {
            return Marshal.PtrToStructure(ptr, structure.GetType());
        }

        public static object GetStructure(int ptr, ValueType structure)
        {
            return GetStructure(new IntPtr(ptr), structure);
        }

        public static void Copy(IntPtr ptr, byte[] data)
        {
            Marshal.Copy(ptr, data, 0, data.Length);
        }

        public static void Copy(int ptr, byte[] data)
        {
            Copy(new IntPtr(ptr), data);
        }

        public static int SizeOf(object structure)
        {
            return Marshal.SizeOf(structure);
        }

        #endregion
    }
}
