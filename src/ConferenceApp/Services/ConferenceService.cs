using ConferenceApp.Infrastructure;
using ConferenceApp.Models;
using ConferenceApp.Services.Models;
using ConferenceApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Services {
    public class ConferenceService {
        private ConferenceRepository _confRepo;
        private AddressRepository _addressRepo;
        private UserRepository _userRepo;

        public ConferenceService(ConferenceRepository confRepo, AddressRepository addressRepo, UserRepository userRepo) {
            _confRepo = confRepo;
            _addressRepo = addressRepo;
            _userRepo = userRepo;
        }

        public IList<ConferenceDTO> GetConferenceList(string organizerName) {
            return (from c in _confRepo.List(organizerName)
                    select new ConferenceDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ApplicationUserId = c.ApplicationUserId,
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
                        ImageUrl = c.ImageUrl,
                        StartDate = c.StartDate,
                        EndDate = c.EndDate
                    }
            ).ToList();
        }

        public ConferenceDTO GetConference(int id) {
            var conf = _confRepo.GetById(id).FirstOrDefault();
            var address = _addressRepo.GetById(conf.AddressId).FirstOrDefault();

            if (conf == null) {
                throw new Exception("Could not find conference with id " + id);
            }
            
            var confDTO = new ConferenceDTO();

            confDTO.Id = conf.Id;
            confDTO.Name = conf.Name;
            confDTO.StartDate = conf.StartDate;
            confDTO.EndDate = conf.EndDate;
            confDTO.ImageUrl = conf.ImageUrl;

            if (conf.AddressId != null) {
                confDTO.AddressId = conf.AddressId;
                confDTO.Street = address.Street;
                confDTO.City = address.City;
                confDTO.State = address.State;
                confDTO.Zip = address.Zip;
            }
            
            return confDTO;
        }

        public void PostConference(ConferenceDTO conference, string currentUser)
        {
            var newAddress = new Address()
            {
                Street = conference.Street,
                City = conference.City,
                State = conference.State,
                Zip = conference.Zip
            };

            _addressRepo.add(newAddress);
            _addressRepo.saveChanges();
            
            var newConference = new Conference
            {
                Name = conference.Name,
                AddressId = newAddress.Id,
            //Address = _addressRepo.FindByAddress(conference.Street, conference.City,
            //conference.State, conference.Zip).FirstOrDefault(),
            ApplicationUserId = (from a in _userRepo.List()
                                     where a.UserName == currentUser
                                     select a.Id).FirstOrDefault(),
                ImageUrl = conference.ImageUrl,
                StartDate = conference.StartDate,
                EndDate = conference.EndDate
            };
            _confRepo.AddConference(newConference);
            _confRepo.SaveChanges();
        }


        public void UpdateConference(int id, ConferenceViewModel conference) {
            var editedConf = _confRepo.GetById(id).FirstOrDefault();

            if (editedConf == null) {
                throw new Exception("Could not find conference with id " + id);
            }

            editedConf.Name = conference.Name;
            editedConf.StartDate = conference.StartDate;
            editedConf.EndDate = conference.EndDate;
            editedConf.ImageUrl = conference.ImageUrl;

            //BROCK - VERIFY THIS WORKS WHEN EDITING A CONFERENCE WITH NULL ADDRESSID
            if (editedConf.AddressId != null) {

                editedConf.AddressId = conference.AddressId;

                var editedAddress = _addressRepo.GetById(editedConf.AddressId).FirstOrDefault();
                editedAddress.Street = conference.Street;
                editedAddress.City = conference.City;
                editedAddress.State = conference.State;
                editedAddress.Zip = conference.Zip;

                _addressRepo.saveChanges();
            }
            else {

                var newAddress = new Address() {
                    Street = conference.Street,
                    City = conference.City,
                    State = conference.State,
                    Zip = conference.Zip
                };

                _addressRepo.add(newAddress);
                _addressRepo.saveChanges();

                //VERIFY THIS WORKS
                editedConf.AddressId = newAddress.Id;
            }

            _confRepo.SaveChanges();
        }

        public void DeleteConference(int id)
        {
            _confRepo.Delete(id);
        }

    }

}

