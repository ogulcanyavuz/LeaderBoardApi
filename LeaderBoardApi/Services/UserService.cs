using LeaderBoardApi.DbSettings;
using LeaderBoardApi.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Net;

namespace LeaderBoardApi.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _userCollection;
        public UserService(
            IOptions<LeaderBoardApiDatabaseSettings> leaderBoardApiDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                leaderBoardApiDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                leaderBoardApiDatabaseSettings.Value.DatabaseName);
            _userCollection = mongoDatabase.GetCollection<User>("User");
        }

        public async Task CreateUserAsync()
        {
            using (WebClient wc = new WebClient())
            {

                string json = wc.DownloadString("https://cdn.mallconomy.com/testcase/points.json");
                var userData = JsonConvert.DeserializeObject<List<User>>(json);
                if (_userCollection.AsQueryable().Count() == 0)
                {
                    _userCollection.InsertMany(userData);
                }

            }
        }

        public async Task<List<User>> GetUserWithPrize()
        {
            var userWithPrice = _userCollection.AsQueryable().Where(x=>x.totalPrize.Count > 1).ToList();
            return userWithPrice;
        }



    }
}
