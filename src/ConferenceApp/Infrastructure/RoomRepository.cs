using ConferenceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Infrastructure
{
    public class RoomRepository
    {
        private ApplicationDbContext _db;
        public RoomRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<Room> List()
        {
            return _db.Rooms;
        }

        public IQueryable<Room> List(int conferenceId)
        {
            return (from r in _db.Rooms
                    where r.ConferenceId == conferenceId
                    select r);
        }

        public IQueryable<Room> GetById(int id)
        {
            return (from r in _db.Rooms
                    where r.Id == id
                   select r);
        }

        public void AddRoom(Room room)
        {
            _db.Rooms.Add(room);

        }
                
        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public void Delete(int roomId)
        {
            var deleteRoom =
                (from c in _db.Rooms
                 where c.Id == roomId
                 select c).FirstOrDefault();
            _db.Rooms.Remove(deleteRoom);
            _db.SaveChanges();
        }
    }
}
