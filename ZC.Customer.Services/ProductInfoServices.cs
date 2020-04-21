using Snowflake;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UMa.Merak.Infrastructure.AutoMap;
using UMa.Merak.Infrastructure.Data.Specification;
using UMa.Merak.Infrastructure.Dto;
using UMa.Merak.Infrastructure.Helper;
using UMa.Merak.Infrastructure.LogHelper;
using ZC.Customer.Dto;
using ZC.Customer.Dto.Product;
using ZC.Customer.IRepository;
using ZC.Customer.IServices;
using ZC.Customer.Model.Models;

namespace ZC.Customer.Services
{
    /// <summary>
    /// ProductInfoServices
    /// </summary>
    public class ProductInfoServices : IProductInfoServices
    {
        /// <summary>
        /// 产品业务逻辑接口
        /// </summary>
        private readonly IProductInfoRepository productInfoRepository;

        public ProductInfoServices(IProductInfoRepository productInfoRepository)
        {
            this.productInfoRepository = productInfoRepository;
        }

        /// <summary>
        /// 获取产品的分页信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ResponseResult> GetProductInfoPageResultAsync(ProductSearchDto dto)
        {
            var result = new ResponseResult(0, "获取成功");
            Expression<Func<ProductInfo, bool>> filter = p => true;
            filter = filter.And(p => !p.IsDeleted);

            if (!string.IsNullOrEmpty(dto.Code))
            {
                filter = filter.And(p => p.Code.Contains(dto.Code));
            }
            if (!string.IsNullOrEmpty(dto.ProductName))
            {
                filter = filter.And(p => p.ProductName.Contains(dto.ProductName));
            }

            var pageResult = await this.productInfoRepository.QueryPageAsync(filter, p => p.Id, dto.PageSize, dto.PageIndex);
            var list = pageResult.List.MapTo<List<ProductInfoItem>>();

            var data = new
            {
                pageResult.PageIndex,
                pageResult.PageSize,
                pageResult.RecordCount,
                pageResult.PageCount,
                List = list
            };

            result.Data = data;
            return result;

        }

        /// <summary>
        /// 添加或修改产品信息
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public async Task<ResponseResult> AddOrUpdateProductInfo(JsonDto jsonStr)
        {
            var dto = JsonHelper.JsonStr2Obj<ProductInfoDto>(jsonStr.JsonStr);

            var result = new ResponseResult(0, "保存成功");

            try
            {
                if (string.IsNullOrWhiteSpace(dto.Id))
                {
                    var model = dto.MapTo<ProductInfo>();
                    model.Id = new IdWorker(1, 1).NextId();
                    await this.productInfoRepository.InsertAsync(model);
                }
                else
                {
                    var productInfo = await this.productInfoRepository.GetByIdAsync(long.Parse(dto.Id));
                    if (productInfo == null)
                    {
                        result.Code = -1;
                        result.Message = "当前用户信息不存在，请刷新后再试";
                        return result;
                    }
                    else
                    {
                        dto.MapTo(productInfo);
                        await this.productInfoRepository.UpdateAsync(productInfo);
                    }
                }

                this.productInfoRepository.SaveChanged();
                // this.unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Code = -1;
                result.Message = ex.Message;
                LogLock.WriteLine("SystemLog", new[] { ex.ToString() });
            }

            return result;
        }

        public ResponseResult GetIdWorker(int count)
        {
            var result = new ResponseResult(0, "保存成功");

            try
            {
                List<string> workId = new List<string>();                
                for (int i = 0; i < count; i++)
                {
                    var workerId = string.Empty;
                    System.Threading.Thread.Sleep(500);
                    workerId = new IdWorker(1, 1).NextId().ToString();
                    //workerId = Guid.NewGuid().ToString();
                    workId.Add(workerId);
                }
                result.Data = workId;
                return result;
            }
            catch (Exception ex)
            {
                result.Code = -1;
                result.Message = ex.Message;
                LogLock.WriteLine("SystemLog", new[] { ex.ToString() });
            }
            return result;
        }
    }
}
