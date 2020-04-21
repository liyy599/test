using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZC.Customer.Model.Enums
{
    /// <summary>
    /// 客户分类
    /// </summary>
    public enum CustomerType :int
    {
        /// <summary>
        /// 大客户
        /// </summary>
        [Description("大客户")]
        Heavy = 1,

        /// <summary>
        /// 小客户
        /// </summary>
        [Description("小客户")]
        Light = 2,

        /// <summary>
        /// 普通客户
        /// </summary>
        [Description("普通客户")]
        Common = 3,
    }
}
