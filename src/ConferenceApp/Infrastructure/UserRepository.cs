using ConferenceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Infrastructure
{
    public class UserRepository
    {
            private ApplicationDbContext _db;

            public UserRepository(ApplicationDbContext db)
            {
                _db = db;
            }

            public IQueryable<ApplicationUser> List()
            {
                return _db.Users;
            }
        }
}
