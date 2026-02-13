using ChalkboardChat.DAL;
using ChalkboardChat.DAL.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ChalkboardChat.BLL.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageModel>> GetAllMessagesAsync();

        Task<bool> AddMessageAsync(ClaimsPrincipal user, string message );

        Task<bool> DeleteMessageAsync(ClaimsPrincipal user, int messageId);
        
    }
}
