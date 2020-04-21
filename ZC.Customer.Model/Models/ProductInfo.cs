using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using UMa.Merak.Infrastructure.Domain.Entities;

namespace ZC.Customer.Model.Models
{
    /// <summary>
    /// 产品基础信息表
    /// </summary>
    public class ProductInfo : IEntity<long>, ISoftDelete
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        /// <summary>
        /// 存货编码
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        /// <summary>
        /// 产品中文名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        /// <summary>
        /// 规格
        /// </summary>        
        [StringLength(50)]
        public string Specs { get; set; }

        /// <summary>
        /// 型号
        /// </summary>        
        [StringLength(50)]
        public string Model { get; set; }

        /// <summary>
        /// 简称
        /// </summary>        
        [StringLength(50)]
        public string Abbreviation { get; set; }

        /// <summary>
        /// 包装
        /// </summary>        
        [StringLength(50)]
        public string Pack { get; set; }

        /// <summary>
        /// 规格/毫升
        /// </summary>
        [StringLength(50)]
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
        [StringLength(50)]
        public string RegisterNo { get; set; }



        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreationTime { get; set ; }



        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModificationTime { get; set; }



        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
