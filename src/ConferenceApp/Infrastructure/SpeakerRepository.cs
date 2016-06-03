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

        public IQueryable<Speaker> List(int conferenceId)
        {
            return (from r in _db.Speakers
                    where r.ConferenceId == conferenceId
                    select r);
        }

        public IQueryable<Speaker> GetById(int id)
        {
            return (from r in _db.Speakers
                    where r.Id == id
                    select r);
        }
    }
}
