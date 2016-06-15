using System;
using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConferenceApp.Models
{
    public class SampleData
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            db.Database.EnsureCreated();

            #region Initialize Users
            // Ensure Stephen (IsAdmin)
            //var stephen = await userManager.FindByNameAsync("Stephen.Walther@CoderCamps.com");
            var stephen = await userManager.FindByNameAsync("Eric");
            if (stephen == null)
            {
                // create user
                stephen = new ApplicationUser
                {
                    UserName = "Eric",
                    Email = "Stephen.Walther@CoderCamps.com"
                };
                await userManager.CreateAsync(stephen, "Secret123!");

                // add claims
                await userManager.AddClaimAsync(stephen, new Claim("IsAdmin", "true"));
            }
            var john = await userManager.FindByNameAsync("John");
            if (john == null)
            {
                // create user
                john = new ApplicationUser
                {
                    UserName = "John",
                    Email = "John@CoderCamps.com"
                };
                await userManager.CreateAsync(john, "Secret123!");
            }
            var tim = await userManager.FindByNameAsync("Tim");
            if (tim == null)
            {
                // create user
                tim = new ApplicationUser
                {
                    UserName = "Tim",
                    Email = "Tim@CoderCamps.com"
                };
                await userManager.CreateAsync(tim, "Secret123!");
            }
            // Ensure Mike (not IsAdmin)
            var mike = await userManager.FindByNameAsync("Mike");
            if (mike == null)
            {
                // create user
                mike = new ApplicationUser
                {
                    UserName = "Mike",
                    Email = "Mike@CoderCamps.com"
                };
                await userManager.CreateAsync(mike, "Secret123!");
            }

            #endregion

            #region Initialize Addresses
            var Addresses = new List<Address>() {
                new Address() {
                    Street = "11200 Broadway Street",
                    City = "Pearland",
                    State = "TX",
                    Zip = "77584",
                },
                new Address() {
                    Street = "100 Main Street",
                    City = "Dallas",
                    State = "TX",
                    Zip = "77584",
                },
                new Address() {
                    Street = "Broadway Street",
                    City = "New York",
                    State = "NY",
                    Zip = "77584",
                },
                new Address() {
                    Street = "123 A Street",
                    City = "Houston",
                    State = "TX",
                    Zip = "77010",
                },
                new Address() {
                    Street = "123 B Street",
                    City = "Houston",
                    State = "TX",
                    Zip = "77010",
                },
                new Address() {
                    Street = "123 C Street",
                    City = "Houston",
                    State = "TX",
                    Zip = "77010",
                },
                new Address() {
                    Street = "123 D Street",
                    City = "Houston",
                    State = "TX",
                    Zip = "77010",
                },
                new Address() {
                    Street = "123 E Street",
                    City = "Houston",
                    State = "TX",
                    Zip = "77010",
                },
                new Address() {
                    Street = "123 F Street",
                    City = "Houston",
                    State = "TX",
                    Zip = "77010",
                },
                new Address() {
                    Street = "123 G Street",
                    City = "Houston",
                    State = "TX",
                    Zip = "77010",
                },
                new Address() {
                    Street = "123 H Street",
                    City = "Houston",
                    State = "TX",
                    Zip = "77010",
                },
                new Address() {
                    Street = "123 I Street",
                    City = "Houston",
                    State = "TX",
                    Zip = "77010",
                }
            };

            for (int i = 0; i < Addresses.Count; i++)
            {
                var address = Addresses[i];

                var dbAddress = (from a in db.Addresses
                                 where a.Street == address.Street
                                 select a).FirstOrDefault();

                if (dbAddress == null)
                {
                    db.Addresses.Add(address);
                }
                else
                {
                    Addresses[i] = dbAddress;
                }
            }

            db.SaveChanges();
            #endregion

            #region Initialize Conferences
            var Conferences = new List<Conference>() {
                new Conference() {
                    Name = "Full Stack Web Development Expo",
                    AddressId = Addresses.FirstOrDefault(a => a.Street == "11200 Broadway Street").Id,
                    StartDate = new DateTime(2016, 8, 1),
                    EndDate = new DateTime(2016, 8, 3),
                    ImageUrl = "ngapp/images/coderCamps.png",
                    ApplicationUserId = stephen.Id
                },
                new Conference() {
                    Name = "Houston Tech Conference",
                    AddressId = Addresses.FirstOrDefault(a => a.Street == "100 Main Street").Id,
                    StartDate = new DateTime(2016, 10, 4),
                    EndDate = new DateTime(2016, 10, 7),
                    ImageUrl = "ngapp/images/SkillCode.png",
                    ApplicationUserId = stephen.Id
                },
                new Conference() {
                    Name = "TechConf",
                    AddressId = Addresses.FirstOrDefault(a => a.Street == "11200 Broadway Street").Id,
                    StartDate = new DateTime(2016, 9, 12),
                    EndDate = new DateTime(2016, 9, 15),
                    ImageUrl = "ngapp/images/vs.png",
                    ApplicationUserId = mike.Id
                },
                new Conference() {
                    Name = "International Technology Conference",
                    AddressId = Addresses.FirstOrDefault(a => a.Street == "Broadway Street").Id,
                    StartDate = new DateTime(2016, 9, 1),
                    EndDate = new DateTime(2016, 9, 7),
                    ImageUrl = "ngapp/images/co.png",
                    ApplicationUserId = stephen.Id
                }
            };

            for (int i = 0; i < Conferences.Count; i++)
            {
                var conference = Conferences[i];

                var dbConference = (from c in db.Conferences
                                    where c.Name == conference.Name && c.ApplicationUserId == conference.ApplicationUserId
                                    select c).FirstOrDefault();

                if (dbConference == null)
                {
                    db.Conferences.Add(conference);
                }
                else
                {
                    Conferences[i] = dbConference;
                }
            }

            db.SaveChanges();
            #endregion

            #region Initialize Rooms
            var Rooms = new List<Room>() {
                new Room() {
                    Name = "Room A",
                    Description = "Building 201, 2nd floor",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Room() {
                    Name = "Room B",
                    Description = "Building 301, 3rd floor",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Room() {
                    Name = "Room C",
                    Description = "Building 1201A, 1st floor",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Room() {
                    Name = "Room D",
                    Description = "Building 1301B, 2nd floor",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Houston Tech Conference").Id
                },
                new Room() {
                    Name = "Room E",
                    Description = "Building 121A, 1st floor",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Houston Tech Conference").Id
                },
                new Room() {
                    Name = "Room F",
                    Description = "Building 121B, 1st floor",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "International Technology Conference").Id
                },
                new Room() {
                    Name = "Room G",
                    Description = "Building 121B, 2nd floor",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "International Technology Conference").Id
                },
                new Room() {
                    Name = "Room H",
                    Description = "Building 311A, 1st floor",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "International Technology Conference").Id
                },
                new Room() {
                    Name = "Room I",
                    Description = "Building 311B, 2nd floor",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "International Technology Conference").Id
                },
            };

            for (int i = 0; i < Rooms.Count; i++)
            {
                var room = Rooms[i];

                var dbRoom = (from r in db.Rooms
                              where r.Name == room.Name && r.ConferenceId == room.ConferenceId
                              select r).FirstOrDefault();

                if (dbRoom == null)
                {
                    db.Rooms.Add(room);
                }
                else
                {
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
                    Phone = "501-324-7800",
                    Email = "jim.smith@marathon.com",
                    Company = "Marathon Petroleum",
                    AddressId = Addresses.FirstOrDefault(a => a.Street == "123 A Street").Id,
                    Bio = ".Net Consultant and Trainer, GeeksWithBlogs Co-Owner, Microsoft Regional Director",
                    ImageUrl = "ngApp/images/JimSmith.jpg",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                 new Speaker() {
                    Title = "Mr.",
                    FirstName = "Jim",
                    LastName = "Smith",
                    Phone = "501-324-7800",
                    Email = "jim.smith@marathon.com",
                    Company = "Marathon Petroleum",
                    AddressId = Addresses.FirstOrDefault(a => a.Street == "123 A Street").Id,
                    Bio = ".Net Consultant and Trainer, GeeksWithBlogs Co-Owner, Microsoft Regional Director",
                    ImageUrl = "ngApp/images/JimSmith.jpg",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "International Technology Conference").Id
                },
                new Speaker() {
                    Title = "Ms.",
                    FirstName = "Jill",
                    LastName = "Jones",
                    Phone = "244-788-6000",
                    Email = "jill.jones@microsoft.com",
                    Company = "Microsoft",
                    AddressId = Addresses.FirstOrDefault(a => a.Street == "123 B Street").Id,
                    Bio = "Jill Jones is a Technical Evangelist in the Developer & Platform Evangelism (DPE) group at Microsoft. She enjoys running, photography and being outdoors.",
                    ImageUrl = "ngApp/images/JillJones.jpg",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "International Technology Conference").Id
                },
                new Speaker() {
                    Title = "Ms.",
                    FirstName = "Jill",
                    LastName = "Jones",
                    Phone = "244-788-6000",
                    Email = "jill.jones@microsoft.com",
                    Company = "Microsoft",
                    AddressId = Addresses.FirstOrDefault(a => a.Street == "123 B Street").Id,
                    Bio = "Jill Jones is a Technical Evangelist in the Developer & Platform Evangelism (DPE) group at Microsoft. She enjoys running, photography and being outdoors.",
                    ImageUrl = "ngApp/images/JillJones.jpg",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Speaker() {
                    Title = "Mr.",
                    FirstName = "Bob",
                    LastName = "Stanton",
                    Phone = "512-555-7800",
                    Email = "bob.stanton@dell.com",
                    Company = "Dell",
                    AddressId = Addresses.FirstOrDefault(a => a.Street == "123 C Street").Id,
                    Bio = "Canadian Geek in NJ. Microsoft Technical Evangelist, NY Metro. X-plat Mobile Developer. Ex-WPDEV MVP. GIS Pro. Game Dev. Speaker. Author. Blogger. Father. Gamer",
                    ImageUrl = "ngApp/images/BobStanton.jpg",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Speaker() {
                    Title = "Mrs.",
                    FirstName = "Nancy",
                    LastName = "Carlson",
                    Phone = "321-543-5566",
                    Email = "nancy.carlson@ibm.com",
                    Company = "IBM",
                    AddressId = Addresses.FirstOrDefault(a => a.Street == "123 D Street").Id,
                    Bio = "Developer | CTO Applied Information Sciences | Azure MVP l Fleeting thoughts on Cloud Computing, BI, SharePoint and stuff.",
                    ImageUrl = "ngApp/images/NancyCarlson.jpg",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Speaker() {
                    Title = "Mr.",
                    FirstName = "Kevin",
                    LastName = "Wilcox",
                    Phone = "801-322-8900",
                    Email = "kevin.wilcox@stompit.com",
                    Company = "Stompit",
                    AddressId = Addresses.FirstOrDefault(a => a.Street == "123 E Street").Id,
                    Bio = "Father. Husband. Wakeboarder. Software Developer. Lead Consultant w/ Magenic. Stomp It Co-founder",
                    ImageUrl = "ngApp/images/KevinWilcox.jpg",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Speaker() {
                    Title = "Ms.",
                    FirstName = "Anne",
                    LastName = "Robertson",
                    Phone = "782-644-8100",
                    Email = "aroberts@microsoft.com",
                    Company = "Microsoft",
                    AddressId = Addresses.FirstOrDefault(a => a.Street == "123 F Street").Id,
                    Bio = "Author, speaker, designer, developer, consultant, trainer, mother.",
                    ImageUrl = "ngApp/images/AnneRobertson.jpg",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Speaker() {
                    Title = "Ms.",
                    FirstName = "Anne",
                    LastName = "Robertson",
                    Phone = "782-644-8100",
                    Email = "aroberts@microsoft.com",
                    Company = "Microsoft",
                    AddressId = Addresses.FirstOrDefault(a => a.Street == "123 F Street").Id,
                    Bio = "Author, speaker, designer, developer, consultant, trainer, mother.",
                    ImageUrl = "ngApp/images/AnneRobertson.jpg",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "International Technology Conference").Id
                },
                new Speaker() {
                    Title = "Mr.",
                    FirstName = "Sam",
                    LastName = "Giles",
                    Phone = "256-334-7878",
                    Email = "sam.giles@phoenix.com",
                    Company = "Phoenix Consulting",
                    AddressId = Addresses.FirstOrDefault(a => a.Street == "123 G Street").Id,
                    Bio = ".NET Full Stack and SQL trainer/consultant. Geek. Marathoner. Husband.",
                    ImageUrl = "ngApp/images/SamGiles.jpg",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Speaker() {
                    Title = "Mrs.",
                    FirstName = "Alice",
                    LastName = "Jean",
                    Phone = "611-855-4300",
                    Email = "ajean@sleektech@com",
                    Company = "Sleek Technologies",
                    AddressId = Addresses.FirstOrDefault(a => a.Street == "123 H Street").Id,
                    Bio = "I'm CTO of Sleek Technologies, and a Microsoft MVP, developer, author, and speaker, passionate about SQL Server and .NET technologies.",
                    ImageUrl = "ngApp/images/AliceJean.jpg",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Speaker() {
                    Title = "Mrs.",
                    FirstName = "Alice",
                    LastName = "Jean",
                    Phone = "611-855-4300",
                    Email = "ajean@sleektech@com",
                    Company = "Sleek Technologies",
                    AddressId = Addresses.FirstOrDefault(a => a.Street == "123 H Street").Id,
                    Bio = "I'm CTO of Sleek Technologies, and a Microsoft MVP, developer, author, and speaker, passionate about SQL Server and .NET technologies.",
                    ImageUrl = "ngApp/images/AliceJean.jpg",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "International Technology Conference").Id
                },
                new Speaker() {
                    Title = "Dr.",
                    FirstName = "Chris",
                    LastName = "Ramsey",
                    Phone = "748-655-9000",
                    Email = "cramsey@hp.com",
                    Company = "HP",
                    AddressId = Addresses.FirstOrDefault(a => a.Street == "123 I Street").Id,
                    Bio = "Jazz musician turned software consultant. Obsessed w/ Scrum & software testing. Scrum.org trainer, project coach, conference speaker.",
                    ImageUrl = "ngApp/images/ChrisRamsey.jpg",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Speaker() {
                    Title = "Dr.",
                    FirstName = "Chris",
                    LastName = "Ramsey",
                    Phone = "748-655-9000",
                    Email = "cramsey@hp.com",
                    Company = "HP",
                    AddressId = Addresses.FirstOrDefault(a => a.Street == "123 I Street").Id,
                    Bio = "Jazz musician turned software consultant. Obsessed w/ Scrum & software testing. Scrum.org trainer, project coach, conference speaker.",
                    ImageUrl = "ngApp/images/ChrisRamsey.jpg",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "International Technology Conference").Id
                },
                new Speaker() {
                    Title = "Dr.",
                    FirstName = "Adam",
                    LastName = "Ramsey",
                    Phone = "748-655-9000",
                    Email = "adam.ramsey@xamarin.com",
                    Company = "Xamarin",
                    AddressId = Addresses.FirstOrDefault(a => a.Street == "123 I Street").Id,
                    Bio = "Live, Love, Bike & Code. Developer Guru.",
                    ImageUrl = "ngApp/images/AdamRamsey.jpg",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Houston Tech Conference").Id
                },
                new Speaker() {
                    Title = "Dr.",
                    FirstName = "Adam",
                    LastName = "Ramsey",
                    Phone = "748-655-9000",
                    Email = "adam.ramsey@xamarin.com",
                    Company = "Xamarin",
                    AddressId = Addresses.FirstOrDefault(a => a.Street == "123 I Street").Id,
                    Bio = "Live, Love, Bike & Code. Developer Guru.",
                    ImageUrl = "ngApp/images/AdamRamsey.jpg",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "International Technology Conference").Id
                }
            };

            for (int i = 0; i < Speakers.Count; i++)
            {
                var speaker = Speakers[i];
                var dbSpeaker = (from s in db.Speakers
                                 where s.FirstName == speaker.FirstName && s.LastName == speaker.LastName && s.ConferenceId == speaker.ConferenceId
                                 select s).FirstOrDefault();

                if (dbSpeaker == null)
                {
                    db.Speakers.Add(speaker);
                }
                else
                {
                    Speakers[i] = dbSpeaker;
                }
            }

            db.SaveChanges();
            #endregion

            #region Initialize Presentations
            var Presentations = new List<Presentation>() {
                new Presentation() {
                    Title = "HTML: Let's Lay This Out",
                    Description = "Utilize HyperText Markup Language (HTML) to enable you to 'mark up' plain-text documents and add images, links, and formatting.",
                    ImageUrl = "",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Presentation() {
                    Title = "CSS: How to Stay in Style",
                    Description = "Understand the ins and outs of CSS animations and transitions, motion design best practices, and how to ensure high performance, 'jank-free' animation on even everyday mobile devices.",
                    ImageUrl = "",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Presentation() {
                    Title = "JavaScript: No, This Talk Is Not About Java",
                    Description = "Cover basic programming concepts like variables, data types, and functions, if/then statements, arrays and loops. They also introduce the Document Object Model (DOM) and how to use JavaScript to interact with the DOM and change HTML pages",
                    ImageUrl = "",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Presentation() {
                    Title = "Typescript: JavaScript, Only So Much Better",
                    Description = "A new language and toolset that makes it easier to write cross-platform, application-scale JavaScript.",
                    ImageUrl = "",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Presentation() {
                    Title = "Angular: What HTML Should Have Been, According To Google",
                    Description = "Learn the basics of how to get started with developing Angular apps and scaling them with further complexity",
                    ImageUrl = "",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Presentation() {
                    Title = "C#: Not Just A Note On Your Piano",
                    Description = "Use C# to create Windows client applications, XML Web services, distributed components, client-server applications, database applications, and much, much more.",
                    ImageUrl = "",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Presentation() {
                    Title = "ASP.NET: Building App That Actually Scale",
                    Description = "Learn to use this unified Web development model, including the services necessary for you to build enterprise-class Web applications.",
                    ImageUrl = "",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Presentation() {
                    Title = "Visual Studio: There Is Only One Visual Studio",
                    Description = "Microsoft Visual Studio is an integrated development environment (IDE) from Microsoft to develop computer and mobile programs.",
                    ImageUrl = "",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Presentation() {
                    Title = "Atom: Can We Just Code Already?",
                    Description = "Use this modern text editor to customize anything but also use productively without ever touching a config file.",
                    ImageUrl = "",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id
                },
                new Presentation() {
                    Title = "HTML: Let's Lay This Out",
                    Description = "Utilize HyperText Markup Language (HTML) to enable you to 'mark up' plain-text documents and add images, links, and formatting.",
                    ImageUrl = "",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "International Technology Conference").Id
                },
                new Presentation() {
                    Title = "CSS: How to Stay in Style",
                    Description = "Understand the ins and outs of CSS animations and transitions, motion design best practices, and how to ensure high performance, 'jank-free' animation on even everyday mobile devices.",
                    ImageUrl = "",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "International Technology Conference").Id
                },
                new Presentation() {
                    Title = "JavaScript: No, This Talk Is Not About Java",
                    Description = "Cover basic programming concepts like variables, data types, and functions, if/then statements, arrays and loops. They also introduce the Document Object Model (DOM) and how to use JavaScript to interact with the DOM and change HTML pages",
                    ImageUrl = "",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "International Technology Conference").Id
                },
                new Presentation() {
                    Title = "Typescript: JavaScript, Only So Much Better",
                    Description = "A new language and toolset that makes it easier to write cross-platform, application-scale JavaScript.",
                    ImageUrl = "",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "International Technology Conference").Id
                },
                new Presentation() {
                    Title = "Angular: What HTML Should Have Been, According To Google",
                    Description = "Learn the basics of how to get started with developing Angular apps and scaling them with further complexity",
                    ImageUrl = "",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "International Technology Conference").Id
                },
                new Presentation() {
                    Title = "Houston Web Development",
                    Description = "Learn about web dev in Houston!",
                    ImageUrl = "",
                    ConferenceId = Conferences.FirstOrDefault(c => c.Name == "Houston Tech Conference").Id
                }

            };

            for (int i = 0; i < Presentations.Count; i++)
            {
                var presentation = Presentations[i];

                var dbPresentation = (from p in db.Presentations
                                      where p.Title == presentation.Title && p.ConferenceId == presentation.ConferenceId
                                      select p).FirstOrDefault();

                if (dbPresentation == null)
                {
                    db.Presentations.Add(presentation);
                }
                else
                {
                    Presentations[i] = dbPresentation;
                }
            }

            db.SaveChanges();
            #endregion

            #region Initialize Slots
            var Slots = new List<Slot>() {
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "HTML: Let's Lay This Out").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Jim" && s.LastName == "Smith" && s.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room A" && r.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    StartTime = new DateTime(2016,8,1,8,0,0),
                    EndTime = new DateTime(2016,8,1,10,0,0)
                },
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "CSS: How to Stay in Style").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Jill" && s.LastName == "Jones" && s.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room B" && r.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    StartTime = new DateTime(2016,8,1,8,30,0),
                    EndTime = new DateTime(2016,8,1,10,30,0)
                },
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "JavaScript: No, This Talk Is Not About Java").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Bob" && s.LastName == "Stanton" && s.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room C" && r.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    StartTime = new DateTime(2016,8,1,10,15,0),
                    EndTime = new DateTime(2016,8,1,11,45,0)
                },
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "Typescript: JavaScript, Only So Much Better").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Nancy" && s.LastName == "Carlson" && s.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room A" && r.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    StartTime = new DateTime(2016,8,1,13,0,0),
                    EndTime = new DateTime(2016,8,1,14,0,0)
                },
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "Angular: What HTML Should Have Been, According To Google").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Kevin" && s.LastName == "Wilcox" && s.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room C" && r.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    StartTime = new DateTime(2016,8,1,13,0,0),
                    EndTime = new DateTime(2016,8,1,15,0,0)
                },
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "C#: Not Just A Note On Your Piano").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Anne" && s.LastName == "Robertson" && s.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room B" && r.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    StartTime = new DateTime(2016,8,1,14,0,0),
                    EndTime = new DateTime(2016,8,1,16,0,0)
                },
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "ASP.NET: Building App That Actually Scale").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Sam" && s.LastName == "Giles" && s.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room A" && r.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    StartTime = new DateTime(2016,8,1,15,0,0),
                    EndTime = new DateTime(2016,8,1,17,0,0)
                },
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "CSS: How to Stay in Style").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Jill" && s.LastName == "Jones" && s.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room A" && r.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    StartTime = new DateTime(2016,8,2,13,30,0),
                    EndTime = new DateTime(2016,8,2,14,30,0)
                },
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "Visual Studio: There Is Only One Visual Studio").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Alice" && s.LastName == "Jean" && s.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room A" && r.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    StartTime = new DateTime(2016,8,2,10,30,0),
                    EndTime = new DateTime(2016,8,2,11,30,0)
                },
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "Atom: Can We Just Code Already?").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Chris" && s.LastName == "Ramsey" && s.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room B" && r.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    StartTime = new DateTime(2016,8,2,10,30,0),
                    EndTime = new DateTime(2016,8,2,11,30,0)
                },
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "HTML: Let's Lay This Out").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Jim" && s.LastName == "Smith" && s.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room B" && r.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    StartTime = new DateTime(2016,8,2,8,0,0),
                    EndTime = new DateTime(2016,8,2,10,0,0)
                },
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "Angular: What HTML Should Have Been, According To Google").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Bob" && s.LastName == "Stanton" && s.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room C" && r.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    StartTime = new DateTime(2016,8,2,10,30,0),
                    EndTime = new DateTime(2016,8,2,11,30,0)
                },
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "C#: Not Just A Note On Your Piano").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Anne" && s.LastName == "Robertson" && s.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room C" && r.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    StartTime = new DateTime(2016,8,2,14,0,0),
                    EndTime = new DateTime(2016,8,2,16,0,0)
                },

                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "CSS: How to Stay in Style").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Jill" && s.LastName == "Jones" && s.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room A" && r.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    StartTime = new DateTime(2016,8,3,9,30,0),
                    EndTime = new DateTime(2016,8,3,11,30,0)
                },
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "Visual Studio: There Is Only One Visual Studio").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Alice" && s.LastName == "Jean" && s.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room A" && r.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    StartTime = new DateTime(2016,8,3,13,30,0),
                    EndTime = new DateTime(2016,8,3,15,30,0)
                },
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "Atom: Can We Just Code Already?").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Chris" && s.LastName == "Ramsey" && s.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room B" && r.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    StartTime = new DateTime(2016,8,3,11,00,0),
                    EndTime = new DateTime(2016,8,3,13,30,0)
                },
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "HTML: Let's Lay This Out").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Jim" && s.LastName == "Smith" && s.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room B" && r.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    StartTime = new DateTime(2016,8,3,15,0,0),
                    EndTime = new DateTime(2016,8,3,17,0,0)
                },
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "Angular: What HTML Should Have Been, According To Google").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Bob" && s.LastName == "Stanton" && s.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room C" && r.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    StartTime = new DateTime(2016,8,3,8,30,0),
                    EndTime = new DateTime(2016,8,3,10,30,0)
                },
                new Slot() {
                    PresentationId = Presentations.FirstOrDefault(p => p.Title == "C#: Not Just A Note On Your Piano").Id,
                    SpeakerId = Speakers.FirstOrDefault(s => s.FirstName == "Anne" && s.LastName == "Robertson" && s.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    RoomId = Rooms.FirstOrDefault(r => r.Name == "Room C" && r.ConferenceId == Conferences.FirstOrDefault(c => c.Name == "Full Stack Web Development Expo").Id).Id,
                    StartTime = new DateTime(2016,8,3,13,0,0),
                    EndTime = new DateTime(2016,8,3,14,0,0)
                }
            };

            for (int i = 0; i < Slots.Count; i++)
            {
                var slot = Slots[i];

                var dbSlot = (from s in db.Slots
                              where s.PresentationId == slot.PresentationId && s.RoomId == slot.RoomId && s.SpeakerId == slot.SpeakerId && s.StartTime == slot.StartTime
                              select s).FirstOrDefault();

                if (dbSlot == null)
                {
                    db.Slots.Add(slot);
                }
                else
                {
                    Slots[i] = dbSlot;
                }
            }

            db.SaveChanges();
            #endregion




        }

    }
}
