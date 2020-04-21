using Snowflake;
using System;
using System.Collections.Generic;
using System.Linq;
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
using ZC.Customer.Dto.Staff;
using ZC.Customer.IRepository;
using ZC.Customer.IServices;
using ZC.Customer.Model.Enums;
using ZC.Customer.Model.Models;

namespace ZC.Customer.Services
{
    /// <summary>
    /// StaffInfoServices
    /// </summary>
    public class StaffInfoServices : IStaffInfoServices
    {
        /// <summary>
        /// 员工业务逻辑接口
        /// </summary>
        private readonly IStaffInfoRepository staffInfoRepository;

        public StaffInfoServices(IStaffInfoRepository staffInfoRepository)
        {
            this.staffInfoRepository = staffInfoRepository;
        }

        /// <summary>
        /// 添加或修改员工信息
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public async Task<ResponseResult> AddOrUpdateStaffInfo(JsonDto jsonStr)
        {
            var dto = JsonHelper.JsonStr2Obj<StaffInfoDto>(jsonStr.JsonStr);

            var result = new ResponseResult(0, "保存成功");

            try
            {
                if (string.IsNullOrWhiteSpace(dto.Id))
                {
                    var model = dto.MapTo<StaffInfo>();
                    model.Id = new IdWorker(1, 1).NextId();
                    model.CreationTime = DateTime.Now;
                    model.IsDeleted = false;
                    await this.staffInfoRepository.InsertAsync(model);
                }
                else
                {
                    var staffInfo = await this.staffInfoRepository.GetByIdAsync(long.Parse(dto.Id));
                    if (staffInfo == null)
                    {
                        result.Code = -1;
                        result.Message = "当前员工信息不存在，请刷新后再试";
                        return result;
                    }
                    else
                    {
                        dto.MapTo(staffInfo);
                        await this.staffInfoRepository.UpdateAsync(staffInfo);
                    }
                }
                this.staffInfoRepository.SaveChanged();
            }
            catch (Exception ex)
            {
                result.Code = -1;
                result.Message = ex.Message;
                LogLock.WriteLine("SystemLog", new[] { ex.ToString() });
            }

            return result;
        }

        /// <summary>
        /// 删除员工信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ResponseResult> DelStaffInfoById(string Id)
        {
            var result = new ResponseResult(0, "获取成功");
            try
            {
                var staffInfo = await this.staffInfoRepository.GetByIdAsync(long.Parse(Id));
                if (staffInfo == null)
                {
                    result.Code = -1;
                    result.Message = "当前员工信息不存在，请刷新后再试";
                    return result;
                }
                else
                {
                    staffInfo.IsDeleted = true;
                    await this.staffInfoRepository.UpdateAsync(staffInfo);
                }
                this.staffInfoRepository.SaveChanged();
            }
            catch (Exception ex)
            {
                result.Code = -1;
                result.Message = ex.Message;
                LogLock.WriteLine("SystemLog", new[] { ex.ToString() });
            }
            return result;
        }

        /// <summary>
        /// 获取员工的分页信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ResponseResult> GetStaffInfoPageResult(StaffInfoSearchDto dto)
        {
            var result = new ResponseResult(0, "获取成功");
            Expression<Func<StaffInfo, bool>> filter = p => true;
            filter = filter.And(p => !p.IsDeleted);

            if (!string.IsNullOrEmpty(dto.Name))
            {
                filter = filter.And(p => p.StaffName.Contains(dto.Name));
            }

            var pageResult = await this.staffInfoRepository.QueryPageAsync(filter, p => p.Id, dto.PageSize, dto.PageIndex);
            var list = pageResult.List.MapTo<List<StaffInfoItem>>();

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
        /// 获取员工岗位下拉列表
        /// </summary>
        /// <returns></returns>
        public ResponseResult GetStaffPostCombobox()
        {
            var result = new ResponseResult(0, "获取成功");
            try
            {
                result.Data = EnumHelper.EnumListDictionary(typeof(Post)).Select(m => new ComboboxItemDto()
                {
                    Value = m.Value,
                    DisplayText = m.Key
                }).ToList();
            }
            catch (Exception ex)
            {
                result.Code = -1;
                result.Message = ex.Message;
                result.Data = new List<ComboboxItemDto>();
            }
            return result;
        }
    }
}
