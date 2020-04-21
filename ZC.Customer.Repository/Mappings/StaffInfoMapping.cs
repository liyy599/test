using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZC.Customer.Model.Models;

namespace ZC.Customer.Repository.Mappings
{
    public class StaffInfoMapping : IEntityTypeConfiguration<Model.Models.StaffInfo>
    {
        public void Configure(EntityTypeBuilder<Model.Models.StaffInfo> builder)
        {
            builder.ToTable("StaffInfo");
        }
    }
}
