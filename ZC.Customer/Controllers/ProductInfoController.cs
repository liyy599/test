using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UMa.Merak.Infrastructure.Dto;
using ZC.Customer.Dto;
using ZC.Customer.Dto.Product;
using ZC.Customer.IServices;

namespace ZC.Customer.Controllers
{
    /// <summary>
    /// 产品信息
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductInfoController : ControllerBase
    {
        readonly IProductInfoServices productInfoServices;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ProductInfoController(IProductInfoServices productInfoServices)
        {
            this.productInfoServices = productInfoServices;
        }

        /// <summary>
        /// 添加产品
        /// api/ProductInfo/AddOrUpdateProductInfo
        /// </summary>
        /// <param name="jsonStr"></param>        
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseResult> AddOrUpdateProductInfo(JsonDto jsonStr)
        {
            var result = await this.productInfoServices.AddOrUpdateProductInfo(jsonStr);

            return result;
        }

        /// <summary>
        /// 获取产品分页列表信息
        /// </summary>
        /// <param name="dto">查询dto</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseResult> GetProductInfoPageResultAsync(ProductSearchDto dto)
        {
            var result = await this.productInfoServices.GetProductInfoPageResultAsync(dto);

            return result;
        }

        /// <summary>
        /// 获取雪花Id
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult GetIdWorker(int count)
        {
            var result = this.productInfoServices.GetIdWorker(count);

            return result;
        }
    }
}
