using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Domain
{
    public sealed class Analyst : Person
    {
        public Analyst(string firstName, string lastName) 
            : base(firstName, lastName)
        {

        }
    }
}
