using ASample.NetCore.Log.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASample.NetCore.Log
{
    public class FileLogServices
    {
        private static readonly string filebasepath = AppDomain.CurrentDomain.BaseDirectory + "Logs\\";

        public static void WriteFileLog(LogParam logParam)
        {
            if (logParam is object)
            {
                if (string.IsNullOrEmpty(logParam.LogFileName))
                {
                    logParam.LogFileName = AppDomain.CurrentDomain.BaseDirectory + "Logs\\";
                }
                if (logParam.LogType == LogType.Error)
                {
                    WriteLogToFile(logParam.LogContent, logParam.LogFileName, ".txt");
                }
            }
        }

        public static void WriteLogToFile(string info, string filename, string filefix)
        {
            try
            {
                var todayRecordPath = GetTodayRecordPath();
                var path = string.Empty;
                info = "\r\n \r\n " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss :") + info;
                if (string.IsNullOrEmpty(filefix))
                {
                    path = todayRecordPath + "\\" + filename + ".log";
                }
                else
                {
                    path = todayRecordPath + "\\" + filename + filefix;
                }
                if (!Directory.Exists(todayRecordPath))
                {
                    Directory.CreateDirectory(todayRecordPath);
                }
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                }
                using (StreamWriter streamWriter = File.AppendText(path))
                {
                    streamWriter.Write(info);
                    streamWriter.Close();
                }
            }
            catch
            {
            }
        }

        public static void WriteLogToFile(string info)
        {
            try
            {
                var todayRecordPath = GetTodayRecordPath();
                var path = string.Empty;
                info = "\r\n \r\n " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss :") + info;
                path = todayRecordPath + "\\" + "log" + ".txt";
                if (!Directory.Exists(todayRecordPath))
                {
                    Directory.CreateDirectory(todayRecordPath);
                }
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                }
                using (StreamWriter streamWriter = File.AppendText(path))
                {
                    streamWriter.Write(info);
                    streamWriter.Close();
                }
            }
            catch
            {
            }
        }

        private static string GetCurrentMonthRecordPath()
        {
            string text;
            if (!Directory.Exists(filebasepath + DateTime.Now.Year.ToString()))
            {
                Directory.CreateDirectory(FileLogServices.filebasepath + DateTime.Now.Year.ToString());
                text = DateTime.Now.Year.ToString() + "\\";
            }
            if (!Directory.Exists(filebasepath + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString()))
            {
                Directory.CreateDirectory(filebasepath + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString());
                text = DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\";
            }
            if (!Directory.Exists(string.Concat(new string[]
            {
                filebasepath,
                DateTime.Now.Year.ToString(),
                "/",
                DateTime.Now.Month.ToString(),
                "/",
                DateTime.Now.ToString("yyyyMMdd")
            })))
            {
                Directory.CreateDirectory(string.Concat(new string[]
                {
                    filebasepath,
                    DateTime.Now.Year.ToString(),
                    "/",
                    DateTime.Now.Month.ToString(),
                    "/",
                    DateTime.Now.ToString("dd")
                }));
                text = string.Concat(new string[]
                {
                    DateTime.Now.Year.ToString(),
                    "\\",
                    DateTime.Now.Month.ToString(),
                    "\\",
                    DateTime.Now.ToString("dd"),
                    "/"
                });
            }
            return string.Concat(new string[]
            {
                filebasepath,
                DateTime.Now.Year.ToString(),
                "\\",
                DateTime.Now.Month.ToString(),
                "\\"
            });
        }

        private static string GetTodayRecordPath()
        {
            if (!Directory.Exists(FileLogServices.filebasepath + DateTime.Now.Year.ToString()))
            {
                Directory.CreateDirectory(FileLogServices.filebasepath + DateTime.Now.Year.ToString());
                _ = DateTime.Now.Year.ToString() + "\\";
            }
            string text;
            if (!Directory.Exists(filebasepath + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString()))
            {
                Directory.CreateDirectory(FileLogServices.filebasepath + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString());
                text = DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\";
            }
            if (!Directory.Exists(string.Concat(new string[]
            {
                filebasepath,
                DateTime.Now.Year.ToString(),
                "/",
                DateTime.Now.Month.ToString(),
                "/",
                DateTime.Now.ToString("yyyyMMdd")
            })))
            {
                Directory.CreateDirectory(string.Concat(new string[]
                {
                    filebasepath,
                    DateTime.Now.Year.ToString(),
                    "/",
                    DateTime.Now.Month.ToString(),
                    "/",
                    DateTime.Now.ToString("dd")
                }));
                text = string.Concat(new string[]
                {
                    DateTime.Now.Year.ToString(),
                    "\\",
                    DateTime.Now.Month.ToString(),
                    "\\",
                    DateTime.Now.ToString("dd"),
                    "/"
                });
            }
            return string.Concat(new string[]
            {
                filebasepath,
                DateTime.Now.Year.ToString(),
                "\\",
                DateTime.Now.Month.ToString(),
                "\\",
                DateTime.Now.ToString("dd"),
                "\\"
            });
        }
    }
}
