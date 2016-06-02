using ConferenceApp.Infrastructure;
using ConferenceApp.Models;
using ConferenceApp.Services.Models;
using ConferenceApp.ViewModels;
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

        public IList<RoomDTO> GetRoomList(int conferenceId)
        {
            return (from r in _roomRepo.List(conferenceId)
                    select new RoomDTO
                    {
                        Id = r.Id,
                        Name = r.Name,
                    }
                    ).ToList();
        }

        public void UpdateRoom(int id, RoomDTO room)
        {
            var editedRoom = _roomRepo.GetById(id).FirstOrDefault();

            if (editedRoom == null)
            {
                throw new Exception("Could not find room with id " + id);
            }

            editedRoom.Name = room.Name;
            editedRoom.Id = room.Id;
            //Add Slots ????

            _roomRepo.SaveChanges();
        }

        public IList<RoomDTO> GetRooms()
        {
            return (from r in _roomRepo.List()
                    select new RoomDTO
                    {
                        Id = r.Id,
                        Name = r.Name
                    }).ToList();
        }
        public RoomDTO GetRoom(int id)
        {
            var room = _roomRepo.GetById(id).FirstOrDefault();

            if (room == null)
            {
                throw new Exception("Could not find conference with id " + id);
            }

            var _roomDTO = new RoomDTO();

            _roomDTO.Id = room.Id;
            _roomDTO.Name = room.Name;
            // Slots???

            return _roomDTO;
        }

        public void PostRoom(RoomViewModel room)
        {
            var newRoom = new Room
            {
                Name = room.Name,
                ConferenceId = room.ConferenceId
            };
            _roomRepo.AddRoom(newRoom);
            _roomRepo.SaveChanges();
        }

        public void DeleteConference(int id)
        {
            _roomRepo.Delete(id);
        }
    }
}
