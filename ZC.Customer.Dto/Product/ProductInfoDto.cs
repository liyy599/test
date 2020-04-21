using System;
using System.Collections.Generic;
using System.Text;
using UMa.Merak.Infrastructure.AutoMap;
using ZC.Customer.Model.Models;

namespace ZC.Customer.Dto.Product
{
    [AutoMapFrom(typeof(ProductInfo))]
    public class ProductInfoDto
    {
        /// <summary>
        /// 主键
        /// </summary>                
        public string Id { get; set; }

        /// <summary>
        /// 存货编码
        /// </summary>        
        public string Code { get; set; }

        /// <summary>
        /// 产品中文名称
        /// </summary>        
        public string ProductName { get; set; }

        /// <summary>
        /// 规格
        /// </summary>                
        public string Specs { get; set; }

        /// <summary>
        /// 型号
        /// </summary>                
        public string Model { get; set; }

        /// <summary>
        /// 简称
        /// </summary>                
        public string Abbreviation { get; set; }

        /// <summary>
        /// 包装
        /// </summary>                
        public string Pack { get; set; }

        /// <summary>
        /// 规格/毫升
        /// </summary>        
        public string Milliliter { get; set; }

        /// <summary>
        /// 报价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 规格/升
        /// </summary>
        public string Rise { get; set; }

        /// <summary>
        /// 注册证号
        /// </summary>        
        public string RegisterNo { get; set; }
    }
}
