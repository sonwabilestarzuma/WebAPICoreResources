using Microsoft.EntityFrameworkCore;
using MyCodeCamp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCoreResources.Models;

namespace WebApiCoreResources.Data
{
    public class CampRepository : ICampRepository
    {
        private CampContext _context;

        public CampRepository(CampContext context)
        {
            _context = context;

        }
        public void Add<T>(T models) where T : class
        {
            _context.Add(models);
        }

        public void Delete<T>(T models) where T : class
        {
            _context.Remove(models);
        }

        public IEnumerable<Camp> GetAllCamps()
        {
            // return all camps with their locations accodingly with ther event dates 
            return _context.Camps
                .Include(m => m.Location)
                .OrderBy(m => m.EventDate)
                .ToList();
                
        }

        public Camp GetByMoniker(string moniker)
        {
            return _context.Camps
                .Include(c => c.Location)
                   .Where(h => h.Moniker.Equals(moniker, StringComparison.CurrentCultureIgnoreCase))
                   .FirstOrDefault();
        }

        public Camp GetCamp(int id)
        {
            // I need a location and place
            return _context.Camps
                .Include(c => c.Location)
                .Where(d => d.Id == id)
                .FirstOrDefault();
        }

        public Camp GetCampByMonikerWithSpeaker(string moniker)
        {
            return _context.Camps
                    .Include(s => s.Location)
                    .Include(p => p.Speakers)
                    .ThenInclude(d => d.Talks)
                    .Where(f => f.Moniker.Equals(moniker, StringComparison.CurrentCultureIgnoreCase))
                    .FirstOrDefault();
        }

        public Camp getCampWithSpeakers(int id)
        {
            // I need a location , speakers, talks and place
            return _context.Camps
                    .Include(m => m.Location)
                    .Include(c => c.Speakers)
                    .ThenInclude(s => s.Talks)
                    .Where(d => d.Id == id)
                    .FirstOrDefault();
        }

        public IEnumerable<Speaker> GetSpeaker(int id)
        {
            // i need a Camp by id's and orderby their names 
            return _context.Speakers
                   .Include(m => m.Camp)
                   .Where(m => m.Camp.Id == id)
                   .OrderBy(c => c.Name)
                   .ToList();
        }

        public IEnumerable<Speaker> GetSpeakerByMoniker(string moniker)
        {
            // I need a speaker with camp and compare to moniker and order by their names 
            return _context.Speakers
                   .Include(c => c.Camp)
                   .Where(c => c.Camp.Moniker.Equals(moniker, StringComparison.CurrentCultureIgnoreCase))
                   .OrderBy(s => s.Name)
                   .ToList();
        }

        public IEnumerable<Speaker> GetSpeakerByMonikerWithTalks(string moniker)
        {
            return _context.Speakers
                     .Include(p => p.Camp)
                     .Include(m => m.Talks)
                     .Where(t => t.Camp.Moniker.Equals(moniker, StringComparison.CurrentCultureIgnoreCase))
                     .OrderBy(s => s.Name)
                     .ToList();
        }

        public Speaker GetSpeakers(int speakerId)
        {
            return _context.Speakers
                   .Include(m => m.Camp)
                   .Where(m => m.Id == speakerId)
                   .FirstOrDefault();
        }

        public Speaker GetSpeakersWithTalks(int speakerId)
        {
            return _context.Speakers
                .Include(d => d.Camp)
                .Include(g => g.Talks)
                .Where(h => h.Id == speakerId)
                .FirstOrDefault();
                    
        }

        public IEnumerable<Speaker> GetSpeakerWithTalks(int id)
        {
            return _context.Speakers
             .Include(s => s.Camp)
             .Include(s => s.Talks)
             .Where(s => s.Camp.Id == id)
             .OrderBy(s => s.Name)
             .ToList();
        }

        public Talk GetTalk(int talk)
        {
            return _context.Talks
            .Include(t => t.Speaker)
            .ThenInclude(s => s.Camp)
            //.Where(t => t.Id == talkId)
            .OrderBy(t => t.Title)
            .FirstOrDefault();
        }

        public IEnumerable<Talk> GetTalks(int speakerId)
        {
            return _context.Talks
                   .Include(t => t.Speaker)
                  .ThenInclude(s => s.Camp)
                  .Where(t => t.Speaker.Id == speakerId)
                  .OrderBy(t => t.Title)
                   .ToList();
        }

        public CampUser GetUser(string userName)
        {
            return _context.Users
                 //.Include(u => u.Claims)
               // .Include(u => u.Roles)
                 .Where(u => u.UserName == userName)
                 .Cast<CampUser>()
                 .FirstOrDefault();
        }

        public async Task<bool> SaveAllAscync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
