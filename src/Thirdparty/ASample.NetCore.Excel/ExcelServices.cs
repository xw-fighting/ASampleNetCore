using ASample.NetCore.Excel.Models;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace ASample.NetCore.Excel
{
    public static class ExcelServices
    {
        /// <summary>
        /// 导出数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static MemoryStream ExportToStream<T>(ExcelExportOption<T> setting)
        {
            var cols = setting.Colums.Count();
            var hssworkbook = new HSSFWorkbook();
            var sheet1 = hssworkbook.CreateSheet("Sheet1");

            if (setting.DefaultRowHeight > 0)
                sheet1.DefaultRowHeight = setting.DefaultRowHeight;

            var docSummaryInfo = PropertySetFactory.CreateDocumentSummaryInformation();
            docSummaryInfo.Company = string.Empty;
            var summaryInfo = PropertySetFactory.CreateSummaryInformation();
            summaryInfo.Subject = setting.Title;
            hssworkbook.DocumentSummaryInformation = docSummaryInfo;
            hssworkbook.SummaryInformation = summaryInfo;

            //自动调整某列宽度
            for (var i = 0; i < cols; i++)
            {
                sheet1.AutoSizeColumn(i, true);
            }

            var currentRowIndex = 0;

            //设置标题信息
            if (!String.IsNullOrWhiteSpace(setting.Title))
            {
                var row0 = sheet1.CreateRow(currentRowIndex);
                for (var i = 0; i < cols; i++)
                {
                    row0.CreateCell(i).Row.Height = 600;
                }
                row0.GetCell(0).SetCellValue(setting.Title);

                currentRowIndex++;

                //标题样式
                sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, cols - 1));
                var titlestyle = hssworkbook.CreateCellStyle();
                titlestyle.WrapText = true;
                var titleFont = hssworkbook.CreateFont();
                titleFont.FontHeight = 20 * 20;
                titlestyle.SetFont(titleFont);
                titlestyle.Alignment = HorizontalAlignment.Center;
                sheet1.GetRow(0).GetCell(0).CellStyle = titlestyle;
            }

            //设置描述信息
            if (!string.IsNullOrWhiteSpace(setting.Description))
            {
                var row1 = sheet1.CreateRow(currentRowIndex);
                for (var i = 0; i < cols; i++)
                {
                    row1.CreateCell(i).Row.Height = 1000;
                }
                row1.GetCell(0).SetCellValue(setting.Description);

                currentRowIndex++;

                //描述样式
                sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 0, cols - 1));
            }

            //设置列分组
            if (setting.ColumGroups != null && setting.ColumGroups.Any())
            {
                var rowGroup = sheet1.CreateRow(currentRowIndex);
                var columGroup = setting.ColumGroups;

                for (var i = 0; i < cols; i++)
                {
                    var cell = rowGroup.CreateCell(i, CellType.String);
                    cell.SetCellValue(String.Empty);
                    cell.Row.Height = 500;
                }
                var groupStyle = hssworkbook.CreateCellStyle();
                var groupFont = hssworkbook.CreateFont();
                groupFont.FontHeight = 15 * 15;
                groupFont.IsBold = true;
                groupStyle.SetFont(groupFont);
                groupStyle.Alignment = HorizontalAlignment.Center;

                var excelColumGroups = columGroup as ExcelColumGroup[] ?? columGroup.ToArray();
                for (var j = excelColumGroups.Length - 1; j >= 0; j--)
                {
                    var cell = rowGroup.GetCell(excelColumGroups.ElementAt(j).StartIndex);
                    cell.SetCellValue(excelColumGroups.ElementAt(j).Name);
                    cell.CellStyle = groupStyle;
                    sheet1.AddMergedRegion(new CellRangeAddress(2, 2, excelColumGroups.ElementAt(j).StartIndex, excelColumGroups.ElementAt(j).StartIndex + excelColumGroups.ElementAt(j).Cols - 1));
                }

                currentRowIndex++;
            }

            //列头行索引
            var columHeaderRowIndex = currentRowIndex;

            //列头样式
            var headerfont = hssworkbook.CreateFont();
            headerfont.FontHeightInPoints = 12;
            headerfont.IsBold = true;
            var headerstyle = hssworkbook.CreateCellStyle();
            headerstyle.SetFont(headerfont);

            //设置列头信息
            var row2 = sheet1.CreateRow(columHeaderRowIndex);
            for (var i = 0; i < cols; i++)
            {
                var cell = row2.CreateCell(i);
                cell.Row.Height = 500;
                cell.SetCellValue(setting.Colums.ElementAt(i).Name);
                cell.CellStyle = headerstyle;

            }

            var dataStyle = hssworkbook.CreateCellStyle();
            //水平对齐 
            dataStyle.Alignment = HorizontalAlignment.Left;
            //垂直对齐  
            dataStyle.VerticalAlignment = VerticalAlignment.Top;
            //自动换行 
            dataStyle.WrapText = true;
            //输出数据
            for (var i = 0; i < setting.Source.Count(); i++)
            {
                var model = setting.Source.ElementAt(i);
                var rowTemp = sheet1.CreateRow(i + columHeaderRowIndex + 1);

                //Type modelType = model.GetType();
                //PropertyInfo[] properties = modelType.GetProperties();
                //var proDic = new Dictionary<string, PropertyInfo>();
                //foreach (var item in properties)
                //{
                //    proDic.Add(item.Name, item);
                //}
                for (var j = 0; j < setting.Colums.Count(); j++)
                {
                    rowTemp.CreateCell(j).SetCellValueT(
                        setting.Colums.ElementAt(j).ColumType,
                        dataStyle,
                        setting.Colums.ElementAt(j).ResultFunc == null ? setting.Colums.ElementAt(j).ResultByFieldName(model, setting.Colums.ElementAt(j).FieldName) : (setting.Colums.ElementAt(j).ResultFunc(model)));
                }
            }

            HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(hssworkbook);
            //对设定并可行的列求和
            var containSum = setting.Colums.Any(item => item.DoColumSum);

            if (containSum)
            {
                var style = hssworkbook.CreateCellStyle();
                var font = hssworkbook.CreateFont();
                font.FontHeight = 15 * 15;
                font.IsBold = true;
                font.Color = HSSFColor.Blue.Index;


                style.SetFont(font);


                //求和 SUM(A2:A12)
                var letter = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                var totalRow = sheet1.CreateRow(columHeaderRowIndex + 1 + setting.Source.Count());
                for (var x = 0; x < setting.Colums.Count(); x++)
                {
                    var rowName = x == 0 ? "A" : (x > 26 ? letter.ElementAt(x / 26).ToString() + letter.ElementAt(x % 26).ToString() : letter.ElementAt(x % 26).ToString());
                    if (setting.Colums.ElementAt(x).DoColumSum)
                    {
                        var tempCell = totalRow.CreateCell(x);
                        tempCell.CellFormula = "SUM(" + rowName + (columHeaderRowIndex + 2) + ":" + rowName + (columHeaderRowIndex + 1 + setting.Source.Count()) + ")";
                        tempCell.CellStyle = style;
                        e.EvaluateInCell(tempCell);
                    }
                    else
                    {
                        var temp = totalRow.CreateCell(x);
                        temp.CellStyle = style;
                        if (x == 0) temp.SetCellValue("合计");
                    }
                }
            }

            //表格样式
            for (var i = 0; i < cols; i++)
            {
                if (setting.Colums.ElementAt(i).Width > 0)
                    sheet1.SetColumnWidth(i, setting.Colums.ElementAt(i).Width);
            }

            var stream = new MemoryStream();
            hssworkbook.Write(stream);
            return stream;
        }

        /// <summary>
        /// 浏览器端导出
        /// </summary>
        /// <param name="cellHeard"></param>
        /// <param name="enList"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static string ExportExcel(Dictionary<string, string> cellHeard, IList enList, string sheetName)
        {
            try
            {
                //第一步：创建文件
                var fileName = sheetName + "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";// 文件名称
                var urlPath = "/UpFiles/ExcelFiles/";// 文件下载的URL地址，供给前台下载
                var webRootPath = Environment.CurrentDirectory;
                var filePath =  $@"{webRootPath}\{urlPath}\{fileName}";// 文件路径

                // 1.检测是否存在文件夹，若不存在就建立个文件夹
                var directoryName = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                // 2.解析单元格头部，设置单元头的中文名称
                var workbook = new HSSFWorkbook(); // 工作簿
                var sheet = workbook.CreateSheet(sheetName); // 工作表
                var row = sheet.CreateRow(0);
                var keys = cellHeard.Keys.ToList();
                for (int i = 0; i < keys.Count; i++)
                {
                    row.CreateCell(i).SetCellValue(cellHeard[keys[i]]); // 列名为Key的值
                }

                // 3.List对象的值赋值到Excel的单元格里
                int rowIndex = 1; // 从第二行开始赋值(第一行已设置为单元头)
                foreach (var en in enList)
                {
                    var rowTmp = sheet.CreateRow(rowIndex);
                    for (int i = 0; i < keys.Count; i++) // 根据指定的属性名称，获取对象指定属性的值
                    {
                        var cellValue = ""; // 单元格的值
                        object properotyValue = null; // 属性的值
                        PropertyInfo properotyInfo = null; // 属性的信息

                        // 3.1 若属性头的名称包含'.',就表示是子类里的属性，那么就要遍历子类，eg：UserEn.UserName
                        if (keys[i].IndexOf(".") >= 0)
                        {
                            // 3.1.1 解析子类属性(这里只解析1层子类，多层子类未处理)
                            var properotyArray = keys[i].Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                            var subClassName = properotyArray[0]; // '.'前面的为子类的名称
                            var subClassProperotyName = properotyArray[1]; // '.'后面的为子类的属性名称
                            var subClassInfo = en.GetType().GetProperty(subClassName); // 获取子类的类型
                            if (subClassInfo != null)
                            {
                                // 3.1.2 获取子类的实例
                                var subClassEn = en.GetType().GetProperty(subClassName).GetValue(en, null);
                                // 3.1.3 根据属性名称获取子类里的属性类型
                                properotyInfo = subClassInfo.PropertyType.GetProperty(subClassProperotyName);
                                if (properotyInfo != null)
                                {
                                    properotyValue = properotyInfo.GetValue(subClassEn, null); // 获取子类属性的值
                                }
                            }
                        }
                        else
                        {
                            // 3.2 若不是子类的属性，直接根据属性名称获取对象对应的属性
                            properotyInfo = en.GetType().GetProperty(keys[i]);
                            if (properotyInfo != null)
                            {
                                properotyValue = properotyInfo.GetValue(en, null);
                            }
                        }
                        // 3.3 属性值经过转换赋值给单元格值
                        if (properotyValue != null)
                        {
                            cellValue = properotyValue.ToString();
                            // 3.3.1 对时间初始值赋值为空
                            if (cellValue.Trim() == "0001/1/1 0:00:00" || cellValue.Trim() == "0001/1/1 23:59:59")
                            {
                                cellValue = "";
                            }
                        }
                        // 3.4 填充到Excel的单元格里
                        rowTmp.CreateCell(i).SetCellValue(cellValue);
                    }
                    rowIndex++;
                }
                // 4.生成文件
                var file = new FileStream(filePath, FileMode.Create);
                workbook.Write(file);
                file.Close();

                // 5.返回下载路径
                return urlPath;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>
        /// 控制台应用程序导出
        /// </summary>
        /// <param name="cellHeard"></param>
        /// <param name="enList"></param>
        /// <param name="sheetName"></param>
        /// <param name="savePath"></param>
        /// <returns></returns>
        public static string ConsoleExportExcel(Dictionary<string, string> cellHeard, IList enList, string sheetName, string savePath = null)
        {
            try
            {
                //第一步：创建文件
                var fileName = sheetName + "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";// 文件名称
                var filePath = string.Empty;
                if (!string.IsNullOrWhiteSpace(savePath))
                {
                    filePath = savePath + fileName;
                }
                else
                {
                    var urlPath = "ExcelFile";// 文件下载的URL地址，供给前台下载
                    var webRootPath = Environment.CurrentDirectory;
                    filePath = $@"{webRootPath}\{urlPath}\{fileName}";// 文件路径
                    //filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, urlPath);
                }

                // 1.检测是否存在文件夹，若不存在就建立个文件夹
                string directoryName = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                // 2.解析单元格头部，设置单元头的中文名称
                var workbook = new HSSFWorkbook(); // 工作簿
                var sheet = workbook.CreateSheet(sheetName); // 工作表
                var row = sheet.CreateRow(0);
                var keys = cellHeard.Keys.ToList();
                for (int i = 0; i < keys.Count; i++)
                {
                    row.CreateCell(i).SetCellValue(cellHeard[keys[i]]); // 列名为Key的值
                }

                // 3.List对象的值赋值到Excel的单元格里
                var rowIndex = 1; // 从第二行开始赋值(第一行已设置为单元头)
                foreach (var en in enList)
                {
                    var rowTmp = sheet.CreateRow(rowIndex);
                    for (int i = 0; i < keys.Count; i++) // 根据指定的属性名称，获取对象指定属性的值
                    {
                        var cellValue = ""; // 单元格的值
                        object properotyValue = null; // 属性的值
                        PropertyInfo properotyInfo = null; // 属性的信息

                        // 3.1 若属性头的名称包含'.',就表示是子类里的属性，那么就要遍历子类，eg：UserEn.UserName
                        if (keys[i].IndexOf(".") >= 0)
                        {
                            // 3.1.1 解析子类属性(这里只解析1层子类，多层子类未处理)
                            var properotyArray = keys[i].Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                            var subClassName = properotyArray[0]; // '.'前面的为子类的名称
                            var subClassProperotyName = properotyArray[1]; // '.'后面的为子类的属性名称
                            var subClassInfo = en.GetType().GetProperty(subClassName); // 获取子类的类型
                            if (subClassInfo != null)
                            {
                                // 3.1.2 获取子类的实例
                                var subClassEn = en.GetType().GetProperty(subClassName).GetValue(en, null);
                                // 3.1.3 根据属性名称获取子类里的属性类型
                                properotyInfo = subClassInfo.PropertyType.GetProperty(subClassProperotyName);
                                if (properotyInfo != null)
                                {
                                    properotyValue = properotyInfo.GetValue(subClassEn, null); // 获取子类属性的值
                                }
                            }
                        }
                        else
                        {
                            // 3.2 若不是子类的属性，直接根据属性名称获取对象对应的属性
                            properotyInfo = en.GetType().GetProperty(keys[i]);
                            if (properotyInfo != null)
                            {
                                properotyValue = properotyInfo.GetValue(en, null);
                            }
                        }
                        // 3.3 属性值经过转换赋值给单元格值
                        if (properotyValue != null)
                        {
                            cellValue = properotyValue.ToString();
                            // 3.3.1 对时间初始值赋值为空
                            if (cellValue.Trim() == "0001/1/1 0:00:00" || cellValue.Trim() == "0001/1/1 23:59:59")
                            {
                                cellValue = "";
                            }
                        }
                        // 3.4 填充到Excel的单元格里
                        rowTmp.CreateCell(i).SetCellValue(cellValue);
                    }
                    rowIndex++;
                }
                // 4.生成文件
                var file = new FileStream(filePath, FileMode.Create);
                workbook.Write(file);
                file.Close();

                // 5.返回下载路径
                return filePath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 从Excel取数据并记录到List集合里
        /// </summary>
        /// <param name="cellHeard">单元头的值和名称：{ { "UserName", "姓名" }, { "Age", "年龄" } };</param>
        /// <param name="filePath">保存文件绝对路径</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns>转换后的List对象集合</returns>
        public static List<T> ExcelToList<T>(Dictionary<string, string> cellHeard, string filePath, out StringBuilder errorMsg) where T : new()
        {
            List<T> enlist = new List<T>();
            errorMsg = new StringBuilder();
            try
            {
                if (Regex.IsMatch(filePath, ".xls$")) // 2003
                {
                    enlist = Excel2003ToEntityList<T>(cellHeard, filePath, out errorMsg);
                }
                else if (Regex.IsMatch(filePath, ".xlsx$")) // 2007
                {
                    enlist = Excel2007ToEntityList<T>(cellHeard, filePath, out errorMsg);
                }
                return enlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rowsHeards"></param>
        /// <param name="excelPath"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        private static List<T> Excel2003ToEntityList<T>(Dictionary<string, string> rowsHeards, string excelPath, out StringBuilder errorMsg) where T : new()
        {
            errorMsg = new StringBuilder(); // 错误信息,Excel转换到实体对象时，会有格式的错误信息
            var enlist = new List<T>(); // 转换后的集合
            var keys = rowsHeards.Keys.ToList(); // 要赋值的实体对象属性名称
            var values = rowsHeards.Values.ToList(); // 要赋值的实体对象属性名称
            try
            {
                //读取文件
                using (var fs = File.OpenRead(excelPath))//new FileStream(excelPath,FileMode.Open
                {
                    var workbook = new HSSFWorkbook(fs);
                    var sheetNum = workbook.NumberOfSheets;
                    for (int m = 0; m < sheetNum; m++)
                    {
                        var sheet = workbook.GetSheetAt(m); // 获取此文件第一个Sheet页
                        var rowHead = sheet.GetRow(0);
                        for (int i = 0; i <= sheet.LastRowNum; i++)
                        {
                            var currentRow = sheet.GetRow(i);
                            // 1.判断当前行是否空行，若空行就不在进行读取下一行操作，结束Excel读取操作
                            if (currentRow == null)
                                break;
                            
                            T en = new T();
                            var errStr = ""; // 当前行转换时，是否有错误信息，格式为：第1行数据转换异常：XXX列；
                            var validatePro = -1;
                            for (int j = 0; j < keys.Count; j++)
                            {
                                var cellHead = rowHead.GetCell(j);
                                if (!values.Contains(cellHead.ToString()))
                                    continue;
                                var cellValue = currentRow.GetCell(j);
                                validatePro += 1;
                                // 2.若属性头的名称包含'.',就表示是子类里的属性，那么就要遍历子类，eg：UserEn.TrueName
                                if (keys[validatePro].IndexOf(".") >= 0)
                                {
                                    // 2.1解析子类属性
                                    var properotyArray = keys[validatePro].Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                                    var subClassName = properotyArray[0]; // '.'前面的为子类的名称
                                    var subClassProperotyName = properotyArray[1]; // '.'后面的为子类的属性名称
                                    var subClassInfo = en.GetType().GetProperty(subClassName); // 获取子类的类型
                                    if (subClassInfo != null)
                                    {
                                        // 2.1.1 获取子类的实例
                                        var subClassEn = en.GetType().GetProperty(subClassName).GetValue(en, null);
                                        // 2.1.2 根据属性名称获取子类里的属性信息
                                        var properotyInfo = subClassInfo.PropertyType.GetProperty(subClassProperotyName);
                                        if (properotyInfo != null)
                                        {
                                            try
                                            {
                                                // Excel单元格的值转换为对象属性的值，若类型不对，记录出错信息
                                                properotyInfo.SetValue(subClassEn, properotyInfo.PropertyType.CellToProperty(cellValue), null);
                                            }
                                            catch (Exception e)
                                            {
                                                if (errStr.Length == 0)
                                                {
                                                    errStr = "第" + i + "行数据转换异常：";
                                                }
                                                errStr += rowsHeards[keys[j]] + "列；";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    // 3.给指定的属性赋值
                                    var properotyInfo = en.GetType().GetProperty(rowsHeards.FirstOrDefault(c => c.Value == cellHead.ToString()).Key);
                                    if (properotyInfo != null)
                                    {
                                        try
                                        {
                                            // Excel单元格的值转换为对象属性的值，若类型不对，记录出错信息
                                            properotyInfo.SetValue(en, properotyInfo.PropertyType.CellToProperty(cellValue), null);
                                        }
                                        catch (Exception e)
                                        {
                                            if (errStr.Length == 0)
                                            {
                                                errStr = "第" + i + "行数据转换异常：【msg"+e.ToString()+"】";
                                            }
                                            errStr += rowsHeards[keys[j]] + "列；";
                                        }
                                    }
                                }

                            }
                            // 若有错误信息，就添加到错误信息里
                            if (errStr.Length > 0)
                            {
                                errorMsg.AppendLine(errStr);
                            }
                            enlist.Add(en);
                        }
                    }
                }
                return enlist;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private static List<T> Excel2007ToEntityList<T>(Dictionary<string, string> rowsHeards, string excelPath, out StringBuilder errorMsg) where T : new()
        {
            errorMsg = new StringBuilder(); // 错误信息,Excel转换到实体对象时，会有格式的错误信息
            var enlist = new List<T>(); // 转换后的集合
            var keys = rowsHeards.Keys.ToList(); // 要赋值的实体对象属性名称
            var values = rowsHeards.Values.ToList(); // 要赋值的实体对象属性名称
            try
            {
                //读取文件
                using (var fs = File.OpenRead(excelPath))//new FileStream(excelPath,FileMode.Open
                {
                    var workbook = new XSSFWorkbook(fs);
                    var sheetNum = workbook.NumberOfSheets;
                    for (int m = 0; m < sheetNum; m++)
                    {
                        var sheet = workbook.GetSheetAt(m); // 获取此文件第一个Sheet页
                        var rowHead = sheet.GetRow(0);
                        for (int i = 1; i <= sheet.LastRowNum; i++)
                        {
                            // 1.判断当前行是否空行，若空行就不在进行读取下一行操作，结束Excel读取操作
                            var currentRow = sheet.GetRow(i);
                            if ( currentRow == null)
                                break;

                            T en = new T();
                            var errStr = ""; // 当前行转换时，是否有错误信息，格式为：第1行数据转换异常：XXX列；
                            var validatePro = -1;
                            for (int j = 0; j < currentRow.LastCellNum; j++)
                            {
                                var cellHead = rowHead.GetCell(j);
                                if (!values.Contains(cellHead.ToString()))
                                    continue;
                                var cellValue = currentRow.GetCell(j);
                                validatePro += 1;
                                //if(keys[j])
                                // 2.若属性头的名称包含'.',就表示是子类里的属性，那么就要遍历子类，eg：UserEn.TrueName
                                if (keys[validatePro].IndexOf(".") >= 0)
                                {
                                    // 2.1解析子类属性
                                    var properotyArray = keys[validatePro].Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                                    var subClassName = properotyArray[0]; // '.'前面的为子类的名称
                                    var subClassProperotyName = properotyArray[1]; // '.'后面的为子类的属性名称
                                    var subClassInfo = en.GetType().GetProperty(subClassName); // 获取子类的类型
                                    if (subClassInfo != null)
                                    {
                                        // 2.1.1 获取子类的实例
                                        var subClassEn = en.GetType().GetProperty(subClassName).GetValue(en, null);
                                        // 2.1.2 根据属性名称获取子类里的属性信息
                                        var properotyInfo = subClassInfo.PropertyType.GetProperty(subClassProperotyName);
                                        if (properotyInfo != null)
                                        {
                                            try
                                            {
                                                // Excel单元格的值转换为对象属性的值，若类型不对，记录出错信息
                                                properotyInfo.SetValue(subClassEn, properotyInfo.PropertyType.CellToProperty(cellValue), null);
                                            }
                                            catch (Exception e)
                                            {
                                                if (errStr.Length == 0)
                                                {
                                                    errStr = "第" + i + "行数据转换异常：";
                                                }
                                                errStr += cellHead + "列;";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    // 3.给指定的属性赋值
                                    var properotyInfo = en.GetType().GetProperty(rowsHeards.FirstOrDefault(c => c.Value == cellHead.ToString()).Key);
                                    if (properotyInfo != null)
                                    {
                                        try
                                        {
                                            // Excel单元格的值转换为对象属性的值，若类型不对，记录出错信息
                                            properotyInfo.SetValue(en, properotyInfo.PropertyType.CellToProperty(cellValue), null);
                                        }
                                        catch (Exception ex)
                                        {
                                            if (errStr.Length == 0)
                                            {
                                                errStr = "第" + i + "行数据转换异常:";
                                            }
                                            errStr += cellHead + "列;";
                                        }
                                    }
                                }
                            }
                            // 若有错误信息，就添加到错误信息里
                            if (errStr.Length > 0)
                            {
                                errorMsg.AppendLine(errStr);
                            }
                            enlist.Add(en);
                        }
                    }
                }
                return enlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
