
namespace ASample.NetCore.Pinyin4Net.Format.Models.GwoyeuRomatzyh
{
    public class PinyinRomanizationType
    {

        public PinyinRomanizationType(string tagName)
        {
            TagName = tagName;
        }

        public string TagName { get; set; }

        /**
         * Hanyu Pinyin system
         */
        public static PinyinRomanizationType HANYU_PINYIN = new PinyinRomanizationType("Hanyu");

        /**
         * Wade-Giles Pinyin system
         */
        public static PinyinRomanizationType WADEGILES_PINYIN = new PinyinRomanizationType("Wade");

        /**
         * Mandarin Phonetic Symbols 2 (MPS2) Pinyin system
         */
        public static PinyinRomanizationType MPS2_PINYIN = new PinyinRomanizationType("MPSII");

        /**
         * Yale Pinyin system
         */
        public static PinyinRomanizationType YALE_PINYIN = new PinyinRomanizationType("Yale");

        /**
         * Tongyong Pinyin system
         */
        public static PinyinRomanizationType TONGYONG_PINYIN = new PinyinRomanizationType("Tongyong");

        /**
         * Gwoyeu Romatzyh system
         */
        public static PinyinRomanizationType GWOYEU_ROMATZYH = new PinyinRomanizationType("Gwoyeu");
    }
}
