using ASample.NetCore.Log.Models;
using System;
using System.IO;

namespace ASample.NetCore.Log
{
    public class FileLogServices
    {
        private static readonly string Filebasepath = AppDomain.CurrentDomain.BaseDirectory + "Logs\\";

        public static void WriteFileLog(LogParam logParam)
        {
            if (logParam != null)
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
                string path;
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
                // ignored
            }
        }

        public static void WriteLogToFile(string info)
        {
            try
            {
                var todayRecordPath = GetTodayRecordPath();
                string path;
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
                // ignored
            }
        }

        private static string GetCurrentMonthRecordPath()
        {
            if (!Directory.Exists(Filebasepath + DateTime.Now.Year))
            {
                Directory.CreateDirectory(Filebasepath + DateTime.Now.Year);
            }
            if (!Directory.Exists(Filebasepath + DateTime.Now.Year + "/" + DateTime.Now.Month))
            {
                Directory.CreateDirectory(Filebasepath + DateTime.Now.Year + "/" + DateTime.Now.Month);
            }
            if (!Directory.Exists(string.Concat(new string[]
            {
                Filebasepath,
                DateTime.Now.Year.ToString(),
                "/",
                DateTime.Now.Month.ToString(),
                "/",
                DateTime.Now.ToString("yyyyMMdd")
            })))
            {
                Directory.CreateDirectory(string.Concat(new string[]
                {
                    Filebasepath,
                    DateTime.Now.Year.ToString(),
                    "/",
                    DateTime.Now.Month.ToString(),
                    "/",
                    DateTime.Now.ToString("dd")
                }));
            }
            return string.Concat(new string[]
            {
                Filebasepath,
                DateTime.Now.Year.ToString(),
                "\\",
                DateTime.Now.Month.ToString(),
                "\\"
            });
        }

        private static string GetTodayRecordPath()
        {
            if (!Directory.Exists(Filebasepath + DateTime.Now.Year.ToString()))
            {
                Directory.CreateDirectory(Filebasepath + DateTime.Now.Year.ToString());
            }

            if (!Directory.Exists(Filebasepath + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString()))
            {
                Directory.CreateDirectory(Filebasepath + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString());
            }
            if (!Directory.Exists(string.Concat(new string[]
            {
                Filebasepath,
                DateTime.Now.Year.ToString(),
                "/",
                DateTime.Now.Month.ToString(),
                "/",
                DateTime.Now.ToString("yyyyMMdd")
            })))
            {
                Directory.CreateDirectory(string.Concat(new string[]
                {
                    Filebasepath,
                    DateTime.Now.Year.ToString(),
                    "/",
                    DateTime.Now.Month.ToString(),
                    "/",
                    DateTime.Now.ToString("dd")
                }));
            }
            return string.Concat(new string[]
            {
                Filebasepath,
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
