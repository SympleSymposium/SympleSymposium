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

        public IQueryable<Room> List(int conferenceId)
        {
            return (from r in _db.Rooms
                    where r.ConferenceId == conferenceId
                    select r);
        }
    }
}
