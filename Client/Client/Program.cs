using System.Diagnostics;
using System.Text.Json;
using Newtonsoft.Json.Linq;


namespace Client;

class Program
{
    public static async Task Main(string[] args)
    {
        var clientId = args[0];
        var redirectUri = "https://oauth.vk.com/blank.html";
        var authString =
            $"https://oauth.vk.com/authorize?client_id={clientId}&display=page&redirect_uri={redirectUri}&response_type=token&v=5.131";
        authString = authString.Replace("&", "^&");

        Process.Start(new ProcessStartInfo("cmd", $"/c start {authString}") { CreateNoWindow = true });

        var client = new HttpClient();
        var accessToken = Console.ReadLine();
        var friendsData = await GetFriendsAsync(client, accessToken);

        foreach (var friend in friendsData)
        {
            Console.WriteLine($"{friend.LastName} {friend.FirstName}");
        }
    }


    private static async Task<List<UserData>> GetFriendsAsync(HttpClient client, string? accessToken)
    {
        var rawResponse =
            await client.GetAsync($"https://api.vk.com/method/friends.get?access_token={accessToken}&v=5.131");

        rawResponse.EnsureSuccessStatusCode();

        var json = await rawResponse.Content.ReadAsStreamAsync();
        var response = await JsonSerializer.DeserializeAsync<GetFriendsResponse>(json);


        return await GetUsersDataAsync(client, accessToken, response.FriendsIds.Ids);
    }


    private static async Task<List<UserData>> GetUsersDataAsync(HttpClient client, string token, IList<int> userIds)
    {
        var rawResponse =
            await client.GetAsync(
                $"https://api.vk.com/method/users.get?user_ids={string.Join(",", userIds)}&access_token={token}&v=5.154");

        rawResponse.EnsureSuccessStatusCode();
        
        var json = await rawResponse.Content.ReadAsStreamAsync();
        var response = await JsonSerializer.DeserializeAsync<GetUsersResponse>(json);

        return response.UsersData;
    }
}
