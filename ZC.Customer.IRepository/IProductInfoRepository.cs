using System;
using System.Collections.Generic;
using System.Text;
using ZC.Customer.Model.Models;

namespace ZC.Customer.IRepository
{
    /// <summary>
    /// 产品信息存储器接口
    /// </summary>
    public interface IProductInfoRepository : IBaseRepository<ProductInfo, long>
    {
    }
}
