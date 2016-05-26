using System;
using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConferenceApp.Models {
    public class SampleData {
        public async static Task Initialize(IServiceProvider serviceProvider) {
            var db = serviceProvider.GetService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            //context.Database.Migrate();

            #region Initialize Conferences
            var Conferences = new List<Conference>() {
                new Conference() {
                    Name = "Full Stack Web Development Expo",
                    Street = "11200 Broadway Street",
                    City = "Pearland",
                    State = "TX",
                    Zip = "77584",
                    StartDate = new DateTime(2016, 8, 1),
                    EndDate = new DateTime(2016, 8, 2)
                }
            };

            for (int i = 0; i < Conferences.Count; i++) {
                var conference = Conferences[i];

                var dbConference = (from c in db.Conferences
                                    where c.Name == conference.Name
                                    select c).FirstOrDefault();

                if (dbConference == null) {
                    db.Conferences.Add(conference);
                }
                else {
                    Conferences[i] = dbConference;
                }
            }

            db.SaveChanges();
            #endregion

            #region Initialize Rooms
            var Rooms = new List<Room>() {
                new Room() {
                    Name = "Room A",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Room() {
                    Name = "Room B",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Room() {
                    Name = "Room C",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                }
            };

            for (int i = 0; i < Rooms.Count; i++) {
                var room = Rooms[i];

                var dbRoom = (from r in db.Rooms
                              where r.Name == room.Name
                              select r).FirstOrDefault();

                if (dbRoom == null) {
                    db.Rooms.Add(room);
                }
                else {
                    Rooms[i] = dbRoom;
                }
            }

            db.SaveChanges();
            #endregion

            #region Initialize Speakers
            var Speakers = new List<Speaker>() {
                new Speaker() {
                    Title = "Mr.",
                    FirstName = "Jim",
                    LastName = "Smith",
                    Phone = "",
                    Email = "",
                    Company = "",
                    CoStreet = "",
                    CoState = "",
                    CoCity = "",
                    CoZip = "",
                    Bio = "",
                    ImageUrl = ""
                },
                new Speaker() {
                    Title = "Ms.",
                    FirstName = "Jill",
                    LastName = "Jones",
                    Phone = "",
                    Email = "",
                    Company = "",
                    CoStreet = "",
                    CoState = "",
                    CoCity = "",
                    CoZip = "",
                    Bio = "",
                    ImageUrl = ""
                },
                new Speaker() {
                    Title = "Mr.",
                    FirstName = "Bob",
                    LastName = "Stanton",
                    Phone = "",
                    Email = "",
                    Company = "",
                    CoStreet = "",
                    CoState = "",
                    CoCity = "",
                    CoZip = "",
                    Bio = "",
                    ImageUrl = ""
                },
                new Speaker() {
                    Title = "Mrs.",
                    FirstName = "Nancy",
                    LastName = "Carlson",
                    Phone = "",
                    Email = "",
                    Company = "",
                    CoStreet = "",
                    CoState = "",
                    CoCity = "",
                    CoZip = "",
                    Bio = "",
                    ImageUrl = ""
                },
                new Speaker() {
                    Title = "Mr.",
                    FirstName = "Kevin",
                    LastName = "Wilcox",
                    Phone = "",
                    Email = "",
                    Company = "",
                    CoStreet = "",
                    CoState = "",
                    CoCity = "",
                    CoZip = "",
                    Bio = "",
                    ImageUrl = ""
                },
                new Speaker() {
                    Title = "Ms.",
                    FirstName = "Anne",
                    LastName = "Robertson",
                    Phone = "",
                    Email = "",
                    Company = "",
                    CoStreet = "",
                    CoState = "",
                    CoCity = "",
                    CoZip = "",
                    Bio = "",
                    ImageUrl = ""
                },
                new Speaker() {
                    Title = "Mr.",
                    FirstName = "Sam",
                    LastName = "Giles",
                    Phone = "",
                    Email = "",
                    Company = "",
                    CoStreet = "",
                    CoState = "",
                    CoCity = "",
                    CoZip = "",
                    Bio = "",
                    ImageUrl = ""
                },
                new Speaker() {
                    Title = "Mrs.",
                    FirstName = "Alice",
                    LastName = "Jean",
                    Phone = "",
                    Email = "",
                    Company = "",
                    CoStreet = "",
                    CoState = "",
                    CoCity = "",
                    CoZip = "",
                    Bio = "",
                    ImageUrl = ""
                },
                new Speaker() {
                    Title = "Dr.",
                    FirstName = "Chris",
                    LastName = "Ramsey",
                    Phone = "",
                    Email = "",
                    Company = "",
                    CoStreet = "",
                    CoState = "",
                    CoCity = "",
                    CoZip = "",
                    Bio = "",
                    ImageUrl = ""
                }
            };

            for (int i = 0; i < Speakers.Count; i++) {
                var speaker = Speakers[i];

                var dbSpeaker = (from s in db.Speakers
                                 where s.FirstName == speaker.FirstName && s.LastName == speaker.LastName
                                 select s).FirstOrDefault();

                if (dbSpeaker == null) {
                    db.Speakers.Add(speaker);
                }
                else {
                    Speakers[i] = dbSpeaker;
                }
            }

            db.SaveChanges();
            #endregion

            #region Initialize Presentations
            var Presentations = new List<Presentation>() {
                new Presentation() {
                    Title = "HTML",
                    Description = "Utilize HyperText Markup Language (HTML) to enable you to 'mark up' plain-text documents and add images, links, and formatting.",
                    ImageUrl = "",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Presentation() {
                    Title = "CSS",
                    Description = "Understand the ins and outs of CSS animations and transitions, motion design best practices, and how to ensure high performance, 'jank-free' animation on even everyday mobile devices.",
                    ImageUrl = "",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Presentation() {
                    Title = "JavaScript",
                    Description = "Cover basic programming concepts like variables, data types, and functions, if/then statements, arrays and loops. They also introduce the Document Object Model (DOM) and how to use JavaScript to interact with the DOM and change HTML pages",
                    ImageUrl = "",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Presentation() {
                    Title = "Typescript",
                    Description = "A new language and toolset that makes it easier to write cross-platform, application-scale JavaScript.",
                    ImageUrl = "",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Presentation() {
                    Title = "Angular",
                    Description = "Learn the basics of how to get started with developing Angular apps and scaling them with further complexity",
                    ImageUrl = "",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Presentation() {
                    Title = "C#",
                    Description = "Use C# to create Windows client applications, XML Web services, distributed components, client-server applications, database applications, and much, much more.",
                    ImageUrl = "",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Presentation() {
                    Title = "ASP.NET",
                    Description = "Learn to use this unified Web development model, including the services necessary for you to build enterprise-class Web applications.",
                    ImageUrl = "",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Presentation() {
                    Title = "Visual Studio",
                    Description = "Microsoft Visual Studio is an integrated development environment (IDE) from Microsoft to develop computer and mobile programs.",
                    ImageUrl = "",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Presentation() {
                    Title = "Atom",
                    Description = "Use this modern text editor to customize anything but also use productively without ever touching a config file.",
                    ImageUrl = "",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                }

            };

            for (int i = 0; i < Presentations.Count; i++) {
                var presentation = Presentations[i];

                var dbPresentation = (from p in db.Presentations
                                      where p.Title == presentation.Title && p.ConferenceId == presentation.ConferenceId
                                      select p).FirstOrDefault();

                if (dbPresentation == null) {
                    db.Presentations.Add(presentation);
                }
                else {
                    Presentations[i] = dbPresentation;
                }
            }

            db.SaveChanges();
            #endregion

            #region Initialize Slots
            var Slots = new List<Slot>() {
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "HTML").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Jim" && s.LastName == "Smith").Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room A").Id,
                    StartTime = new DateTime(2016,8,1,8,0,0),
                    EndTime = new DateTime(2016,8,1,10,0,0)
                },
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "CSS").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Jill" && s.LastName == "Jones").Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room B").Id,
                    StartTime = new DateTime(2016,8,1,8,0,0),
                    EndTime = new DateTime(2016,8,1,10,0,0)
                },
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "JavaScript").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Bob" && s.LastName == "Stanton").Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room C").Id,
                    StartTime = new DateTime(2016,8,1,8,0,0),
                    EndTime = new DateTime(2016,8,1,10,0,0)
                },
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "Typescript").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Nancy" && s.LastName == "Carlson").Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room A").Id,
                    StartTime = new DateTime(2016,8,1,13,0,0),
                    EndTime = new DateTime(2016,8,1,14,0,0)
                },
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "Angular").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Kevin" && s.LastName == "Wilcox").Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room C").Id,
                    StartTime = new DateTime(2016,8,1,13,0,0),
                    EndTime = new DateTime(2016,8,1,15,0,0)
                },
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "C#").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Anne" && s.LastName == "Robertson").Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room B").Id,
                    StartTime = new DateTime(2016,8,1,14,0,0),
                    EndTime = new DateTime(2016,8,1,16,0,0)
                },
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "ASP.NET").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Sam" && s.LastName == "Giles").Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room A").Id,
                    StartTime = new DateTime(2016,8,1,15,0,0),
                    EndTime = new DateTime(2016,8,1,17,0,0)
                },
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "Visual Studio").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Alice" && s.LastName == "Jean").Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room A").Id,
                    StartTime = new DateTime(2016,8,2,8,0,0),
                    EndTime = new DateTime(2016,8,2,11,0,0)
                },
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "Atom").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Chris" && s.LastName == "Ramsey").Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room C").Id,
                    StartTime = new DateTime(2016,8,2,8,0,0),
                    EndTime = new DateTime(2016,8,2,12,0,0)
                }
            };

            for (int i = 0; i < Slots.Count; i++) {
                var slot = Slots[i];

                var dbSlot = (from s in db.Slots
                              where s.PresentationId == slot.PresentationId && s.RoomId == slot.RoomId && s.SpeakerId == slot.SpeakerId && s.StartTime == slot.StartTime
                              select s).FirstOrDefault();

                if (dbSlot == null) {
                    db.Slots.Add(slot);
                }
                else {
                    Slots[i] = dbSlot;
                }
            }

            db.SaveChanges();
            #endregion

            // Ensure Stephen (IsAdmin)
            var stephen = await userManager.FindByNameAsync("Stephen.Walther@CoderCamps.com");
            if (stephen == null) {
                // create user
                stephen = new ApplicationUser {
                    UserName = "Stephen.Walther@CoderCamps.com",
                    Email = "Stephen.Walther@CoderCamps.com"
                };
                await userManager.CreateAsync(stephen, "Secret123!");

                // add claims
                await userManager.AddClaimAsync(stephen, new Claim("IsAdmin", "true"));
            }

            // Ensure Mike (not IsAdmin)
            var mike = await userManager.FindByNameAsync("Mike@CoderCamps.com");
            if (mike == null) {
                // create user
                mike = new ApplicationUser {
                    UserName = "Mike@CoderCamps.com",
                    Email = "Mike@CoderCamps.com"
                };
                await userManager.CreateAsync(mike, "Secret123!");
            }


        }

    }
}
