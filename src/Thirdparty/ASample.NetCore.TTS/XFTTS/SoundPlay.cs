using System;
using System.Runtime.InteropServices;
using ASample.NetCore.TTS.XFTTS.Enums;

namespace ASample.NetCore.TTS.XFTTS
{
    public class SoundPlay
    {
        #region TTS dll import

        [DllImport("msc.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern int MSPLogin(string user, string password, string configs);

        [DllImport("msc.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern int MSPLogout();

        [DllImport("msc.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr QTTSSessionBegin(string _params, ref int errorCode);

        [DllImport("msc.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern int QTTSTextPut(string sessionID, string textString, uint textLen, string _params);

        [DllImport("msc.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr QTTSAudioGet(string sessionID, ref uint audioLen, ref SynthStatusEnum synthStatus, ref int errorCode);

        [DllImport("msc.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr QTTSAudioInfo(string sessionID);

        [DllImport("msc.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern int QTTSSessionEnd(string sessionID, string hints);
        #endregion
    }
}
