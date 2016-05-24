using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Models
{
    public class Speaker
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Company { get; set; }

        public string CoStreet { get; set; }

        public string CoCity { get; set; }

        public string CoState { get; set; }

        public string CoZip { get; set; }

        public string Bio { get; set; }

        public IList<Slot> Slots { get; set; }
    }
}
