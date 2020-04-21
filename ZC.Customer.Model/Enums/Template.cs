using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZC.Customer.Model.Enums
{
    /// <summary>
    /// 模板
    /// </summary>
    public enum Template : int
    {
        /// <summary>
        /// 标准A4
        /// </summary>
        [Description("标准A4")]
        A4 = 1,

        /// <summary>
        /// 价格模板
        /// </summary>
        [Description("价格模板")]
        Price = 2,

        /// <summary>
        /// 产注价A4
        /// </summary>
        [Description("产注价A4")]
        ProductA4 = 3,

        /// <summary>
        /// 产注价A4+无价格
        /// </summary>
        [Description("产注价A4+无价格")]
        ProductA4NoPrice = 4,

        /// <summary>
        /// 产注价模板
        /// </summary>
        [Description("产注价模板")]
        Product = 5,

        /// <summary>
        /// 产注价模板+生产日期
        /// </summary>
        [Description("产注价模板+生产日期")]
        ProductDate = 6,

        /// <summary>
        /// 产注价模板+生产许可
        /// </summary>
        [Description("产注价模板+生产许可")]
        ProductPermit = 7,

        /// <summary>
        /// 产注价A4+SCQY
        /// </summary>
        [Description("产注价A4+SCQY")]
        ProductSCQY = 8,

        /// <summary>
        /// 价生模板
        /// </summary>
        [Description("价生模板")]
        Valence = 9,

        /// <summary>
        /// 标准A4无价格
        /// </summary>
        [Description("标准A4无价格可")]
        A4NoPrice = 10,

        /// <summary>
        /// 标准A4+生产日期
        /// </summary>
        [Description("标准A4+生产日期")]
        A4Date = 11,
    }
}
