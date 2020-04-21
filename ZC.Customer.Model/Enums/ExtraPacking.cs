using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZC.Customer.Model.Enums
{
    /// <summary>
    /// 特殊包装
    /// </summary>
    public enum ExtraPacking : int
    {
        /// <summary>
        /// 7170
        /// </summary>
        [Description("7170")]
        P7170 = 1,

        /// <summary>
        /// 7170Z
        /// </summary>
        [Description("7170Z")]
        P7170Z = 2,

        /// <summary>
        /// 7170S
        /// </summary>
        [Description("7170S")]
        P7170S = 3,

        /// <summary>
        /// 7170X
        /// </summary>
        [Description("7170X")]
        P7170X = 4,

        /// <summary>
        /// 7170XX
        /// </summary>
        [Description("7170XX")]
        P7170XX = 5,

        /// <summary>
        /// 7170J
        /// </summary>
        [Description("7170J")]
        P7170J = 6,

        /// <summary>
        /// 7170O
        /// </summary>
        [Description("7170O")]
        P7170O = 7,
    }
}
