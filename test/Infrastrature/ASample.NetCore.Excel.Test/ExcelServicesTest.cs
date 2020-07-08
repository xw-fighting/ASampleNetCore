using ASample.NetCore.Excel.Test.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASample.NetCore.Excel.Test
{
    public class ExcelServicesTest
    {
        [Test]
        public void ImportData()
        {
            var fileUrl = @"D:\OneDrive\桌面\excels\离港旅客名单数据导入模板0424（新）.xlsx";
            var errorMsg = new StringBuilder(); // 错误信息
            var dic = new Dictionary<string, string>()
            {
                { "PassengerName", "姓名" },
                { "PassengerEngName", "英文名" },
                { "PassengerType", "旅客类型" },
                { "Sex", "性别" },
                { "Telphone", "联系电话" },
                { "CertType", "证件类型" },
                { "CertNumber", "证件号" },
                { "Class", "舱位" },
                { "TicketNumber", "票号" }
            };
            var list = ExcelServices.ExcelToList<ImportTestModel>(dic, fileUrl, out errorMsg);
            Console.WriteLine(JsonConvert.SerializeObject(list));
        }

        [Test]
        public void ExportData()
        {

        }
    }
}
