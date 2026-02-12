using ChalkboardChat.DAL;
using ChalkboardChat.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChalkboardChat.BLL.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageModel>> GetAllMessagesAsync();

        Task AddMessageAsync(string user, string message );

        Task<MessageModel> DeleteMessageAsync(MessageModel message);
        
    }
}
