using System;

namespace LdCms.Common.Logs
{
    /// <summary>
    /// Text文件日志操作
    /// </summary>
    public static class LogsManager
    {
        /// <summary>
        /// 定义对象，用于加锁
        /// </summary>
        private static readonly object writeFile = new object();
        /// <summary>
        /// 保存路径
        /// </summary>
        private static string logPath = string.Empty;
        /// <summary>
        /// 保存日志的文件夹
        /// </summary>
        public static string LogPath
        {
            get
            {
                if (logPath == string.Empty)
                {
                    logPath = DefaultFolder();
                }
                return logPath;
            }
            set
            {
                logPath = value;
            }
        }
        /// <summary>
        /// 日志文件前缀
        /// </summary>
        public static string logFielPrefix = string.Empty;
        /// <summary>
        /// 日志文件前缀
        /// </summary>
        private static string LogFielPrefix
        {
            get
            {
                return logFielPrefix;
            }
            set
            {
                logFielPrefix = value;
            }
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logFile">日志类型</param>
        /// <param name="msg">日志内容</param>
        private static void WriteLog(string logFile, string msg)
        {
            lock (writeFile)
            {
                try
                {
                    string FileName = DateTime.Now.ToString("yyyy-MM-dd_HH");
                    string PathName = string.Format("{0}{1}{2}-{3}.Log", LogPath, LogFielPrefix, logFile, FileName);
                    System.IO.StreamWriter sw = System.IO.File.AppendText(PathName);
                    sw.WriteLine(string.Format("{0}：{1} \r", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg));
                    sw.Flush();
                    sw.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logFile">日志类型</param>
        /// <param name="msg">日志内容</param>
        public static void WriteLog(LogsFile logFile, string msg)
        {
            WriteLog(logFile.ToString(), msg);
        }
        /// <summary>
        /// 日志默认保存文件夹
        /// </summary>
        /// <returns></returns>
        private static string DefaultFolder(string Root = "Logs")
        {
            try
            {
                DateTime Time = DateTime.Now;
                string YearMonth = Time.ToString("yyyy-MM");
                string Day = Time.ToString("dd");
                string RootFolder = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, Root);
                if (!System.IO.Directory.Exists(RootFolder))
                {
                    System.IO.Directory.CreateDirectory(RootFolder);
                }
                string YearMonthFolder = string.Format("{0}/{1}", RootFolder, YearMonth);
                if (!System.IO.Directory.Exists(YearMonthFolder))
                {
                    System.IO.Directory.CreateDirectory(YearMonthFolder);
                }
                string DayFolder = string.Format("{0}/{1}/{2}日/", RootFolder, YearMonth, Day);
                if (!System.IO.Directory.Exists(DayFolder))
                {
                    System.IO.Directory.CreateDirectory(DayFolder);
                }
                return DayFolder;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
