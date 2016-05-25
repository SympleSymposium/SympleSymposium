using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ConferenceApp.Services;
using ConferenceApp.Services.Models;

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

        // GET: api/values
        [HttpGet]
        public IEnumerable<ConferenceDTO> GetConferenceList()
        {
            return _confServ.GetConferenceList(); ;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
