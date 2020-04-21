using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZC.Customer.Model.Enums
{
    /// <summary>
    /// 发票
    /// </summary>
    public enum Invoice : int
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
    }
}
