using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ConferenceApp.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public IList<Conference> Conferences { get; set; }
        //public IList<Slot> Slots { get; set; } // NO - needs to link to the join table
    }
}
