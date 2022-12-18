using LeaderBoardApi.DbSettings;
using LeaderBoardApi.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Net;

namespace LeaderBoardApi.Services
{
    public class LeaderBoardApiService
    {
        int i = 1;
        private readonly IMongoCollection<LeaderBoard> _leaderBoardCollection;
        private readonly IMongoCollection<Prize> _PrizeCollection;
        private readonly IMongoCollection<User> _userCollection;

        public LeaderBoardApiService(
            IOptions<LeaderBoardApiDatabaseSettings> leaderBoardApiDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                leaderBoardApiDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                leaderBoardApiDatabaseSettings.Value.DatabaseName);

            _leaderBoardCollection = mongoDatabase.GetCollection<LeaderBoard>("LeaderBoard");
            _PrizeCollection = mongoDatabase.GetCollection<Prize>("Prize");
            _userCollection = mongoDatabase.GetCollection<User>("User");
        }


        public async Task CreateLeaderBoard(int month, int year)
        {

            var result = _leaderBoardCollection.AsQueryable().ToList();
            var leaderBoardCheck = result.FirstOrDefault(r => Convert.ToDateTime(r.createDate).Month == month && Convert.ToDateTime(r.createDate).Year == year);
            using (WebClient wc = new WebClient())
            {

                string json = wc.DownloadString("https://cdn.mallconomy.com/testcase/points.json");
                var userData = JsonConvert.DeserializeObject<List<User>>(json).Where(x => x.approved == true).OrderByDescending(x => x.point).Take(1000).ToList();
                var updateUserData = new User();


                if (leaderBoardCheck == null)
                {



                    var prize = _PrizeCollection.AsQueryable().ToList();

                    foreach (var item in userData)
                    {
                        item.Rank = i++;
                        if (item.Rank == 1)
                            item.prize = prize.Find(x => x.Id == 1);
                        else if (item.Rank == 2)
                            item.prize = prize.Find(x => x.Id == 2);
                        else if (item.Rank == 3)
                            item.prize = prize.Find(x => x.Id == 3);
                        else if (item.Rank <= 100 && item.Rank > 3)
                            item.prize = prize.Find(x => x.Id == 4);
                        else if (item.Rank <= 1000 && item.Rank > 100)
                            item.prize = prize.Find(x => x.Id == 5);
                        
                            _userCollection.FindOneAndUpdate(Builders<User>.Filter.Eq(x => x.user_id.oid, item.user_id.oid), Builders<User>.Update.PushEach(x => x.totalPrize, new List<Prize>() { item.prize }, position: 0));
                        
                       

                    }

                    i = 1;
                    //var join = _userCollection.Aggregate().Lookup<User, Prize, UserWithPrize>(_PrizeCollection, x => x.prize.Id, y => y.Id, z => z.Prize).ToList();
                    var leaderBoard = new LeaderBoard()
                    {
                        createDate = DateTime.Parse("02." + month + "." + year),
                        _user = userData.ToList()

                    };

                     _leaderBoardCollection.InsertOne(leaderBoard);

                }
                else
                    throw new Exception();
            }
        }

        public async Task<LeaderBoard?> GetAsync(int gMonth, int gYear)
        {
            var leaderBoardList = _leaderBoardCollection.AsQueryable().ToList();
            var leaderBoardSingle = leaderBoardList.SingleOrDefault(r => Convert.ToDateTime(r.createDate).Month == gMonth && Convert.ToDateTime(r.createDate).Year == gYear);
            return leaderBoardSingle;
        }
        public async Task<User?> GetLeaderBoardUserAsync(int gMonth, int gYear, string userId)
        {
            var leaderBoardList = _leaderBoardCollection.AsQueryable().ToList();
            var leaderBoardSingle = leaderBoardList.SingleOrDefault(r => Convert.ToDateTime(r.createDate).Month == gMonth && Convert.ToDateTime(r.createDate).Year == gYear);
            var user = leaderBoardSingle._user.Find(x => x.user_id.oid == userId);
            return user;
        }
    }
}

