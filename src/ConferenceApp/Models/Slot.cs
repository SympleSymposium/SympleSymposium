using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Models
{
    public class Slot
    {
        public int Id { get; set; }

        public int IdPresentation { get; set; }
        [ForeignKey("IdPresentation")]
        public Presentation Presentation { get; set; }

        public int IdSpeaker { get; set; }
        [ForeignKey("IdSpeaker")]
        public Speaker Speaker { get; set; }

        public int IdRoom { get; set; }
        [ForeignKey("IdRoom")]
        public Room Room { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
