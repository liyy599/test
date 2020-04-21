using Microsoft.EntityFrameworkCore;
using ZC.Customer.IRepository;
using ZC.Customer.Model.Models;

namespace ZC.Customer.Repository
{
    /// <summary>
    /// 文件上传信息存储器
    /// </summary>
    public class FileUploadsInfoInfoRepository : BaseRepository<Model.Models.FileUploadsInfo, long>, IFileUploadsInfoRepository
    {
        public FileUploadsInfoInfoRepository(DbContext context) : base(context)
        {
        }
    }
}