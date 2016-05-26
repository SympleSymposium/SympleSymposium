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

        public IQueryable<Conference> List(string organizerName)
        {
            //User.Identity.Name
            return from c in _db.Conferences
                   where c.ApplicationUser.UserName == organizerName
                   select c;
        }
        
        //add a Conference by the organizer
        public void Add(Conference conference)
        {
            _db.Conferences.Add(conference);
        }
    }
}
