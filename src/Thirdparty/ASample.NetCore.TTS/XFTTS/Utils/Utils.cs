using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using ASample.NetCore.TTS.XFTTS.Enums;

namespace ASample.NetCore.TTS.XFTTS
{
    public static class Utils
    {

        /// <summary>
        /// 指针转字符串
        /// <param name="p">指向非托管代码字符串的指针</param>
        /// <returns>返回指针指向的字符串</returns>
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string Ptr2Str(IntPtr p)
        {
           var lb = new List<byte>();

            while (Marshal.ReadByte(p) != 0)
            {
                lb.Add(Marshal.ReadByte(p));
                p = p + 1;
            }
            var bs = lb.ToArray();
            return Encoding.Default.GetString(lb.ToArray());
        }

        /// <summary>
        /// 结构体初始化赋值
        /// </summary>
        /// <param name="dataLength"></param>
        /// <returns></returns>
        public static WaveHeaderStruct GetWaveHeader(int dataLength)
        {
            var result = new WaveHeaderStruct
            {
                RIFF_ID = 1179011410,
                File_Size = dataLength + 36,
                RIFF_Type = 1163280727,
                FMT_ID = 544501094,
                FMT_Size = 16,
                FMT_Tag = 1,
                FMT_Channel = 1,
                FMT_SamplesPerSec = 16000,
                AvgBytesPerSec = 32000,
                BlockAlign = 2,
                BitsPerSample = 16,
                DATA_ID = 1635017060,
                DATA_Size = dataLength
            };

            return result;
        }

        /// <summary>
        /// 结构体转字符串
        /// </summary>
        /// <param name="structure"></param>
        /// <returns></returns>
        public static byte[] StructToBytes(object structure)
        {
            int num = Marshal.SizeOf(structure);
            IntPtr intPtr = Marshal.AllocHGlobal(num);
            byte[] result;
            try
            {
                Marshal.StructureToPtr(structure, intPtr, false);
                byte[] array = new byte[num];
                Marshal.Copy(intPtr, array, 0, num);
                result = array;
            }
            finally
            {
                Marshal.FreeHGlobal(intPtr);
            }
            return result;
        }
    }
}
