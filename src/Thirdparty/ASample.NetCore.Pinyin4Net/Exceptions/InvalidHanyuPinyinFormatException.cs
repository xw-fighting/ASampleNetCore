using System;

namespace ASample.NetCore.Pinyin4Net.Exceptions
{
    public class InvalidHanyuPinyinFormatException:ApplicationException
    {
        public InvalidHanyuPinyinFormatException() : base() { }
        public InvalidHanyuPinyinFormatException(string message) : base(message) { }
    }
}
