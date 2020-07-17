using System;
using System.Collections.Generic;
using System.Text;
using ASample.NetCore.Excel.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace ASample.NetCore.Excel.Tests
{
    public class ExcelServicesTest
    {
        [TestMethod]
        public void ImportData()
        {
            var fileUrl = @"D:\OneDrive\桌面\excels\离港旅客名单数据导入模板0424（新）.xlsx";
            var errorMsg = new StringBuilder(); // 错误信息
            var dic = new Dictionary<string, string>()
            {
                {"PassengerName", "姓名"},
                {"PassengerEngName", "英文名"},
                {"PassengerType", "旅客类型"},
                {"Sex", "性别"},
                {"Telphone", "联系电话"},
                {"CertType", "证件类型"},
                {"CertNumber", "证件号"},
                {"Class", "舱位"},
                {"TicketNumber", "票号"}
            };
            var list = ExcelServices.ExcelToList<ImportTestModel>(dic, fileUrl, out errorMsg);
            Console.WriteLine(JsonConvert.SerializeObject(list));
        }

        [TestMethod]
        public void ExportData()
        {

        }
    }
}
