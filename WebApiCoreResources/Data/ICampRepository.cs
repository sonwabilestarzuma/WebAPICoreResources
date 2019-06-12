using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCoreResources.Models;

namespace WebApiCoreResources.Data
{
    public interface ICampRepository
    {
        // Basic DB Operation

        void Add<T>(T models) where T : class;
        void Delete<T>(T models) where T : class;
        //Task<bool> SaveAllAscync();

        // Camps

        IEnumerable<Camp> GetAllCamps();
        Camp GetCamp(int id);
        Camp getCampWithSpeakers(int id);
        Camp GetByMoniker(string moniker);
        Camp GetCampByMonikerWithSpeaker(string moniker);

        // Speakers

        IEnumerable<Speaker> GetSpeaker(int id);
        IEnumerable<Speaker> GetSpeakerWithTalks(int id);
        IEnumerable<Speaker> GetSpeakerByMoniker(string moniker);
        IEnumerable<Speaker> GetSpeakerByMonikerWithTalks(string moniker);

        Speaker GetSpeakers(int speakerId);
        //Task<List<Speaker>> GetSpeakers(int speakerId);
       Speaker GetSpeakersWithTalks(int speakerId);
     // Task<List<Speaker> GetSpeakerWithTalks(int speakerId);
      

        // Talks

        IEnumerable<Talk> GetTalks(int speakerId);
        Talk GetTalk(int talkId);


        // CampUser

        CampUser GetUser(string userName);
        Task<bool> SaveAllAsync();
    }
}
