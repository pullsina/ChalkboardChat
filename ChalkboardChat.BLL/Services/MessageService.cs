using Azure;
using ChalkboardChat.DAL;
using ChalkboardChat.DAL.Models;
using ChalkboardChat.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ChalkboardChat.BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repo;
        private readonly UserManager<IdentityUser> _userManager;
        public MessageService(IMessageRepository repo, UserManager<IdentityUser> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }
        public async Task<IEnumerable<MessageModel>> GetAllMessagesAsync()
        {
            return await _repo.GetAllMessagesAsync();
        }

        public async Task<bool> AddMessageAsync(ClaimsPrincipal user, string message)
        {

            if (user.Identity?.IsAuthenticated != true || string.IsNullOrWhiteSpace(message))
            {
                return false;
            }
            var messageModel = new MessageModel
            {
                Username = user.Identity!.Name!,
                UserId = _userManager.GetUserId(user)!,
                Message = message,
                Date = DateTime.Now
            };

            await _repo.AddMessageAsync(messageModel);
            return true;
        }

        public async Task<bool> DeleteMessageAsync(ClaimsPrincipal user, int messageId)
        {
            // Först hämtar vi alla meddelanden och hittar det som ska tas bort
            var allMessages = await _repo.GetAllMessagesAsync();
            var messageToDelete = allMessages.FirstOrDefault(m => m.Id == messageId);

            if (messageToDelete == null)
            {
                return false; // Meddelandet finns inte
            }

            var currentUserId = _userManager.GetUserId(user);

            if (messageToDelete.UserId != currentUserId)
            {
                return false; // AnvändarId matchar ej med meddelandets UserId. Har inte rätt att ta bort detta meddelande
            }

            await _repo.DeleteMessageAsync(messageToDelete);
            return true;
        }

        public async Task<bool> ChangeUserNameOnMessagesAsync(string userId, string newUsername)
        {
            var messages = await _repo.GetMessagesByUserIdAsync(userId);
            foreach (var message in messages)
            {
                message.Username = newUsername;
            }
            
            return await _repo.UpdateMessagesAsync(messages);
            
        }
    }
}
