using Azure;
using ChalkboardChat.DAL;
using ChalkboardChat.DAL.Models;
using ChalkboardChat.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChalkboardChat.BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly MessageRepository _repo;

        public MessageService(MessageRepository repo)
        {
            _repo=repo;
        }
        public async Task<IEnumerable<MessageModel>> GetAllMessagesAsync()
        {
           return await _repo.GetAllMessagesAsync();
        }

        public async Task AddMessageAsync(string user, string message)
        {
            
            if (!string.IsNullOrWhiteSpace(user)||!string.IsNullOrWhiteSpace(message))
            {
                //kan inte returna page()
                //kan inte RedirectToPage()
                throw new Exception("User eller message är null");
            }
            var messageModel = new MessageModel
            {
               Username = user,
               Message = message,
               Date = DateTime.Now
            };

             await _repo.AddMessageAsync(messageModel);

        }

        public async Task<MessageModel> DeleteMessageAsync(MessageModel message)
        {
            await _repo.DeleteMessageAsync(message);
            if (message.Message == null) 
            {
                throw new Exception("Message är null");
            }
            return (message);
        }
    }
}
