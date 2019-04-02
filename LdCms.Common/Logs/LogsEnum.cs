using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.Common.Logs
{
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogsFile
    {
        /// <summary>
        /// 调试类型
        /// </summary>
        Debug,
        /// <summary>
        /// 微量类型
        /// </summary>
        Trace,
        /// <summary>
        /// 警告类型
        /// </summary>
        Warning,
        /// <summary>
        /// 错误类型
        /// </summary>
        Error,
        /// <summary>
        /// 监测
        /// </summary>
        Monitoring
    }
   
}
