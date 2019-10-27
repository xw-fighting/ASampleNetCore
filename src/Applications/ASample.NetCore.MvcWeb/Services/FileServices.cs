using ASample.NetCore.MvcWeb.Models.SignalRs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.MvcWeb.Services
{
    public class FileServices
    {
        private List<FileParam> list;
        public FileServices()
        {
            list = new List<FileParam>();
            CreateTestFile();
        }

        // 取得文件
        public FileParam GetFile(string fileName)
        {
            var file = (from f in list where f.FileName == fileName select f).FirstOrDefault();

            return file;
        }

        // 新增文件
        public bool AddFile(FileParam file)
        {
            list.Add(file);
            return true;
        }

        // 編輯文件
        public CellParam EditFileCell(string fileName, string cellName, string text)
        {
            // 找出哪個file
            var file = (from f in list where f.FileName == fileName select f).FirstOrDefault();
            // 找出哪個cell
            var cell = (from c in file.TextList where c.CellName == cellName select c).FirstOrDefault();
            cell.Text = text;

            return cell;
        }

        // 建立測試File
        public void CreateTestFile()
        {
            var fileModel = new FileParam
            {
                Creator = "Test",
                FileName = "TestFile",
                Row = 5,
                Column = 5,
                Editor = new List<UserParam>()
            };
            List<CellParam> textList = new List<CellParam>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var cellModel = new CellParam();
                    cellModel.LockState = true;
                    cellModel.Text = "";
                    cellModel.CellName = $"{i}{j}";
                    textList.Add(cellModel);
                }
            }
            fileModel.TextList = textList;
            list.Add(fileModel);
        }

    }
}
