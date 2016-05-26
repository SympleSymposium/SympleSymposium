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
        private AddressRepository _addressRepo;

        public SpeakerService(SpeakerRepository speakerRepo, AddressRepository addressRepo)
        {
            _speakerRepo = speakerRepo;
            _addressRepo = addressRepo;
        }

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
                        CoStreet = (from a in _addressRepo.List()
                                  where a.Id == s.AddressId
                                  select a).FirstOrDefault().Street,
                        CoCity = (from a in _addressRepo.List()
                                where a.Id == s.AddressId
                                select a).FirstOrDefault().City,
                        CoState = (from a in _addressRepo.List()
                                 where a.Id == s.AddressId
                                 select a).FirstOrDefault().State,
                        CoZip = (from a in _addressRepo.List()
                               where a.Id == s.AddressId
                               select a).FirstOrDefault().Zip,
                        Bio = s.Bio
                    }
                    ).ToList();
        }
    }
}
