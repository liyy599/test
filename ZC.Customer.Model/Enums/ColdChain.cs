using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZC.Customer.Model.Enums
{
    /// <summary>
    /// 冷链记录
    /// </summary>
    public enum ColdChain : int
    {
        /// <summary>
        /// 随货
        /// </summary>
        [Description("随货")]
        Carry = 1,

        /// <summary>
        /// 单独寄
        /// </summary>
        [Description("单独寄")]
        Alone = 2,

        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        None = 3,
        
    }
}
