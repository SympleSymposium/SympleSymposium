using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Models
{
    public class Presentation
    {
        public int Id { get; set; }

        public int IdConference { get; set; }
        [ForeignKey("IdConference")]
        public Conference Conference { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public IList<Slot> Slots { get; set; }
    }
}
