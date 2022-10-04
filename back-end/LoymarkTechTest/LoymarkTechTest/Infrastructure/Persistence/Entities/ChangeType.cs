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
    public class ChangeType
    {
        public ChangeType()
        {
            Histories = new HashSet<History>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<History> Histories { get; set; }
    }
}
