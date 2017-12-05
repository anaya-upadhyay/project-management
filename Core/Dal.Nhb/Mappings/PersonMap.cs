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
        }
    }
}