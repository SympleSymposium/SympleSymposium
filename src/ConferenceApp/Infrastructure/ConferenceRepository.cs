using ConferenceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Infrastructure
{
    public class ConferenceRepository
    {
        private ApplicationDbContext _db;
        public ConferenceRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<Conference> List()
        {
            return _db.Conferences;
        }
    }
}
