using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ConferenceApp.Services.Models;
using ConferenceApp.Services;
using ConferenceApp.ViewModels;
using Microsoft.AspNet.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ConferenceApp.Controllers
{
    // api/slots
    [Route("api/[controller]")]
    public class SlotsController : Controller
    {
        private SlotService _slotServ;
        public SlotsController(SlotService slotServ)
        {
            _slotServ = slotServ;
        }

        // Get slots for specific conference
        // GET: api/slots/2
        [HttpGet("{conferenceId}")]
        public IEnumerable<SlotDTO> GetSlotList(int conferenceId)
        {
            return _slotServ.GetSlotList(conferenceId);
        }

        // Get slot for specific conference
        // GET: api/slots/edit/2
        [HttpGet("edit/{id}")]
        public SlotDTO GetSlot(int id)
        {
            return _slotServ.GetSlot(id);
        }

        // Add slot
        // POST api/slots
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]SlotViewModel slot)
        {

            if (ModelState.IsValid)
            {
                _slotServ.AddSlot(slot);
                return Ok(slot);
            }
            return HttpBadRequest(ModelState);
        }

        // Edit slot
        // POST api/slots/5
        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody]SlotViewModel slot)
        {
            if (slot == null)
            {
                throw new Exception("Could not find slot with id " + id);
            }

            //Wendy - NEED TO MAKE SURE Admins can add new rooms
            _slotServ.UpdateSlot(id, slot);

            return Ok();
        }
        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // Delete slot
        // DELETE api/slots/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _slotServ.DeleteSlot(id);
        }
    }
}
