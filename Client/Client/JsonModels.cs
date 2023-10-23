using System.Text.Json.Serialization;

namespace Client;

internal class GetFriendsResponse
{
    [JsonPropertyName("response")]
    public ResponseInnerInfo FriendsIds { get; set; }
    
    public class ResponseInnerInfo
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("items")]
        public List<int> Ids { get; set; }
    } 
}

internal class GetUsersResponse
{
    [JsonPropertyName("response")]
    public List<UserData> UsersData { get; set; }
}


internal class UserData
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }
    
    [JsonPropertyName("last_name")]
    public string LastName { get; set; }
    
    [JsonPropertyName("can_access_closed")]
    public bool CanAccessClosed { get; set; }
    
    [JsonPropertyName("is_closed")]
    public bool IsClosed { get; set; }
}