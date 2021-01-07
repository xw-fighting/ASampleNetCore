using System;
using ASample.NetCore.TTS.XFTTS;
using Xunit;

namespace ASample.NetCore.ThidParty.Test
{
    public class XfTtsTest
    {
        [Fact]
        public void TextToSpeechTest()
        {
            var txt = "";
            var xfTtsServices = new XfTtsServices();
            xfTtsServices.TextToSpeech(txt);
        }
    }
}
