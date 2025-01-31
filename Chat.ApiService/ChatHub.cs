using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System;
using Chat.Domain.AggregateModels.MessageAggregate;

namespace Chat.ApiService
{
    public class ChatHub : Hub
    {
        private readonly IMessageRepo _messageRepo;
        // 使用并发字典存储用户连接信息
        private static readonly ConcurrentDictionary<string, string> _userConnections = new ConcurrentDictionary<string, string>();

        public ChatHub(IMessageRepo messageRepo)
        {
            _messageRepo = messageRepo ?? throw new ArgumentNullException(nameof(messageRepo));
        }

        // 连接时处理逻辑
        public override async Task OnConnectedAsync()
        {
            
            /*
            var userId = Context.GetHttpContext().Request.Query["userId"];
            if (!string.IsNullOrEmpty(userId))
            {
                _userConnections[userId] = Context.ConnectionId;
                Console.WriteLine($"User connected: {userId} with Connection ID: {Context.ConnectionId}");
            }
            else
            {
                Console.WriteLine("User connected without userId.");
            }
            */
            await base.OnConnectedAsync();
        }

        // 断开连接时处理逻辑
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = _userConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            if (!string.IsNullOrEmpty(userId))
            {
                _userConnections.TryRemove(userId, out _);
                Console.WriteLine($"User disconnected: {userId}");
            }
            await base.OnDisconnectedAsync(exception);
        }

        // 发送消息逻辑
        public async Task SendMessage(string targetUserId, string message)
        {
            var senderUserId = _userConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;

            if (!string.IsNullOrEmpty(senderUserId) && _userConnections.TryGetValue(targetUserId, out var connectionId))
            {
                try
                {
                    // 创建消息实体
                    var messageEntity = new Message(
                        Guid.Parse(senderUserId),
                        Guid.Parse(targetUserId),
                        message,
                        DateTime.UtcNow
                    );

                    // 保存到数据库
                    _messageRepo.Add(messageEntity);
                    await _messageRepo.UnitOfWork.SaveEntitiesAsync();

                    // 发送实时消息
                    Console.WriteLine($"Sending message from {senderUserId} to {targetUserId} (Connection ID: {connectionId})");
                    await Clients.Client(connectionId).SendAsync("ReceiveMessage", senderUserId, message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving message to database: {ex.Message}");
                    throw;
                }
            }
            else
            {
                Console.WriteLine($"Message not delivered. Either sender or target user is not connected.");
            }
        }
        
        // login
        public void Login(string userId)
        {

            Console.WriteLine($"{userId} trying to login，ConnectionId={Context.ConnectionId}");
            if (!_userConnections.ContainsKey(userId))
            {
                _userConnections[userId] = Context.ConnectionId;

                Console.WriteLine($"{userId}登录成功，ConnectionId={Context.ConnectionId}");
            }

        }
        
        
    }
}
