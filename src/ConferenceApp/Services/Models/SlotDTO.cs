using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Services.Models
{
    public class SlotDTO
    {
        public int Id { get; set; }
        public int IdPresentation { get; set; }
        public int IdSpeaker { get; set; }
        public int IdRoom { get; set; }
    }
}
