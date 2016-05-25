using ConferenceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Infrastructure
{
    public class SpeakerRepository
    {
        private ApplicationDbContext _db;
        public SpeakerRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<Speaker> List()
        {
            return _db.Speakers;
        }
    }
}
