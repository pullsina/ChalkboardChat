using ChalkboardChat.BLL.Services;
using ChalkboardChat.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ChalkboardChat.UI.Pages
{
    [Authorize]
    public class MessagesModel : PageModel
    {
        private readonly IMessageService _messageService;
        private readonly UserManager<IdentityUser> _userManager;

        public MessagesModel(IMessageService messageService, UserManager<IdentityUser> userManager)
        {
            _messageService = messageService;
            _userManager = userManager;
        }

        public IEnumerable<MessageViewModel> Messages { get; set; } = new List<MessageViewModel>();

        [BindProperty]
        public string NewMessageText { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            await LoadMessagesAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(NewMessageText))
            {
                ModelState.AddModelError("", "Message cannot be empty.");
                await LoadMessagesAsync();
                return Page();
            }

            var success = await _messageService.AddMessageAsync(User, NewMessageText);

            if (!success)
            {
                ModelState.AddModelError("", "Failed to add message.");
                await LoadMessagesAsync();
                return Page();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var success = await _messageService.DeleteMessageAsync(User, id);

            if (!success)
                return Forbid();

            return RedirectToPage();
        }

        private async Task LoadMessagesAsync()
        {
            var data = await _messageService.GetAllMessagesAsync();
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var messages = new List<MessageViewModel>();

            foreach (var m in data)
            {
                var user = await _userManager.FindByIdAsync(m.UserId);
                var username = user?.UserName ?? "Deleted user";

                messages.Add(new MessageViewModel
                {
                    Id = m.Id,
                    Username = username,
                    Text = m.Message ?? "",
                    Date = m.Date,
                    IsMine = m.UserId == currentUserId,
                    IsDeleted = m.Message == null
                });
            }

            Messages = messages;
        }
    }
}