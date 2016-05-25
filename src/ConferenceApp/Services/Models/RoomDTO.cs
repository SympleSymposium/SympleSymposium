using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Services.Models
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public int ConferenceId { get; set; }
        public string Name { get; set; }
        public IList<SlotDTO> Slots { get; set; }
    }
}
