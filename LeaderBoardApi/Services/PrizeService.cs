using LeaderBoardApi.DbSettings;
using LeaderBoardApi.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LeaderBoardApi.Services
{
    public class PrizeService
    {
        private readonly IMongoCollection<Prize> _PrizeCollection;

        public PrizeService(
            IOptions<LeaderBoardApiDatabaseSettings> leaderBoardApiDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                leaderBoardApiDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                leaderBoardApiDatabaseSettings.Value.DatabaseName);
            _PrizeCollection = mongoDatabase.GetCollection<Prize>("Prize");
        }
        public async Task CreatePrizeAsync()
        {
            var listPrize = _PrizeCollection.AsQueryable().ToList();
            if (listPrize.Count == 0)
            {
                var newPrize = new List<Prize>();
                newPrize.Add(new Prize { Id = 1, PrizeName = "First Prize", Price = 1000 });
                newPrize.Add(new Prize { Id = 2, PrizeName = "Second Prize", Price = 750 });
                newPrize.Add(new Prize { Id = 3, PrizeName = "Third Prize", Price = 500 });
                newPrize.Add(new Prize { Id = 4, PrizeName = "25$", Price = 25 });
                newPrize.Add(new Prize { Id = 5, PrizeName = "Consolation Prize", Price = 12500 / 900 });
                await _PrizeCollection.InsertManyAsync(newPrize);
            }
            throw new NotImplementedException();
        }

        public async Task<List<Prize>> GetAllAsync() =>
            await _PrizeCollection.Find(_ => true).ToListAsync();


    }
}
