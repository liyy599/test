using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UMa.Merak.Infrastructure.Dto;
using ZC.Customer.Dto;
using ZC.Customer.Dto.Company;
using ZC.Customer.IServices;

namespace ZC.Customer.Controllers
{

    /// 公司信息业务控制器
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompanyInfoController:ControllerBase
    {
        /// <summary>
        /// 公司信息业务逻辑接口
        /// </summary>
        private readonly ICompanyInfoServices companyInfoServices;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="companyInfoServices"></param>
        public CompanyInfoController(ICompanyInfoServices companyInfoServices)
        {
            this.companyInfoServices = companyInfoServices;
        }

        /// <summary>
        /// 获取公司分页列表信息
        /// </summary>
        /// <param name="dto">查询dto</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseResult> GetCompanyInfoPageResult(CompanyInfoSearchDto dto)
        {
            return await this.companyInfoServices.GetCompanyInfoPageResult(dto);
        }

        /// <summary>
        /// 新增或者修改公司信息
        /// </summary>
        /// <param name="jsonStr">Json格式公司信息str</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseResult> AddOrUpdateCompanyInfo(JsonDto jsonStr)
        {
            return await this.companyInfoServices.AddOrUpdateCompanyInfo(jsonStr);
        }

        /// <summary>
        /// 根据Id获取公司基本信息
        /// </summary>
        /// <param name="id">公司Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseResult> GetCompanyInfoById(string id) {
            return await this.companyInfoServices.GetCompanyInfoById(id);
        }

        /// <summary>
        /// 根据Id获取公司附件信息
        /// </summary>
        /// <param name="id">公司Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseResult> GetFileInfoDataById(string id)
        {
            return await this.companyInfoServices.GetFileInfoDataById(id);
        }

        /// <summary>
        /// 根据Id删除公司基本信息
        /// </summary>
        /// <param name="id">公司Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseResult> DeleteCompanyInfoById(string id)
        {
            return await this.companyInfoServices.DeleteCompanyInfoById(id);
        }

        /// <summary>
        /// 根据Id删除公司附件本信息
        /// </summary>
        /// <param name="id">附件Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseResult> DeleteUploadFileInfoById(string id)
        {
            return await this.companyInfoServices.DeleteUploadFileInfoById(id);
        }

        /// <summary>
        /// 上传公司附件
        /// DisableRequestSizeLimit 不限制文件大小
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<ResponseResult> uploadCompanyInfoFile()
        {
            FileUploadsInfoDto dto = new FileUploadsInfoDto();
            dto.Id = Request.Form["Id"];
            dto.File = Request.Form.Files.FirstOrDefault();
            return await this.companyInfoServices.uploadCompanyInfoFile(dto);
        }

        /// <summary>
        /// 获取客户分类下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult GetCustomerTypeCombobox() {
            var result = this.companyInfoServices.GetCustomerTypeCombobox();

            return result;
        }

        /// <summary>
        /// 获取客户等级下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult GetCustomerLevelCombobox()
        {
            var result = this.companyInfoServices.GetCustomerLevelCombobox();

            return result;
        }
        /// <summary>
        /// 获取区域下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult GetAreaCombobox()
        {
            var result = this.companyInfoServices.GetAreaCombobox();

            return result;
        }

        /// <summary>
        /// 获取省份下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult GetProvinceCombobox()
        {
            var result = this.companyInfoServices.GetProvinceCombobox();

            return result;
        }

        /// <summary>
        /// 获取总监下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult GetDirectorCombobox()
        {
            var result = this.companyInfoServices.GetDirectorCombobox();

            return result;
        }

        /// <summary>
        /// 获取业务员下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult GetSalesmanCombobox()
        {
            var result = this.companyInfoServices.GetSalesmanCombobox();

            return result;
        }

        /// <summary>
        /// 获取快递下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult GetExpressCombobox()
        {
            var result = this.companyInfoServices.GetExpressCombobox();

            return result;
        }

        /// <summary>
        /// 获取发票下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult GetInvoiceCombobox()
        {
            var result = this.companyInfoServices.GetInvoiceCombobox();

            return result;
        }

        /// <summary>
        /// 获取出库单模板下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult GetTemplateCombobox()
        {
            var result = this.companyInfoServices.GetTemplateCombobox();

            return result;
        }

        /// <summary>
        /// 获取出库单联数下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult GetLinkNumCombobox()
        {
            var result = this.companyInfoServices.GetLinkNumCombobox();

            return result;
        }

        /// <summary>
        /// 获取质检报告下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult GetQTCombobox()
        {
            var result = this.companyInfoServices.GetQTCombobox();

            return result;
        }

        /// <summary>
        /// 获取冷链记录下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult GetColdChainCombobox()
        {
            var result = this.companyInfoServices.GetColdChainCombobox();

            return result;
        }

        /// <summary>
        /// 获取特殊产品名称下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult GetSpecialProductCombobox()
        {
            var result = this.companyInfoServices.GetSpecialProductCombobox();

            return result;
        }

        /// <summary>
        /// 获取特殊包装下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult GetExtraPackingCombobox()
        {
            var result = this.companyInfoServices.GetExtraPackingCombobox();

            return result;
        }

        /// <summary>
        /// 获取账期下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult GetAccountPeriodCombobox()
        {
            var result = this.companyInfoServices.GetAccountPeriodCombobox();

            return result;
        }
    }

}
