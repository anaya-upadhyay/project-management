using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain;

namespace ProjectManagement.Dal.EfCore.Mapping
{
    public sealed class DonorMap : IEntityTypeConfiguration<DonorAggregate>
    {
        public void Configure(EntityTypeBuilder<DonorAggregate> builder)
        {
            builder.ToTable("Donors");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
            builder.Property(x => x.Name)
                .UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction)
                .IsRequired();
            builder.Ignore(x => x.Projects);
        }
    }
}