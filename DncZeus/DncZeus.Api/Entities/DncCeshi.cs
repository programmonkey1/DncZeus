using System;
using System.Collections.Generic;

namespace DncZeus.Api.Entities
{
    public partial class DncCeshi
    {
        /// <summary>
        /// 是否已删（枚举）
        /// </summary>
        public enum IsDeleted
        {
            /// <summary>
            /// 所有
            /// </summary>
            All = -1,
            /// <summary>
            /// 否
            /// </summary>
            No = 0,
            /// <summary>
            /// 是
            /// </summary>
            Yes = 1
        }
        public int Id { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public IsDeleted C { get; set; }
        public string D { get; set; }
        public string E { get; set; }
        public string F { get; set; }
        public string G { get; set; }
    }
}
