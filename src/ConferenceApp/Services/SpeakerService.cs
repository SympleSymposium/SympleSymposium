using ConferenceApp.Infrastructure;
using ConferenceApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Services
{
    public class SpeakerService
    {
        private SpeakerRepository _speakerRepo;

        public SpeakerService(SpeakerRepository speakerRepo)
        {
            _speakerRepo = speakerRepo;
        }


        //public int Id { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Title { get; set; }
        //public string Phone { get; set; }
        //public string Email { get; set; }
        //public string Company { get; set; }
        //public string CoStreet { get; set; }
        //public string CoCity { get; set; }
        //public string CoState { get; set; }
        //public string CoZip { get; set; }
        //public string Bio { get; set; }

        public IList<SpeakerDTO> GetSpeakerList()
        {
            return (from s in _speakerRepo.List()
                    select new SpeakerDTO
                    {
                        Id = s.Id,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        Title = s.Title,
                        Phone = s.Phone,
                        Email = s.Email,
                        Company = s.Company,
                        CoStreet = s.CoStreet,
                        CoCity = s.CoCity,
                        CoState = s.CoState,
                        CoZip = s.CoZip,
                        Bio = s.Bio
                    }
                    ).ToList();
        }
    }
}
