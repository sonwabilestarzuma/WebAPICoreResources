using MyCodeCamp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCoreResources.Models;

namespace WebApiCoreResources.Data
{
    public class campDbInitializer
    {
        private CampContext _context;

        public campDbInitializer(CampContext context)
        {
            _context = context;
        }

        public async Task EnsureSeedData()
        {
            if (!_context.Camps.Any())
            {
                // Add Data
                _context.AddRange(_sample);
                await _context.SaveChangesAsync();
            }
        }
        List<Camp> _sample = new List<Camp>
        {
            new Camp()
            {
        Name = "CampSoftCamp",
        Moniker = "SZS2016",
        EventDate = DateTime.Today.AddDays(45),
        Length = 1,
        Description = "This is the first code camp ever",
        Location = new Location()
        {
          Address1 = "4158 Angular Street",
          CityTown = "Cape Town",
          StateProvince = "CA",
          PostalCode = "8000",
          Country = "SA"
        },
        Speakers = new List<Speaker>
        {
          new Speaker()
          {
            Name = "Star Zuma",
            Bio = "I'm a speaker",
            CompanyName = "CondorGreen",
            GitHubName = "sonwabilestarzuma",
            TwitterName = "sonwabilestarzuma",
            PhoneNumber = "076 408 9743",
            HeadShotUrl = "https://images.app.goo.gl/DLGbbBu8axXfWRbk6",
            WebsiteUrl = "https://github.com/sonwabilestarzuma",
            Talks = new List<Talks>()
            {
              new Talks()
              {
                Title =  "How to do ASP.NET Core",
                Abstract = "How to do ASP.NET Core",
                Category = "Web Development",
                Level = "100",
                Prerequisites = "C# Experience",
                Room = "245",
                StartingTime = DateTime.Parse("14:30")
              },
              new Talks()
              {
                Title =  "How to do Angular 6",
                Abstract = "How to do Angular 6",
                Category = "Web Development Life Cycle",
                Level = "100",
                Prerequisites = "CSS and HTML Experience",
                Room = "246",
                StartingTime = DateTime.Parse("13:00")
              },
            }
          },
          new Speaker()
          {
            Name = "Star Zuma",
            Bio = "I'm a speaker",
            CompanyName = "CondorGreen",
            GitHubName = "sonwabilestarzuma",
            TwitterName = "sonwabilestarzuma",
            PhoneNumber = "076 408 9743",
            HeadShotUrl = "https://images.app.goo.gl/DLGbbBu8axXfWRbk6",
            WebsiteUrl = "https://github.com/sonwabilestarzuma",
            Talks = new List<Talks>()
            {
              new Talks()
              {
                Title =  "Managing a Consulting Business",
                Abstract = "Managing a Consulting Business",
                Category = "Software Skills",
                Level = "100",
                Room = "230",
                StartingTime = DateTime.Parse("10:30")
              }
            }
          }
        }
            }
    };

    }
}