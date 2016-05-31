using ConferenceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Infrastructure
{
    public class SlotRepository
    {
        private ApplicationDbContext _db;
        public SlotRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<Slot> List(int conferenceId)
        {
            return (from s in _db.Slots
                    where s.Presentation.ConferenceId == conferenceId
                    select s);
        }
    }
}
