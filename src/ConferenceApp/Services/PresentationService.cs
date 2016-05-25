using ConferenceApp.Infrastructure;
using ConferenceApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Services
{
    public class PresentationService
    {
        private PresentationRepository _presentRepo;
        public PresentationService(PresentationRepository presentRepo)
        {
            _presentRepo = presentRepo;
        }
        
        public IList<PresentationDTO> GetPresentationList()
        {
            return (from p in _presentRepo.List()
                    select new PresentationDTO
                    {
                        Id = p.Id,
                        ConferenceId = p.ConferenceId,
                        Title = p.Title,
                        Description = p.Description,                        
                        StartTime = p.StartTime,
                        EndTime = p.EndTime
                    }
                    ).ToList();
        }
    }
}
