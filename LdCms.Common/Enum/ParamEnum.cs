using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.Common.Enum
{
    /// <summary>
    /// 
    /// </summary>
    public class ParamEnum
    {
        /// <summary>
        /// 性别
        /// </summary>
        public enum Sex
        {
            保密 = 0,
            男 = 1,
            女 = 2
        }
        public enum Client
        {
            Empty = 0,
            Web = 1,
            M = 2,
            WX = 3,
            App = 4
        }

    }
}
