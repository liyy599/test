using ZC.Customer.Model.Models;

namespace ZC.Customer.IRepository
{
    /// <summary>
    /// 公司信息存储器接口
    /// </summary>
    public interface IComapnyInfoRepository : IBaseRepository<Model.Models.CompanyInfo, long>
    {
    }
}