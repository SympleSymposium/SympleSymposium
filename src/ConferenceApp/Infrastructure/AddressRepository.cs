using ConferenceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Infrastructure
{
    public class AddressRepository
    {
        private ApplicationDbContext _db;

        public AddressRepository(ApplicationDbContext db) {
            _db = db;
        }

        public IQueryable<Address> List() {
            return _db.Addresses;
        }
    }
}
