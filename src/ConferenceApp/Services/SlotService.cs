using ConferenceApp.Infrastructure;
using ConferenceApp.Services.Models;
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

        //public int Id { get; set; }
        //public int IdPresentation { get; set; }
        //public int IdSpeaker { get; set; }
        //public int IdRoom { get; set; }

        //    return new ReportDTO() {
        //    ReportCategories = (from e in expenses
        //                        group e by e.CategoryType into c
        //                        select new ReportCategoryDTO()
        //                        {
        //                            Name = c.Key,
        //                            CategoryTotal = c.Sum(exp => exp.Cost.Value), /*Need to use .Value since Cost could be null.*/
        //                            NumExpenses = c.Count()
        //                        }).ToList(),
        //        Expenses = (from e in expenses
        //                    orderby e.ApptDate descending
        //                    select e).ToList(),
        //        GrandTotal = expenses.Sum(exp => exp.Cost.Value)
        //    };

        // public PresentationDTO Presentation { get; set; }
        public IList<SlotDTO> GetSlotList()
        {
            return (from s in _slotRepo.List()
                    select new SlotDTO
                    {
                        Id = s.Id,
                        Presentation = new PresentationDTO()
                        {
                            Title = s.Presentation.Title,
                            Description = s.Presentation.Description,
                        }
                    }
                    ).ToList();
        }
    }
}
