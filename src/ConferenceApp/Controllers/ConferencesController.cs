using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ConferenceApp.Services;
using ConferenceApp.Services.Models;
using Microsoft.AspNet.Authorization;
using ConferenceApp.ViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ConferenceApp.Controllers
{
    //api/conferences
    [Route("api/[controller]")]
    public class ConferencesController : Controller
    {
        private ConferenceService _confServ;
        public ConferencesController(ConferenceService confServ)
        {
            _confServ = confServ;
        }

        // Get conferences of current user
        // GET: api/conferences/manage
        [HttpGet("manage")]
        [Authorize]
        public IEnumerable<ConferenceDTO> GetConferenceList()
        {
            return _confServ.GetConferenceList(User.Identity.Name);
        }

        // Get specific conference
        // GET api/conferences/2
        [HttpGet("{id}")]
        public ConferenceDTO GetConference(int id) {
            //BROCK - NEED TO MAKE SURE USERS CAN ONLY GET THEIR OWN CONFERENCES
            return _confServ.GetConference(id);
        }

        // Add conference
        // POST api/conferences
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]ConferenceDTO conference)
        {
            if (ModelState.IsValid)
            {
                _confServ.PostConference(conference, User.Identity.Name);
                return Ok(conference);
            }
            return HttpBadRequest(ModelState);
        }


        // POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        // Update conference
        // POST api/conferences/5
        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody]ConferenceViewModel conference)
        {
            if (conference == null) {
                throw new Exception("Could not find conference with id " + id);
            }

            //BROCK - NEED TO MAKE SURE USERS CAN ONLY UPDATE THEIR OWN CONFERENCES
            _confServ.UpdateConference(id, conference);

            return Ok();
        }

        // Delete conference
        // DELETE api/conferences/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _confServ.DeleteConference(id);
        }
    }
}
