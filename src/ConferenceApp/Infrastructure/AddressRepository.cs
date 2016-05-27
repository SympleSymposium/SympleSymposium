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
        
//Find Conference Street
        public IQueryable<Address> FindByAddress(string street, string city, string state, string zip)
        {
            return from s in _db.Addresses
                   where s.Street == street && s.City == city && s.State == state && s.Zip == zip
                   select s;
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
