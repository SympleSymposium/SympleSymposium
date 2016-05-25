using ConferenceApp.Infrastructure;
using ConferenceApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Services
{
    public class ConferenceService
    {
        private ConferenceRepository _confRepo;
        public ConferenceService(ConferenceRepository confRepo)
        {
            _confRepo = confRepo;
        }

        //public int Id { get; set; }
        //public string Name { get; set; }
        //public string Street { get; set; }
        //public string City { get; set; }
        //public string Zip { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        //public IList<PresentationDTO> Presentations { get; set; }

        //Presentations = new IList<PresentationDTO>
        //    {
        //        Title = c.Presentations.Title,                
        //        Description = c.Presentations.Description

        public IList<ConferenceDTO> GetConferenceList()
        {
            return (from c in _confRepo.List()
                    select new ConferenceDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Street = c.Street,
                        City = c.City,
                        Zip = c.Zip,
                        StartDate = c.StartDate,
                        EndDate = c.EndDate
                    }                        
            ).ToList();

        }
    }
}
