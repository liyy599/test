using System;
using System.Collections.Generic;
using System.Text;
using ZC.Customer.Model.Enums;
using ZC.Customer.Model.Models;
using UMa.Merak.Infrastructure.AutoMap;
using UMa.Merak.Infrastructure.Domain.Enums;
using UMa.Merak.Infrastructure.Helper;

namespace ZC.Customer.Dto.Company
{
    [AutoMapFrom(typeof(CompanyInfo))]
    public class CompanyInfoItem
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
        /// 联系地址
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
        /// 客户分类字符串
        /// </summary>
        public string CustomerTypeStr {
            get
            {
                return EnumHelper.GetDescriptionByValue(typeof(CustomerType), ((int)this.CustomerType).ToString());
            }
        }

        /// <summary>
        /// 客户等级
        /// </summary>
        public CustomerLevel CustomerLevel { get; set; }

        /// <summary>
        /// 客户等级字符串
        /// </summary>
        public string CustomerLevelStr
        {
            get
            {
                return EnumHelper.GetDescriptionByValue(typeof(CustomerLevel), ((int)this.CustomerLevel).ToString());
            }
        }

        /// <summary>
        /// 区域
        /// </summary>
        public Area Area { get; set; }

        /// <summary>
        /// 区域字符串
        /// </summary>
        public string AreaStr
        {
            get
            {
                return EnumHelper.GetDescriptionByValue(typeof(Area), ((int)this.Area).ToString());
            }
        }

        /// <summary>
        /// 省份
        /// </summary>
        public Province Province { get; set; }

        /// <summary>
        /// 省份字符串
        /// </summary>
        public string ProvinceStr
        {
            get
            {
                return EnumHelper.GetDescriptionByValue(typeof(Province), ((int)this.Province).ToString());
            }
        }

        /// <summary>
        /// 总监id
        /// </summary>
        public long DirectorId { get; set; }

        /// <summary>
        /// 总监姓名
        /// </summary>
        public string DirectorName { get; set; }

        /// <summary>
        /// 业务员id
        /// </summary>
        public long SalesmanId { get; set; }

        /// <summary>
        /// 业务员姓名
        /// </summary>
        public string SalesmanName { get; set; }

        /// <summary>
        /// 快递
        /// </summary>
        public Express Express { get; set; }

        /// <summary>
        /// 快递字符串
        /// </summary>
        public string ExpressStr
        {
            get
            {
                return EnumHelper.GetDescriptionByValue(typeof(Express), ((int)this.Express).ToString());
            }
        }

        /// <summary>
        /// 发票
        /// </summary>
        public Invoice Invoice { get; set; }

        /// <summary>
        /// 发票字符串
        /// </summary>
        public string InvoiceStr
        {
            get
            {
                return EnumHelper.GetDescriptionByValue(typeof(Invoice), ((int)this.Invoice).ToString());
            }
        }

        /// <summary>
        /// 出库单模板
        /// </summary>
        public Template Template { get; set; }

        /// <summary>
        /// 出库单模板字符串
        /// </summary>
        public string TemplateStr
        {
            get
            {
                return EnumHelper.GetDescriptionByValue(typeof(Template), ((int)this.Template).ToString());
            }
        }

        /// <summary>
        /// 出库单联数
        /// </summary>
        public LinkNum LinkNum { get; set; }

        /// <summary>
        /// 出库单联数字符串
        /// </summary>
        public string LinkNumStr
        {
            get
            {
                return EnumHelper.GetDescriptionByValue(typeof(LinkNum), ((int)this.LinkNum).ToString());
            }
        }

        /// <summary>
        /// 出库单是否盖章
        /// </summary>
        public bool IsSeal { get; set; }

        /// <summary>
        /// 出库单是否盖章字符串
        /// </summary>
        public string IsSealStr {
            get
            {
                if(this.IsSeal)
                {
                    return "是";
                }
                else
                {
                    return "否";
                }
            }
        }

        /// <summary>
        /// 质检报告
        /// </summary>
        public QT QT { get; set; }

        /// <summary>
        /// 质检报告字符串
        /// </summary>
        public string QTStr
        {
            get
            {
                return EnumHelper.GetDescriptionByValue(typeof(QT), ((int)this.QT).ToString());
            }
        }

        /// <summary>
        /// 冷链记录
        /// </summary>
        public ColdChain ColdChain { get; set; }

        /// <summary>
        /// 冷链记录字符串
        /// </summary>
        public string ColdChainStr
        {
            get
            {
                return EnumHelper.GetDescriptionByValue(typeof(ColdChain), ((int)this.ColdChain).ToString());
            }
        }

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
        /// 特殊产品名称字符串
        /// </summary>
        public string SpecialProductStr
        {
            get
            {
                var info = this.SpecialProduct.Split(',');
                var str = "";
                foreach (var product in info)
                {
                    str += ",";
                    str += EnumHelper.GetDescriptionByValue(typeof(SpecialProduct), product);
                }
                str = str.Substring(1, str.Length - 1);
                return str;
            }
        }

        /// <summary>
        /// 特殊包装
        /// </summary>
        public string ExtraPacking { get; set; }

        /// <summary>
        /// 特殊包装名称字符串
        /// </summary>
        public string ExtraPackingStr
        {
            get
            {
                var info = this.ExtraPacking.Split(',');
                var str = "";
                foreach (var packing in info)
                {
                    str += ",";
                    str += EnumHelper.GetDescriptionByValue(typeof(ExtraPacking), packing);
                }
                str = str.Substring(1, str.Length - 1);
                return str;
            }
        }

        /// <summary>
        /// 账期
        /// </summary>
        public AccountPeriod AccountPeriod { get; set; }

        /// <summary>
        /// 账期字符串
        /// </summary>
        public string AccountPeriodStr
        {
            get
            {
                return EnumHelper.GetDescriptionByValue(typeof(AccountPeriod), ((int)this.AccountPeriod).ToString());
            }
        }

        /// <summary>
        /// 注意事项
        /// </summary>
        public string Remark { get; set; }

    }
}
