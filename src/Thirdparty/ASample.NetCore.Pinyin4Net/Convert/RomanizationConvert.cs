using ASample.NetCore.Log;
using ASample.NetCore.Pinyin4Net.Convert;
using ASample.NetCore.Pinyin4Net.Format.Models.GwoyeuRomatzyh;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace ASample.NetCore.Pinyin4Net
{
    public class RomanizationConvert
    {
        /**
         * A DOM model contains variable pinyin presentations
         */
        private static XmlDocument _pinyinMappingDoc = new XmlDocument();
        private RomanizationConvert()
        {
            InitializeResource();
        }

        private static RomanizationConvert pinyinRomanizationConvert;

        public static RomanizationConvert GetInstance()
        {
            // 如果类的实例不存在则创建，否则直接返回
            if (pinyinRomanizationConvert == null)
            {
                pinyinRomanizationConvert = new RomanizationConvert();
            }
            return pinyinRomanizationConvert;
        }

        private void InitializeResource()
        {
            try
            {
                var mappingFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pinyindb/pinyin_mapping.xml");
                var doc = new XmlDocument();
                doc.Load(mappingFileName);
                _pinyinMappingDoc = doc;
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

        public string GetRomanizationString(string sourcePinyinStr,  PinyinRomanizationType sourcePinyinSystem, PinyinRomanizationType targetPinyinSystem)
        {
            var pinyinString = Util.ExtractPinyinString(sourcePinyinStr);
            var toneNumberStr = Util.ExtractToneNumber(sourcePinyinStr);

            // return value
            var targetPinyinStr = string.Empty;
            try
            {
                // find the node of source Pinyin system
                var xpathQuery1 = "//" + sourcePinyinSystem.TagName
                        + "[text()='" + pinyinString + "']";

                var pinyinMappingDoc = _pinyinMappingDoc;

                var hanyuNode = pinyinMappingDoc.SelectSingleNode(xpathQuery1);

                if (null != hanyuNode)
                {
                    // find the node of target Pinyin system
                    var xpathQuery2 = "../" + targetPinyinSystem.TagName
                            + "/text()";
                    var targetPinyinStrWithoutToneNumber = hanyuNode.SelectSingleNode(xpathQuery2).Value;

                    targetPinyinStr = targetPinyinStrWithoutToneNumber
                            + toneNumberStr;
                }
            }
            catch (Exception e)
            {
                FileLogServices.WriteLogToFile(e.ToString());
            }

            return targetPinyinStr;
        }

    }
}
