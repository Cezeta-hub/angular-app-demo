using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CEZ.LoymarkTechTest.WebAPI.Infrastructure.Persistence.Entities
{
    public class Country
    {
        public Country()
        {
            Users = new HashSet<User>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Value { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
