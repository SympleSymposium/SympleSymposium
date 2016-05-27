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

        public IQueryable<Address> GetById(int? id) {

            return from a in _db.Addresses
                   where a.Id == id
                   select a;
        }

        public void add(Address address) {

            _db.Addresses.Add(address);
        }

        public void saveChanges() {
            _db.SaveChanges();
        }
    }
}
