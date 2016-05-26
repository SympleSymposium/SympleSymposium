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
                        Title = p.Title,
                        Description = p.Description,
                        ImageUrl = p.ImageUrl,
                        Slots = (from s in p.Slots
                                 select new SlotDTO
                                 {
                                     Id = s.Id,
                                     StartTime = s.StartTime,
                                     EndTime = s.EndTime,
                                     Room = s.Room.Name,
                                     Speaker = new SpeakerDTO
                                     {
                                         FirstName = s.Speaker.FirstName,
                                         LastName = s.Speaker.LastName
                                     }
                                 }).ToList()                        
                    }).ToList();
        }
    }
}
