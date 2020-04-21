using Microsoft.EntityFrameworkCore;
using ZC.Customer.IRepository;
using ZC.Customer.Model.Models;

namespace ZC.Customer.Repository
{
    /// <summary>
    /// 员工信息存储器
    /// </summary>
    public class StaffInfoRepository : BaseRepository<Model.Models.StaffInfo, long>, IStaffInfoRepository
    {
        public StaffInfoRepository(DbContext context) : base(context)
        {
        }
    }
}