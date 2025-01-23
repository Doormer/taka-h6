using Microsoft.AspNetCore.SignalR;

namespace Chat.ApiService
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string userId, string message)
        {
            // 广播消息到所有连接的客户端
            await Clients.All.SendAsync("ReceiveMessage", userId, message);
        }
    }
}