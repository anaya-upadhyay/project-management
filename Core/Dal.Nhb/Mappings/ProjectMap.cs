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

            // properties
            Map(x => x.IsDeleted).Not.Nullable();
            Map(x => x.Acronym).Not.Nullable();
            Map(x => x.ProjectType).Not.Nullable();
            Map(x => x.TenderProcessType).Not.Nullable();
            Map(x => x.ExpectedStartDate).Not.Nullable();

            // soft delete
            Where("IsDeleted = 0");

            // relationships
            References(x => x.Donor)
                .Not.Nullable()
                .Column("DonorId")
                .LazyLoad(Laziness.NoProxy)
                .Cascade.None();
            References(x => x.Analyst)
                .Not.Nullable()
                .Column("AnalystId")
                .LazyLoad(Laziness.NoProxy)
                .Cascade.None();
        }
    }
}