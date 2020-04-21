using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UMa.Merak.Infrastructure.Dto;
using ZC.Customer.Dto;
using ZC.Customer.Dto.Product;

namespace ZC.Customer.IServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProductInfoServices
    {
        /// <summary>
        /// 获取产品分页列表信息
        /// </summary>
        /// <param name="dto">查询dto</param>
        /// <returns></returns>
        Task<ResponseResult> GetProductInfoPageResultAsync(ProductSearchDto dto);

        /// <summary>
        /// 新增或者修改产品信息
        /// </summary>
        /// <param name="jsonStr">Json格式公司信息dto</param>
        /// <returns></returns>
        Task<ResponseResult> AddOrUpdateProductInfo(JsonDto jsonStr);

        /// <summary>
        /// 获取雪花Id
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        ResponseResult GetIdWorker (int count);
    }
}
