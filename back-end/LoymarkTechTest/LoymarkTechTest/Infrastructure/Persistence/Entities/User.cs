using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CEZ.LoymarkTechTest.WebAPI.Infrastructure.Persistence.Entities
{
    public class User
    {
        public User()
        {
            ChangeHistory = new HashSet<History>();
        }
        public int Id { get; set; }

        // Basic Info
        public string Name { get; set; }         // Required   // Only letters & spaces
        public string Surname { get; set; }      // Required   // Only letters & spaces
        public DateTime Birthday { get; set; }   // Required   // YYYY-MM-DD

        // Contact Info
        public string Email { get; set; }        // Required   // Valid Email
        public int? Telephone { get; set; }       // Not-Required  // Only numbers
        public int CountryId { get; set; }      // Required   // Dropdown with countries using their International code
        public virtual Country Country { get; set; }

        // Extra
        public bool WishesToBeContacted { get; set; } // Required

        // Tracking metadata
        public virtual ICollection<History> ChangeHistory { get; set; }
        public bool Active { get; set; }
    }
}
