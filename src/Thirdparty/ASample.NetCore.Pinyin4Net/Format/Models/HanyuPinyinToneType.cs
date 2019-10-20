
namespace ASample.NetCore.Pinyin4Net.Format
{
    /// <summary>
    /// Define the output format of Hanyu Pinyin tones.
    /// 
    /// Chinese has four pitched tones and a "toneless" tone. They are called Píng(平,
    /// flat), Shǎng(上, rise), Qù(去, high drop), Rù(入, drop) and Qing(轻, toneless).
    /// Usually, we use 1, 2, 3, 4 and 5 to represent them.
    /// 
    /// This class provides several options for output of Chinese tones, which are
    /// listed below. For example, Chinese character '打'
    /// 
    /// 1、WITH_TONE_NUMBER  -> da3
    /// 2、WITHOUT_TONE      -> da
    /// 3、WITH_TONE_MARK    -> dǎ
    /// </summary>
    public enum HanyuPinyinToneType
    {
        WITH_TONE_NUMBER,   //  With tone numbers, for example: li3.
        WITHOUT_TONE,       //  Without tone numbers.
        WITH_TONE_MARK      //  With tone marks
    }
}
