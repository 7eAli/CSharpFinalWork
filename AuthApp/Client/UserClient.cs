using AuthApp.Models.User;
using System.Text.Json;


namespace AuthApp.Client
{
    public class UserClient : IUserClient
    {
        readonly HttpClient httpClient = new HttpClient();
        public async Task<Guid> DeleteUser(string email)
        {
            using (HttpResponseMessage response = await httpClient.DeleteAsync($"https://localhost:7028/Users/DeleteUser?email={email}"))
            {
                response.EnsureSuccessStatusCode();
                return Guid.Parse(await response.Content.ReadAsStringAsync());
            }
        }
        

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            using (HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7028/Users/GetUsers"))
            {
                response.EnsureSuccessStatusCode();
                var users = JsonSerializer.Deserialize<IEnumerable<UserDto>>(await response.Content.ReadAsStringAsync());
                return users;
            }
        }
    }
}
