using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZC.Customer.Model.Enums
{
    /// <summary>
    /// 岗位
    /// </summary>
    public enum Post : int
    {
        /// <summary>
        /// 总监
        /// </summary>
        [Description("总监")]
        Director = 1,

        /// <summary>
        /// 其他
        /// </summary>
        [Description("业务员")]
        Other = 100,
    }
}
