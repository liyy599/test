using System;
using System.Collections.Generic;
using System.Text;

namespace ZC.Customer.Dto.Product
{
    /// <summary>
    /// 产品查询Dto
    /// </summary>
    public class ProductSearchDto
    {
        /// <summary>
        /// 存货编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>
        public string ProductName { get; set; }

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
