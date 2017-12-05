using FluentNHibernate.Mapping;
using ProjectManagement.Domain;

namespace ProjectManagement.Dal.Nhb.Mappings
{
    public class ProjectMap : ClassMap<ProjectAggregate>
    {
        public ProjectMap()
        {
            Table("Projects");
            Id(x => x.Id);
            Map(x => x.Acronym).Not.Nullable();
            Map(x => x.ProjectType);
            References(x => x.Donor)
                .Not.Nullable()
                .Column("DonorId")
                .Cascade.All();
        }
    }
}