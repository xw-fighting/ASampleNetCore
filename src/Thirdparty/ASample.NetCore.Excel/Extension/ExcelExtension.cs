using ASample.NetCore.Excel.Values;
using NPOI.SS.UserModel;
using System;

namespace ASample.NetCore.Excel
{
    public static class ExcelExtension
    {
        /// <summary>
        /// 给单元格赋值，并设置数据类型
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="type"></param>
        /// <param name="cellStyle"></param>
        /// <param name="value"></param>
        public static void SetCellValueT(this ICell cell, ExcelColumType type, ICellStyle cellStyle, object value)
        {
            if (value == null)
                return;
            if (type == ExcelColumType.Bool)
                cell.SetCellValue((bool)value);
            else if (type == ExcelColumType.DateTime)
                cell.SetCellValue((DateTime)value);
            else if (type == ExcelColumType.Double)
            {
                if (double.TryParse(value.ToString(), out double number))
                    cell.SetCellValue(number);
                else
                    cell.SetCellValue(value.ToString());
            }
            else
                cell.SetCellValue(value.ToString());
            cell.CellStyle = cellStyle;
        }

        /// <summary>
        /// 从Excel获取值传递到对象的属性里
        /// </summary>
        /// <param name="distanceType">目标对象类型</param>
        /// <param name="sourceCell">对象属性的值</param>
        public static object CellToProperty(this Type distanceType, ICell sourceCell)
        {
            object rs = distanceType.IsValueType ? Activator.CreateInstance(distanceType) : null;

            // 1.判断传递的单元格是否为空
            if (sourceCell == null || string.IsNullOrEmpty(sourceCell.ToString()))
            {
                return rs;
            }

            // 2.Excel文本和数字单元格转换，在Excel里文本和数字是不能进行转换，所以这里预先存值
            object sourceValue = null;
            switch (sourceCell.CellType)
            {
                case CellType.Blank:
                    break;

                case CellType.Boolean:
                    break;

                case CellType.Error:
                    break;

                case CellType.Formula:
                    break;

                case CellType.Numeric:
                    sourceValue = sourceCell.NumericCellValue;
                    break;

                case CellType.String:
                    sourceValue = sourceCell.StringCellValue;
                    break;

                case CellType.Unknown:
                    break;

                default:
                    break;
            }

            string valueDataType = distanceType.Name;

            // 在这里进行特定类型的处理
            switch (valueDataType.ToLower()) // 以防出错，全部小写
            {
                case "string":
                    rs = sourceValue.ToString();
                    break;
                case "int":
                case "int16":
                case "int32":
                    rs = (int)Convert.ChangeType(sourceCell.NumericCellValue.ToString(), distanceType);
                    break;
                case "float":
                case "single":
                    rs = (float)Convert.ChangeType(sourceCell.NumericCellValue.ToString(), distanceType);
                    break;
                case "datetime":
                    rs = sourceCell.DateCellValue;
                    break;
                case "guid":
                    rs = (Guid)Convert.ChangeType(sourceCell.NumericCellValue.ToString(), distanceType);
                    return rs;
            }
            return rs;
        }
    }
}
