using AuthApp.Models.Authorization;
using AuthApp.Models.User;
using System.Net.Http;

namespace AuthApp.Client
{
    public class LoginClient : ILoginClient
    {
        readonly HttpClient client = new HttpClient();

        public async Task<RoleType> GetUserRole(LoginModel loginModel)
        {
            using (HttpResponseMessage response = await client.GetAsync($"https://localhost:7028/Users/GetUserRole?email={loginModel.Email}&password={loginModel.Password}"))
            {
                response.EnsureSuccessStatusCode();
                string responseContent = await response.Content.ReadAsStringAsync();
                if (responseContent == "0")
                {
                    return RoleType.Administrator;
                }
                else
                {
                    return RoleType.User;
                }

            }
        }

        public async Task<Guid> RegisterAdmin(UserDto admin)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7028/Users/AddAdmin", admin);
            response.EnsureSuccessStatusCode();
            return Guid.Parse(await response.Content.ReadAsStringAsync());

        }

        public async Task<Guid> RegisterUser(UserDto user)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7028/Users/AddUser", user);
            response.EnsureSuccessStatusCode();
            return Guid.Parse(await response.Content.ReadAsStringAsync());
        }

        public async Task<Guid> GetUserId(string email)
        {
            using (HttpResponseMessage response = await client.GetAsync($"https://localhost:7028/Users/GetUserId?email={email}"))
            {
                response.EnsureSuccessStatusCode();
                return Guid.Parse(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
