using ChalkboardChat.BLL.Services;
using ChalkboardChat.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ChalkboardChat.UI.Pages
{
    [Authorize]
    public class MessagesModel : PageModel
    {
        
        private readonly IMessageService _messageService;

        public MessagesModel(IMessageService messageService)
        {
            _messageService = messageService;
        }       
        
        public IEnumerable<MessageViewModel> Messages { get; set; } = new List<MessageViewModel>();

        [BindProperty]
        public string NewMessageText { get; set; } = "";

        public async Task OnGetAsync()
        {
            // UI -> Logic layer
            var data = await _messageService.GetAllMessagesAsync();
            //Mappar data från service (MessageModel) till viewmodel (MessageViewModel)
            Messages = data.Select(m => new MessageViewModel
            {
                Id = m.Id,
                Username = m.Username,
                Text = m.Message,
                Date = m.Date,
                IsMine = m.Username == User.Identity!.Name,
                IsDeleted = m.Message == null
            }).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {


            if (string.IsNullOrWhiteSpace(NewMessageText))
            {
                return Page();
            }
            else
            {
                var success = await _messageService.AddMessageAsync(User, NewMessageText);
                if (!success)
                {
                    ModelState.AddModelError("", "Failed to add message. Please try again.");
                    return Page();
                }
                return RedirectToPage();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            //Här anpassar jag delete -funktionen så att endast den användare
            //som skapat meddelandet kan ta bort det
            var allMessages = await _messageService.GetAllMessagesAsync();
            //skulle vara bättre att i MessegeService Delete metoden tog id
            //som parametr istället av hela user    
            var messageToDelete = allMessages.FirstOrDefault(m => m.Id == id);

            if (messageToDelete == null || messageToDelete.Username != User.Identity!.Name)
            {
                return Forbid();
            }
            await _messageService.DeleteMessageAsync(messageToDelete);
            return RedirectToPage();
        }
    }
}

