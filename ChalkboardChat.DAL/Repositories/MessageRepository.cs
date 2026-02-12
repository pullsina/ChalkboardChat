using ChalkboardChat.DAL.Data;
using ChalkboardChat.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace ChalkboardChat.DAL.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _context;
        public MessageRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddMessageAsync(MessageModel message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MessageModel>> GetAllMessagesAsync()
        {
             return await _context.Messages.OrderByDescending(m => m.Date).ToListAsync();
        }

        public async Task DeleteMessageAsync(MessageModel message)
        {
            await _context.Messages.Where(m => m.Id == message.Id).ExecuteDeleteAsync();
        }

        public async Task UpdateMessageAsync(MessageModel message)
        {
            _context.Messages.Update(message);
            await _context.SaveChangesAsync();
        }
    }
}
