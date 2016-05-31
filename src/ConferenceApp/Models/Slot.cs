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

        public int PresentationId { get; set; }
        [ForeignKey("PresentationId")]
        public Presentation Presentation { get; set; }

        public int SpeakerId { get; set; }
        [ForeignKey("SpeakerId")]
        public Speaker Speaker { get; set; }

        public int? RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Room Room { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
