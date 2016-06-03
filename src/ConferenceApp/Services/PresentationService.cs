using ConferenceApp.Infrastructure;
using ConferenceApp.Models;
using ConferenceApp.Services.Models;
using ConferenceApp.ViewModels.Manage;
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

        // Get all the presentations
        public IList<PresentationDTO> GetList()
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

        //Get all the presentations associated with a conference
        public IList<PresentationDTO> GetPresentationList(int conferenceId)
        {
            return (from p in _presentRepo.List(conferenceId)
                    select new PresentationDTO
                    {
                        Id = p.Id,
                        Title = p.Title,
                        ConferenceId = p.ConferenceId,
                        Description = p.Description,
                        ImageUrl = p.ImageUrl
                    }
                    ).ToList();
        }

        // Get a specific presentation
        public PresentationDTO GetPresentation(int id)
        {
            var presentation = this._presentRepo.GetById(id).FirstOrDefault();

            if (presentation == null)
            {
                throw new Exception("Could not find conference with id " + id);
            }

            var _presentRepo = new PresentationDTO();

            _presentRepo.Id = presentation.Id;
            _presentRepo.Title = presentation.Title;
            _presentRepo.ConferenceId = presentation.ConferenceId;
            _presentRepo.Description = presentation.Description;
            _presentRepo.ImageUrl = presentation.ImageUrl;

            return _presentRepo;
        }

        // Add a new presentation
        public void PostPresentation(PresentationViewModel presentation)
        {
            var newPresentation = new Presentation
            {
                Title = presentation.Title,
                ConferenceId = presentation.ConferenceId,
                Description = presentation.Description,
                ImageUrl = presentation.ImageUrl
            };
            _presentRepo.AddPresentation(newPresentation);
            _presentRepo.SaveChanges();
        }

        // This is an Edit of a specific presentation
        public void UpdatePresentation(int id, PresentationDTO presentation)
        {
            var editedPresentation = _presentRepo.GetById(id).FirstOrDefault();

            if (editedPresentation == null)
            {
                throw new Exception("Could not find presentation with id " + id);
            }
            editedPresentation.Title = presentation.Title;
            editedPresentation.ConferenceId = presentation.ConferenceId;
            editedPresentation.Description = presentation.Description;
            editedPresentation.ImageUrl = presentation.ImageUrl;

            _presentRepo.SaveChanges();
        }
    }
}
