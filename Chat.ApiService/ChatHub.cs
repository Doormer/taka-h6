using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System;
using Chat.Domain.AggregateModels.MessageAggregate;
using Serilog;

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

        // 修改发送消息逻辑
        public async Task SendMessage(string targetUserId, string message)
        {
            var senderUserId = _userConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;

            if (!string.IsNullOrEmpty(senderUserId))
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
                    // 如果用户在线，直接发送消息并标记为已读
                    if (_userConnections.TryGetValue(targetUserId, out var connectionId))
                    {
                        Console.WriteLine($"Sending message from {senderUserId} to {targetUserId} (Connection ID: {connectionId})");
                        await Clients.Client(connectionId).SendAsync("ReceiveMessage", senderUserId, message);
                        
                        // 标记为已读
                        messageEntity.MarkAsRead();
                        _messageRepo.UpdateMessageStatus(messageEntity);
                        await _messageRepo.UnitOfWork.SaveEntitiesAsync();
                    }
                    else
                    {
                        Console.WriteLine($"User {targetUserId} is offline, message stored for later delivery");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving message to database: {ex.Message}");
                    throw;
                }
            }
        }
        
        // 修改登录方法，添加离线消息推送
        public async Task Login(string userId)
        {
            Console.WriteLine($"{userId} trying to login，ConnectionId={Context.ConnectionId}");
            
            if (!_userConnections.ContainsKey(userId))
            {
                _userConnections[userId] = Context.ConnectionId;
                Console.WriteLine($"{userId}登录成功，ConnectionId={Context.ConnectionId}");

                try
                {
                    // 获取未读消息
                    var unreadMessages = await _messageRepo.GetUnreadMessages(Guid.Parse(userId));
                    
                    // 推送未读消息
                    foreach (var message in unreadMessages)
                    {
                        await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", 
                            message.SenderId.ToString(), 
                            message.Content);
                        
                        // 标记消息为已读
                        message.MarkAsRead();
                        _messageRepo.UpdateMessageStatus(message);
                    }

                    if (unreadMessages.Any())
                    {
                        await _messageRepo.UnitOfWork.SaveEntitiesAsync();
                        Console.WriteLine($"Pushed {unreadMessages.Count} offline messages to {userId}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing offline messages: {ex.Message}");
                    throw;
                }
            }
        }
        
        
    }
}
