using FluentNHibernate.Mapping;
using ProjectManagement.Domain;

namespace ProjectManagement.Dal.Nhb.Mappings
{
    public sealed class DonorMap : ClassMap<Donor>
    {
        public DonorMap()
        {
            Table("Donors");
            Id(x => x.Id);
            Map(x => x.Name).Not.Nullable();
        }
    }
}