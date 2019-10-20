using ASample.NetCore.Log;
using ASample.NetCore.Pinyin4Net.Convert;
using ASample.NetCore.Pinyin4Net.Format.Models.GwoyeuRomatzyh;
using System;
using System.IO;
using System.Xml;

namespace ASample.NetCore.Pinyin4Net
{
    public class GwoyeuRomatzyhConvert
    {
        private GwoyeuRomatzyhConvert()
        {
            InitializeResource();
        }
        private static XmlDocument _pinyinToGwoyeuMappingDoc;

        private static GwoyeuRomatzyhConvert gwoyeuRomatzyhConvert;

        /**
         * The postfixs to distinguish different tone of Gwoyeu Romatzyh
         * 
         * <i>Should be removed if new xPath parser supporting tag name with number.</i>
         */
        private static readonly string[] tones = new String[] { "_I", "_II", "_III", "_IV",
            "_V" };
        public static  GwoyeuRomatzyhConvert GetInstance()
        {
            // 如果类的实例不存在则创建，否则直接返回
            if (gwoyeuRomatzyhConvert == null)
            {
                gwoyeuRomatzyhConvert = new GwoyeuRomatzyhConvert();
            }
            return gwoyeuRomatzyhConvert;
        }

        private void InitializeResource()
        {
            try
            {
                var mappingFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pinyindb/pinyin_gwoyeu_mapping.xml");
                XmlDocument doc = new XmlDocument();
                doc.Load(mappingFileName);
                _pinyinToGwoyeuMappingDoc = doc;
            }
            catch (FileNotFoundException ex)
            {
                FileLogServices.WriteLogToFile(ex.ToString());
            }
            catch (IOException ex)
            {
                FileLogServices.WriteLogToFile(ex.ToString());
            }
            catch (Exception ex)
            {
                FileLogServices.WriteLogToFile(ex.ToString());
            }
        }

        public string GetGwoyeuRomatzyh(string hanyuPinyinStr)
        {
            var pinyinString = Util.ExtractPinyinString(hanyuPinyinStr);
            var toneNumberStr = Util.ExtractToneNumber(hanyuPinyinStr);

            // return value
            var gwoyeuStr = string.Empty;
            try
            {
                // find the node of source Pinyin system
                var xpathQuery1 = "//"
                        + PinyinRomanizationType.HANYU_PINYIN.TagName
                        + "[text()='" + pinyinString + "']";

                var pinyinToGwoyeuMappingDoc =_pinyinToGwoyeuMappingDoc;

                var hanyuNode = pinyinToGwoyeuMappingDoc.SelectSingleNode(xpathQuery1);

                if (null != hanyuNode)
                {
                    // find the node of target Pinyin system
                    var xpathQuery2 = "../"
                            + PinyinRomanizationType.GWOYEU_ROMATZYH.TagName
                            + tones[int.Parse(toneNumberStr) - 1]
                            + "/text()";
                    var targetPinyinStrWithoutToneNumber = hanyuNode.SelectSingleNode(xpathQuery2).Value;

                    gwoyeuStr = targetPinyinStrWithoutToneNumber;
                }
            }
            catch (Exception e)
            {
                FileLogServices.WriteLogToFile(e.ToString());
            }

            return gwoyeuStr;
        }
    }
}
