using FluentNHibernate.Mapping;
using ProjectManagement.Domain;

namespace ProjectManagement.Dal.Nhb.Mappings
{
    public sealed class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Table("Persons");
            Id(x => x.Id);
            Map(x => x.FirstName).Not.Nullable();
            Map(x => x.LastName).Not.Nullable();

            DiscriminateSubClassesOnColumn("ClassType").Not.Nullable();
        }
    }

    public sealed class AnalystMap : SubclassMap<Analyst>
    {
        public AnalystMap()
        {
            DiscriminatorValue("Analyst");
            Map(x => x.Acronym);
            //Map(x => x.Display).ReadOnly();
        }
    }

    public sealed class ConsultantMap : SubclassMap<Consultant>
    {
        public ConsultantMap()
        {
            DiscriminatorValue("Consultant");
            //Map(x => x.Display).ReadOnly();
        }
    }
}