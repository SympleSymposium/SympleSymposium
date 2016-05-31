using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ConferenceApp.Services.Models;
using ConferenceApp.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ConferenceApp.Controllers
{

    // api/rooms
    [Route("api/[controller]")]
    public class RoomsController : Controller
    {
        private RoomService _roomServ;
        public RoomsController(RoomService roomServ)
        {
            _roomServ = roomServ;
        }

        // Get rooms for specific conference
        // GET: api/slots/2
        [HttpGet("{conferenceId}")]
        public IEnumerable<RoomDTO> GetRoomList(int conferenceId)
        {
            return _roomServ.GetRoomList(conferenceId);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
