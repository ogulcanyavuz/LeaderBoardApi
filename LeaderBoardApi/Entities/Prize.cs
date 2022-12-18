using MongoDB.Bson.Serialization.Attributes;

namespace LeaderBoardApi.Entities
{
    public class Prize
    {
        [BsonId]
        public int Id { get; set; }
        public string PrizeName { get; set; }
        public double Price { get; set; }
    }
}
