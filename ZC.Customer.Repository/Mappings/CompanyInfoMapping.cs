using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZC.Customer.Model.Models;

namespace ZC.Customer.Repository.Mappings
{
    public class CompanyInfoMapping : IEntityTypeConfiguration<Model.Models.CompanyInfo>
    {
        public void Configure(EntityTypeBuilder<Model.Models.CompanyInfo> builder)
        {
            builder.ToTable("CompanyInfo");
        }
    }
}
