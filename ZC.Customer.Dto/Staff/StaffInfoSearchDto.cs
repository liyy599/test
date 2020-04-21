using System;
using System.Collections.Generic;
using System.Text;

namespace ZC.Customer.Dto.Staff
{
    /// <summary>
    /// 人员搜索Dto
    /// </summary>
    public class StaffInfoSearchDto
    {
        /// <summary>
        /// 工作人员姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
