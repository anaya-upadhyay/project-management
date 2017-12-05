using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Domain
{
    public sealed class Analyst : Person
    {
        /// <summary>
        ///     For OR/M usage
        /// </summary>
        private Analyst()
        {
            
        }

        /// <inheritdoc />
        public Analyst(string firstName, string lastName) 
            : base(firstName, lastName)
        {

        }
    }
}
