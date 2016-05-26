using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Models
{
    public class Conference
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IList<Presentation> Presentations { get; set; }

        //public IList<Room> Rooms { get; set; }

    }
}
