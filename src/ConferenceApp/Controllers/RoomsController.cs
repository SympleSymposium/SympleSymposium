using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ConferenceApp.Services.Models;
using ConferenceApp.Services;
using Microsoft.AspNet.Authorization;
using ConferenceApp.ViewModels;

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

        // Get all the rooms
        // GET api/rooms
        [HttpGet]
        public IList<RoomDTO> GetRooms()
        {
            return _roomServ.GetRooms();
        }

        // Get rooms for specific conference
        // GET: api/rooms/manage/2
        [HttpGet("manage/{conferenceId}")]
        [Authorize]
        public IEnumerable<RoomDTO> GetRoomList(int conferenceId)
        {
            return _roomServ.GetRoomList(conferenceId);
        }

        // Get specific room
        // GET api/rooms/2
        [HttpGet("{id}")]
        public RoomDTO GetRoom(int id)
        {
            //Wendy - Make sure the Admin can get the room to Edit
            return _roomServ.GetRoom(id);
        }

        // POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        // Add room
        // POST api/rooms
        // This is an Add new room
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]RoomViewModel room)
        {
            if (ModelState.IsValid)
            {
                _roomServ.PostRoom(room);
                return Ok(room);
            }
            return HttpBadRequest(ModelState);
        }

        // POST api/rooms/5
        // This is an Edit of a specific room
        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody]RoomDTO room)
        {
            if (room == null)
            {
                throw new Exception("Could not find room with id " + id);
            }

            //Wendy - NEED TO MAKE SURE Admins can add new rooms
            _roomServ.UpdateRoom(id, room);

            return Ok();
        }

        // PUT api/rooms/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        

        // Delete room
        // DELETE api/rooms/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _roomServ.DeleteRoom(id);
        }
    }
}
