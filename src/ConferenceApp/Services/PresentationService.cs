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
        private SlotRepository _slotRepo;

        public PresentationService(PresentationRepository presentRepo, SlotRepository slotRepo)
        {
            _presentRepo = presentRepo;
            _slotRepo = slotRepo;
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
                                     Room = new RoomDTO()
                                     {
                                         Id = s.Room.Id,
                                         Name = s.Room.Name,
                                         ConferenceId = s.Room.ConferenceId
                                     },
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
        //refactored 6/5/16
        public PresentationDTO GetPresentation(int presentationId)
        {
            var presentation = (from p in _presentRepo.GetById(presentationId)
                                select new PresentationDTO()
                                {
                                    Id = presentationId,
                                    Title = p.Title,
                                    ConferenceId = p.ConferenceId,
                                    Description = p.Description,
                                    ImageUrl = p.ImageUrl,
                                }).FirstOrDefault();

            if (presentation == null)
            {
                throw new Exception("Could not find presentation with id " + presentationId);
            }

            return presentation;
        }

        // Add a new presentation
        public void PostPresentation(PresentationViewModel presentation)
        {
            var newPresentation = new Presentation()
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
        public void UpdatePresentation(int presentationId, PresentationDTO presentation)
        {
            var editedPresentation = _presentRepo.GetById(presentationId).FirstOrDefault();

            if (editedPresentation == null)
            {
                throw new Exception("Could not find presentation with id " + presentationId);
            }
            editedPresentation.Title = presentation.Title;
            editedPresentation.ConferenceId = presentation.ConferenceId;
            editedPresentation.Description = presentation.Description;
            editedPresentation.ImageUrl = presentation.ImageUrl;

            _presentRepo.SaveChanges();
        }

        //this method has been refactored 6/5/16
        public void DeletePresentation(int presentationId)
        {
            var deletedPresentation = (from p in _presentRepo.GetById(presentationId)
                                       select p).FirstOrDefault();

            var relatedSlots = (from s in _slotRepo.List(deletedPresentation.ConferenceId)
                                where s.PresentationId == deletedPresentation.Id
                                select s).ToList();

            _slotRepo.DeleteRelatedSlots(relatedSlots);

            _presentRepo.Delete(deletedPresentation);
            _presentRepo.SaveChanges();
        }
    }
}
