using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZC.Customer.Model.Models;

namespace ZC.Customer.Repository.Mappings
{
    public class FileUploadsInfoMapping : IEntityTypeConfiguration<Model.Models.FileUploadsInfo>
    {
        public void Configure(EntityTypeBuilder<Model.Models.FileUploadsInfo> builder)
        {
            builder.ToTable("FileUploadsInfo");
        }
    }
}
