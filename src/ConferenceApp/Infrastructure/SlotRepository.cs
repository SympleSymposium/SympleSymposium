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

        public IQueryable<Slot> List()
        {
            return _db.Slots;
        }
    }
}
