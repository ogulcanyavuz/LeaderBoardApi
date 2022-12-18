using Newtonsoft.Json;

namespace LeaderBoardApi.Entities
{
    public class Id
    {
        [JsonProperty("$oid")]
        public string oid { get; set; }
    }
}
