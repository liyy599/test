using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZC.Customer.Model.Enums
{
    /// <summary>
    /// 质检报告
    /// </summary>
    public enum QT : int
    {
        /// <summary>
        /// 随货盖章
        /// </summary>
        [Description("随货盖章")]
        CarrySeal = 1,

        /// <summary>
        /// 随货不盖章
        /// </summary>
        [Description("随货不盖章")]
        CarryNoSeal = 2,

        /// <summary>
        /// 单独寄
        /// </summary>
        [Description("单独寄")]
        Alone = 3,

        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        None = 4,
    }
}
