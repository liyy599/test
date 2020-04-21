using System;
using System.Collections.Generic;
using System.Text;
using ZC.Customer.Model.Enums;
using ZC.Customer.Model.Models;
using UMa.Merak.Infrastructure.AutoMap;

namespace ZC.Customer.Dto.Company
{
    [AutoMap(typeof(CompanyInfo))]
    public class CompanyInfoDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string ContractAddress { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string ContractName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContractNum { get; set; }

        /// <summary>
        /// 客户分类
        /// </summary>
        public CustomerType CustomerType { get; set; }

        /// <summary>
        /// 客户等级
        /// </summary>
        public CustomerLevel CustomerLevel { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public Area Area { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public Province Province { get; set; }

        /// <summary>
        /// 总监id
        /// </summary>
        public string DirectorId { get; set; }

        /// <summary>
        /// 总监姓名
        /// </summary>
        public string DirectorName { get; set; }

        /// <summary>
        /// 业务员id
        /// </summary>
        public string SalesmanId { get; set; }

        /// <summary>
        /// 业务员姓名
        /// </summary>
        public string SalesmanName { get; set; }

        /// <summary>
        /// 快递
        /// </summary>
        public Express Express { get; set; }

        /// <summary>
        /// 发票
        /// </summary>
        public Invoice Invoice { get; set; }

        /// <summary>
        /// 出库单模板
        /// </summary>
        public Template Template { get; set; }

        /// <summary>
        /// 出库单联数
        /// </summary>
        public LinkNum LinkNum { get; set; }

        /// <summary>
        /// 出库单是否盖章
        /// </summary>
        public bool IsSeal { get; set; }

        /// <summary>
        /// 质检报告
        /// </summary>
        public QT QT { get; set; }

        /// <summary>
        /// 冷链记录
        /// </summary>
        public ColdChain ColdChain { get; set; }

        /// <summary>
        /// 变更公司名称
        /// </summary>
        public string AlterationCompany { get; set; }

        /// <summary>
        /// 关联公司名称
        /// </summary>
        public string AffiliatedCompany { get; set; }

        /// <summary>
        /// 特殊产品名称
        /// </summary>
        public string SpecialProduct { get; set; }

        /// <summary>
        /// 特殊包装
        /// </summary>
        public string ExtraPacking { get; set; }

        /// <summary>
        /// 账期
        /// </summary>
        public AccountPeriod AccountPeriod { get; set; }

        /// <summary>
        /// 注意事项
        /// </summary>
        public string Remark { get; set; }
    }
}
