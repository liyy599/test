using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZC.Customer.Model.Enums
{
    /// <summary>
    /// 快递
    /// </summary>
    public enum Express : int
    {
        /// <summary>
        /// 顺丰
        /// </summary>
        [Description("顺丰")]
        SF = 1,

        /// <summary>
        /// 韵达
        /// </summary>
        [Description("韵达")]
        YD = 2,

        /// <summary>
        /// 德邦
        /// </summary>
        [Description("德邦")]
        DB = 3,

        /// <summary>
        /// 美乐维冷链
        /// </summary>
        [Description("美乐维冷链")]
        MLW = 4,
    }
}
