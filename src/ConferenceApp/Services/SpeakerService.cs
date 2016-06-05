using ConferenceApp.Infrastructure;
using ConferenceApp.Models;
using ConferenceApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Services
{
    public class SpeakerService
    {

        //This service has been refactored - copy same logic to RoomService

        private SpeakerRepository _speakerRepo;
        private AddressRepository _addressRepo;
        private SlotRepository _slotRepo;

        public SpeakerService(SpeakerRepository speakerRepo, AddressRepository addressRepo, SlotRepository slotRepo)
        {
            _speakerRepo = speakerRepo;
            _addressRepo = addressRepo;
            _slotRepo = slotRepo;
        }

        // Get a specific speaker
        public SpeakerDTO GetSpeaker(int speakerId)
        {
            var speaker = (from s in _speakerRepo.GetById(speakerId)
                           select new SpeakerDTO()
                           {
                               FirstName = s.FirstName,
                               LastName = s.LastName,
                               Title = s.Title,
                               Phone = s.Phone,
                               Email = s.Email,
                               Company = s.Company,
                               CoStreet = s.Address.Street,
                               CoCity = s.Address.City,
                               CoState = s.Address.State,
                               CoZip = s.Address.Zip,
                               Bio = s.Bio,
                               ImageUrl = s.ImageUrl,
                               ConferenceId = s.ConferenceId,
                               Id = speakerId
                           }
                           ).FirstOrDefault();

            if (speaker == null)
            {
                throw new Exception("Could not find speaker with id " + speakerId);
            }

            return speaker;
        }

        //Get all the speakers associated with a conference
        public IList<SpeakerDTO> GetSpeakerList(int conferenceId)
        {
            return (from s in _speakerRepo.List(conferenceId)
                    select new SpeakerDTO
                    {
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        Title = s.Title,
                        Phone = s.Phone,
                        Email = s.Email,
                        Company = s.Company,
                        CoStreet = s.Address.Street,
                        CoCity = s.Address.City,
                        CoState = s.Address.State,
                        CoZip = s.Address.Zip,
                        Bio = s.Bio,
                        ImageUrl = s.ImageUrl,
                        Id = s.Id
                    }
                    ).ToList();
        }

        // This is an Edit of a specific room
        public void UpdateSpeaker(int speakerId, SpeakerDTO speaker)
        {
            var editedSpeaker = _speakerRepo.GetById(speakerId).FirstOrDefault();
            

            if (editedSpeaker == null)
            {
                throw new Exception("Could not find speaker with id " + speakerId);
            }

            editedSpeaker.FirstName = speaker.FirstName;
            editedSpeaker.LastName = speaker.LastName;
            editedSpeaker.Title = speaker.Title;
            editedSpeaker.Phone = speaker.Phone;
            editedSpeaker.Email = speaker.Email;
            editedSpeaker.Company = speaker.Company;
            editedSpeaker.Bio = speaker.Bio;
            editedSpeaker.ImageUrl = speaker.ImageUrl;
            editedSpeaker.ConferenceId = speaker.ConferenceId;

            //BROCK - VERIFY THIS WORKS WHEN EDITING A CONFERENCE WITH NULL ADDRESSID
            if (speaker.AddressId != null)
            {

                editedSpeaker.AddressId = speaker.AddressId;
                //var editedAddress = _addressRepo.GetById(speakerId).FirstOrDefault();

                var editedAddress = _addressRepo.GetById(editedSpeaker.AddressId).FirstOrDefault();
                editedAddress.Street = speaker.CoStreet;
                editedAddress.City = speaker.CoCity;
                editedAddress.State = speaker.CoState;
                editedAddress.Zip = speaker.CoZip;                

                _addressRepo.saveChanges();
            }
            else
            {

                var newAddress = new Address()
                {
                    Street = speaker.CoStreet,
                    City = speaker.CoCity,
                    State = speaker.CoState,
                    Zip = speaker.CoZip
                };

                _addressRepo.add(newAddress);
                _addressRepo.saveChanges();

                //VERIFY THIS WORKS
                editedSpeaker.AddressId = newAddress.Id;
            }
            
            _speakerRepo.SaveChanges();
           

        }

        // Add a new speaker
        public void PostSpeaker(SpeakerDTO speaker)
        {

            var newAddress = new Address()
            {
                Street = speaker.CoStreet,
                City = speaker.CoCity,
                State = speaker.CoState,
                Zip = speaker.CoZip
            };
            _addressRepo.add(newAddress);
            _addressRepo.saveChanges();

            var addedSpeaker = new Speaker()
            {
                FirstName = speaker.FirstName,
                LastName = speaker.LastName,
                AddressId = newAddress.Id,
                Title = speaker.Title,
                Phone = speaker.Phone,
                Email = speaker.Email,
                Company = speaker.Company,
                Bio = speaker.Bio,
                ImageUrl = speaker.ImageUrl,
                ConferenceId = speaker.ConferenceId
                //Id = speaker.Id
            };

            _speakerRepo.AddSpeaker(addedSpeaker);
            _speakerRepo.SaveChanges();
        }

        public void DeleteSpeaker(int speakerId)
        {
            var deletedSpeaker = (from s in _speakerRepo.GetById(speakerId)
                                  select s).FirstOrDefault();

            var relatedSlots = (from s in _slotRepo.List(deletedSpeaker.ConferenceId)
                                select s).ToList();

            _slotRepo.DeleteSlotsSpeakerRelated(relatedSlots);
            _speakerRepo.Delete(deletedSpeaker);
            _speakerRepo.SaveChanges();
        }
    }
}
