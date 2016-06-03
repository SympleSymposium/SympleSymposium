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

        public IQueryable<Presentation> List(int conferenceId)
        {
            return (from p in _db.Presentations
                    where p.ConferenceId == conferenceId
                    select p);
        }

        public IQueryable<Presentation> GetById(int id)
        {
            return (from p in _db.Presentations
                    where p.Id == id
                    select p);
        }

        public void AddPresentation(Presentation presentation)
        {
            _db.Presentations.Add(presentation);

        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
