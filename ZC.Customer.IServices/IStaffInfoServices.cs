using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UMa.Merak.Infrastructure.Dto;
using ZC.Customer.Dto;
using ZC.Customer.Dto.Staff;

namespace ZC.Customer.IServices
{
    /// <summary>
    /// 工作人员接口
    /// </summary>
    public interface IStaffInfoServices
    {

        /// <summary>
        /// 获取工作人员分页列表信息
        /// </summary>
        /// <param name="dto">查询dto</param>
        /// <returns></returns>
        Task<ResponseResult> GetStaffInfoPageResult(StaffInfoSearchDto dto);

        /// <summary>
        /// 添加或修改员工信息
        /// </summary>
        /// <param name="jsonStr">Json格式员工信息dto</param>
        /// <returns></returns>
        Task<ResponseResult> AddOrUpdateStaffInfo(JsonDto jsonStr);

        /// <summary>
        /// 根据Id删除员工信息
        /// </summary>
        /// <param name="id">长者Id</param>
        /// <returns></returns>
        Task<ResponseResult> DelStaffInfoById(string id);

        /// <summary>
        /// 获取员工岗位下拉列表
        /// </summary>
        /// <returns></returns>
        ResponseResult GetStaffPostCombobox();
    }
}
