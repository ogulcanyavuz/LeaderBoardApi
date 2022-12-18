namespace LeaderBoardApi.Entities
{
    public class UserWithPrize:User
    {
        public List<Prize> Prize { get; set; }
    }
}
