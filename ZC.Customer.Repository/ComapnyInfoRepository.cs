using Microsoft.EntityFrameworkCore;
using ZC.Customer.IRepository;
using ZC.Customer.Model.Models;

namespace ZC.Customer.Repository
{
    /// <summary>
    /// 合同信息存储器
    /// </summary>
    public class ComapnyInfoRepository : BaseRepository<Model.Models.CompanyInfo, long>, IComapnyInfoRepository
    {
        public ComapnyInfoRepository(DbContext context) : base(context)
        {
        }
    }
}