using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZC.Customer.IRepository;
using ZC.Customer.Model.Models;

namespace ZC.Customer.Repository
{
    /// <summary>
    /// 产品信息存储器
    /// </summary>
    public class ProductInfoRepository : BaseRepository<ProductInfo, long>, IProductInfoRepository
    {
        public ProductInfoRepository(DbContext context) : base(context)
        {
        }
    }
}
