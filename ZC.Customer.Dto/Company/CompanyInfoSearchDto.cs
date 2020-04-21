using System;
using System.Collections.Generic;
using System.Text;
using ZC.Customer.Model.Enums;
using ZC.Customer.Model.Models;
using UMa.Merak.Infrastructure.AutoMap;

namespace ZC.Customer.Dto.Company
{
    [AutoMap(typeof(CompanyInfo))]
    public class CompanyInfoSearchDto
    {

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderBy { get; set; }
    }
}
