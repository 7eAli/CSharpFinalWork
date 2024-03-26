using AuthApp.Models.Message;
using System.Text.Json;

namespace AuthApp.Client
{
    public class MessageClient : IMessageClient
    {
        readonly HttpClient httpClient = new HttpClient();
        public async Task<IEnumerable<MessageModel>> GetAllReceivedMessages(Guid receiverId)
        {
            using(HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7127/Message/ReceiveAllMessages?receiverId={receiverId.ToString()}"))
            {
                response.EnsureSuccessStatusCode();
                var responseMsg = await response.Content.ReadAsStringAsync();
                var messages = JsonSerializer.Deserialize<IEnumerable<MessageModel>>(responseMsg);
                return messages;
            }
        }

        public async Task<IEnumerable<MessageModel>> GetSentMessages(Guid senderId)
        {
            using (HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7127/Message/ReceiveSentMessages?senderId={senderId.ToString()}"))
            {
                response.EnsureSuccessStatusCode();
                var responseMsg = await response.Content.ReadAsStringAsync();
                var messages = JsonSerializer.Deserialize<IEnumerable<MessageModel>>(responseMsg);
                return messages;
            }
        }

        public async Task<IEnumerable<MessageModel>> GetUnreadReceivedMessages(Guid receiverId)
        {
            using (HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7127/Message/ReceiveUnreadMessages?receiverId={receiverId.ToString()}"))
            {
                response.EnsureSuccessStatusCode();
                var responseMsg = await response.Content.ReadAsStringAsync();
                var messages = JsonSerializer.Deserialize<IEnumerable<MessageModel>>(responseMsg);
                return messages;
            }
        }

        public async Task<Guid> SendMessage(MessageModel message)
        {
            using (HttpResponseMessage response = await httpClient.PostAsJsonAsync("https://localhost:7127/Message/SendMessage", message))
            {
                response.EnsureSuccessStatusCode();
                return Guid.Parse(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
