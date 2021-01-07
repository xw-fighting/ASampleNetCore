
using System;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using ASample.NetCore.TTS.XFTTS.Enums;

namespace ASample.NetCore.TTS.XFTTS
{
    /// <summary>
    /// 讯飞TTS
    /// </summary>
    public class XfTtsServices
    {
        private static int ret = 0;
        private static IntPtr sessionID;
        /// <summary>
        /// 文字转语音
        /// </summary>
        public void TextToSpeech(string txt)
        {
            try
            {
                //APPID请勿随意改动
                var login_configs = "appid =5ce2167e1";//登录参数,自己注册后获取的appid
                var text = txt;//待合成的文本


                var filename = "D:\\p.wav"; //合成的语音文件
                uint audio_len = 0;

                var synthStatus = SynthStatusEnum.MSP_TTS_FLAG_STILL_HAVE_DATA;
                ret = SoundPlay.MSPLogin(string.Empty, string.Empty, login_configs);//第一个参数为用户名，第二个参数为密码，第三个参数是登录参数，用户名和密码需要在http://open.voicecloud.cn
                //MSPLogin方法返回失败
                if (ret != (int)ErrorCodeEnum.MSP_SUCCESS)
                {
                    return;
                }
                //string parameter = "engine_type = local, voice_name=xiaoyan, tts_res_path =fo|res\\tts\\xiaoyan.jet;fo|res\\tts\\common.jet, sample_rate = 16000";
                var _params = "ssm=1,ent=sms16k,vcn=xiaoyan,spd=medium,aue=speex-wb;7,vol=x-loud,auf=audio/L16;rate=16000";
                //string @params = "engine_type = local,voice_name=xiaoyan,speed=50,volume=50,pitch=50,rcn=1, text_encoding = UTF8, background_sound=1,sample_rate = 16000";
                sessionID = SoundPlay.QTTSSessionBegin(_params, ref ret);
                //QTTSSessionBegin方法返回失败
                if (ret != (int)ErrorCodeEnum.MSP_SUCCESS)
                {
                    return;
                }
                ret = SoundPlay.QTTSTextPut(Utils.Ptr2Str(sessionID), text, (uint)Encoding.Default.GetByteCount(text), string.Empty);
                //QTTSTextPut方法返回失败
                if (ret != (int)ErrorCodeEnum.MSP_SUCCESS)
                {
                    return;
                }

                var memoryStream = new MemoryStream();
                memoryStream.Write(new byte[44], 0, 44);
                while (true)
                {
                    var source = SoundPlay.QTTSAudioGet(Utils.Ptr2Str(sessionID), ref audio_len, ref synthStatus, ref ret);
                    var array = new byte[(int)audio_len];
                    if (audio_len > 0)
                    {
                        Marshal.Copy(source, array, 0, (int)audio_len);
                    }
                    memoryStream.Write(array, 0, array.Length);
                    Thread.Sleep(150);
                    if (synthStatus == SynthStatusEnum.MSP_TTS_FLAG_DATA_END || ret != 0)
                        break;
                }
                var waveHeader = Utils.GetWaveHeader((int)memoryStream.Length - 44);
                var array2 = Utils.StructToBytes(waveHeader);
                memoryStream.Position = 0L;
                memoryStream.Write(array2, 0, array2.Length);
                memoryStream.Position = 0L;
                var soundPlayer = new SoundPlayer(memoryStream);
                soundPlayer.Stop();
                soundPlayer.Play();

                var fileStream = new FileStream(filename, FileMode.Create);
                memoryStream.WriteTo(fileStream);
                memoryStream.Close();
                fileStream.Close();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                ret = SoundPlay.QTTSSessionEnd(Utils.Ptr2Str(sessionID), "");
                ret = SoundPlay.MSPLogout();//退出登录
            }
        }
    }
}
