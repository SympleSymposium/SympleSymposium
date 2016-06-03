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
        private SlotRepository _slotRepo;

        public RoomService(RoomRepository roomRepo, SlotRepository slotRepo)
        {
            _roomRepo = roomRepo;
            _slotRepo = slotRepo;
        }

        //Get all rooms
        public IList<RoomDTO> GetRooms()
        {
            var rooms = (from r in _roomRepo.List()
                    select new RoomDTO {
                        Id = r.Id,
                        Name = r.Name,
                        ConferenceId = r.ConferenceId
                    }).ToList();

            return rooms;
        }

        // Get a specific room
        public RoomDTO GetRoom(int id)
        {
            var room = (from r in _roomRepo.GetById(id)
                         select new RoomDTO {
                             Id = r.Id,
                             Name = r.Name,
                             ConferenceId = r.ConferenceId
                         }).FirstOrDefault();

            if (room == null)
            {
                throw new Exception("Could not find conference with id " + id);
            }

            return room;
        }


        //Get all the rooms associated with a conference
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

        // This is an Edit of a specific room
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

        
        // Add a new room
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

        public void DeleteRoom(int roomId)
        {
            _slotRepo.DeleteSlotsRoomRelated(roomId);
            _roomRepo.Delete(roomId);
        }
    }
}
