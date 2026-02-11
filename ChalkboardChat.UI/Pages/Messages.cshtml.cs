using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardChat.UI.Pages
{
    public class MessagesModel : PageModel
    {
        /*
          private readonly MessageService _messageService;

        public MessagesModel(MessageService messageService)
        {
            _messageService = messageService;
        }       
         */
        public List<MessageViewModel> Messages { get; set; } = new();

        [BindProperty]
        public string NewMessageText { get; set; } = "";

        public async Task OnGetAsync()
        {
            // FEJKDATA tills service finns
            Messages = new List<MessageViewModel>
            {
                new() { Username="Alice", Text="Hello world", Date=DateTime.Now.AddMinutes(-10), IsMine=false },
                new() { Username="You", Text="Hi Alice!", Date=DateTime.Now.AddMinutes(-5), IsMine=true }
            };

            /*
             {
            // UI -> Logic layer
            Messages = await _messageService.GetAllMessagesForUserAsync(User);
        }
             */

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(NewMessageText))
                return Page();

            // TODO: await _messageService.CreateMessageAsync(User, NewMessageText);
            return RedirectToPage();
        }
    }

    public class MessageViewModel
    {
        public string Username { get; set; } = "";
        public string Text { get; set; } = "";
        public DateTime Date { get; set; }
        public bool IsMine { get; set; }
    }
}

