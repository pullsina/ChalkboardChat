using ChalkboardChat.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChalkboardChat.DAL.Repositories
{
    public interface IMessageRepository
    {
        //Använder Enum för att hålla det mer abstrakt och skydda databaslogiken
        Task<IEnumerable<MessageModel>> GetAllMessagesAsync();
        Task AddMessageAsync (MessageModel message);
        Task UpdateMessageAsync (MessageModel message);
        Task DeleteMessageAsync (MessageModel message);
    }
}
