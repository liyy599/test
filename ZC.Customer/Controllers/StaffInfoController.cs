using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UMa.Merak.Infrastructure.Dto;
using ZC.Customer.Dto;
using ZC.Customer.Dto.Staff;
using ZC.Customer.IServices;

namespace ZC.Customer.Controllers
{
    /// <summary>
    /// 员工信息
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class StaffInfoController : ControllerBase
    {
        readonly IStaffInfoServices staffInfoServices;

        /// <summary>
        /// 构造函数
        /// </summary>
        public StaffInfoController(IStaffInfoServices staffInfoServices)
        {
            this.staffInfoServices = staffInfoServices;
        }

        /// <summary>
        /// 添加员工信息
        /// api/StaffInfo/AddOrUpdateStaffInfo
        /// </summary>
        /// <param name="jsonStr"></param>        
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseResult> AddOrUpdateStaffInfo(JsonDto jsonStr)
        {
            var result = await this.staffInfoServices.AddOrUpdateStaffInfo(jsonStr);

            return result;
        }

        /// <summary>
        /// 获取员工分页列表信息
        /// </summary>
        /// <param name="dto">查询dto</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseResult> GetStaffInfoPageResult(StaffInfoSearchDto dto)
        {
            var result = await this.staffInfoServices.GetStaffInfoPageResult(dto);

            return result;
        }

        /// <summary>
        /// 根据Id删除员工信息
        /// </summary>
        /// <param name="Id">员工Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseResult> DelStaffInfoById(string Id)
        {
            var result = await this.staffInfoServices.DelStaffInfoById(Id);

            return result;
        }

        /// <summary>
        /// 获取员工岗位下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult GetStaffPostCombobox()
        {
            var result = this.staffInfoServices.GetStaffPostCombobox();

            return result;
        }
    }
}
