using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZC.Customer.Model.Enums
{
    /// <summary>
    /// 区域
    /// </summary>
    public enum Area : int
    {
        /// <summary>
        /// 苏沪区
        /// </summary>
        [Description("苏沪区")]
        SH = 1,

        /// <summary>
        /// 华中区
        /// </summary>
        [Description("华中区")]
        HZ = 2,

        /// <summary>
        /// 西南华南区
        /// </summary>
        [Description("西南华南区")]
        XNHN = 3,

        /// <summary>
        /// 北区
        /// </summary>
        [Description("北区")]
        North = 4,
    }
}
