using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Services.Models
{
    public class PresentationDTO
    {
        public int Id { get; set; }
        public int IdConference { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public IList<SlotDTO> Slots { get; set; }
    }
}
