using ConferenceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Infrastructure
{
    public class PresentationRepository
    {
        private ApplicationDbContext _db;
        public PresentationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<Presentation> List()
        {
            return _db.Presentations;
        }
    }
}
