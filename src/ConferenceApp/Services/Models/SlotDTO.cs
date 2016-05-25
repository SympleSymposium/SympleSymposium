﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Services.Models
{
    public class SlotDTO
    {
        public int Id { get; set; }
        public int PresentationId { get; set; }
        public int SpeakerId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public PresentationDTO Presentation { get; set; }
    }
}
