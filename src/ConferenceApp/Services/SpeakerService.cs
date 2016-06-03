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
        private SpeakerRepository _speakerRepo;
        private AddressRepository _addressRepo;

        public SpeakerService(SpeakerRepository speakerRepo, AddressRepository addressRepo)
        {
            _speakerRepo = speakerRepo;
            _addressRepo = addressRepo;
        }

        // Get all Speakers
        public IList<SpeakerDTO> GetSpeakers()
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
                        Bio = s.Bio,
                        ImageUrl = s.ImageUrl
                    }
                    ).ToList();
        }


        // Get a specific speaker
        public SpeakerDTO GetSpeaker(int id)
        {
            var speaker = _speakerRepo.GetById(id).FirstOrDefault();

            if (speaker == null)
            {
                throw new Exception("Could not find speaker with id " + id);
            }

            var _speakerDTO = new SpeakerDTO();


            _speakerDTO.FirstName = speaker.FirstName;
            _speakerDTO.LastName = speaker.LastName;
            _speakerDTO.Title = speaker.Title;
            _speakerDTO.Phone = speaker.Phone;
            _speakerDTO.Email = speaker.Email;
            _speakerDTO.Company = speaker.Company;
            //_speakerDTO.CoStreet = speaker.CoStreet;
            //_speakerDTO.CoCity = speaker.CoCity;
            //_speakerDTO.CoState = speaker.CoState;
            //_speakerDTO.CoZip = speaker.CoZip;
            _speakerDTO.Bio = speaker.Bio;
            _speakerDTO.ImageUrl = speaker.ImageUrl;


            return _speakerDTO;
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
                        //CoStreet = s.CoStreet,
                        //CoCity = s.CoCity,
                        //CoState = s.CoState,
                        //CoZip = s.CoZip,
                        Bio = s.Bio,
                        ImageUrl = s.ImageUrl
                    }
                    ).ToList();
        }

        // This is an Edit of a specific room
        public void UpdateSpeaker(int id, SpeakerDTO speaker)
        {
            var editedSpeaker = _speakerRepo.GetById(id).FirstOrDefault();

            if (editedSpeaker == null)
            {
                throw new Exception("Could not find speaker with id " + id);
            }

            editedSpeaker.FirstName = speaker.FirstName;
            editedSpeaker.LastName = speaker.LastName;
            editedSpeaker.Title = speaker.Title;
            editedSpeaker.Phone = speaker.Phone;
            editedSpeaker.Email = speaker.Email;
            editedSpeaker.Company = speaker.Company;
            //editedSpeaker.CoStreet = speaker.CoStreet;
            //editedSpeaker.CoCity = speaker.CoCity;
            //editedSpeaker.CoState = speaker.CoState;
            //editedSpeaker.CoZip = speaker.CoZip;
            editedSpeaker.Bio = speaker.Bio;
            editedSpeaker.ImageUrl = speaker.ImageUrl;

            //_speakerRepo.SaveChanges();
        }

        // Add a new room
        public void PostRoom(SpeakerDTO speaker)
        {
            var newSpeaker = new Speaker
            {
                FirstName = speaker.FirstName,
            LastName = speaker.LastName,
            Title = speaker.Title,
            Phone = speaker.Phone,
            Email = speaker.Email,
            Company = speaker.Company,
            //CoStreet = speaker.CoStreet,
            //CoCity = speaker.CoCity,
            //CoState = speaker.CoState,
            //CoZip = speaker.CoZip,
            Bio = speaker.Bio,
            ImageUrl = speaker.ImageUrl

        };
            //_speakerRepo.AddSpeaker(newSpeaker);
            //_speakerRepo.SaveChanges();
        }

    public void DeleteRoom(int speakerId)
    {
            //_speakerRepo.DeleteSlotsSpeakerRelated(speakerId);
            //_speakerRepo.Delete(speakerId);
    }
}
}
