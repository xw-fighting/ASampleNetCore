
namespace ASample.NetCore.Pinyin4Net.Format
{
    /// <summary>
    /// Define the output format of character 'ü';
    /// 
    /// 'ü' is a special character of Hanyu pinyin, which can not be simply
    /// represented by English letters. In Hanyu pinyin, such characters include 'ü',
    /// 'üe', 'üan', and 'ün'.
    /// 
    /// This class provides several options for output of 'ü', which are listed
    /// below:
    /// 
    /// 1. WITH_U_AND_COLON -> u:
    /// 2. WITH_V           -> v
    /// 3. WITH_U_UNICODE   -> ü
    /// </summary>
    public enum HanyuPinyinVCharType
    {
        WITH_U_AND_COLON,       //  This option indicates that the output of 'ü' is "u:"
        WITH_V,                 //  This option indicates that the output of 'ü' is "v"
        WITH_U_UNICODE          //  This option indicates that the output of 'ü' is "ü" in Unicode form
    }
}
