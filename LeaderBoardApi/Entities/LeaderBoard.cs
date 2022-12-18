using MongoDB.Bson;

namespace LeaderBoardApi.Entities
{
    public class LeaderBoard
    {
        public ObjectId Id { get; set; }
        public DateTime createDate { get; set; }
        public List<User> _user { get; set; }
    }
}
