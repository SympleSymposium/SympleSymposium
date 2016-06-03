using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ConferenceApp.Services;
using ConferenceApp.Services.Models;
using Microsoft.AspNet.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ConferenceApp.Controllers
{

    //api/speakers
    [Route("api/[controller]")]
    public class SpeakersController : Controller
    {
        private SpeakerService _speakerServ;
        public SpeakersController(SpeakerService speakerServ)
        {
            _speakerServ = speakerServ;
        }

        // GET: api/speakers
        // Get all the speakers
        [HttpGet]
        public IEnumerable<SpeakerDTO> GetSpeakers()
        {
            return _speakerServ.GetSpeakers();
        }

        // Get speakers for a specific conference
        // GET: api/speakers/manage/2
        [HttpGet("manage/{conferenceId}")]
        [Authorize]
        public IList<SpeakerDTO> GetSpeakerList(int conferenceId)
        {
            return _speakerServ.GetSpeakerList(conferenceId);
        }

        // Get specific speaker
        // GET api/speakers/2
        [HttpGet("{id}")]
        public SpeakerDTO GetSpeaker(int id)
        {
            //Wendy - Make sure the Admin can get the speaker to Edit
            return _speakerServ.GetSpeaker(id);
        }

        // POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        // Add speaker
        // POST api/speakers
        // This is an Add new speaker
        //[HttpPost]
        //[Authorize]
        //public IActionResult Post([FromBody]SpeakerDTO speaker)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _speakerServ.PostSpeaker(speaker);
        //        return Ok(speaker);
        //    }
        //    return HttpBadRequest(ModelState);
        //}

        // POST api/speakers/5
        // This is an Edit of a specific speaker
        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody]SpeakerDTO speaker)
        {
            if (speaker == null)
            {
                throw new Exception("Could not find speaker with id " + id);
            }

            //Wendy - NEED TO MAKE SURE Admins can add new rooms
            _speakerServ.UpdateSpeaker(id, speaker);

            return Ok();
        }

        // PUT api/rooms/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }


        // Delete speaker
        // DELETE api/speakers/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //    _speakerServ.DeleteSpeaker(id);
        //}
    }
}
