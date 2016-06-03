﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ConferenceApp.Services.Models;
using ConferenceApp.Services;
using Microsoft.AspNet.Authorization;
using ConferenceApp.ViewModels.Manage;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ConferenceApp.Controllers
{

    // api/presentations
    [Route("api/[controller]")]
    public class PresentationsController : Controller
    {
        private PresentationService _presentServ;
        public PresentationsController(PresentationService presentServ)
        {
            _presentServ = presentServ;
        }
        
        // Get all the presentations
        // GET api/presentations
        [HttpGet]
        public IEnumerable<PresentationDTO> GetList()
        {
            return _presentServ.GetList();
        }

        // Get all the presentations for a specific conference
        // GET: api/presentations/manage/2
        [HttpGet("manage/{conferenceId}")]
        [Authorize]
        public IEnumerable<PresentationDTO> GetPresentationList(int conferenceId)
        {
            return _presentServ.GetPresentationList(conferenceId);
        }

        // Get a specific presentation
        // GET api/presentations/2
        [HttpGet("{id}")]
        public PresentationDTO GetPresentation(int id)
        {
            //Wendy - Make sure the Admin can get the room to Edit
            return _presentServ.GetPresentation(id);
        }



        // GET a specific presentation
        // GET api/presentations/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}


        // Add room
        // POST api/presentations
        // This is an Add new room
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]PresentationViewModel presentation)
        {

            if (ModelState.IsValid)
            {
                _presentServ.PostPresentation(presentation);
                return Ok(presentation);
            }
            return HttpBadRequest(ModelState);
        }

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        // POST api/rooms/5
        // This is an Edit of a specific presentation
        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody]PresentationDTO presentation)
        {
            if (presentation == null)
            {
                throw new Exception("Could not find presentation with id " + id);
            }

            //Wendy - NEED TO MAKE SURE Admins can add new rooms
            _presentServ.UpdatePresentation(id, presentation);

            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // Delete presentation
        // DELETE api/presentation/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _presentServ.DeletePresentation(id);
        }
    }
}
