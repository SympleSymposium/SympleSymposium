using ConferenceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Infrastructure
{
    public class SlotRepository
    {
        private ApplicationDbContext _db;
        public SlotRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<Slot> List(int conferenceId)
        {
            return (from s in _db.Slots
                    where s.Presentation.ConferenceId == conferenceId
                    select s);
        }

        public void AddSlot(Slot slot) {
            _db.Slots.Add(slot);

        }

        public void SaveChanges() {
            _db.SaveChanges();
        }

        public void DeleteSlotsRoomRelated(int roomId) {
            var deleteSlots =
                (from s in _db.Slots
                 where s.RoomId == roomId
                 select s).ToList();
            _db.Slots.RemoveRange(deleteSlots);
            _db.SaveChanges();
        }

        //REFACTOR like DeleteSlotsSpeakerRelated() below
        public void DeleteSlotsPresentationRelated(int presentationId)
        {
            var deleteSlots =
                (from s in _db.Slots
                 where s.PresentationId == presentationId
                 select s).ToList();
            _db.Slots.RemoveRange(deleteSlots);
            _db.SaveChanges();
        }

        public void DeleteSlotsSpeakerRelated(IList<Slot> relatedSlots)
        {
            _db.Slots.RemoveRange(relatedSlots);
        }
    }
}
