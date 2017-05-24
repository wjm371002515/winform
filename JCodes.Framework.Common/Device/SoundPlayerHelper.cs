using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace JCodes.Framework.Common.Device
{
    /// <summary>
    ///声音文件播放操作辅助类。除了MP3声音文件外，还可以播放WAV格式、midi格式声音文件。
    /// </summary>
    public class SoundPlayerHelper
    {
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);

        [DllImport("winmm.dll")]
        private static extern int waveOutGetVolume(IntPtr hwo, out uint dwVolume);

        [DllImport("winmm.dll")]
        private static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

        /// <summary>
        /// 播放声音文件
        /// </summary>
        /// <param name="soundFileName">声音文件路径（可以是MP3、WAV、Midi等声音文件）</param>
        /// <param name="Repeat">是否重复播放</param>
        public static void Play(string soundFileName,bool Repeat)
        {
            mciSendString("open \"" + soundFileName + "\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
            mciSendString("play MediaFile" + (Repeat ? " repeat" :String.Empty), null, 0, IntPtr.Zero);
        }

        /// <summary>
        /// 播放声音嵌入资源字节数组
        /// </summary>
        /// <param name="soundEmbeddedResource">声音文件嵌入资源字节数组（可以是MP3、WAV、Midi等声音格式）</param>
        /// <param name="Repeat">是否重复播放</param>
        public static void Play(byte[] soundEmbeddedResource, bool Repeat)
        {
            extractResource(soundEmbeddedResource, Path.GetTempPath() + "resource.tmp");
            mciSendString("open \"" + Path.GetTempPath() + "resource.tmp" + "\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
            mciSendString("play MediaFile" + (Repeat ? " repeat" : String.Empty), null, 0, IntPtr.Zero);
        }

        /// <summary>
        /// 暂停播放
        /// </summary>
        public static void Pause()
        {
            mciSendString("stop MediaFile", null, 0, IntPtr.Zero);
        }

        /// <summary>
        /// 停止播放
        /// </summary>
        public static void Stop()
        {
            mciSendString("close MediaFile", null, 0, IntPtr.Zero);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public static void Dispose()
        {
            mciSendString("close all", null, 0, IntPtr.Zero);
            mciSendString("clear all", null, 0, IntPtr.Zero);
        }

        /// <summary>
        /// 获取或设置音量的百分比
        /// </summary>
        /// <returns></returns>
        public static float VolumePercent
        {
            get
            {
                float currentVolume = (float)Math.Round(GetVolume() * 100, 0);
                return currentVolume;
            }
            set
            {
                SetVolume((float)Math.Round(value, 0) / 100);
            }
        }

        /// <summary>
        /// 获取音量
        /// </summary>
        /// <returns></returns>
        public static float GetVolume()
        {
            uint curVol = 0;
            waveOutGetVolume(IntPtr.Zero, out curVol);
            ushort calcVol = (ushort)(curVol & 0xffff);
            float currentVolume = (float)calcVol / ushort.MaxValue;
            return currentVolume;
        }
        
        /// <summary>
        /// 设置音量
        /// </summary>
        /// <param name="volume"></param>
        public static void SetVolume(float volume)
        {
            volume = ushort.MaxValue * volume;
            uint volumeBothChannels = (((uint)volume & 0xffff) | ((uint)volume << 16));
            waveOutSetVolume(IntPtr.Zero, volumeBothChannels);
        }

        /// <SUMMARY>
        /// 返回当前状态播放：播放，暂停，停止等
        /// </SUMMARY>
        public static string Status
        {
            get
            {
                int i = 128;
                StringBuilder stringBuilder = new StringBuilder(i);
                mciSendString("status MediaFile mode", stringBuilder, i, IntPtr.Zero);
                return stringBuilder.ToString();
            }
        }

        private static void extractResource(byte[] res,string filePath)
        {
            FileStream fs;
            BinaryWriter bw;

            if (!File.Exists(filePath))
            {
                fs = new FileStream(filePath, FileMode.OpenOrCreate);
                bw = new BinaryWriter(fs);

                foreach (byte b in res)
                    bw.Write(b);

                bw.Close();
                fs.Close();
            }
        }
    }
}
