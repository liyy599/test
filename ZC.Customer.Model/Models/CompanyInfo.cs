using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UMa.Merak.Infrastructure.Domain.Entities;
using ZC.Customer.Model.Enums;

namespace ZC.Customer.Model.Models
{
    /// <summary>
    /// 公司信息表
    /// </summary>
    public class CompanyInfo : IEntity<long>, ISoftDelete
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string CompanyName { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        [StringLength(150)]
        public string ContractAddress { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [StringLength(50)]
        public string ContractName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [StringLength(50)]
        public string ContractNum { get; set; }

        /// <summary>
        /// 客户分类
        /// </summary>
        [Required]
        public CustomerType CustomerType { get; set; }

        /// <summary>
        /// 客户等级
        /// </summary>
        [Required]
        public CustomerLevel CustomerLevel { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        [Required]
        public Area Area { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [Required]
        public Province Province { get; set; }

        /// <summary>
        /// 总监id
        /// </summary>
        [Required]
        public long DirectorId { get; set; }

        /// <summary>
        /// 总监姓名
        /// </summary>
        [Required]
        public string DirectorName { get; set; }

        /// <summary>
        /// 业务员id
        /// </summary>
        [Required]
        public long SalesmanId { get; set; }

        /// <summary>
        /// 业务员姓名
        /// </summary>
        [Required]
        public string SalesmanName { get; set; }

        /// <summary>
        /// 快递
        /// </summary>
        [Required]
        public Express Express { get; set; }

        /// <summary>
        /// 发票
        /// </summary>
        [Required]
        public Invoice Invoice { get; set; }

        /// <summary>
        /// 出库单模板
        /// </summary>
        [Required]
        public Template Template { get; set; }

        /// <summary>
        /// 出库单联数
        /// </summary>
        [Required]
        public LinkNum LinkNum { get; set; }

        /// <summary>
        /// 出库单是否盖章
        /// </summary>
        [Required]
        public bool IsSeal { get; set; }

        /// <summary>
        /// 质检报告
        /// </summary>
        [Required]
        public QT QT { get; set; }

        /// <summary>
        /// 冷链记录
        /// </summary>
        [Required]
        public ColdChain ColdChain { get; set; }

        /// <summary>
        /// 变更公司名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string AlterationCompany { get; set; }

        /// <summary>
        /// 关联公司名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string AffiliatedCompany { get; set; }

        /// <summary>
        /// 特殊产品名称
        /// </summary>
        [Required]
        public string SpecialProduct { get; set; }

        /// <summary>
        /// 特殊包装
        /// </summary>
        [Required]
        public string ExtraPacking { get; set; }

        /// <summary>
        /// 账期
        /// </summary>
        [Required]
        public AccountPeriod AccountPeriod { get; set; }

        /// <summary>
        /// 注意事项
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Remark { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModificationTime { get; set; }
        

        /// <summary>
        /// 是否逻辑删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
