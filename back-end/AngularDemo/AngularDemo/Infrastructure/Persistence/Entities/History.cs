using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CEZ.AngularDemo.WebAPI.Infrastructure.Persistence.Entities
{
    public class History
    {
        public History() { }
        public int Id { get; set; }

        public int ChangeType { get; set; }
        public string? PrevValue { get; set; }
        public string? CurrValue { get; set; }

        public DateTime ChangeDate { get; set; }
        
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
