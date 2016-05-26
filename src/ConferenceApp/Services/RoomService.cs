using ConferenceApp.Infrastructure;
using ConferenceApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Services
{
    public class RoomService
    {
        private RoomRepository _roomRepo;

        public RoomService(RoomRepository roomRepo)
        {
            _roomRepo = roomRepo;
        }


        public IList<RoomDTO> GetRoomList()
        {
            return (from r in _roomRepo.List()
                    select new RoomDTO
                    {
                        Id = r.Id,
                        Name = r.Name,
                    }
                    ).ToList();
        }
    }
}
