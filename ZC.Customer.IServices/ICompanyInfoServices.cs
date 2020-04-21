using System.Threading.Tasks;
using ZC.Customer.Dto;
using UMa.Merak.Infrastructure.Dto;
using ZC.Customer.Dto.Company;
using Microsoft.AspNetCore.Http;

namespace ZC.Customer.IServices
{
    /// <summary>
    /// 公司信息业务逻辑接口
    /// </summary>
    public interface ICompanyInfoServices
    {
        /// <summary>
        /// 获取公司分页列表信息
        /// </summary>
        /// <param name="dto">查询dto</param>
        /// <returns></returns>
        Task<ResponseResult> GetCompanyInfoPageResult(CompanyInfoSearchDto dto);

        /// <summary>
        /// 新增或者修改公司信息
        /// </summary>
        /// <param name="jsonStr">Json格式公司信息dto</param>
        /// <returns></returns>
        Task<ResponseResult> AddOrUpdateCompanyInfo(JsonDto jsonStr);

        /// <summary>
        /// 根据Id获取公司基本信息
        /// </summary>
        /// <param name="id">公司Id</param>
        /// <returns></returns>
        Task<ResponseResult> GetCompanyInfoById(string id);

        /// <summary>
        /// 根据Id获取公司附件信息
        /// </summary>
        /// <param name="id">公司Id</param>
        /// <returns></returns>
        Task<ResponseResult> GetFileInfoDataById(string id);
        
        /// <summary>
        /// 根据Id删除取公司基本信息
        /// </summary>
        /// <param name="id">公司Id</param>
        /// <returns></returns>
        Task<ResponseResult> DeleteCompanyInfoById(string id);

        /// <summary>
        /// 根据Id删除取公司附件本信息
        /// </summary>
        /// <param name="id">公司Id</param>
        /// <returns></returns>
        Task<ResponseResult> DeleteUploadFileInfoById(string id);

        /// <summary>
        /// 上传公司附件
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns></returns>
        Task<ResponseResult> uploadCompanyInfoFile(FileUploadsInfoDto dto);

        /// <summary>
        /// 获取客户分类下拉列表
        /// </summary>
        /// <returns></returns>
        ResponseResult GetCustomerTypeCombobox();

        /// <summary>
        /// 获取客户等级下拉列表
        /// </summary>
        /// <returns></returns>
        ResponseResult GetCustomerLevelCombobox();

        /// <summary>
        /// 获取区域下拉列表
        /// </summary>
        /// <returns></returns>
        ResponseResult GetAreaCombobox();

        /// <summary>
        /// 获取省份下拉列表
        /// </summary>
        /// <returns></returns>
        ResponseResult GetProvinceCombobox();

        /// <summary>
        /// 获取总监下拉列表
        /// </summary>
        /// <returns></returns>
        ResponseResult GetDirectorCombobox();

        /// <summary>
        /// 获取业务员下拉列表
        /// </summary>
        /// <returns></returns>
        ResponseResult GetSalesmanCombobox();

        /// <summary>
        /// 获取快递下拉列表
        /// </summary>
        /// <returns></returns>
        ResponseResult GetExpressCombobox();

        /// <summary>
        /// 获取发票下拉列表
        /// </summary>
        /// <returns></returns>
        ResponseResult GetInvoiceCombobox();

        /// <summary>
        /// 获取出库单模板下拉列表
        /// </summary>
        /// <returns></returns>
        ResponseResult GetTemplateCombobox();

        /// <summary>
        /// 获取出库单联数下拉列表
        /// </summary>
        /// <returns></returns>
        ResponseResult GetLinkNumCombobox();

        /// <summary>
        /// 获取质检报告下拉列表
        /// </summary>
        /// <returns></returns>
        ResponseResult GetQTCombobox();

        /// <summary>
        /// 获取冷链记录下拉列表
        /// </summary>
        /// <returns></returns>
        ResponseResult GetColdChainCombobox();

        /// <summary>
        /// 获取特殊产品名称下拉列表
        /// </summary>
        /// <returns></returns>
        ResponseResult GetSpecialProductCombobox();

        /// <summary>
        /// 获取特殊包装下拉列表
        /// </summary>
        /// <returns></returns>
        ResponseResult GetExtraPackingCombobox();

        /// <summary>
        /// 获取账期下拉列表
        /// </summary>
        /// <returns></returns>
        ResponseResult GetAccountPeriodCombobox();
        


    }
}