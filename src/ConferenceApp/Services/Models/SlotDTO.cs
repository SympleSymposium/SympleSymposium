using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Services.Models
{
    public class SlotDTO
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public RoomDTO Room { get; set; }
        public SpeakerDTO Speaker { get; set; }               
        public PresentationDTO Presentation { get; set; }
    }
}
