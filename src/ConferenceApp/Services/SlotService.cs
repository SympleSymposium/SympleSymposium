using ConferenceApp.Infrastructure;
using ConferenceApp.Models;
using ConferenceApp.Services.Models;
using ConferenceApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Services
{
    public class SlotService
    {
        private SlotRepository _slotRepo;

        public SlotService(SlotRepository slotRepo)
        {
            _slotRepo = slotRepo;
        }

        // Get a specific slot
        public SlotDTO GetSlot(int slotId)
        {
            var slot = (from s in _slotRepo.GetById(slotId)
                        select new SlotDTO()
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
                            Presentation = new PresentationDTO()
                            {
                                Id = s.PresentationId,
                                ConferenceId = s.Room.ConferenceId,
                                Title = s.Presentation.Title,
                                Description = s.Presentation.Description
                            },
                            Speaker = new SpeakerDTO()
                            {
                                FirstName = s.Speaker.FirstName,
                                LastName = s.Speaker.LastName,
                                Title = s.Speaker.Title,
                                Phone = s.Speaker.Phone,
                                Email = s.Speaker.Email,
                                Company = s.Speaker.Company,
                                CoStreet = "",
                                CoCity = "",
                                CoState = "",
                                CoZip = "",
                                Bio = s.Speaker.Bio,
                                ImageUrl = s.Speaker.ImageUrl,
                                Id = s.Speaker.Id

                            }
                        }).FirstOrDefault();

            if (slot == null)
            {
                throw new Exception("Could not find slot with id " + slotId);
            }

            return slot;
        }

        // Get all slots for a specific conference        
        public IList<SlotDTO> GetSlotList(int conferenceId)
        {
            var slots = (from s in _slotRepo.List(conferenceId)
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
                             Presentation = new PresentationDTO()
                             {
                                 Title = s.Presentation.Title,
                                 Description = s.Presentation.Description
                             },
                             Speaker = new SpeakerDTO()
                             {
                                 FirstName = s.Speaker.FirstName,
                                 LastName = s.Speaker.LastName,
                                 Title = s.Speaker.Title,
                                 Phone = s.Speaker.Phone,
                                 Email = s.Speaker.Email,
                                 Company = s.Speaker.Company,
                                 CoStreet = "",
                                 CoCity = "",
                                 CoState = "",
                                 CoZip = "",
                                 Bio = s.Speaker.Bio,
                                 ImageUrl = s.Speaker.ImageUrl

                             }
                         }).ToList();

            return slots;
        }

        // Add a new slot
        public void AddSlot(SlotViewModel slot)
        {
            var newSlot = new Slot
            {
                PresentationId = slot.PresentationId,
                SpeakerId = slot.SpeakerId,
                RoomId = slot.RoomId,
                StartTime = slot.StartTime,
                EndTime = slot.EndTime
            };
            _slotRepo.AddSlot(newSlot);
            _slotRepo.SaveChanges();
        }

        // This is an Edit of a specific slot
        public void UpdateSlot(int slotId, SlotViewModel slot)
        {
            var editedSlot = _slotRepo.GetById(slotId).FirstOrDefault();

            if (editedSlot == null)
            {
                throw new Exception("Could not find slot with id " + slotId);
            }
            editedSlot.PresentationId = slot.PresentationId;
            editedSlot.SpeakerId = slot.SpeakerId;
            editedSlot.RoomId = slot.RoomId;
            editedSlot.StartTime = slot.StartTime;
            editedSlot.EndTime = slot.EndTime;

            _slotRepo.SaveChanges();
        }

        public void DeleteSlot(int slotId)
        {
            var deletedSlot = (from r in _slotRepo.GetById(slotId)
                               select r).FirstOrDefault();

            //var relatedSlots = (from s in _slotRepo.List(deletedRoom.ConferenceId)
            //                    select s).ToList();

            //_slotRepo.DeleteRelatedSlots(relatedSlots);
            _slotRepo.Delete(deletedSlot);
            _slotRepo.SaveChanges();
        }
    }
}
