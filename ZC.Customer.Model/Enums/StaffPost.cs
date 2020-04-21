using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZC.Customer.Model.Enums
{
    /// <summary>
    /// 员工岗位
    /// </summary>
    public enum StaffPost : int
    {
        /// <summary>
        /// 总监
        /// </summary>
        [Description("总监")]
        Director = 1,

        /// <summary>
        /// 业务员
        /// </summary>
        [Description("业务员")]
        Salesman = 2,
        
    }
}
