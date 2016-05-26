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
        private AddressRepository _addressRepo;

        public ConferenceService(ConferenceRepository confRepo, AddressRepository addressRepo)
        {
            _confRepo = confRepo;
            _addressRepo = addressRepo;
        }

        public IList<ConferenceDTO> GetConferenceList(string organizerName)
        {
            return (from c in _confRepo.List( organizerName)
                    select new ConferenceDTO {
                        Id = c.Id,
                        Name = c.Name,
                        Street = (from a in _addressRepo.List()
                                  where a.Id == c.AddressId
                                  select a).FirstOrDefault().Street,
                        City = (from a in _addressRepo.List()
                                where a.Id == c.AddressId
                                select a).FirstOrDefault().City,
                        State = (from a in _addressRepo.List()
                                 where a.Id == c.AddressId
                                 select a).FirstOrDefault().State,
                        Zip = (from a in _addressRepo.List()
                               where a.Id == c.AddressId
                               select a).FirstOrDefault().Zip,
                        StartDate = c.StartDate,
                        EndDate = c.EndDate
                    }                        
            ).ToList();

        }

       


        }

    }

