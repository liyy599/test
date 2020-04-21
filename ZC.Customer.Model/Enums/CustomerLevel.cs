using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZC.Customer.Model.Enums
{
    /// <summary>
    /// 客户等级
    /// </summary>
    public enum CustomerLevel : int
    {
        /// <summary>
        /// 1级
        /// </summary>
        [Description("1级")]
        One = 1,

        /// <summary>
        /// 2级
        /// </summary>
        [Description("2级")]
        Two = 2,

        /// <summary>
        /// 3级
        /// </summary>
        [Description("3级")]
        Three = 3,
    }
}
