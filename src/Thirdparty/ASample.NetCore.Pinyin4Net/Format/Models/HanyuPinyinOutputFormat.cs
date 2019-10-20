
namespace ASample.NetCore.Pinyin4Net.Format
{
    public class HanyuPinyinOutputFormat
    {
        public HanyuPinyinVCharType VCharType { get; set; } = HanyuPinyinVCharType.WITH_U_UNICODE;

        public HanyuPinyinCaseType CaseType { get; set; } = HanyuPinyinCaseType.LOWERCASE;

        public HanyuPinyinToneType ToneType { get; set; } = HanyuPinyinToneType.WITH_TONE_MARK;

        public HanyuPinyinOutputFormat() { }
    }
}
