using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZC.Customer.Model.Enums
{
    /// <summary>
    /// 账期
    /// </summary>
    public enum AccountPeriod : int
    {
        /// <summary>
        /// 提款现货
        /// </summary>
        [Description("提款现货")]
        CC = 1,

        /// <summary>
        /// 30天
        /// </summary>
        [Description("30天")]
        D30 = 2,

        /// <summary>
        /// 45天
        /// </summary>
        [Description("45天")]
        D45 = 3,

        /// <summary>
        /// 60天
        /// </summary>
        [Description("60天")]
        D60 = 4,

        /// <summary>
        /// 90天
        /// </summary>
        [Description("90天")]
        D90 = 5,

        /// <summary>
        /// 120天
        /// </summary>
        [Description("120天")]
        D120 = 6,

        /// <summary>
        /// 150天
        /// </summary>
        [Description("150天")]
        D150 = 7,

        /// <summary>
        /// 180天
        /// </summary>
        [Description("180天")]
        D180 = 8,

        /// <summary>
        /// 240天
        /// </summary>
        [Description("240天")]
        D240 = 9,
    }
}
