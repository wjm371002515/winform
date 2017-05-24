using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.ComponentModel;
using System.IO;
using System.Media;
using System.Security;
using System.Security.Permissions;

namespace JCodes.Framework.Common.Device
{
    /// <summary>
    /// WAV声音格式文件播放辅助类
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public class AudioHelper
    {
        /// <summary>
        /// 播放 .wav 格式的声音文件
        /// </summary>
        /// <param name="location">声音文件路径 </param>
        public static void Play(string location)
        {
            Play(location, AudioPlayMode.Background);
        }

        /// <summary>
        /// 播放 .wav 格式的声音文件
        /// </summary>
        /// <param name="playMode">播放声音的枚举模式。默认为AudioPlayMode.Background。</param>
        /// <param name="location">声音文件路径</param>
        public static void Play(string location, AudioPlayMode playMode)
        {
            ValidateAudioPlayModeEnum(playMode, "playMode");
            string fileName = ValidateFilename(location);
            SoundPlayer player1 = new SoundPlayer(fileName);
            Play(player1, playMode);
        }

        /// <summary>
        /// 播放 .wav 格式的声音文件
        /// </summary>
        /// <param name="stream"><see cref="T:System.IO.Stream"></see>声音文件的流对象</param>
        /// <param name="playMode">播放声音的枚举模式。默认为AudioPlayMode.Background。</param>
        public static void Play(Stream stream, AudioPlayMode playMode)
        {
            ValidateAudioPlayModeEnum(playMode, "playMode");
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            Play(new SoundPlayer(stream), playMode);
        }

        /// <summary>
        /// 播放 .wav 格式的声音文件
        /// </summary>
        /// <param name="data">声音文件的字节数组</param>
        /// <param name="playMode">播放声音的枚举模式。默认为AudioPlayMode.Background。</param>
        public static void Play(byte[] data, AudioPlayMode playMode)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            ValidateAudioPlayModeEnum(playMode, "playMode");
            MemoryStream stream1 = new MemoryStream(data);
            Play(stream1, playMode);
            stream1.Close();
        }

        /// <summary>
        /// 播放系统声音
        /// </summary>
        public static void PlaySystemSound(SystemSound systemSound)
        {
            if (systemSound == null)
            {
                throw new ArgumentNullException("systemSound");                
            }
            systemSound.Play();
        }

        /// <summary>
        /// 停止正在后台播放的声音
        /// </summary>
        public static void Stop()
        {
            SoundPlayer player1 = new SoundPlayer();
            InternalStop(player1);
        }

        #region 辅助方法

        private static SoundPlayer _SoundPlayer;
        private static void InternalStop(SoundPlayer sound)
        {
            new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
            try
            {
                sound.Stop();
            }
            finally
            {
                CodeAccessPermission.RevertAssert();
            }
        }

        /// <summary>
        /// 播放声音函数
        /// </summary>
        /// <param name="sound">声音文件</param>
        /// <param name="mode">播放模式</param>
        private static void Play(SoundPlayer sound, AudioPlayMode mode)
        {
            if (_SoundPlayer != null)
            {
                InternalStop(_SoundPlayer);
            }

            _SoundPlayer = sound;
            switch (mode)
            {
                case AudioPlayMode.WaitToComplete:
                    _SoundPlayer.PlaySync();
                    return;

                case AudioPlayMode.Background:
                    _SoundPlayer.Play();
                    return;

                case AudioPlayMode.BackgroundLoop:
                    _SoundPlayer.PlayLooping();
                    return;
            }
        }

        private static void ValidateAudioPlayModeEnum(AudioPlayMode value, string paramName)
        {
            if ((value < AudioPlayMode.WaitToComplete) || (value > AudioPlayMode.BackgroundLoop))
            {
                throw new InvalidEnumArgumentException(paramName, (int)value, typeof(AudioPlayMode));
            }
        }
        
        private static string ValidateFilename(string location)
        {
            if (String.IsNullOrEmpty(location))
            {
                throw new ArgumentNullException("location");
            }
            return location;
        }         

        #endregion
    }

   
}
