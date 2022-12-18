namespace LeaderBoardApi.Entities
{
    public class User
    {
        public Id _id { get; set; }
        public bool approved { get; set; }
        public UserId user_id { get; set; }
        public int point { get; set; }
        public Prize? prize { get; set; }
        public int Rank { get; set; }
        public List<Prize> totalPrize { get; set; } = new List<Prize>() { new Prize { Id = 6, Price = 0, PrizeName = "NoPrize" } };
    }
}
