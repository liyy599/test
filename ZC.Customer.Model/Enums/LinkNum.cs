using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZC.Customer.Model.Enums
{
    /// <summary>
    /// 出库单联数
    /// </summary>
    public enum LinkNum : int
    {
        /// <summary>
        /// 白联
        /// </summary>
        [Description("白联")]
        White = 1,

        /// <summary>
        /// 2张白联
        /// </summary>
        [Description("2张白联")]
        WhiteDouble = 2,

        /// <summary>
        /// 红白两联
        /// </summary>
        [Description("红白两联")]
        RedWhite = 3,

        /// <summary>
        /// 三联
        /// </summary>
        [Description("三联")]
        Three = 4,

        /// <summary>
        /// 四联
        /// </summary>
        [Description("四联")]
        Four = 2,

        /// <summary>
        /// 五联
        /// </summary>
        [Description("五联")]
        Five = 3,

        /// <summary>
        /// 四联+黄蓝两联
        /// </summary>
        [Description("四联+黄蓝两联")]
        FourYellowBlue = 4,
    }
}
