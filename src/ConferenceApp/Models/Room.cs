using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Models
{
    public class Room
    {
        public int Id { get; set; }

        public int ConferenceId { get; set; }
        [ForeignKey("ConferenceId")]
        public Conference Conference { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public IList<Slot> Slots { get; set; }
    }
}
