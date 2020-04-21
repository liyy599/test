using ZC.Customer.Model.Models;

namespace ZC.Customer.IRepository
{
    /// <summary>
    /// 员工信息存储器接口
    /// </summary>
    public interface IStaffInfoRepository : IBaseRepository<Model.Models.StaffInfo, long>
    {
    }
}