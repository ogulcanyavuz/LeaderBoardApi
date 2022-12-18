using Newtonsoft.Json;

namespace LeaderBoardApi.Entities
{
    public class UserId
    {

        [JsonProperty("$oid")]
        public string oid { get; set; }
    }
}
